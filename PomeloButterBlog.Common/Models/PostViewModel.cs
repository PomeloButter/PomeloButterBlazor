using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PomeloButterBlog.Common.Models
{
    public class PostViewModel
    {
        [DataMember]
        public int Year { get; set; }
        [DataMember]

        public IList<PostModel> Posts { get; set; }
    }

    public class PostModel
    {
        [DataMember]
        public  string Id { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Url { get; set; }
        [DataMember]
        public int Year { get; set; }
        [DataMember]
        public string CreationTime { get; set; }
    }
}