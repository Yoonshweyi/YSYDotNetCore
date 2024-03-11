using Microsoft.AspNetCore.Components;
using MudBlazor;
using Newtonsoft.Json;
using System.Net.Mime;
using System.Text;
using YSYDotNetCore.Models;
using static MudBlazor.CategoryTypes;

namespace YSYDotNetCore.BlazorWasm.Pages.Blog
{
    public partial class PageBlogUpdate
    {
        [Parameter]
        public int id { get; set; }

        private BlogDataModel model;
        protected override async Task OnInitializedAsync()
        {
            var result = await HttpClient.GetAsync($"api/Blog/{id}");
            if (result.IsSuccessStatusCode)
            {
                string jsonStr = await result.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<BlogDataModel>(jsonStr)!;
            }
        }

     private async Task Update()
        {
            string jsonStr = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(jsonStr, Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await HttpClient.PutAsync($"api/Blog/{id}", content);
            if (response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                Snackbar.Add(message, Severity.Success);
                Nav.NavigateTo("/setup/blog");
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                Snackbar.Add(message, Severity.Error);
                Nav.NavigateTo("/setup/blog");
            }
        }



    }
}
