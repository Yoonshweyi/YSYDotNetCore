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

        public IActionResult LineStyles()
        {
            var model = new List<LineStylesChart>
            {
                new LineStylesChart
                {
                    Categories= new List<string> { "Dec. 2010", "May 2012", "Jan. 2014", "July 2015", "Oct. 2017", "Sep. 2019" },
                    Series= new List<SeriesDataSet>
                    {
                        new SeriesDataSet
                        {
                        Name = "NVDA",
                        Data = new List<double?> { 34.8, 43.0, 51.2, 41.4, 64.9, 72.4 },
                        Website = "https://www.nvaccess.org",
                        Color = "#ff0000", // Example color
                        Accessibility = new AccessibilityInfo { Description = "This is the most used screen reader in 2019." }
                        },
                        new SeriesDataSet
                        {
                        Name = "JAWS",
                        Data = new List<double?> {69.6, 63.7, 63.9, 43.7, 66.0, 61.7 },
                        Website = "https://www.freedomscientific.com/Products/Blindness/JAWS",
                        DashStyle= "ShortDashDot",
                        Color = "#ff0000" // Example color
                        },
                         new SeriesDataSet
                        {
                        Name = "VoiceOver",
                        Data = new List<double?> {20.2, 30.7, 36.8, 30.9, 39.6, 47.1},
                        Website = "http://www.apple.com/accessibility/osx/voiceover",
                        DashStyle= "ShortDot",
                        Color = "#00ff00" // Example color
                        },
                          new SeriesDataSet
                        {
                        Name = "Narrator",
                        Data = new List<double?> {null, null, null, null, 21.4, 30.3},
                        Website = "https://support.microsoft.com/en-us/help/22798/windows-10-complete-guide-to-narrator",
                        DashStyle= "Dash",
                        Color = "#ff8000" // Example color
                        },
                           new SeriesDataSet
                        {
                        Name = "ZoomText/Fusion",
                        Data = new List<double?> {6.1, 6.8, 5.3, 27.5, 6.0, 5.5},
                        Website = "http://www.zoomtext.com/products/zoomtext-magnifierreader",
                        DashStyle= "ShortDot",
                        Color = "#00ffff" // Example color
                        },
                            new SeriesDataSet
                        {
                        Name = "Other",
                        Data = new List<double?> {42.6, 51.5, 54.2, 45.8, 20.2, 15.4},
                        Website = "http://www.disabled-world.com/assistivedevices/computer/screen-readers.php",
                        DashStyle= "ShortDash",
                        Color = "#ffff00" // Example color
                        },

                    }
                }
            };
            return View(model);
        }




    }
}
