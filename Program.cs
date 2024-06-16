using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline
app.MapGet("/", () => "Welcome to the F1 API!");

app.MapGet("/drivers", async (AppDbContext db) =>
{
    return await db.Drivers.ToListAsync();
});

app.MapGet("/drivers/{driverId}/results", async (AppDbContext db, string driverId) =>
{

    var driverResults = await db.DriverResults
        .Where(dr => dr.driverId == driverId)
        .Include(dr => dr.Races)
            .ThenInclude(r => r.Results)
                .ThenInclude(res => res.Time)
        .Include(dr => dr.Races)
            .ThenInclude(r => r.Results)
                .ThenInclude(res => res.FastestLap)
                    .ThenInclude(f => f.AverageSpeed)
        .Include(dr => dr.Races)
            .ThenInclude(r => r.Results)
                .ThenInclude(res => res.Driver)
        .Include(dr => dr.Races)
            .ThenInclude(r => r.Results)
                .ThenInclude(res => res.Constructor)
        .FirstOrDefaultAsync();

    return Results.Ok(driverResults);
});

app.MapPost("/sync-drivers", async (AppDbContext db, IHttpClientFactory httpClientFactory) =>
{
    var client = httpClientFactory.CreateClient();
    var response = await client.GetFromJsonAsync<ErgastApiDriverResponse>("http://ergast.com/api/f1/2024/drivers.json");

    if (response == null)
        return Results.BadRequest();

    foreach (var driver in response.MRData.DriverTable.Drivers)
    {
        if (!db.Drivers.Any(d => d.driverId == driver.driverId))
        {
            db.Drivers.Add(new Driver
            {
                driverId = driver.driverId,
                givenName = driver.givenName,
                familyName = driver.familyName
            });
        }
    }

    await db.SaveChangesAsync();
    return Results.Ok(await db.Drivers.ToListAsync());
});


app.MapPost("/sync-calendar", async (AppDbContext db, IHttpClientFactory httpClientFactory) =>
{
    var client = httpClientFactory.CreateClient();
    var response = await client.GetFromJsonAsync<ErgastApiCalendarResponse>("http://ergast.com/api/f1/current.json");

    if (response == null)
        return Results.BadRequest();

    foreach (var race in response.MRData.RaceTable.Races)
    {
        if (!db.Calendar.Any(dbRace => dbRace.raceName == race.raceName))
        {
            db.Calendar.Add(new CalendarRace
            {
                raceName = race.raceName,
                date = race.date,
                time = race.time,
                Qualifying = new Qualifying
                {
                    date = race.Qualifying.date,
                    time = race.Qualifying.time,
                }
            });
        }
    }

    await db.SaveChangesAsync();
    return Results.Ok(await db.Calendar.ToListAsync());
});

app.MapGet("/calendar", async (AppDbContext db) =>
{
    var calendarResults = await db.Calendar
        .Include(race => race.Qualifying)
        .ToListAsync();

    return Results.Ok(calendarResults);
});

app.MapPost("/sync-drivers-results", async (AppDbContext db, IHttpClientFactory httpClientFactory) =>
{
    var DriversArray = db.Drivers;

    foreach (var driver in DriversArray)
    {
        var driverId = driver.driverId;

        var client = httpClientFactory.CreateClient();
        var response = await client.GetFromJsonAsync<ErgastApiDriverResultsResponse>($"http://ergast.com/api/f1/2024/drivers/{driverId}/results.json");

        if (!db.DriverResults.Any(d => d.driverId == driver.driverId))
        {
            var driverResult = new DriverResult
            {
                driverId = driverId,
                Races = response.MRData.RaceTable.Races.Select(apiRace => new Race
                {
                    Results = apiRace.Results?.Select(apiResult => new Result
                    {
                        position = apiResult?.position ?? "unknown",
                        positionText = apiResult?.positionText ?? "unknown",
                        points = apiResult?.points ?? "unknown",
                        grid = apiResult?.grid ?? "unknown",
                        Time = apiResult?.Time != null ? new TimeDetail
                        {
                            time = apiResult.Time.time ?? "unknown",
                            millis = apiResult.Time.millis ?? "unknown"
                        } : new TimeDetail
                        {
                            time = "unknown",
                            millis = "unknown"
                        },
                        FastestLap = apiResult?.FastestLap != null ? new FastestLapDetail
                        {
                            AverageSpeed = apiResult.FastestLap.AverageSpeed != null ? new AverageSpeedDetail
                            {
                                speed = apiResult.FastestLap.AverageSpeed.speed ?? "unknown"
                            } : new AverageSpeedDetail
                            {
                                speed = "unknown"
                            }
                        } : new FastestLapDetail
                        {
                            AverageSpeed = new AverageSpeedDetail
                            {
                                speed = "unknown"
                            }
                        },
                        Driver = apiResult?.Driver != null ? new DriverDetail
                        {
                            nationality = apiResult.Driver.nationality ?? "unknown",
                            dateOfBirth = apiResult.Driver.dateOfBirth ?? "unknown",
                            permanentNumber = apiResult.Driver.permanentNumber ?? "unknown",
                            givenName = apiResult.Driver.givenName ?? "unknown",
                            familyName = apiResult.Driver.familyName ?? "unknown"
                        } : new DriverDetail
                        {
                            nationality = "unknown",
                            dateOfBirth = "unknown",
                            permanentNumber = "unknown",
                            givenName = "unknown",
                            familyName = "unknown"
                        },
                        Constructor = apiResult?.Constructor != null ? new ConstructorDetail
                        {
                            name = apiResult.Constructor.name ?? "unknown"
                        } : new ConstructorDetail
                        {
                            name = "unknown"
                        }
                    }).ToList() ?? new List<Result>()
                }).ToList()
            };

            db.DriverResults.Add(driverResult);
            await db.SaveChangesAsync();
        }
    }

    var driverResults = await db.DriverResults
        .Include(dr => dr.Races)
            .ThenInclude(r => r.Results)
                .ThenInclude(res => res.Time)
        .Include(dr => dr.Races)
            .ThenInclude(r => r.Results)
                .ThenInclude(res => res.FastestLap)
                    .ThenInclude(f => f.AverageSpeed)
        .Include(dr => dr.Races)
            .ThenInclude(r => r.Results)
                .ThenInclude(res => res.Driver)
        .Include(dr => dr.Races)
            .ThenInclude(r => r.Results)
                .ThenInclude(res => res.Constructor)
          .ToListAsync();

    return Results.Ok(driverResults);

});


app.MapHelloWorldEndpoint();

app.Run();



public class ErgastApiDriverResponse
{
    public DriverMRData MRData { get; set; }
}

public class DriverMRData
{
    public DriverTable DriverTable { get; set; }
}

public class DriverTable
{
    public List<Driver> Drivers { get; set; }
}

public class ErgastApiDriverResultsResponse
{
    public RaceMRData MRData { get; set; }
}

public class RaceMRData
{
    public RaceTable RaceTable { get; set; }
}

public class RaceTable
{
    public List<Race> Races { get; set; }
}
public class ErgastApiCalendarResponse
{
    public CalendarMRData MRData { get; set; }
}
public class CalendarMRData
{
    public RaceTableCalendar RaceTable { get; set; }
}
public class RaceTableCalendar
{
    public List<RaceInfo> Races { get; set; }
}
public class RaceInfo
{
    public string raceName { get; set; }
    public string date { get; set; }
    public string time { get; set; }
    public QualifyingTime Qualifying { get; set; }

}
public class QualifyingTime
{
    public string date { get; set; }
    public string time { get; set; }
}