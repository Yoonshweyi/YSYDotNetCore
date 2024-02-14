using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YSYDotNetCore.MvcApp.Models;
using Newtonsoft.Json;

namespace YSYDotNetCore.MvcApp.Controllers
{
    public class UserLoginAjaxController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public UserLoginAjaxController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IActionResult> Index()
        {
            var lst = await _appDbContext.Users.OrderByDescending(x => x.UserId).ToListAsync();
            return View(lst);
           
        }
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Save(UserModel requestModel)
        {
            await _appDbContext.AddAsync(requestModel);
            int result = await _appDbContext.SaveChangesAsync();
            // return RedirectToAction("Index");
            // return Json({ new Message = result > 0 ? "Saving Successful." : "Saving Failed." });
            return Json(new { Message = result > 0 ? "Saving Successful." : "Saving Failed." });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await _appDbContext.Users.FirstOrDefaultAsync(x => x.UserId == id);

            if (item is null)
            {
                return RedirectToAction("Index");
            }

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, UserModel requestModel)
        {
            var item = await _appDbContext.Users.FirstOrDefaultAsync(x => x.UserId == id);

            if (item is null)
            {
                return Json(new { Message = "No Data Found" });
            }
            item.UserName = requestModel.UserName;
            item.UserPassword = requestModel.UserPassword;
            int result = await _appDbContext.SaveChangesAsync();
            // return RedirectToAction("Index");
            return Json(new { Message = result > 0 ? "Updating Successful." : "Updating Failed." });

        }

        public async Task<IActionResult> Delete(UserModel requestModel)
        {
            var item = await _appDbContext.Users.FirstOrDefaultAsync(x => x.UserId == requestModel.UserId);

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
