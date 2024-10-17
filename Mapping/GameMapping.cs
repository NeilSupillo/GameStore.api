using System;
using GameStore.api.Dtos;
using GameStore.api.Entities;

namespace GameStore.api.Mapping;

public static class GameMapping
{
    public static Game ToEntity(this CreateGameDto game)
    {

        return new Game()
        {
            Name = game.Name,
            GenreId = game.GenreId,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate,

        };
    }

    public static GameSummaryDto ToGameSummaryDto(this Game game)
    {

        return new(
            game.Id,
            game.Name,
            game.Genre!.Name,
            game.Price,
            game.ReleaseDate
        );
    }
    public static GameDetailsDto ToGameDetailsDto(this Game game)
    {

        return new(
            game.Id,
            game.Name,
            game.GenreId,
            game.Price,
            game.ReleaseDate
        );
    }
}