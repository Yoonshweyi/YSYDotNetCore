using Microsoft.JSInterop;
using MudBlazor;
using Newtonsoft.Json;
using System.Net.Mime;
using System.Text;
using YSYDotNetCore.Models;

namespace YSYDotNetCore.BlazorWasm.Pages.Blog
{
    public partial class PageBlogCreate
    {
        private BlogDataModel requestModel=new BlogDataModel();

        private async Task Save()
        {
            var BlogJson=JsonConvert.SerializeObject(requestModel);
            HttpContent content = new StringContent(BlogJson,Encoding.UTF8,MediaTypeNames.Application.Json);
            var response=await   HttpClient.PostAsync("api/Blog", content);
            if(response.IsSuccessStatusCode)
            {
               var message= await response.Content.ReadAsStringAsync();
                //await JSRuntime.InvokeVoidAsync("alert", message);
                Snackbar.Add(message, Severity.Success);
                Nav.NavigateTo("/setup/Blog");
               // Console.WriteLine(message);
            }
        }
    }
}
