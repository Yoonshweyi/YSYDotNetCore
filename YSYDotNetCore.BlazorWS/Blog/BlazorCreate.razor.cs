using Microsoft.AspNetCore.Components;
using MudBlazor;
using Newtonsoft.Json;
using System.Net.Mime;
using System.Text;
using YSYDotNetCore.BlazorWS.Services;
using YSYDotNetCore.Models;
using static MudBlazor.CategoryTypes;

namespace YSYDotNetCore.BlazorWS.Blog
{

    public partial class BlazorCreate
    {


        [Inject]
        protected IBlogService BlogService { get; set; }
        BlogDataModel Blog = new BlogDataModel();
        protected async void Save()
        {
            await BlogService.CreateBlogAsync(Blog);
            Nav.NavigateTo("/bloglist");
        }

    }
}
