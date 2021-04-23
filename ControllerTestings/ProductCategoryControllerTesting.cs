using FoodYeah.Commons;
using FoodYeah.Controllers;
using FoodYeah.Dto;
using FoodYeah.Model;
using FoodYeah.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FoodGoTesting.ControllerTestings
{
    public class ProductCategoryControllerTesting :IntegrationTest
    {
        private Mock<Product_CategoryService> service = new Mock<Product_CategoryService>();



        [Fact]
        public async Task GetAllProduct_Categories()
        {
            service.Setup(x => x.GetAll(1, 30)).Returns(new DataCollection<Product_CategoryDto>());


        }

        [Fact]
        public async Task CreateProduct_Category()
        {

            service.Setup(x => x.Create(new Product_CategoryCreateDto
            {
               Product_CategoryDescription="Descripcion",
               Product_CategoryName="Nombre"
            })).Returns(new Product_CategoryDto());


        }

        [Fact]
        public async Task DeleteProductById()
        {

            service.Setup(x => x.Remove(1));

            var controller = new Product_CategoryController(service.Object);
            var result = controller.Remove(It.IsAny<int>());

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetProduct_CategoryById()
        {
            service.Setup(x => x.GetById(It.IsAny<int>())).Returns(new Product_CategoryDto());
          }
    }
}
