using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Runtime.Serialization;
using YSYDotNetCore.MvcApp.Models;

namespace YSYDotNetCore.MvcApp.Controllers
{
    public class ApexChartController : Controller
    {
        public IActionResult RangeArea()
        {
            var temperatureData = new List<TemperatureData>
        {
            new TemperatureData
            {
                Name = "New York Temperature",
                Data = new List<DataPoint>
                {
                    new DataPoint { X = "Jan", Y = new int[] {-2, 4} },
                    new DataPoint { X = "Feb", Y = new int[] {-1, 6} },
                    new DataPoint { X = "Mar", Y = new int[] {3, 10} },
                    new DataPoint { X = "Apr", Y = new int[] {8, 16} },
                    new DataPoint { X = "May", Y = new int[] {13, 22} },
                    new DataPoint { X = "Jun", Y = new int[] {18, 26} },
                    new DataPoint { X = "Jul", Y = new int[] {21, 29} },
                    new DataPoint { X = "Aug", Y = new int[] {21, 28} },
                    new DataPoint { X = "Sep", Y = new int[] {17, 24} },
                    new DataPoint { X = "Oct", Y = new int[] {11, 18} },
                    new DataPoint { X = "Nov", Y = new int[] {6, 12} },
                    new DataPoint { X = "Dec", Y = new int[] {1, 7} }
                }
            }
        };

            return View(temperatureData);

        }
    }
}
