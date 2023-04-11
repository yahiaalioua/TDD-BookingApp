using Microsoft.AspNetCore.Mvc;
using Moq;
using RoomBokingTDD.Core.Services;
using RoomBokingTDD.Core.Services.Abstract;
using RoomBokingTDD.Domain.Models;
using RoomBookingTDD.Api.Controllers;

namespace RoomBookingTDD.Api.Tests
{
    public class RoomBookingControllerTests
    {
        private Mock<IRoomBookingProcessor> _roomBookingProcessorInterface;
        private RoomBookingController _controller;
        private RoomBookingRequest _request;
        private RoomBookingResult _result;
        public RoomBookingControllerTests()
        {
            _roomBookingProcessorInterface = new Mock<IRoomBookingProcessor>();
            _controller = new RoomBookingController(_roomBookingProcessorInterface.Object);
            _request = new RoomBookingRequest() { Date=new DateTime(2024,04,11),Name="Omar",Email="test@gmail.com"};
            _result= new RoomBookingResult();
        }

        [Theory]
        [InlineData(1,true, typeof(OkObjectResult))]
        [InlineData(0, false, typeof(BadRequestObjectResult))]
        public async Task ShouldReturnOKResponse(int expectedMethodCalls, bool isModelValid, Type actionResult)
        {
            //arrange
            _roomBookingProcessorInterface.Setup(x=>x.Book(_request)).Returns(_result);
            if (!isModelValid)
            {
                _controller.ModelState.AddModelError("key", "resultError");
            }

            //act
            var result=await _controller.Book(_request);
            //assert
            
            Assert.IsType(actionResult,result);
            _roomBookingProcessorInterface.Verify(x => x.Book(_request), Times.Exactly(expectedMethodCalls));
        }
    }
}