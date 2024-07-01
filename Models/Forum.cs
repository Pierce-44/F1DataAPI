
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Forum
{
  public ObjectId Id { get; set; } // MongoDB will automatically generate ObjectId

  public string forumName { get; set; }
}
