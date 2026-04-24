using System.ComponentModel.DataAnnotations;
namespace TrainingApp.Api.Dtos;

public class CreateCwiczenieDto
{
    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string NazwaCwiczenia {get; set;} = "";
    [Range(1, 20)]
    public int LiczbaSerii {get; set;}
    [Range(1, 500)]
    public int LiczbaPowtorzen {get; set;}
    [Range(0, 600)]
    public int PrzerwaMiedzySeriami {get; set;}
}