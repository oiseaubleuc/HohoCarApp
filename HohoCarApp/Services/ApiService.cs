using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Json;
using HohoCarApp.Models;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Car>> GetCarsAsync()
    {
        var response = await _httpClient.GetStringAsync("https://localhost:7231/api/cars");
        return JsonConvert.DeserializeObject<List<Car>>(response);
    }

    public async Task AddCarAsync(Car car)
    {
        var response = await _httpClient.PostAsJsonAsync("https://localhost:7231/api/cars", car); 
    }
}
