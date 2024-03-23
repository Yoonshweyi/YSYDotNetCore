using Microsoft.AspNetCore.Components;
using YSYDotNetCore.BlazorWS.Services;
using YSYDotNetCore.Models;

namespace YSYDotNetCore.BlazorWS.Blog
{
    public partial class BlogDetail
    {
        [Inject]
        protected IBlogService BlogService { get; set; }

        [Parameter]
        public int Id { get; set; }

        public BlogDataModel bmodel { get; set; }

        protected override async Task OnInitializedAsync()
        {
            bmodel = await BlogService.GetBlogByIdAsync(Id);
        }

        private async Task Update()
        {
            await BlogService.UpdateBlogAsync(bmodel, Id);
            Nav.NavigateTo("/bloglist");
        }

    }

    }

