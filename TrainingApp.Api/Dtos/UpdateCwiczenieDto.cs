namespace TrainingApp.Api.Dtos;

public class UpdateCwiczenieDto
{
    public string NazwaCwiczenia {get; set;} = "";
    public int LiczbaSerii {get; set;}
    public int LiczbaPowtorzen {get; set;}
    public int PrzerwaMiedzySeriami {get; set;}
}