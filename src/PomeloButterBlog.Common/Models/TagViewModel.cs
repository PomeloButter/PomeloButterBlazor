using System.Runtime.Serialization;

namespace PomeloButterBlog.Common.Models
{
    public class TagViewModel
    {
        [DataMember]
        public string TagName { get; set; }
        [DataMember]
        public string DisplayName { get; set; }
        [DataMember]
        public int Count { get; set; }
    }
}