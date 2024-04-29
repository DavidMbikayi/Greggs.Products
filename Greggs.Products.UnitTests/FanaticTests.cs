using System.Net.Http;
using System.Threading.Tasks;
using Greggs.Products.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
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
        var response = await _client.GetAsync("/Product?pageStart=0&pageSize=6"); 
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        Assert.Contains("Sausage Roll", responseString);
        Assert.Contains("Vegan Sausage Roll", responseString);
        Assert.Contains("Steak Bake", responseString);
        Assert.Contains("Yum Yum", responseString);
        Assert.Contains("Pink Jammie", responseString);
    }
    

}