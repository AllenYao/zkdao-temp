using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using zic_dotnet;
using zic_dotnet.Domain;

namespace zkdao.Domain {
    public class InfoReply : IEntity {
        [Key]
        public Guid ID { get; set; }
        public int ActEnum { get; set; }
        public eAct Act {
            get { return (eAct)ActEnum; }
        }

        public Guid PosterID { get; set; }
        public Guid InfoID { get; set; }

        public string ContentRich { get; set; } //受限制的Rich，参考知呼
        public int PlusAmount { get; set; }
        public int MinusAmount { get; set; }
        public DateTime CreatTime { get; set; }
    }

    public class InfoReplyData {
        public string ID { get; set; }
        public int ActEnum { get; set; }
        public UserData Poster { get; set; }
        public InfoData Info { get; set; }
        public string ContentRich { get; set; }
        public int PlusAmount { get; set; }
        public int MinusAmount { get; set; }
        public DateTime CreatTime { get; set; }
    }
}
