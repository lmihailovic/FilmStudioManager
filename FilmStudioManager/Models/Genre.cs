using System.ComponentModel.DataAnnotations;

public class Genre
{
    [Key]
    public int GenreId { get; set; }

    [Required]
    public required string GenreName { get; set; }
}