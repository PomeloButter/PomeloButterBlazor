using System.Net.Http;
using System.Threading.Tasks;

namespace PomeloButterBlog.App.HttpClientApi
{
    public partial class BlogApi
    {
        private readonly HttpClient _client;

        public BlogApi(HttpClient client)
        {
            this._client = client;
        }

        private async Task<string> WorkingAsync(HttpMethod httpMethod,string url)
        {
            var request = new HttpRequestMessage(httpMethod, url);
            var response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            return null;
        }
    }
}