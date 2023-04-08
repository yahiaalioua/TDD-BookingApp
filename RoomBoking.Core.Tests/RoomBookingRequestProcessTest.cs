using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RoomBoking.Core.Tests
{
    public class RoomBookingRequestProcessTest
    {
        [Fact]
        public void ShouldReturnRoomBookingRequest()
        {
            //arrrange
            // bookroomprocessor, roombookingresult, roombookingrequest
            var request=new RoomBookingRequest()
            {
                Name="test name",
                Email="test@email.com",
                Date=new DateTime(2023,01,16)
            };
            var bookRoomProcessor = new RoomBookingProcessor();

            // act
            RoomBookingResult response=bookRoomProcessor.Book(request);


            // assert
            Assert.NotNull(response);


        }
    }
}
