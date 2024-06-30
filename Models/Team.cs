
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Team
{
  public ObjectId Id { get; set; } // MongoDB will automatically generate ObjectId

  public string constructorId { get; set; }
  public List<TeamDrivers> Drivers { get; set; }
}

public class TeamDrivers
{
  public ObjectId Id { get; set; } // MongoDB will automatically generate ObjectId

  public string givenName { get; set; }
  public string familyName { get; set; }
  public string driverId { get; set; }
}