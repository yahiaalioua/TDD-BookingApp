using RoomBokingTDD.Core.Domain.Entities;
using RoomBokingTDD.Core.Models;
using RoomBokingTDD.Core.Services;

namespace RoomBookingTDD.Core
{
    public class RoomBookingProcessor
    {
        private readonly IRoomBookingInterface _roomBookingInterface;

        public RoomBookingProcessor(IRoomBookingInterface roomBookingInterface)
        {
            _roomBookingInterface = roomBookingInterface;
        }
        private TRoomBooking CreateRoomBookingObject<TRoomBooking>(RoomBookingRequest request) where TRoomBooking
            : RoomBookingBase,new() 
        {
            return new TRoomBooking()
            {
                Name = request.Name,
                Email = request.Email,
                Date = request.Date

            };
        }

        public RoomBookingResult Book(RoomBookingRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(RoomBookingRequest));
            }
            _roomBookingInterface.Save(CreateRoomBookingObject<RoomBooking>(request));
            var response = CreateRoomBookingObject<RoomBookingResult>(request);
            return response;
        }
    }
}