
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Driver
{
    public ObjectId Id { get; set; } // MongoDB will automatically generate ObjectId

    public string driverId { get; set; }
    public string givenName { get; set; }
    public string familyName { get; set; }
}
