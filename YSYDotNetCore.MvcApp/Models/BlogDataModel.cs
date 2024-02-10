using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YSYDotNetCore.MvcApp.Models
{
    [Table("Tbl_Blog")]
    public class BlogDataModel
    {
        [Key]
        public int Blog_Id { get; set; }

        public string? Blog_Title { get; set; }

        public string? Blog_Author { get; set; }

        public string? Blog_content { get; set; }
    }

    public class TemperatureData
    {
        public string Name { get; set; }
        public List<DataPoint> Data { get; set; }
    }

    public class DataPoint
    {
        public string X { get; set; }
        public int[] Y { get; set; }
    }

    public class ScatterDataPoint
    {
        public List<ScatterData> Data { get; set; }
    }
    public class ScatterData
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

    public class StackedColumnModel
    {
        public List<string> Category { get; set; }
        public List<SeriesData> series { get; set; }
       

    }

    public class SeriesData
    {
        public string Name { get; set; }
        public List<int> Data { get; set; }
    }


}
