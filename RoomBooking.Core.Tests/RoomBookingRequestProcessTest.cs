using Moq;
using RoomBokingTDD.Core.Services;
using RoomBokingTDD.Domain.Entities;
using RoomBokingTDD.Domain.Models;
using RoomBokingTDD.Models.Enums;
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
        private readonly List<Room> _availableRooms;

        public RoomBookingRequestProcessTest()
        {
            //arrange
            _request = new RoomBookingRequest()
            {
                Name = "testname",
                Email = "test@email.com",
                Date = new DateTime(2023, 01, 16)
            };
            _roomBookingServiceMock = new Mock<IRoomBookingInterface>();

            _bookingProcessor = new RoomBookingProcessor(_roomBookingServiceMock.Object);
            _availableRooms = new List<Room>() { new Room() {RoomId=1} };
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
            _roomBookingServiceMock.Setup(x => x.GetAvailableRooms(_request.Date)).Returns(_availableRooms);
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
            Assert.Equal(savedBooking.RoomBookingId, _availableRooms.First().RoomId);

        }

        [Fact]
        public void ShouldNotBookRoomIfRoomUnavailable()
        {
            // arrange
            _roomBookingServiceMock.Setup(x => x.GetAvailableRooms(_request.Date)).Returns(_availableRooms);

            //act
            _availableRooms.Clear();
            _bookingProcessor.Book(_request);
            
            //assert
            _roomBookingServiceMock.Verify(x=>x.Save(It.IsAny<RoomBooking>()), Times.Never());

        }

        [Theory]
        [InlineData(BookingResultFlag.Success,true)]
        [InlineData(BookingResultFlag.Failure,false)]
        public void ShouldReturnSuccessOrFailureFlagInBookingResult(BookingResultFlag bookingResultFlag, bool isAvailable)
        {
             RoomBooking savedBooking = null;

            // arrange
            _roomBookingServiceMock.Setup(x => x.GetAvailableRooms(_request.Date)).Returns(_availableRooms);
            _roomBookingServiceMock.Setup(x => x.Save(It.IsAny<RoomBooking>())).Callback<RoomBooking>(booking =>
            {
                savedBooking=booking;
            });
            //act
            if(!isAvailable)
            {
                _availableRooms.Clear();
            }
            var response= _bookingProcessor.Book(_request);
            //assert
            Assert.Equal(response.Flag,bookingResultFlag);

            
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(null, false)]
        public void ShouldReturnRoomBookingIdInResults(int? roomBookingId, bool isAvailable)
        {
            //arrange
            if (!isAvailable)
            {
                _availableRooms.Clear();
            }
            else
            {
                _roomBookingServiceMock.Setup(x => x.GetAvailableRooms(_request.Date)).Returns(_availableRooms);
                _roomBookingServiceMock.Setup(x => x.Save(It.IsAny<RoomBooking>())).Callback<RoomBooking>(booking =>
                {
                    booking.Id = roomBookingId.Value;
                });
            }
          
            // act           

            var results=_bookingProcessor.Book(_request);

            //assert

            Assert.Equal(roomBookingId,results.RoomBookingId);


        }
    }




}