﻿namespace WebApi.Test
{
    public class MyRecipeBookClassFixture : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;

        public MyRecipeBookClassFixture(CustomWebApplicationFactory factory) => _httpClient = factory.CreateClient();

        protected async Task<HttpResponseMessage> DoPost(string method, object request, string culture = "en")
        {
            ChangeRequestCulture(culture);
            var teste =  await _httpClient.PostAsJsonAsync(method, request);
            var t =  teste.Content.ReadAsStreamAsync();
            return teste;
        }

        private void ChangeRequestCulture(string culture)
        {
            if (_httpClient.DefaultRequestHeaders.Contains("Accept-Language"))
                _httpClient.DefaultRequestHeaders.Remove("Accept-Language");

            _httpClient.DefaultRequestHeaders.Add("Accept-Language", culture);
        }
    }
}
