
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class DriverQualyResults
{
  public ObjectId Id { get; set; }
  public string driverId { get; set; }
  public List<RacesQualy> Races { get; set; }
}

public class RacesQualy
{
  public ObjectId Id { get; set; }
  public string raceName { get; set; }
  public QualyCircuit Circuit { get; set; }
  public List<QualifyingResults> QualifyingResults { get; set; }
}

public class QualifyingResults
{
  public ObjectId Id { get; set; }

  public string position { get; set; }
  public QualyDriver Driver { get; set; }

}

public class QualyDriver
{
  public ObjectId Id { get; set; }

  public string givenName { get; set; }
  public string familyName { get; set; }
}

public class QualyCircuit
{
  public ObjectId Id { get; set; }

  public string circuitId { get; set; }
}