import { useNavigate, useParams, useLocation } from 'react-router-dom';
import CancelledDetailsContainer from '../CancelledDetailsContainer/CancelledDetailsContainer';
import BusOperatorNavbar from '../BusOperatorNavbar/BusOperatorNavbar'
import React, { useEffect, useState } from 'react';
import axios from 'axios';

function BookingDetails() {
  const [allBookings, setAllBookings] = useState([]);
  const token = sessionStorage.getItem('authToken');
  const navigate = useNavigate();
  const { busId } = useParams();
  const location = useLocation();

  useEffect(() => {
    const fetchBusDetails = async () => {
      if (!token) {
        navigate('/login');
        return;
      }

      try {
        const response = await axios.get(
          `https://localhost:7114/api/BookingHistories/getAllBookingsByBusId/${busId}`,
          {
            headers: {
              Authorization: `Bearer ${token}`
            }
          }
        );
        console.log(response.data)
        const transformedAllBooking = response.data.flatMap((booking) =>({
          busType: booking.booking.bus.busType,
          busName: booking.booking.bus.busName,
          busNumber: booking.booking.bus.busNumber,
          bookingTime: booking.bookingDateTime,
          boarding: booking.booking.boarding?booking.booking.boarding.placeName:'Not Specified',
          boardingTime: booking.booking.boarding.timings,
          dropping: booking.booking.dropping.placeName,
          droppingTime: booking.booking.dropping.timings,
          departureDate: new Date(booking.booking.bus.departureDate),
          origin: booking.booking.bus ? booking.booking.bus.origin : 'Not specified',
          destination:  booking.booking.bus ? booking.booking.bus.destination : 'Not specified',
          seatNo: booking.seats,
          passengerName: booking.passengerName,
          gender: booking.gender,
          age: booking.age
      })
  )
      setAllBookings(transformedAllBooking);
      } catch (error) {
        if (error.response && error.response.status === 403) {
          window.alert('Unauthorized');
          navigate('/login');
        }
        console.error('Error fetching bus details:', error.message);
      }
    };

    fetchBusDetails();
  }, [busId, token, navigate]); // Include dependencies for useEffect

  // Determine if this tab should be active based on the route
  const isActiveTab = location.pathname.includes('/booking-details');

  return (
    <>
      <BusOperatorNavbar />
      <div className="container-fluid status-container">
        <ul className="nav nav-tabs statustabs">
          <li className="nav-item tab-item">
            <a className={`nav-link ${isActiveTab ? 'active' : ''}`}>
              All Bookings
            </a>
          </li>
        </ul>

        <div className="tab-content">
          <div className={`tab-pane fade ${isActiveTab ? 'show active' : ''}`}>
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
        </div>
      </div>
    </>
  );
}

export default BookingDetails;
