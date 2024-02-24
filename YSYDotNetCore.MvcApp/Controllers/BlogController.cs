﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YSYDotNetCore.MvcApp.Models;

namespace YSYDotNetCore.MvcApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public BlogController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
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
            return View("BlogList", model);

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(BlogDataModel requestModel)
        {
            await _appDbContext.AddAsync(requestModel);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
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
        public async Task<IActionResult> Update(int id,BlogDataModel requestModel)
        {
            var item = await _appDbContext.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == id);

            if (item is null)
            {
                return RedirectToAction("Index");
            }
            item.Blog_Title = requestModel.Blog_Title;
            item.Blog_Author = requestModel.Blog_Author;
            item.Blog_content = requestModel.Blog_content;
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }




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
