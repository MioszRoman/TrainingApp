namespace TrainingApp.Api.Dtos;

public class PlanDto
{
    public int Id {get; set;}
    public string Nazwa {get; set;} = "";
    public int Poziom {get; set;}
    public string Rodzaj {get; set;} = "";
    public int IloscObwodow {get; set;}
    public int PrzerwaMiedzyObwodami {get; set;}
    public List<CwiczenieDto> Cwiczenia {get; set;} = new();
}