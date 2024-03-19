using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using YSYDotNetCore.BlazorWasm.Shared;
using YSYDotNetCore.Models;
using static MudBlazor.CategoryTypes;



namespace YSYDotNetCore.BlazorWasm.Pages.Blog
{
    public partial class PageBlog
    {
        private int _pageNo = 1;
        private int _pageSize = 10;
       

        private BloglistResponseModel? _bloglistResponseModel;
        private BlogDataModel requestModel = new BlogDataModel();
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

        private async Task Delete(int id)
        {
            var parameters = new DialogParameters<ConfirmDialog>();
            parameters.Add(x => x.Message, "Are You Sure Want to delete");
            
            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

           var dialog=await DialogService.ShowAsync<ConfirmDialog>("Confirm", parameters, options);
            var result= await dialog.Result;
            if (result.Canceled) return;

            var response = await HttpClient.DeleteAsync($"api/Blog/{id}");
            if (response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                await List(_pageNo, _pageSize);
                await JSRuntime.InvokeVoidAsync("alert", message);
                //this.StateHasChanged();
                

            }


        }

        private async Task Edit(int id)
        {
            Nav.NavigateTo($"setup/blog/update/{id}");
        }


    }

    
}
