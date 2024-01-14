using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YSYDotNetCore.ConsoleApp.Models;

namespace YSYDotNetCore.ConsoleApp.RefitExamples
{
    public class RefitExample
    {
      private readonly  IBlogApi _blogApi = RestService.For<IBlogApi>("https://localhost:7094");
        public async Task Run()
        {
            // await Read();
            // await Edit(31);
            //await Create("Title1", "Author1", "Content1");
            await Update(8, "Title1", "Author1", "Content1");
           //await Delete(8);
        }

        public async Task Read()
        {
            
            var lst = await _blogApi.GetBlogs();
            foreach (BlogDataModel item in lst)
            {
                Console.WriteLine(item.Blog_Id);
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Author);
                Console.WriteLine(item.Blog_content);
                Console.WriteLine(item.Blog_content);

            }
        }

        public async Task Edit(int id)
        {
            try
            {
                var item = await _blogApi.GetBlog(id);

                Console.WriteLine(item.Blog_Id);
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Author);
                Console.WriteLine(item.Blog_content);
                Console.WriteLine(item.Blog_content);


            }
            catch (Refit.ApiException ex)
            {
                Console.WriteLine(ex.ReasonPhrase!.ToString());
                Console.WriteLine(ex.Content!.ToString());

            }
        }

        public async Task Create(string title, string author, string content)
        {
            var message = await _blogApi.CreateBlog(new BlogDataModel
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_content = content
            });

            Console.WriteLine(message);

        }
        public async Task Update(int id,string title, string author, string content)
        {
            try
            {
                var message = await _blogApi.UpdateBlog(id, new BlogDataModel
                {

                    Blog_Title = title,
                    Blog_Author = author,
                    Blog_content = content
                });

                Console.WriteLine(message);

            }
            catch (Refit.ApiException ex)
            {
                Console.WriteLine(ex.ReasonPhrase!.ToString());
                Console.WriteLine(ex.Content!.ToString());
            }
           
        }


        public async Task Delete(int id)
        {
            var message=await _blogApi.DeleteBlog(id);
            Console.WriteLine(message);
        }
    }
}
