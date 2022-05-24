using MVCClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MVCClient.Services
{
    public class DoBeFlyServices : IDoBeFlyServices
    {
        private readonly HttpClient _client;
        private readonly string _baseuri;

        public DoBeFlyServices (HttpClient client)
        {
            _client = client;
            _baseuri = "https://localhost:44395/api/";
        }

        public async Task<IEnumerable<FlightM>> GetFlights()
        {
            var uri = _baseuri + "Flights";
            var responseString = await _client.GetStringAsync(uri);
            var flightList = JsonConvert.DeserializeObject<IEnumerable<FlightM>>(responseString);

            return flightList;
        }

        public async Task<IEnumerable<FlightM>> GetFlight(int id)
        {
            var uri = _baseuri + "Flights";
            var responseString = await _client.GetStringAsync(uri);
            var flightList = JsonConvert.DeserializeObject<IEnumerable<FlightM>>(responseString);

            return flightList;
        }

        public async Task<IEnumerable<BookingM>> GetAllBookings()
        {
            var uri = _baseuri + "Bookings";
            var responseString = await _client.GetStringAsync(uri);
            var bookingList = JsonConvert.DeserializeObject<IEnumerable<BookingM>>(responseString);

            return bookingList;
        }
    }
}
