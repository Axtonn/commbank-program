using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommBank.Models;
using CommBank.Services;

namespace CommBank.Tests.Fakes;

public class FakeGoalsService : IGoalsService
{
    private readonly List<Goal> _goals;

    public FakeGoalsService(List<Goal> goals)
    {
        _goals = goals;
    }

    public Task<List<Goal>> GetAsync() =>
        Task.FromResult(_goals);

    public Task<Goal?> GetAsync(string id) =>
        Task.FromResult(_goals.FirstOrDefault(g => g.Id == id));

    public Task CreateAsync(Goal newGoal)
    {
        _goals.Add(newGoal);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(string id, Goal updatedGoal)
    {
        var idx = _goals.FindIndex(g => g.Id == id);
        if (idx >= 0) _goals[idx] = updatedGoal;
        return Task.CompletedTask;
    }

    public Task RemoveAsync(string id)
    {
        _goals.RemoveAll(g => g.Id == id);
        return Task.CompletedTask;
    }

    public Task<List<Goal>> GetForUserAsync(string userId) =>
        Task.FromResult(_goals.Where(g => g.UserId == userId).ToList());
}
