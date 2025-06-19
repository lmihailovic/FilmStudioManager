using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FilmStudioManager.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace FilmStudioManager.Models;

public class Film
{
    [Key]
    public int FilmId { get; set; }

    [Required]
    public required string FilmName { get; set; }

    [ForeignKey("Genre")]
    public int GenreId { get; set; }

    public DateTime? DateOfRelease { get; set; }

    public List<Employee> Employees { get; } = [];
    public List<FilmEmployee> FilmEmployees { get; } = [];
    
    [ValidateNever]
    public Genre Genre { get; set; }
}