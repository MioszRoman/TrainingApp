namespace TrainingApp.Api.Dtos;

public class CwiczenieDto
{
    public int Id {get; set;}
    public string NazwaCwiczenia {get; set;} = "";
    public int LiczbaSerii {get; set;}
    public int LiczbaPowtorzen {get; set;}
    public int PrzerwaMiedzySeriami {get; set;}
}