using Microsoft.AspNetCore.Mvc;
using YSYDotNetCore.MvcApp.Models;

namespace YSYDotNetCore.MvcApp.Controllers
{
    public class ChartJsController : Controller
    {
        public IActionResult ScatterChart()
        {
            var scatterDataset = new List<ScatterDataPoint>
            {new ScatterDataPoint
            {
                Data=new List<ScatterData>
                {
                     new ScatterData { X = -10, Y = 0 },
                     new ScatterData { X = 0, Y = 10 },
                     new ScatterData { X = 10, Y = 5 },
                     new ScatterData { X = 0.5, Y = 5.5 }
                }
               }
               
            };
            return View(scatterDataset);
        }
    }
}
