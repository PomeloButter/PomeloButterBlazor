using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PomeloButterBlog.Common.Models;

namespace PomeloButterBlog.App.HttpClientApi
{
    public partial class BlogApi
    {
        public async Task<IList<TagViewModel>> GetTagListAsync()
        {
            
            var result = await WorkingAsync(HttpMethod.Get, "/BlogApi/GetTagList/");
            IList<TagViewModel> models = JsonConvert.DeserializeObject<IList<TagViewModel>>(result);
            return models;
        }
    }
}