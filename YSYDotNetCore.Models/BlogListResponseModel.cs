
namespace YSYDotNetCore.Models
{
    public class BloglistResponseModel
    {
        public bool EndOfPage {  get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int PageNo { get; set; }
        public List<BlogDataModel> Data { get; set; }
    }
}
