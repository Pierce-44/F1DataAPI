// using Microsoft.Extensions.Options;
// using MongoDB.Driver;
// using MongoDB.Bson;

// public class MongoDBService
// {

//   private readonly IMongoCollection<Driver> _driverCollection;

//   public MongoDBService()
//   {

//     const string connectionUri = "mongodb+srv://pierce:Ui8SqcpRkO2ym3iy@cluster0.pq4hwjk.mongodb.net/?appName=Cluster0";

//     var settings = MongoClientSettings.FromConnectionString(connectionUri);

//     // Set the ServerApi field of the settings object to set the version of the Stable API on the client
//     settings.ServerApi = new ServerApi(ServerApiVersion.V1);



//     MongoClient client = new MongoClient(settings);
//     IMongoDatabase database = client.GetDatabase("f1fastfacts");

//   }

// }