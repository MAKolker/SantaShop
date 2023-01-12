using System.Net;
using System.Security.Cryptography;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using SantaShop.Db;
using SantaShop.Domain.Dto;

namespace SantaShop.Test;

public class GiftServiceTests: IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private RequestFactory _requestFactory;

    public GiftServiceTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _requestFactory = new RequestFactory();

    }
    
    private void ClearDb()
    {
         var factory = _factory.Server.Services.GetService<IDbContextFactory<GiftShopContext>>();
         if (factory != null)
        {
             using var context = factory.CreateDbContext();
             context.GiftRequests.ExecuteDelete();
             context.Children.ExecuteDelete();
             context.SaveChanges();
        }
    }
    
    
    [Fact]
    public async Task AddGiftRequest_ShouldReturnSuccess()
    {
        //Arrange
        ClearDb();
        var client = _factory.CreateClient();
        var request = _requestFactory.CreateEmptyGiftRequest();
        request.GiftsWanted.Add(new GiftInfo("Lego", "Red"));
        request.GiftsWanted.Add(new GiftInfo("RC Car", "Black"));
        var content = JsonContent.Create(request);
        
        //Act

        var response = await client.PostAsync("/giftrequest", content);
        
        //Assert
        response.EnsureSuccessStatusCode();
        var responseObject = await response.Content.ReadFromJsonAsync<GiftRequest>();
        responseObject.Id.Should().NotBeNull();
    }
    
    [Fact]
    public async Task AddGiftRequest_ShouldFailWithExceedPrice()
    {
        //Arrange
        ClearDb();
        var client = _factory.CreateClient();
        var request = _requestFactory.CreateEmptyGiftRequest();
        request.GiftsWanted.Add(new GiftInfo("Rocket", "Red"));
        request.GiftsWanted.Add(new GiftInfo("RC Car", "Black"));
        var content = JsonContent.Create(request);
        //Act

        var response = await client.PostAsync("/giftrequest", content);
        
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var msg = await response.Content.ReadAsStringAsync();
        msg.Should().Be("Gift price exceeded");
    } 
    
    [Fact]
    public async Task AddGiftRequest_ShouldFailWithInvalidGifts()
    {
        //Arrange
        ClearDb();
        var client = _factory.CreateClient();
        var request = _requestFactory.CreateEmptyGiftRequest();
        request.GiftsWanted.Add(new GiftInfo("Lego ", "Red"));
        request.GiftsWanted.Add(new GiftInfo("RC Car", "Black"));
        var newAddress = $"NewAddress_{request.Address.Take(request.Address.Length - 3)}"; 
        var content = JsonContent.Create(request);
        //Act

        var response = await client.PostAsync("/giftrequest", content);
        response.EnsureSuccessStatusCode();
        var request2  = request with{Address = newAddress};
        request2.GiftsWanted[0]= request2.GiftsWanted[0] with{Color = "Green"};
        
        response = await client.PutAsync("/giftrequest", JsonContent.Create(request2));
        
        //Assert
        response.EnsureSuccessStatusCode();
        var responseObject = await response.Content.ReadFromJsonAsync<GiftRequest>();
        responseObject.Address.Should().Be(newAddress);
    } 
    
    [Fact]
    public async Task AddAndUpdateGiftRequest_ShouldSuccess()
    {
        //Arrange
        ClearDb();
        var client = _factory.CreateClient();
        var request = _requestFactory.CreateEmptyGiftRequest();
        request.GiftsWanted.Add(new GiftInfo("Shuttle", "Red"));
        request.GiftsWanted.Add(new GiftInfo("RC Car", "Black"));
        var content = JsonContent.Create(request);
        //Act

        var response = await client.PostAsync("/giftrequest", content);
        
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var msg = await response.Content.ReadAsStringAsync();
        msg.Should().Be("Invalid gifts");
    } 
    
    
    [Fact]
    public async Task AddTwoGiftRequest_CheckList()
    {
        //Arrange
        ClearDb();
        var client = _factory.CreateClient();
        var request1 = _requestFactory.CreateEmptyGiftRequest();
        request1.GiftsWanted.Add(new GiftInfo("Lego", "Red"));
        request1.GiftsWanted.Add(new GiftInfo("RC Car", "Black")); 
        var request2 = _requestFactory.CreateEmptyGiftRequest();
        request2.GiftsWanted.Add(new GiftInfo("Candies", "Red"));
        request2.GiftsWanted.Add(new GiftInfo("Mittens", "Black"));
      
        //Act

        var response = await client.PostAsync("/giftrequest", JsonContent.Create(request1));
        response.EnsureSuccessStatusCode();
        response = await client.PostAsync("/giftrequest", JsonContent.Create(request2));
        response.EnsureSuccessStatusCode();

        response = await client.GetAsync("/giftlist");
        
        //Assert
        response.EnsureSuccessStatusCode();
        var responseObject = await response.Content.ReadFromJsonAsync<List<GiftList>>();
        var response1 = responseObject.FirstOrDefault(c => c.Address.Equals(request1.Address));
        var response2 = responseObject.FirstOrDefault(c => c.Address.Equals(request2.Address));
        response1.Should().NotBeNull();
        response1.Gifts.Should().HaveCount(2);
        response1.Gifts.Should().Contain("Lego"); 
        response2.Should().NotBeNull();
        response2.Gifts.Should().HaveCount(2);
        response2.Gifts.Should().Contain("Mittens");
    }   
    
    [Fact]
    public async Task AddTwoGiftRequestWithSameAddress_CheckList()
    {
        //Arrange
        ClearDb();
        var client = _factory.CreateClient();
        var request1 = _requestFactory.CreateEmptyGiftRequest();
        request1.GiftsWanted.Add(new GiftInfo("Lego", "Red"));
        request1.GiftsWanted.Add(new GiftInfo("RC Car", "Black"));
        var request2 = request1 with { Name = request1.Name + "_", Age = request1.Age - 1 };
        request2.GiftsWanted.Add(new GiftInfo("Candies", "Red"));
        request2.GiftsWanted.Add(new GiftInfo("Mittens", "Black"));
      
        //Act

        var response = await client.PostAsync("/giftrequest", JsonContent.Create(request1));
        response.EnsureSuccessStatusCode();
        response = await client.PostAsync("/giftrequest", JsonContent.Create(request2));
        response.EnsureSuccessStatusCode();

        response = await client.GetAsync("/giftlist");
        
        //Assert
        response.EnsureSuccessStatusCode();
        var responseObject = await response.Content.ReadFromJsonAsync<List<GiftList>>();
        var response1 = responseObject.FirstOrDefault(c => c.Address.Equals(request1.Address));
        response1.Should().NotBeNull();
        response1.Gifts.Should().HaveCount(4);
        response1.Gifts.Should().Contain("Lego"); 
        response1.Gifts.Should().Contain("Mittens");
    }
}