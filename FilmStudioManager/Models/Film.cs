using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FilmStudioManager.Models;

public class Film
{
    [Key]
    public int IDFilm;

    [Required]
    public required string FilmName { get; set; }

    [ForeignKey("Genre")]
    public int IDGenre { get; set; }

    public DateTime? DateOfRelease { get; set; }

    public List<Employee> Employees { get; } = [];
    public List<FilmEmployee> FilmEmployees { get; } = [];
}