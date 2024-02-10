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




}
