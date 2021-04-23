using FoodYeah;
using FoodYeah.Dto;
using FoodYeah.Persistence;
using LinqToDB;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;
using System;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.DependencyInjection;

namespace FoodGoTesting
{
    public class IntegrationTest
    {
        protected readonly HttpClient TestClient;

        public IntegrationTest()
        {
    
            var appFactory = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder=> {

                builder.ConfigureServices(services=> {

                    services.RemoveAll(typeof(DataContext));

                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseInMemoryDatabase("TestDB"));
                });
            });

         

            TestClient = appFactory.CreateClient();
        }

        
        public async Task LoginAndGetToken(String userType)
        {
            var login = JsonConvert.SerializeObject(new ApplicationUserLoginDto
            {
                Email = "testing@foodgo.com",
                Password = "test123"
            });


            userType = userType.ToLower();
            if (userType == "admin") {
                 login = JsonConvert.SerializeObject(new ApplicationUserLoginDto
                {
                    Email = "admin@foodgo.com",
                    Password = "test123"
                });
            }
            else {
                 login = JsonConvert.SerializeObject(new ApplicationUserLoginDto
                {
                    Email = "testing@foodgo.com",
                    Password = "test123"
                });
            }

           
            var buffer = System.Text.Encoding.UTF8.GetBytes(login);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await TestClient.PostAsync("identity/login", byteContent);
            var token = await response.Content.ReadAsStringAsync();
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        }
    }
}
