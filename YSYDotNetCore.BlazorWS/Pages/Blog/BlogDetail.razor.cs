using Microsoft.AspNetCore.Blazor.Components;
using YSYDotNetCore.BlazorWS.Services;
using YSYDotNetCore.Models;

namespace YSYDotNetCore.BlazorWS.Pages.Blog
{
    public partial class BlogDetail
    {
       

    [Parameter]
    public int BlogId { get; set; }
    private BlogDataModel blog;
    private IBlogService BlogService { get; set; }

    private async Task OnInitializedAsync(int id)
    {
        blog = await BlogService.GetBlogByIdAsync(BlogId);
            Nav.NavigateTo($"/setup/blog/detail/{id}");
                
    }

    private async Task UpdateBlog()
    {
        
    }
}
}
