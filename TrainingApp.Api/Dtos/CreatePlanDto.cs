using System.ComponentModel.DataAnnotations;
namespace TrainingApp.Api.Dtos;

public class CreatePlanDto
{
    [Required]
    [StringLength(50, MinimumLength =2)]
    public string Nazwa {get; set;} = "";
    [Range(1,10)]
    public int Poziom {get; set;}
    [Required]
    [StringLength(30, MinimumLength = 2)]
    public string Rodzaj {get; set;} = "";
    [Range(1, 20)]
    public int IloscObwodow {get; set;}
    [Range(0, 600)]
    public int PrzerwaMiedzyObwodami {get; set;}
    
}