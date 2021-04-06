using BowlingLeague.Models;
using BowlingLeague.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLeague.Controllers
{
    public class HomeController : Controller
    {
        private BowlingLeagueContext context { get; set; }
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, BowlingLeagueContext ctx)
        {
            _logger = logger;
            context = ctx;
        }

        public IActionResult Index(long? teamId, string teamName, int pageNum = 0)
        {
            int pageSize = 5;
            return View(new IndexViewModel
            {
                Bowlers = context.Bowlers
                    .Where(b => b.TeamId == teamId || teamId == null)
                    .OrderBy(b => b.Team)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize)
                    .ToList(),
                PageNumberingInfo = new PageNumberingInfo
                {
                    NumItemsPerPage = pageSize,
                    CurrentPage = pageNum,
                    TotalNumItems = (teamId == null ? context.Bowlers.Count() :
                    context.Bowlers.Where(b => b.TeamId == teamId).Count())
                },
                TeamName = teamName
            });
        }
            public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
