﻿using MyRecipeBook.Comunication.Requests;

namespace WebApi.Test.User.Login
{
    public class DoLoginTest : MyRecipeBookClassFixture
    {
        private readonly string method = "Login";
        private readonly string _email;
        private readonly string _password;
        private readonly string _name;

        public DoLoginTest(CustomWebApplicationFactory factory) : base(factory)
        {
            _email = factory.GetEmail();
            _password = factory.GetPassowrd();
            _name = factory.GetName();
        }

        [Fact]
        public async Task Success()
        {
            var request = new RequestLoginJson
            {
                Email = _email,
                Password = _password
            };

            var response = await DoPost(method, request);

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            await using var responseBody = await response.Content.ReadAsStreamAsync();
            var responseData = await JsonDocument.ParseAsync(responseBody);
            responseData.RootElement.GetProperty("name").GetString().ShouldNotBeNullOrWhiteSpace();
            responseData.RootElement.GetProperty("name").GetString().ShouldBe(_name);
        }

        [Theory]
        [ClassData(typeof(CultureInlineDataTest))]
        public async Task Error_Login_Invalid(string culture)
        {
            var request = RequestLoginJsonBuilder.Build();

            var response = await DoPost(method, request, culture);

            response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);

            await using var responseBody = await response.Content.ReadAsStreamAsync();
            var responseData = await JsonDocument.ParseAsync(responseBody);
            var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();
            var expectedMessage = ResourceMessagesException.ResourceManager.GetString("EMAIL_OR_PASSWORD_INVALID", new CultureInfo(culture));

            errors.ShouldBeUnique();
            errors.ShouldContain(error => error.GetString()!.Equals(expectedMessage));
        }
    }
}
