using Microsoft.AspNetCore.Mvc;
using YSYDotNetCore.MvcApp.Models;

namespace YSYDotNetCore.MvcApp.Controllers
{
    public class CanvasJsController : Controller
    {
        public IActionResult SimpleLineChart()
        {
			var dataPoints = new List<LineDataPoint>
			{
				new LineDataPoint { Y = 450 },
				new LineDataPoint { Y = 414 },
				new LineDataPoint { Y = 520, IndexLabel = "\u2191 highest", MarkerColor = "red", MarkerType = "triangle" },
				new LineDataPoint { Y = 460 },
				new LineDataPoint { Y = 450 },
				new LineDataPoint { Y = 500 },
				new LineDataPoint { Y = 480 },
				new LineDataPoint { Y = 480 },
				new LineDataPoint { Y = 410, IndexLabel = "\u2193 lowest", MarkerColor = "DarkSlateGrey", MarkerType = "cross" },
				new LineDataPoint { Y = 500 },
				new LineDataPoint { Y = 480 },
				new LineDataPoint { Y = 510 },
			};

			return View(dataPoints);
        }

		public IActionResult ParetoChart()
		{
			var ParetoDataSet = new List<ParetoData>
		{
			new ParetoData { Label = "Subways", Y = 44853 },
			new ParetoData { Label = "McDonald", Y = 36525 },
			new ParetoData { Label = "Starbucks", Y = 23768 },
			new ParetoData { Label = "KFC", Y = 19420 },
			new ParetoData { Label = "Pizza Hut", Y = 13528 },
			new ParetoData { Label = "Dunkin Donuts", Y = 11906 }
		};

			return View(ParetoDataSet);
			
		}
    }
}
