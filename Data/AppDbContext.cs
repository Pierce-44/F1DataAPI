using Microsoft.EntityFrameworkCore;


public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options)
{
    public DbSet<Driver> Drivers => Set<Driver>();
    public DbSet<DriverResult> DriverResults => Set<DriverResult>();
    public DbSet<Race> Races => Set<Race>();
    public DbSet<Result> Results => Set<Result>();
    public DbSet<TimeDetail> Time => Set<TimeDetail>();
    public DbSet<FastestLapDetail> FastestLap => Set<FastestLapDetail>();
    public DbSet<AverageSpeedDetail> AverageSpeed => Set<AverageSpeedDetail>();
    public DbSet<DriverDetail> Driver => Set<DriverDetail>();
    public DbSet<ConstructorDetail> Constructor => Set<ConstructorDetail>();
    public DbSet<CalendarRace> Calendar => Set<CalendarRace>();
    public DbSet<Team> Teams => Set<Team>();
    public DbSet<DriverQualyResults> DriverQualyResults => Set<DriverQualyResults>();
}



