using RoomBokingTDD.Core.Services;
using RoomBokingTDD.Core.Services.Abstract;
using RoomBokingTDD.Domain.Entities;
using RoomBokingTDD.Domain.Models;
using RoomBokingTDD.Models.Enums;

namespace RoomBookingTDD.Core
{
    public class RoomBookingProcessor : IRoomBookingProcessor
    {
        private readonly IRoomBookingInterface _roomBookingInterface;

        public RoomBookingProcessor(IRoomBookingInterface roomBookingInterface)
        {
            _roomBookingInterface = roomBookingInterface;
        }
        private TRoomBooking CreateRoomBookingObject<TRoomBooking>(RoomBookingRequest request) where TRoomBooking
            : RoomBookingBase, new()
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
            var response = CreateRoomBookingObject<RoomBookingResult>(request);
            var availableRooms = _roomBookingInterface.GetAvailableRooms(request.Date);
            if (availableRooms.Any())
            {
                var availableRoom = availableRooms.First();
                var roomBooking = CreateRoomBookingObject<RoomBooking>(request);
                roomBooking.RoomBookingId = availableRoom.RoomId;
                _roomBookingInterface.Save(roomBooking);
                response.Flag = BookingResultFlag.Success;
                response.RoomBookingId = roomBooking.Id;
            }
            else
            {
                response.Flag = BookingResultFlag.Failure;

            }
            return response;
        }
    }
}