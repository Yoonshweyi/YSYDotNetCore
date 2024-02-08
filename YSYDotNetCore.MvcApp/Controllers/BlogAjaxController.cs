using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YSYDotNetCore.MvcApp.Models;
using Newtonsoft.Json;

namespace YSYDotNetCore.MvcApp.Controllers
{
    public class BlogAjaxController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public BlogAjaxController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IActionResult> Index()
        {
            var lst = await _appDbContext.Blogs.OrderByDescending(x => x.Blog_Id).ToListAsync();
            return View(lst);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(BlogDataModel requestModel)
        {
            await _appDbContext.AddAsync(requestModel);
           int result= await _appDbContext.SaveChangesAsync();
            // return RedirectToAction("Index");
            // return Json({ new Message = result > 0 ? "Saving Successful." : "Saving Failed." });
            return Json(new { Message = result > 0 ? "Saving Successful." : "Saving Failed." });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await _appDbContext.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == id);

            if (item is null)
            {
                return RedirectToAction("Index");
            }

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, BlogDataModel requestModel)
        {
            var item = await _appDbContext.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == id);

            if (item is null)
            {
                return Json(new {Message="No Data Found"});
            }
            item.Blog_Title = requestModel.Blog_Title;
            item.Blog_Author = requestModel.Blog_Author;
            item.Blog_content = requestModel.Blog_content;
            int result=  await _appDbContext.SaveChangesAsync();
            // return RedirectToAction("Index");
            return Json(new { Message = result > 0 ? "Updating Successful." : "Updating Failed." });

        }




        public async Task<IActionResult> Delete(BlogDataModel requestModel)
        {
            var item = await _appDbContext.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == requestModel.Blog_Id);

            if (item is null)
            {
                return Json(new { Message = "No Data Found" });
            }
            _appDbContext.Remove(item);
           // await _appDbContext.SaveChangesAsync();
            int result = await _appDbContext.SaveChangesAsync();
            // return RedirectToAction("Index");
            return Json(new { Message = result > 0 ? "Deleting Successful." : "Deleting Failed." });
        }
    }
}
