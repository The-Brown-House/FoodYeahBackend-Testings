using FakeItEasy;
using FluentAssertions;
using FoodYeah.Commons;
using FoodYeah.Controllers;
using FoodYeah.Dto;
using FoodYeah.Model;
using FoodYeah.Persistence;
using FoodYeah.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static FoodYeah.Commons.Enums;

namespace FoodGoTesting.ControllerTestings
{
   public class ProductControllerTesting : IntegrationTest
    {

        private Mock<ProductService> service = new Mock<ProductService>();



        [Fact]
        public async Task GetAllProducts()
        {
            service.Setup(x => x.GetAll(1, 30)).Returns(new DataCollection<ProductDto>());
            

        }

        [Fact]
        public async Task CreateProduct()
        {

            service.Setup(x => x.Create(new ProductCreateDto { 
            ImageUrl="test",ProductName="Test",ProductPrice=4,Product_CategoryId=1,SellDay=DaySold.Jueves,  Stock=4
            })).Returns(new ProductDto());


        }

        [Fact]
        public async Task DeleteProductById()
        {

            service.Setup(x => x.Remove(1));

            var controller = new ProductController(service.Object);
            var result = controller.Remove(It.IsAny<int>());

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetProductById()
        {
            service.Setup(x => x.GetById(It.IsAny<int>())).Returns(new ProductDto());
        }
    }
}
