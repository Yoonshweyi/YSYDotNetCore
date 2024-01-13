using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YSYDotNetCore.ConsoleApp.Models;

namespace YSYDotNetCore.ConsoleApp.RestClientExamples
{
    internal class RestClientExample
    {
        private string _Blogendpoint = "https://localhost:7094/api/Blog";

        public async Task run()
        {
           // await read();
            //await Edit(6);

            // await create("Test Title", "Test Author", "Test Content");
             //await update(10, "Test Title", "Test Author", "Test Content");
            await delete(10);

        }

        public async Task read()
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest(_Blogendpoint,Method.Get);
          var response=  await client.ExecuteAsync(request);
            
            if (response.IsSuccessStatusCode)
            {
                string jsonstr =  response.Content!;
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
            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"{_Blogendpoint}/{id}", Method.Get);
            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string jsonstr = response.Content!;
                BlogDataModel item = JsonConvert.DeserializeObject<BlogDataModel>(jsonstr)!;

                Console.WriteLine(item.Blog_Id);
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Author);
                Console.WriteLine(item.Blog_content);
                Console.WriteLine(item.Blog_content);

            }
            else
            {
                Console.WriteLine( response.Content);
            }
        }

        public async Task create(string title, string author, string content)
        {
            var blog = new BlogDataModel()
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_content = content

            };
            RestClient client = new RestClient();
            RestRequest request = new RestRequest(_Blogendpoint, Method.Post);
            request.AddJsonBody(blog);
            var response = await client.ExecuteAsync(request); 
            Console.WriteLine( response.Content!);
        }

        public async Task update(int id, string title, string author, string content)
        {
            var blog = new BlogDataModel()
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_content = content

            };

            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"{_Blogendpoint}/{id}", Method.Put);
            request.AddJsonBody(blog);
            var response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content!);
        }

        public async Task delete(int id)
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"{_Blogendpoint}/{id}", Method.Delete);
            var response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content!);
        }
    }
}

