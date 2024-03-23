using Blazored.Toast.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MudBlazor;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mime;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using YSYDotNetCore.Models;

namespace YSYDotNetCore.BlazorWS.Services
{
    public class BlogService : IBlogService
    {
        public HttpClient _httpClient;
        private readonly IToastService _toastService;


        public BlogService(HttpClient httpClient, IToastService toastService)
        {
            _httpClient = httpClient;
            _toastService = toastService;
        }
        public async Task CreateBlogAsync(BlogDataModel blog)
        {
            //var BlogJson = JsonConvert.SerializeObject(blog);
            //HttpContent content = new StringContent(BlogJson, Encoding.UTF8, MediaTypeNames.Application.Json);
            var response= await _httpClient.PostAsJsonAsync($"api/Blog",blog);
            await response.Content.ReadAsStringAsync();
        }

       

        public async Task DeleteBlogAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/Blog/{id}");
        }

        public async Task<IEnumerable<BlogDataModel>> GetBlogListAsync()
        {
          return  await _httpClient.GetFromJsonAsync<IEnumerable<BlogDataModel>>("api/Blog");
        }

        public async Task<BlogDataModel?> GetBlogByIdAsync(int id)
        {
            var result= await _httpClient.GetFromJsonAsync<BlogDataModel>($"api/Blog/{id}");
            return result;
        }

        public async Task UpdateBlogAsync(BlogDataModel blog, int id)
        {
            string jsonStr = JsonConvert.SerializeObject(blog);
            HttpContent content = new StringContent(jsonStr, Encoding.UTF8, MediaTypeNames.Application.Json);
            await _httpClient.PutAsync($"api/Blog/{id}", content);
        }
    }
}
