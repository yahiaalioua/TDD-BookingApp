using RoomBokingTDD.Domain.Models;

namespace RoomBokingTDD.Core.Services.Abstract
{
    public interface IRoomBookingProcessor
    {
        RoomBookingResult Book(RoomBookingRequest request);
    }
}