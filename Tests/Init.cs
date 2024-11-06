using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace MongoDB.Entities.Tests;

[TestClass]
public static class InitTest
{
    public static MongoClientSettings ClientSettings { get; set; }
    public static string ConnectionString { get; set; }
    [AssemblyInitialize]
    public static async Task Init(TestContext _)
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        ConnectionString=System.Environment.GetEnvironmentVariable("APP_MONGO_CONNECTIONSTRING")!;
        ClientSettings= MongoClientSettings.FromConnectionString(ConnectionString);
        await InitTestDatabase("test");
    }

    public static async Task InitTestDatabase(string databaseName)
    {
        await DB.InitAsync(databaseName, ClientSettings);
    }
}