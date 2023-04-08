using Moq;
using RoomBokingTDD.Core.Domain.Entities;
using RoomBokingTDD.Core.Models;
using RoomBokingTDD.Core.Services;
using RoomBookingTDD.Core;
using Shouldly;
using Xunit;

namespace RoomBookingTDD.Core.Tests
{
    public class RoomBookingRequestProcessTest
    {
        private readonly RoomBookingProcessor _bookingProcessor;
        private readonly RoomBookingRequest _request;
        private readonly Mock<IRoomBookingInterface> _roomBookingServiceMock;

        public RoomBookingRequestProcessTest()
        {
            //arrange
            _request= new RoomBookingRequest()
            {
                Name = "testname",
                Email = "test@email.com",
                Date = new DateTime(2023, 01, 16)
            };
            _roomBookingServiceMock = new Mock<IRoomBookingInterface>();

            _bookingProcessor = new RoomBookingProcessor(_roomBookingServiceMock.Object);
        }

        [Fact]
        public void ShouldReturnRoomBookingRequest()
        {
            //arrrange
            

            // act
            RoomBookingResult response = _bookingProcessor.Book(_request);


            // assert
            Assert.NotNull(response);
            Assert.Equal(_request.Name, response.Name);
            Assert.Equal(_request.Email, response.Email);
            Assert.Equal(_request.Date, response.Date);
        }

        [Fact]
        public void ShouldThrowNullExceptionEmptyBookingRequest()
        {
            //arrrange

            // act
            var exception= Assert.Throws<ArgumentNullException>(() =>_bookingProcessor.Book(null));

            // assert
            Assert.Throws<ArgumentNullException>(() => _bookingProcessor.Book(null));
            Assert.Equal(nameof(RoomBookingRequest), exception.ParamName);


        }

        [Fact]
        public void ShouldSaveRoomBookingRequest()
        {
            //arrrange
            RoomBooking savedBooking = null;
            // act
            _roomBookingServiceMock.Setup(x => x.Save(It.IsAny<RoomBooking>())).Callback<RoomBooking>(booking =>
            {
                savedBooking=booking;
            });
            _bookingProcessor.Book(_request);

            // assert
            _roomBookingServiceMock.Verify(x => x.Save(It.IsAny<RoomBooking>()), Times.Once());
            Assert.NotNull(savedBooking);
            Assert.Equal(_request.Name, savedBooking.Name);
            Assert.Equal(_request.Email, savedBooking.Email);
            Assert.Equal(_request.Date, savedBooking.Date);
        }
    }
}