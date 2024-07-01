using System.Globalization;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.Bson;


var builder = WebApplication.CreateBuilder(args);

var connectionUri = builder.Configuration.GetConnectionString("MongoDb");

var settings = MongoClientSettings.FromConnectionString(connectionUri);

// Set the ServerApi field of the settings object to set the version of the Stable API on the client
settings.ServerApi = new ServerApi(ServerApiVersion.V1);

// Create a new client and connect to the server
var client = new MongoClient(settings);

var driversCollection = client.GetDatabase("f1fastfacts").GetCollection<Driver>("drivers");
var driversResultsCollection = client.GetDatabase("f1fastfacts").GetCollection<DriverResult>("driverResults");
var calendarCollection = client.GetDatabase("f1fastfacts").GetCollection<CalendarRace>("calendar");
var teamCollection = client.GetDatabase("f1fastfacts").GetCollection<Team>("teams");
var driverQualyCollection = client.GetDatabase("f1fastfacts").GetCollection<DriverQualyResults>("driverQualyResults");
var forumsCollection = client.GetDatabase("f1fastfacts").GetCollection<Forum>("forums");


builder.Services.AddHttpClient();

var app = builder.Build();

// REMOVE FOR LOCAL TESTING
// no need to hide this part always uses 80 now
// var port = Environment.GetEnvironmentVariable("PORT") ?? "80";
// app.Urls.Add($"http://*:{port}");

app.MapPost("/sync-drivers", async (IHttpClientFactory httpClientFactory) =>
{
    var clientFetch = httpClientFactory.CreateClient();
    var response = await clientFetch.GetFromJsonAsync<ErgastApiDriverResponse>("http://ergast.com/api/f1/2024/drivers.json");

    if (response == null)
        return Results.BadRequest();

    var driversList = await driversCollection.Find(new BsonDocument()).ToListAsync();

    foreach (var driver in response.MRData.DriverTable.Drivers)
    {
        if (!driversList.Any(d => d.driverId == driver.driverId))
        {
            await driversCollection.InsertOneAsync(new Driver
            {
                driverId = driver.driverId,
                givenName = driver.givenName,
                familyName = driver.familyName
            });
        }
    }

    var driversListUpdated = await driversCollection.Find(new BsonDocument()).ToListAsync();

    return Results.Ok(driversListUpdated);
});

app.MapGet("/drivers", async () =>
{
    var driversList = await driversCollection.Find(new BsonDocument()).ToListAsync();

    return Results.Ok(driversList);
});

app.MapPost("/sync-teams", async (IHttpClientFactory httpClientFactory) =>
{
    var clientFetch = httpClientFactory.CreateClient();
    var responseConstructors = await clientFetch.GetFromJsonAsync<ErgastApiConstructorsResponse>("http://ergast.com/api/f1/2024/constructors.json");

    var teamList = await teamCollection.Find(new BsonDocument()).ToListAsync();

    if (responseConstructors == null)
        return Results.BadRequest();

    foreach (var team in responseConstructors.MRData.ConstructorTable.Constructors)
    {
        if (!teamList.Any(dbTeam => dbTeam.constructorId == team.constructorId))
        {
            var constructorId = team.constructorId;
            var responseTeam = await clientFetch.GetFromJsonAsync<ErgastApiTeamResponse>($"http://ergast.com/api/f1/2024/constructors/{constructorId}/drivers.json");

            var newTeam = new Team
            {
                constructorId = team.constructorId,
                teamName = team.name,
                Drivers = responseTeam.MRData.DriverTable.Drivers.Select(driverHere => new TeamDrivers
                {
                    familyName = driverHere.familyName,
                    givenName = driverHere.givenName,
                    driverId = driverHere.driverId
                }).ToList() ?? new List<TeamDrivers>()
            };

            await teamCollection.InsertOneAsync(newTeam);
        }
    }

    var teamListUpdated = await teamCollection.Find(new BsonDocument()).ToListAsync();

    return Results.Ok(teamListUpdated);
});


app.MapGet("/teams", async () =>
{
    var teamList = await teamCollection.Find(new BsonDocument()).ToListAsync();

    return Results.Ok(teamList);
});

app.MapPost("/sync-driver-qualy", async (IHttpClientFactory httpClientFactory) =>
{

    var DriversArray = await driversCollection.Find(new BsonDocument()).ToListAsync();
    var DriversQualyResultsArray = await driverQualyCollection.Find(new BsonDocument()).ToListAsync();

    foreach (var driver in DriversArray)
    {
        var driverId = driver.driverId;

        var clientFetch = httpClientFactory.CreateClient();
        var response = await clientFetch.GetFromJsonAsync<ErgastApiQualiResponse>($"http://ergast.com/api/f1/2024/drivers/{driverId}/qualifying.json");

        if (!DriversQualyResultsArray.Any(d => d.driverId == driver.driverId))
        {
            var driverResult = new DriverQualyResults
            {
                driverId = driver.driverId,
                Races = response.MRData.RaceTable.Races.Select(apiRace => new RacesQualy
                {
                    raceName = apiRace.raceName,
                    Circuit = new QualyCircuit
                    {
                        circuitId = apiRace.Circuit.circuitId
                    },
                    QualifyingResults = apiRace.QualifyingResults.Select(result =>
                    new QualifyingResults
                    {
                        position = result.position,
                        Driver = new QualyDriver
                        {
                            familyName = result.Driver.familyName,
                            givenName = result.Driver.givenName
                        }
                    }).ToList()
                }).ToList()
            };

            await driverQualyCollection.InsertOneAsync(driverResult);
        }
    }

    var driverQualyListUpdated = await driverQualyCollection.Find(new BsonDocument()).ToListAsync();

    return Results.Ok(driverQualyListUpdated);

});

app.MapGet("/qualyfying/{driverId}/results", async (string driverId) =>
{
    var driverResult = await driverQualyCollection.Find(Builders<DriverQualyResults>.Filter.Eq(d => d.driverId, driverId)).ToListAsync();

    return Results.Ok(driverResult);
});

app.MapPost("/sync-calendar", async (IHttpClientFactory httpClientFactory) =>
{
    var clientFetch = httpClientFactory.CreateClient();
    var response = await clientFetch.GetFromJsonAsync<ErgastApiCalendarResponse>("http://ergast.com/api/f1/current.json");

    var calendarList = await calendarCollection.Find(new BsonDocument()).ToListAsync();

    if (response == null)
        return Results.BadRequest();

    foreach (var race in response.MRData.RaceTable.Races)
    {
        if (!calendarList.Any(dbRace => dbRace.raceName == race.raceName))
        {

            await calendarCollection.InsertOneAsync(new CalendarRace
            {
                raceName = race.raceName,
                date = race.date,
                time = race.time,
                Qualifying = new Qualifying
                {
                    date = race.Qualifying.date,
                    time = race.Qualifying.time,
                },
                Circuit = new CalendarCircuit
                {
                    circuitId = race.Circuit.circuitId,
                    circuitName = race.Circuit.circuitName,
                    Location = race.Circuit.Location,
                }
            });
        }
    }

    var calendarListUpdated = await calendarCollection.Find(new BsonDocument()).ToListAsync();

    return Results.Ok(calendarListUpdated);
});

app.MapGet("/calendar", async () =>
{
    var calendarList = await calendarCollection.Find(new BsonDocument()).ToListAsync();

    return Results.Ok(calendarList);
});

app.MapPost("/sync-forums", async () =>
{
    var teamList = await teamCollection.Find(new BsonDocument()).ToListAsync();
    var driversList = await driversCollection.Find(new BsonDocument()).ToListAsync();
    var forumList = await forumsCollection.Find(new BsonDocument()).ToListAsync();

    foreach (var team in teamList)
    {
        var forum = new Forum
        {
            forumName = team.teamName,
        };

        await forumsCollection.InsertOneAsync(forum);
    }

    foreach (var driver in driversList)
    {
        var forum = new Forum
        {
            forumName = driver.givenName + "_" + driver.familyName,
        };

        await forumsCollection.InsertOneAsync(forum);
    }

    var forumListResponse = await forumsCollection.Find(new BsonDocument()).ToListAsync();


    return Results.Ok(forumListResponse);
});

app.MapGet("/forums", async () =>
{
    var forumList = await forumsCollection.Find(new BsonDocument()).ToListAsync();

    return Results.Ok(forumList);
});

app.MapPost("/sync-drivers-results", async (IHttpClientFactory httpClientFactory) =>
{
    var DriversArray = await driversCollection.Find(new BsonDocument()).ToListAsync();
    var DriversResultsArray = await driversResultsCollection.Find(new BsonDocument()).ToListAsync();


    foreach (var driver in DriversArray)
    {
        var driverId = driver.driverId;

        var clientFetch = httpClientFactory.CreateClient();
        var response = await clientFetch.GetFromJsonAsync<ErgastApiDriverResultsResponse>($"http://ergast.com/api/f1/2024/drivers/{driverId}/results.json");

        var DatabaseDriverInfo = DriversResultsArray
            .Where(result => result.driverId == driverId)
            .ToArray();


        if (response.MRData.RaceTable.Races.Count > DatabaseDriverInfo[0].Races.Count)
        {
            for (int i = DriversResultsArray.Count; i < response.MRData.RaceTable.Races.Count; i++)
            {

                var driverResult = new DriverResult
                {
                    driverId = driverId,
                    Races = response.MRData.RaceTable.Races.Select(apiRace => new Race
                    {
                        raceName = apiRace.raceName,
                        Circuit = new CircuitDriver
                        {
                            circuitId = apiRace.Circuit.circuitId,
                            circuitName = apiRace.Circuit.circuitName
                        },
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
                                name = apiResult.Constructor.name ?? "unknown",
                                constructorId = apiResult.Constructor.constructorId ?? "unknown"
                            } : new ConstructorDetail
                            {
                                name = "unknown",
                                constructorId = "unknown"
                            }
                        }).ToList() ?? new List<Result>()
                    }).ToList()
                };

                await driversResultsCollection.InsertOneAsync(driverResult);

            }







        }
    }
    var driversResultsCollectionUpdated = await driversResultsCollection.Find(new BsonDocument()).ToListAsync();

    return Results.Ok(driversResultsCollectionUpdated);
});

app.MapGet("/drivers/{driverId}/results", async (string driverId) =>
{
    var driverResult = await driversResultsCollection.Find(Builders<DriverResult>.Filter.Eq(d => d.driverId, driverId)).ToListAsync();

    return Results.Ok(driverResult);
});

app.Run();
