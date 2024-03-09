using Newtonsoft.Json;
using YSYDotNetCore.Models;
using static MudBlazor.CategoryTypes;



namespace YSYDotNetCore.BlazorWasm.Pages.Blog
{
    public partial class PageBlog
    {
        private int _pageNo = 1;
        private int _pageSize = 10;

        private BloglistResponseModel? _bloglistResponseModel;
        protected override async Task OnAfterRenderAsync(bool firstRender)
       {
           if (firstRender)
            {
                await List(_pageNo,_pageSize);
               
            }
        }

        private async Task List(int pageNo,int pageSize=10)
        {
            _pageNo = pageNo;
            _pageSize = pageSize;
            var result = await HttpClient.GetAsync($"api/Blog/{pageNo}/{pageSize}");
            if (result.IsSuccessStatusCode)
            {
                var jsonstr = await result.Content.ReadAsStringAsync();
              _bloglistResponseModel = JsonConvert.DeserializeObject<BloglistResponseModel>(jsonstr)!;
                StateHasChanged();
            }
            
        }

        private async void PageChanged(int i)
        {
            _pageNo = i;
           await List(_pageNo);
        }
    }

    
}
