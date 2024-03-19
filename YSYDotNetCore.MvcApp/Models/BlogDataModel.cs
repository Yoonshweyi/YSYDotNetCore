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

	public class LineDataPoint
	{
		public int Y { get; set; }
		public string IndexLabel { get; set; }
		public string MarkerColor { get; set; }
		public string MarkerType { get; set; }
	}

	public class ParetoData
	{
		public string Label { get; set; }
		public int Y { get; set; }
	}

    public class PolarAreaModel
    {
        public string[] Labels { get; set; }
        public int[] PolarData { get; set; }
        
    }

    public class LineStylesChart
    {
        public List<SeriesDataSet> Series { get; set; }
        public List<string>Categories { get; set; }
    }

    public class SeriesDataSet
    {
        public string Name { get; set; }
        public List<double?> Data { get; set; }
        public string Website { get; set; }
        public string Color { get; set; }
        public string DashStyle { get; set; }
        public AccessibilityInfo Accessibility { get; set; }
    }
    public class AccessibilityInfo
    {
        public string Description { get; set; }
    }
}
