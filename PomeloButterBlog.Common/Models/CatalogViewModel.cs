using System.Runtime.Serialization;

namespace PomeloButterBlog.Common.Models
{
    public class CatalogViewModel
    {
        [DataMember]
        public string CategoryName { get; set; }
        [DataMember]
        public string DisplayName { get; set; }
        [DataMember]
        public int Count { get; set; }
    }
}