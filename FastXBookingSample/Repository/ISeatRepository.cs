﻿using FastXBookingSample.Models;

namespace FastXBookingSample.Repository
{
    public interface ISeatRepository
    {
        List<Seat> GetSeatsByBookingId(int bookingId);
        List<Seat> GetSeatsByUserId(int userId);
        void PostSeatByBookingId(Seat seat);
        void DeleteSeatBySeatId(int seatId);
        bool IsBookingExists(int id);

    }
}
