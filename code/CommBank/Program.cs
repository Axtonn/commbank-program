using CommBank.Services;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("Secrets.json");

// mongo + db instance
var MongoClient = new MongoClient(builder.Configuration.GetConnectionString("Commbank"));
var mongoDatabase = MongoClient.GetDatabase("data");

// test connection to db
try
{
    await mongoDatabase.RunCommandAsync((Command<MongoDB.Bson.BsonDocument>)"{ping:1}");
    Console.WriteLine("✅ MongoDB connection OK (ping succeeded)");
}
catch (Exception ex)
{
    Console.WriteLine("❌ MongoDB connection failed: " + ex.Message);
}


// register dependencies for DI
IGoalsService goalsService = new GoalsService(mongoDatabase);
builder.Services.AddSingleton(goalsService);

// enable controoller to handle routes
builder.Services.AddControllers();

var app = builder.Build();

// map routes
app.MapControllers();

app.Run();