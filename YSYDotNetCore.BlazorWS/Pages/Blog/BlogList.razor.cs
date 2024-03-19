using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Reflection.Metadata;
using YSYDotNetCore.BlazorWS.Services;
using YSYDotNetCore.Models;

namespace YSYDotNetCore.BlazorWS.Pages.Blog
{


    public partial class BlogList:ComponentBase
    {
        
        [Inject]
        protected IBlogService BlogService { get; set; }

        private IEnumerable<BlogDataModel> blogs;
        public BlogDataModel Blog { get; set; }


        protected override async Task OnInitializedAsync()
        {
            blogs = await BlogService.GetBlogListAsync();
        }


        private async Task Edit(int id)
        {
            // await BlogService.GetBlogByIdAsync(id);
             Nav.NavigateTo($"/blogdetail/{id}");

        }

    }
}
