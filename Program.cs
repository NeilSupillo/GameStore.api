using GameStore.api.Data;
using GameStore.api.Dtos;
using GameStore.api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(connString);

var app = builder.Build();

// List<GameDto> games = [
//     new (1, "Game 1", "Genre 1", 10.99m, new DateOnly(1991,3,10)),
//     new (2, "Game 2", "Genre 2", 15.99m, new DateOnly(1991,3,5)),
//     new (3, "Game 3", "Genre 3", 20.99m, new DateOnly(1991,3,2)) 
// ];

app.MapGamesEndpoints();

app.MigrateDb();

app.Run();
