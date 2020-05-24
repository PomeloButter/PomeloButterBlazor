using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using PomeloButterBlog.Common.Models;

namespace PomeloButterBlog.App.HttpClientApi
{
    public partial class BlogApi
    {
        public async Task<IList<TagViewModel>> GetTagListAsync()
        {
            
            var result = await WorkingAsync(HttpMethod.Get, "/BlogApi/GetTagList/");
            IList<TagViewModel> models = JsonSerializer.Deserialize<IList<TagViewModel>>(result, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return models;
        }
    }
}