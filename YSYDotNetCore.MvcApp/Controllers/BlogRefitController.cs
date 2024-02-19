using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using YSYDotNetCore.ConsoleApp.RefitExamples;
using YSYDotNetCore.MvcApp.Models;

namespace YSYDotNetCore.MvcApp.Controllers
{
    public class BlogRefitController : Controller
    {
        private readonly IBlogApi _blogApi;

        public BlogRefitController (IBlogApi blogApi)
        {
            _blogApi = blogApi;
        }
        public async Task<IActionResult> Index()
        {
            var lst = new List<BlogDataModel>();
            lst = await _blogApi.GetBlogs();
            return View(lst);
        }

        public async Task<IActionResult> edit(int id)
        {
            var item = new BlogDataModel();
            item = await _blogApi.GetBlog(id);
            return View(item);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        public async Task<IActionResult> Save(BlogDataModel blog)
        {
            var blogdata = await _blogApi.CreateBlog(blog);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id, BlogDataModel blog)
        {
            var blogdata = await _blogApi.UpdateBlog(id, blog);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> delete(int id)
        {
            var message = await _blogApi.DeleteBlog(id);

            return RedirectToAction("Index");

        }


    }
}
