using TrainingApp.Api.Dtos;

namespace TrainingApp.Api.Services;

public interface IStatsService
{
    StatsDto? GetStats();
    MostFrequentPlanDto? GetMostFrequentPlan();
    StatsDto? GetLastWeekStats();
}