using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using YSYDotNetCore.RealTimeChart.Hubs;

namespace YSYDotNetCore.RealTimeChart.Controllers
{
    public class TeamController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly IHubContext<TeamHub> _hubContext;

        public TeamController(AppDbContext dbContext,IHubContext<TeamHub> hubContext)
        {
            _dbContext = dbContext;
            _hubContext = hubContext;
        }

        [ActionName("Index")]
        public IActionResult TeamIndex()
        {
            return View("TeamIndex");
        }

        [ActionName("Create")]

        public IActionResult TeamCreate()
        {
            return View("TeamCreate");
        }

        [ActionName("Save")]
        public async Task<IActionResult> TeamSave(TeamDataModel team)
        {
            await _dbContext.AddAsync(team);
            await _dbContext.SaveChangesAsync();

            var lst = await _dbContext.Teams
                .AsNoTracking()
                .ToListAsync();

            var data = new
            {
                Series = lst.Select(x => x.Score).ToList(),
                Labels = lst.Select(x => x.TeamName).ToList()
            };

            string json = JsonSerializer.Serialize(data);
            await _hubContext.Clients.All.SendAsync("ReceiveTeamClientEvent", json);
            return Redirect("/Team/Create");
        }
    }
}
