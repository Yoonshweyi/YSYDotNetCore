using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YSYDotNetCore.ConsoleApp.Models;

namespace YSYDotNetCore.ConsoleApp.EFCoreExamples
{
    public  class EFCoreExample
    {
        private readonly AppDBContext _dbContext=new AppDBContext();
        public void run()
        {
            // Read();

            // edit(2);
            // edit(100);

            // create("Test Title", "Test Author", "Test Content");
            //update(4, "Test Title1", "Test Author", "Test Content");
            Delete(14);

        }

        public void Read()
        {
           var lst= _dbContext.Blogs.AsNoTracking().ToList();
            foreach (BlogDataModel item in lst)
            {
                Console.WriteLine(item.Blog_Id);
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Author);
                Console.WriteLine(item.Blog_content);
                Console.WriteLine(item.Blog_content);

            }
        }

        private void edit(int id)
        {
            BlogDataModel? item=_dbContext.Blogs.FirstOrDefault(x=>x.Blog_Id == id);

            if (item is null)
            {
                Console.WriteLine("No data found");

                return;
            }
            Console.WriteLine(item.Blog_Id);
            Console.WriteLine(item.Blog_Title);
            Console.WriteLine(item.Blog_Author);
            Console.WriteLine(item.Blog_content);
        }

        private void create(string title, string author, string content)
        {
            BlogDataModel blog = new BlogDataModel()
            {
               Blog_Title = title,
                Blog_Author = author,
                Blog_content = content
            };

            _dbContext.Blogs.Add(blog);
            

            int result= _dbContext.SaveChanges();

            string message = result > 0 ? "Saving Successful.." : "Saving Fail";
            Console.WriteLine(message);
        }

        private void update(int id, string title, string author, string content)
        {

            BlogDataModel? item = _dbContext.Blogs.FirstOrDefault(x => x.Blog_Id == id);

            if (item is null)
            {
                Console.WriteLine("No data found");

                return;
            }

            item.Blog_Title = title;
            item.Blog_Author = author;
            item.Blog_content = content;
            int result= _dbContext.SaveChanges();

            string message = result > 0 ? "Update Successful.." : "Update Fail";
            Console.WriteLine(message);
        }

        private void Delete(int id)
        {

            BlogDataModel? item = _dbContext.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item is null)
            {
                Console.WriteLine("No data found");

                return;
            }

            _dbContext.Blogs.Remove(item);
            int result= _dbContext.SaveChanges();


            string message = result > 0 ? "Delete Successful.." : "Delete Fail";
            Console.WriteLine(message);



        }
    }

   
}
