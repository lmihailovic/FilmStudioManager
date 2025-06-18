using System.ComponentModel.DataAnnotations;

namespace FilmStudioManager.Models;


public class Genre
{
    [Key]
    public int GenreId { get; set; }

    [Required]
    public required string GenreName { get; set; }
}