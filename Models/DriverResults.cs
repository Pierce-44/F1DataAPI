

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class DriverResult
{
    public ObjectId Id { get; set; }
    public string driverId { get; set; }
    public List<Race> Races { get; set; }
}

public class Race
{
    public ObjectId Id { get; set; }
    public string raceName { get; set; }
    public CircuitDriver Circuit { get; set; }
    public List<Result> Results { get; set; }
}

public class Result
{
    public ObjectId Id { get; set; }
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

public class CircuitDriver
{
    public ObjectId Id { get; set; }
    public string circuitId { get; set; }
    public string circuitName { get; set; }
}

public class TimeDetail
{
    public ObjectId Id { get; set; }
    public string time { get; set; }
    public string millis { get; set; }
}

public class FastestLapDetail
{
    public ObjectId Id { get; set; }
    public AverageSpeedDetail AverageSpeed { get; set; }
}

public class AverageSpeedDetail
{
    public ObjectId Id { get; set; }
    public string speed { get; set; }
}

public class DriverDetail
{
    public ObjectId Id { get; set; }
    public string nationality { get; set; }
    public string dateOfBirth { get; set; }
    public string permanentNumber { get; set; }
    public string givenName { get; set; }
    public string familyName { get; set; }
}

public class ConstructorDetail
{
    public ObjectId Id { get; set; }
    public string name { get; set; }
    public string constructorId { get; set; }

}
