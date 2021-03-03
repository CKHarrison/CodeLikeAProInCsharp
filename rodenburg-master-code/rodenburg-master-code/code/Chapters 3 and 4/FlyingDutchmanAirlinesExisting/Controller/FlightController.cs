using FlyingDutchmanAirlinesExisting.Objects;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using FlyingDutchmanAirlinesExisting.ReturnViews;

namespace FlyingDutchmanAirlinesExisting.Controller
{
    public class FlightController : ApiController
    {
        // GET: api/Flight
        [ResponseType(typeof(IEnumerable<FlightReturnView>))]
        public HttpResponseMessage Get()
        {
            var flightReturnViews = new List<FlightReturnView>();
            var flights = new List<Flight>();

            var connectionString =
                "Server=tcp:codelikeacsharppro.database.windows.net,1433;Initial Catalog=FlyingDutchmanAirlines;Persist Security Info=False;User ID=dev;Password=FlyingDutchmanAirlines1972!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Get Flights
                var cmd = new SqlCommand("SELECT * FROM Flight", connection);

                using (var reader = cmd.ExecuteReader())
                { 
                    while (reader.Read())
                    {
                        flights.Add(new Flight(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2)));
                    }
                }

                cmd.Dispose();

                foreach (var flight in flights)
                {
                    // Get Destination Airport details
                    cmd = new SqlCommand("SELECT City FROM Airport WHERE AirportID = " + flight.DestinationID, connection);
                  
                    var returnView = new FlightReturnView();
                    returnView.FlightNumber = flight.FlightNumber;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            returnView.Destination = reader.GetString(0);
                            break;
                        }
                    }

                    cmd.Dispose();

                    // Get Origin Airport details
                    cmd = new SqlCommand("SELECT City FROM Airport WHERE AirportID = " + flight.OriginID, connection);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            returnView.Origin = reader.GetString(0);
                            break;
                        }
                    }

                    cmd.Dispose();

                    flightReturnViews.Add(returnView);
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, flightReturnViews);
        }

        // GET: api/Flight/5
        [ResponseType(typeof(FlightReturnView))]
        public HttpResponseMessage Get(int id)
        {
            var flightReturnView = new FlightReturnView();
            Flight flight = null;

            string connectionString =
                "Server=tcp:codelikeacsharppro.database.windows.net,1433;Initial Catalog=FlyingDutchmanAirlines;Persist Security Info=False;User ID=dev;Password=FlyingDutchmanAirlines1972!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Get Flight
                var cmd = new SqlCommand("SELECT * FROM Flight WHERE FlightNumber = " + id, connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        flight = new Flight(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2));
                        flightReturnView.FlightNumber = flight.FlightNumber;
                        break;
                    }
                }

                cmd.Dispose();

                // Get Destination Airport details
                cmd = new SqlCommand("SELECT City FROM Airport WHERE AirportID = " + flight.DestinationID, connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        flightReturnView.Destination = reader.GetString(0);
                        break;
                    }
                }

                cmd.Dispose();

                // Get Origin Airport details
                cmd = new SqlCommand("SELECT City FROM Airport WHERE AirportID = " + flight.OriginID, connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        flightReturnView.Origin = reader.GetString(0);
                        break;
                    }
                }

                cmd.Dispose();
            }

            return Request.CreateResponse(HttpStatusCode.OK, flightReturnView);
        }

        // POST: api/Flight
        [ResponseType(typeof(HttpResponseMessage))]
        public HttpResponseMessage Post([FromBody] Booking value)
        {
            string connectionString =
                "Server=tcp:codelikeacsharppro.database.windows.net,1433;Initial Catalog=FlyingDutchmanAirlines;Persist Security Info=False;User ID=dev;Password=FlyingDutchmanAirlines1972!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Get Destination Airport ID
                var cmd = new SqlCommand("SELECT AirportID FROM Airport WHERE IATA = '" + value.DestinationAirportIATA + "'", connection);
                var destinationAirportID = 0;

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        destinationAirportID = reader.GetInt32(0);
                        break;
                    }
                }

                cmd.Dispose();

                // Get Origin Airport ID
                cmd = new SqlCommand("SELECT AirportID FROM Airport WHERE IATA = '" + value.OriginAirportIATA + "'", connection);
                var originAirportID = 0;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        originAirportID = reader.GetInt32(0);
                        break;
                    }
                }

                cmd.Dispose();

                // Get Flight details
                cmd = new SqlCommand("SELECT * FROM Flight WHERE Origin = " + originAirportID + " AND Destination = " + destinationAirportID,
                        connection);

                Flight flight = null;

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        flight = new Flight(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2));
                        break;
                    }
                }

                cmd.Dispose();

                // Create new customer
                cmd = new SqlCommand("SELECT COUNT(*) FROM Customer", connection);
                var newCustomerID = 0;

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        newCustomerID = reader.GetInt32(0);
                        break;
                    }
                }

                cmd.Dispose();

                cmd = new SqlCommand("INSERT INTO Customer (CustomerID, Name) VALUES ('" + (newCustomerID + 1) + "', '" + value.Name + "')", connection);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                var customer = new Customer(newCustomerID, value.Name, value.SSN);

                // Book flight
                cmd = new SqlCommand("INSERT INTO Booking (FlightNumber, CustomerID) VALUES (" + flight.FlightNumber + ", '" + customer.CustomerID + "')", connection);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                return Request.CreateResponse(HttpStatusCode.Created, "Hooray! A customer with the name \"" + customer.Name + "\" and SSN \"" +
                    customer.SSN + "\" has booked a flight!!!");
            }
        }

        // DELETE: api/Flight/5
        [ResponseType(typeof(HttpResponseMessage))]
        public HttpResponseMessage Delete(int id)
        {
            var connectionString =
                "Server=tcp:codelikeacsharppro.database.windows.net,1433;Initial Catalog=FlyingDutchmanAirlines;Persist Security Info=False;User ID=dev;Password=FlyingDutchmanAirlines1972!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var cmd = new SqlCommand("DELETE FROM Booking WHERE BookingID = '" + id + "'", connection);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }
    }
}
