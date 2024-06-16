using Microsoft.EntityFrameworkCore;

class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

}