using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using zic_dotnet;
using zic_dotnet.Domain;

namespace zkdao.Domain {
    public class ProductReply : IEntity {
        [Key]
        public Guid ID { get; set; }
        public int ActEnum { get; set; }
        public eAct Act {
            get { return (eAct)ActEnum; }
        }

        public Guid PosterID { get; set; }
        public User Poster { get; set; }
        public Guid ProductID { get; set; }
        public Product Product { get; set; }

        public string AdvContentRich { get; set; } //受限制的Rich，参考知呼
        public string ShortContentRich { get; set; }
        public int PlusAmount { get; set; }
        public int MinusAmount { get; set; }
        public DateTime CreatTime { get; set; }
    }

    public class ProductReplyData {
        public string ID { get; set; }
        public int ActEnum { get; set; }
        public UserData Poster { get; set; }
        public ProductData Product { get; set; }
        public string AdvContentRicj { get; set; }
        public string ShortContentRich { get; set; }
        public int PlusAmount { get; set; }
        public int MinusAmount { get; set; }
        public DateTime CreatTime { get; set; }
    }
}
