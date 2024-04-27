using System;
using System.Net.Http;
using System.Threading.Tasks;
using Greggs.Products.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Xunit;

namespace Greggs.Products.UnitTests;


public class FanaticTests
{
    private TestServer _server;
    private HttpClient _client;
    
    public FanaticTests()
    {
        _server = new TestServer(new WebHostBuilder()
            .UseStartup<Startup>());
        _client = _server.CreateClient();
    }
    
    //Get Values From DataAccess
    [Fact]
    public async Task GetValuesFromDataAccess()
    {
        var message = new HttpRequestMessage();
        var pagination = JsonConvert.SerializeObject(new
        {
            pageStart = 0,
            pageSize = 5
        });
        message.RequestUri = new Uri("/product", UriKind.Relative);

        message.Content = new StringContent(pagination);
        var response = await _client.SendAsync(message);
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        Assert.Contains("Sausage Roll", responseString);
        Assert.Contains("Vegan Sausage Roll", responseString);
        Assert.Contains("Steak Bake", responseString);
        Assert.Contains("Yum Yum", responseString);
        Assert.Contains("Pink Jammie", responseString);
    }
    

}