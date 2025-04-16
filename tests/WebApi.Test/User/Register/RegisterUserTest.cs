namespace WebApi.Test.User.Register
{
    public class RegisterUserTest : MyRecipeBookClassFixture
    {
        private readonly string method = "user";

        public RegisterUserTest(CustomWebApplicationFactory factory) : base(factory) { }

        [Fact]
        public async Task Success()
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            var httplClient = new HttpClient();
            
            var response =  await DoPost(method, request);
            
            response.StatusCode.ShouldBe(HttpStatusCode.Created);
            await using var responseBody = await response.Content.ReadAsStreamAsync();
            var responseData = await JsonDocument.ParseAsync(responseBody);
            responseData.RootElement.GetProperty("name").GetString().ShouldNotBeNullOrWhiteSpace();
        }

        [Theory]
        [ClassData(typeof(CultureInlineDataTest))]
        public async Task Error_Empty_Name(string culture)
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Name = string.Empty;
            var response = await DoPost(method, request, culture);

            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

            await using var responseBody = await response.Content.ReadAsStreamAsync();
            var responseData = await JsonDocument.ParseAsync(responseBody);
            var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();
            var expectedMessage  = ResourceMessagesException.ResourceManager.GetString("NAME_EMPTY", new CultureInfo(culture));

            errors.ShouldBeUnique();
            errors.ShouldContain(error => error.GetString()!.Equals(expectedMessage));
        }
    }
}
