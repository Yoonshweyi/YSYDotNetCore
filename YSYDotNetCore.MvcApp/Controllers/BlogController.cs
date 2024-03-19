using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;
using Serilog.Core;
using YSYDotNetCore.MvcApp.Models;

namespace YSYDotNetCore.MvcApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<BlogController> _logger;


        public BlogController(AppDbContext appDbContext, ILogger<BlogController> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }


        public async Task <IActionResult> Index()
        {
            var lst=await _appDbContext.Blogs.OrderByDescending(x=>x.Blog_Id).ToListAsync();
            return View(lst);
           

        }

        //https://localhost:3000/blog/index?pageNo=1&pageSize=10

       // [ActionName("List")]
        public async Task<IActionResult>BlogList(int pageNo = 1, int pageSize = 10)
        {
            var query = _appDbContext.Blogs
            .AsNoTracking()
            .OrderByDescending(x => x.Blog_Id);
            var lst=   await query.Skip((pageNo-1) * pageSize).Take(pageSize).ToListAsync();
            var rowCount=await query.CountAsync();
            var pageCount = rowCount / pageSize;
            if(rowCount% pageSize > 0)
            {
                pageCount++;
            }
           

            BlogResponseModel model = new BlogResponseModel()
            {
                Data = lst,
                PageSetting=new PageSettingModel(pageNo,pageSize,pageCount,rowCount,"/Blog/BlogList") 

            };
            return View( model);

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(BlogDataModel requestModel)
        {
            _logger.LogInformation("Create blog Model => " + JsonConvert.SerializeObject(requestModel));
            await _appDbContext.AddAsync(requestModel);
            var result=await _appDbContext.SaveChangesAsync();
            var message = result > 0 ? "Saving Successful!" : "Saving Fail!";
            _logger.LogInformation(message);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {

           
            var item = await _appDbContext.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == id);

            if (item is null)
            {
                return RedirectToAction("Index");
            }

            return View(item);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return RedirectToAction("Index");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id,BlogDataModel requestModel)
        {
            _logger.LogInformation("Update blog Model => " + JsonConvert.SerializeObject(requestModel));
            var item = await _appDbContext.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == id);

            if (item is null)
            {
                return RedirectToAction("Index");
            }
            item.Blog_Title = requestModel.Blog_Title;
            item.Blog_Author = requestModel.Blog_Author;
            item.Blog_content = requestModel.Blog_content;
           var result= await _appDbContext.SaveChangesAsync();
            var message = result > 0 ? "Updating Successful" : "Updating Failed";
            
            return RedirectToAction("Index");
            _logger.LogInformation(message);
        }



        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var item=await _appDbContext.Blogs.FirstOrDefaultAsync(x=>x.Blog_Id==id);

            if(item is null)
            {
                return RedirectToAction("Index");
            }
            _appDbContext.Remove(item);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
           

        }
    }
}
