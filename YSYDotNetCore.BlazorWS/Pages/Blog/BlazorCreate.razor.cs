using Microsoft.AspNetCore.Components;
using MudBlazor;
using Newtonsoft.Json;
using System.Net.Mime;
using System.Text;
using YSYDotNetCore.BlazorWS.Services;
using YSYDotNetCore.Models;
using static MudBlazor.CategoryTypes;

namespace YSYDotNetCore.BlazorWS.Pages.Blog
{

    public partial class BlazorCreate
    {


        [Inject]
        protected IBlogService BlogService { get; set; }
        BlogDataModel Blog = new BlogDataModel();
        protected async void Save()
        {
            //var BlogJson = JsonConvert.SerializeObject(Blog);
            // HttpContent content = new StringContent(BlogJson, Encoding.UTF8, MediaTypeNames.Application.Json);
            await BlogService.CreateBlogAsync(Blog);
            Nav.NavigateTo("/bloglist");
        }

    }
}
