using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SensorApp.Model;
namespace SensorApp.Services
{
    public class ApiService
    {
        private String awsApiUrl = "http://localhost:8080/dynamoDb/all";

        public async Task<List<TemperatureData>> GetTemperatureValues()
        {
            var client = new HttpClient();
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, awsApiUrl);
            var responseMessage = await client.SendAsync(requestMessage);
            var values = await responseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<TemperatureData>>(values);
        }
    }
}
