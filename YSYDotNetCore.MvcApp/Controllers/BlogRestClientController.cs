using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Net.Http;
using System.Text;
using YSYDotNetCore.MvcApp.Models;
using static System.Net.Mime.MediaTypeNames;

namespace YSYDotNetCore.MvcApp.Controllers
{
    public class BlogRestClientController : Controller
    {
        private readonly RestClient _restClient;

        public BlogRestClientController(RestClient restClient)
        {
            _restClient = restClient;
        }
        public async Task<IActionResult> Index()
        {
            var lst = new List<BlogDataModel>();
            RestRequest restRequest = new RestRequest("api/Blog",Method.Get);
            var response =await _restClient.ExecuteAsync(restRequest);
            if(response.IsSuccessStatusCode)
            {
                string jsonstr=response.Content!;
                lst = JsonConvert.DeserializeObject<List<BlogDataModel>>(jsonstr)!;
            }
            return View(lst);
        }
        public async Task<IActionResult> edit(int id)
        {
            var item = new BlogDataModel();
            RestRequest restRequest = new RestRequest($"api/Blog/{id}", Method.Get);
            var response = await _restClient.ExecuteAsync(restRequest);
            if(response.IsSuccessStatusCode)
            {
                var jsonstr=response.Content!;
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
            RestRequest restRequest = new RestRequest("api/Blog", Method.Post);
            restRequest.AddJsonBody(blog);
            var response = await _restClient.ExecuteAsync(restRequest);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id, BlogDataModel blog)
        {
            RestRequest restRequest = new RestRequest($"api/Blog/{id}", Method.Put);
            restRequest.AddJsonBody(blog);
            var response = await _restClient.ExecuteAsync(restRequest);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            RestRequest restRequest = new RestRequest($"api/Blog/{id}", Method.Delete);
            var response = await _restClient.ExecuteAsync(restRequest);
            return RedirectToAction("Index");
        }


    }
}
