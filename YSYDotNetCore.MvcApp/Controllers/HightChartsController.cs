using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using YSYDotNetCore.MvcApp.Models;

namespace YSYDotNetCore.MvcApp.Controllers
{
    public class HightChartsController : Controller
    {
        public IActionResult StackedColumn()
        {
            List<StackedColumnModel> modelList = new List<StackedColumnModel>
            {
                new StackedColumnModel

            {
                Category = new List<string> { "Arsenal", "Chelsea", "Liverpool", "Manchester United" },
                series = new List<SeriesData>
                {
                    new SeriesData { Name = "BPL", Data = new List<int> { 3, 5, 1, 13 } },
                    new SeriesData { Name = "FA Cup", Data = new List<int> { 14, 8, 8, 12 } },
                    new SeriesData { Name = "CL", Data = new List<int> { 0, 2, 6, 3 } }
                }
            }
           };
        

            return View(modelList);
        }




    }
}
