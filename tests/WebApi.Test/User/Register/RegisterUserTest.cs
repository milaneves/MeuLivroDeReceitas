﻿using CommonTestUtilities.Requests;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace WebApi.Test.User.Register
{
    public class RegisterUserTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;

        public RegisterUserTest(WebApplicationFactory<Program> factory) 
            => _httpClient = factory.CreateClient();

        [Fact]
        public async Task Success()
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            var httplClient = new HttpClient();
            
            var response =  await httplClient.PostAsJsonAsync("User", request);
            
            response.ShouldBeNull();
            response.StatusCode.ShouldBe(HttpStatusCode.Created);
            await using var responseBody = await response.Content.ReadAsStreamAsync();
            var responseData = await JsonDocument.ParseAsync(responseBody);
            responseData.RootElement.GetProperty("name").GetString().ShouldNotBeNullOrWhiteSpace();
                //ShouldBe(request.Name);
        }
    }
}
