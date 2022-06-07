using MVCClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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

        public async Task<FlightM> GetFlight(int id)
        {
            var uri = _baseuri + "Flights/" + id  ;
            var responseString = await _client.GetStringAsync(uri);
            var flight = JsonConvert.DeserializeObject<FlightM>(responseString);
            return flight;
        }

        public async Task<IEnumerable<BookingM>> GetAveragePrice()
        {
            var uri = _baseuri + "Bookings";
            var responseString = await _client.GetStringAsync(uri);
            var bookingList = JsonConvert.DeserializeObject<IEnumerable<BookingM>>(responseString);

            return bookingList;
        }

        public async Task<IEnumerable<PassengerM>> GetAllPassengers()
        {
            var uri = _baseuri + "Passenger";
            var responseString = await _client.GetStringAsync(uri);
            var passengerList = JsonConvert.DeserializeObject<IEnumerable<PassengerM>>(responseString);

            return passengerList;
        }

        public async Task<PassengerM> GetPassenger(int id)
        {
            var uri = _baseuri + "Passenger/" + id;
            var responseString = await _client.GetStringAsync(uri);
            var passenger = JsonConvert.DeserializeObject<PassengerM>(responseString);

            return passenger;
        }

        public async Task<bool> BuyTicket(TicketM ticketM)
        {
            var json = JsonConvert.SerializeObject(ticketM);
            HttpContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var uri = _baseuri + "Bookings";
            var response = await _client.PostAsync(uri, stringContent);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<bool>(data);
        }

        public async Task<int> GetAveragePrice(string Destination)
        {
            var uri = _baseuri + "Bookings/GetAveragePrice/" + Destination;
            var responseString = await _client.GetStringAsync(uri);
            var avg = JsonConvert.DeserializeObject<int>(responseString);
            return avg;
        }

        public async Task<int> GetTotalPrice(int idFlight)
        {
            var uri = _baseuri + "Bookings/GetTotalPrice/" + idFlight;
            var responseString = await _client.GetStringAsync(uri);
            var total = JsonConvert.DeserializeObject<int>(responseString);
            return total;
        }

    }
}
