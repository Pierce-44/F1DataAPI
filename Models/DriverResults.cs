
public class DriverResult
{
    public int Id { get; set; }
    public string driverId { get; set; }
    public List<Race> Races { get; set; }
}

public class Race
{
    public int Id { get; set; }
    public List<Result> Results { get; set; }
}

public class Result
{
    public int Id { get; set; }
    public string position { get; set; }
    public string positionText { get; set; }
    public string points { get; set; }
    public string grid { get; set; }
    // public string status { get; set; }
    public TimeDetail Time { get; set; }
    public FastestLapDetail FastestLap { get; set; }
    public DriverDetail Driver { get; set; }
    public ConstructorDetail Constructor { get; set; }

}

public class TimeDetail
{
    public int Id { get; set; }
    public string time { get; set; }
    public string millis { get; set; }
}

public class FastestLapDetail
{
    public int Id { get; set; }
    public AverageSpeedDetail AverageSpeed { get; set; }
}

public class AverageSpeedDetail
{
    public int Id { get; set; }
    public string speed { get; set; }
}

public class DriverDetail
{
    public int Id { get; set; }
    public string nationality { get; set; }
    public string dateOfBirth { get; set; }
    public string permanentNumber { get; set; }
    public string givenName { get; set; }
    public string familyName { get; set; }
}

public class ConstructorDetail
{
    public int Id { get; set; }
    public string name { get; set; }
}
