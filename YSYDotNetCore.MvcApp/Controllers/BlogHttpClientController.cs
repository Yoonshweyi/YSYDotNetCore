using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Text;
using YSYDotNetCore.MvcApp.Models;
using static System.Net.Mime.MediaTypeNames;

namespace YSYDotNetCore.MvcApp.Controllers
{
    public class BlogHttpClientController : Controller
    {
        private readonly HttpClient _httpClient;
        


        public BlogHttpClientController(HttpClient httpclient)
        {
            _httpClient = httpclient;
        }
        public async Task<IActionResult> Index()
        {
            var lst = new List<BlogDataModel>();
            var response = await _httpClient.GetAsync("api/Blog");
            if(response.IsSuccessStatusCode)
            {
                string jsonstr = await response.Content.ReadAsStringAsync();
                 lst = JsonConvert.DeserializeObject<List<BlogDataModel>>(jsonstr)!;
            }
            return View(lst);
        }

        public async Task<IActionResult> edit(int id)
        {
            var item=new BlogDataModel();
            var response = await _httpClient.GetAsync($"api/Blog/{id}");
            if (response.IsSuccessStatusCode)
            {
                string jsonstr = await response.Content.ReadAsStringAsync();
                 item = JsonConvert.DeserializeObject<BlogDataModel>(jsonstr)!;

            }
            return View(item);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        public async Task<IActionResult> Save(BlogDataModel blog)
        {
            string jsonBlog = JsonConvert.SerializeObject(blog);
            var httpContent = new StringContent(jsonBlog, Encoding.UTF8, Application.Json);
            var response = await _httpClient.PostAsync("api/Blog",httpContent);
            // return View(response.Content.ReadAsStringAsync());
          return  RedirectToAction("Index");
        }

        public async Task<IActionResult> update(int id, BlogDataModel blog)
        {
            string jsonBlog = JsonConvert.SerializeObject(blog);
            var httpContent = new StringContent(jsonBlog, Encoding.UTF8, Application.Json);
            var response = await _httpClient.PutAsync($"api/Blog/{id}", httpContent);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Blog/{id}");
            return RedirectToAction("Index");
        }
    }
}
