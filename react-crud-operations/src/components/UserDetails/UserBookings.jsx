import CancelledDetailsContainer from '../CancelledDetailsContainer/CancelledDetailsContainer';
import { useEffect, useState } from 'react';
import axios from 'axios';
import './UserDetails.css';
import { useNavigate, useParams } from 'react-router-dom';
import './UserBookings.css'
import AdminNavbar from '../AdminNavbar/AdminNavbar';

function UserBookings() {
  const [allBookings, setAllBookings] = useState([]);
  const { userId } = useParams();
  const navigate = useNavigate(); // Initialize the navigate function

  useEffect(() => {
    const token = sessionStorage.getItem('authToken');
    if (!token) {
      navigate('/login');
      return;
    }

    const fetchDetails = async () => {
      try {
        const allBookResponse = await axios.get(`https://localhost:7114/api/BookingHistories/getAllBookingsByUserId/${userId}`, {
          headers: {
            Authorization: `Bearer ${token}`
          }
        });

        const transformedAllBooking = allBookResponse.data.flatMap((booking) => ({
          busType: booking.booking.bus.busType,
          busName: booking.booking.bus.busName,
          busNumber: booking.booking.bus.busNumber,
          bookingTime: booking.bookingDateTime,
          boarding: booking.booking.boarding.placeName,
          boardingTime: booking.booking.boarding.timings,
          dropping: booking.booking.dropping.placeName,
          droppingTime: booking.booking.dropping.timings,
          departureDate: new Date(booking.booking.bus.departureDate),
          origin: booking.booking.bus ? booking.booking.bus.origin : 'Not specified',
          destination: booking.booking.bus ? booking.booking.bus.destination : 'Not specified',
          seatNo: booking.seats,
          passengerName: booking.passengerName,
          gender: booking.gender,
          age: booking.age,
          isCancelled: booking.isCancelled
        }));

        setAllBookings(transformedAllBooking);
        console.log(transformedAllBooking)
      } catch (error) {
        console.error('Error fetching bookings:', error);
      }
    };
    fetchDetails();
  }, [userId, navigate]);

  return (
    <>
      <AdminNavbar />
      <div>
        <h4>All Bookings</h4>
        {allBookings.length === 0 ? (
          <p>No bookings</p>
        ) : (
          <div className="busListingContainer">
            <div className="busCardContainer">
              {allBookings.map((booking) => (
                <div key={booking.bookingId} className="historyBusCard">
                  <CancelledDetailsContainer booking={booking} />
                </div>
              ))}
            </div>
          </div>
        )}
      </div>
    </>
  );
}

export default UserBookings;
