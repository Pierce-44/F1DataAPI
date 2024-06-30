
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class CalendarRace
{
  public ObjectId Id { get; set; }
  public string raceName { get; set; }
  public string date { get; set; }
  public string time { get; set; }

  public CalendarCircuit Circuit { get; set; }
  public Qualifying Qualifying { get; set; }
}

public class Qualifying
{
  public ObjectId Id { get; set; }
  public string date { get; set; }
  public string time { get; set; }
}

public class CalendarCircuit
{
  public ObjectId Id { get; set; }
  public string circuitId { get; set; }
  public string circuitName { get; set; }
  public CalendarLocation Location { get; set; }
}

public class CalendarLocation
{
  public ObjectId Id { get; set; }
  public string locality { get; set; }
  public string country { get; set; }
}