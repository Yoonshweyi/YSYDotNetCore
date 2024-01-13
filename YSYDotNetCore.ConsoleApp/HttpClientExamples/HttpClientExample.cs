using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using YSYDotNetCore.ConsoleApp.Models;
using static System.Net.Mime.MediaTypeNames;

namespace YSYDotNetCore.ConsoleApp.HttpClientExamples
{
    public class HttpClientExample
    {
        private string _Blogendpoint = "https://localhost:7094/api/Blog";
       
        public async Task run()
        {
            //await read();
            //await Edit(6);

            // await create("Test Title", "Test Author", "Test Content");
            // await update(7, "Test Title", "Test Author", "Test Content");
            await delete(7);
          
        }

        public async Task read()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(_Blogendpoint);
            if (response.IsSuccessStatusCode)
            {
                string jsonstr = await response.Content.ReadAsStringAsync();
                List<BlogDataModel> lst = JsonConvert.DeserializeObject<List<BlogDataModel>>(jsonstr)!;
                foreach (BlogDataModel item in lst)
                {
                    Console.WriteLine(item.Blog_Id);
                    Console.WriteLine(item.Blog_Title);
                    Console.WriteLine(item.Blog_Author);
                    Console.WriteLine(item.Blog_content);
                    Console.WriteLine(item.Blog_content);

                }
            }
        }
        public async Task Edit(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{_Blogendpoint}/{id}");
            if (response.IsSuccessStatusCode)
            {
                string jsonstr = await response.Content.ReadAsStringAsync();
                BlogDataModel item = JsonConvert.DeserializeObject<BlogDataModel>(jsonstr)!;
                
                    Console.WriteLine(item.Blog_Id);
                    Console.WriteLine(item.Blog_Title);
                    Console.WriteLine(item.Blog_Author);
                    Console.WriteLine(item.Blog_content);
                    Console.WriteLine(item.Blog_content);

            }
            else
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task create(string title,string author,string content)
        {
            var blog = new BlogDataModel()
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_content = content

            };
            string jsonBlog= JsonConvert.SerializeObject(blog);
            HttpContent httpContent = new StringContent(jsonBlog,Encoding.UTF8,Application.Json);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync(_Blogendpoint,httpContent);
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }

        public async Task update(int id,string title, string author, string content)
        {
            var blog = new BlogDataModel()
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_content = content

            };
            string jsonBlog = JsonConvert.SerializeObject(blog);
            HttpContent httpContent = new StringContent(jsonBlog, Encoding.UTF8, Application.Json);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PutAsync($"{_Blogendpoint}/{id}", httpContent);
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }

        public async Task delete(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.DeleteAsync($"{_Blogendpoint}/{id}");
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }
    }
}
