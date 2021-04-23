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
    public class CardControllerTesting
    {
        private Mock<CardService> service = new Mock<CardService>();
        private Mock<OrderService> clientservice = new Mock<OrderService>();



        [Fact]
        public async Task GetAllCards()
        {
            service.Setup(x => x.GetAll(1, 30)).Returns(new DataCollection<CardDto>());


        }

        [Fact]
        public async Task CreateCard()
        {

            service.Setup(x => x.Create(new CardCreateDto
            {
               CardCvi=312,
               CardExpireDate= DateTime.Now.ToString(),
               CardNumber=31231312312,
               CardType=true,
                CustomerId=2
            })).Returns(new CardDto());


        }

        [Fact]
        public async Task DeleteCardById()
        {

            service.Setup(x => x.Remove(1));

            var controller = new CardController(service.Object, clientservice.Object);
            var result = controller.Remove(It.IsAny<int>());

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetCardById()
        {
            service.Setup(x => x.GetById(It.IsAny<int>())).Returns(new CardDto());
        }
    }
}
