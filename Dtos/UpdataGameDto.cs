using System.ComponentModel.DataAnnotations;

namespace GameStore.api.Dtos;

public record class UpdateGameDto
(
    [Required][StringLength(50)] string Name,
    [Required][StringLength(20)] string Genre,
    [Range(1, 10)] decimal Price,
    DateOnly ReleaseDate
);