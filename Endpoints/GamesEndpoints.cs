
using GameStore.api.Dtos;

namespace GameStore.api.Endpoints;

public static class GamesEndpoints
{
 private static List<GameDto> games = [
    new (1, "Game 1", "Genre 1", 10.99m, new DateOnly(1991,3,10)),
    new (2, "Game 2", "Genre 2", 15.99m, new DateOnly(1991,3,5)),
    new (3, "Game 3", "Genre 3", 20.99m, new DateOnly(1991,3,2)) 
];

public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app){

    const string GetGameEndpointName = "GetGame";
    var group = app.MapGroup("games").WithParameterValidation();

    group.MapGet("", () => games);

group.MapGet("/{id}", (int id) => {

    GameDto? game = games.Find(game => game.Id == id);

return game is null ? Results.NotFound() : Results.Ok(game);

}).WithName(GetGameEndpointName);  

group.MapPost("/",(CreateGameDto newGame) => {
    GameDto game = new (
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );
    games.Add(game);
     return Results.CreatedAtRoute(GetGameEndpointName, new {id = game.Id}, game);
});

group.MapPut("/{id}", (int id, UpdateGameDto updateGame) =>{
    var index = games.FindIndex(game => game.Id == id);

   if (index == -1) {
        return Results.NotFound();  // Return 404 if game is not found
    }
  games[index] = new GameDto(
    id,
    updateGame.Name,
    updateGame.Genre,
    updateGame.Price,
    updateGame.ReleaseDate
  );

  return Results.NoContent();
});


group.MapDelete("/{id}", (int id) => {

games.RemoveAll(game => game.Id == id);

return Results.NoContent();  // Return 204 if game is deleted 
});
return group;
}

}
