using System.ComponentModel.DataAnnotations;

public class Employee
{
    [Key]
    public int EmployeeId { get; set; }

    [Required]
    public required string FirstName { get; set; }

    [Required]
    public required string LastName { get; set; }

    public string? Role { get; set; }

    public List<Film> Films { get; } = [];
    public List<FilmEmployee> FilmEmployees { get; } = [];
}