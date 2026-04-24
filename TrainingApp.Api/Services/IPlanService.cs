using TrainingApp.Api.Dtos;
using TrainingApp.Api.Models;

namespace TrainingApp.Api.Services;

public interface IPlanService
{
    List<PlanDto> GetAllPlans();
    PlanDto? GetPlanById(int id);
    Plan CreatePlan(CreatePlanDto dto);
    int DeletePlanById(int id);
    int UpdatePlan(int id, UpdatePlanDto dto);

    List<CwiczenieDto> GetCwiczeniaByPlanId(int planId);
    CwiczenieDto? AddCwiczenieToPlan(int planId, CreateCwiczenieDto dto);
    int UpdateCwiczenie(int id, UpdateCwiczenieDto dto);
    int DeleteCwiczenie(int id);
}