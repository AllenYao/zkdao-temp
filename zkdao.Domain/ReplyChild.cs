using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using zic_dotnet;
using zic_dotnet.Domain;

namespace zkdao.Domain {
    public class ReplyChild : IAggregateRoot {
        [Key]
        public Guid ID { get; set; }
        public int ActEnum { get; set; }
        public eAct Act {
            get { return (eAct)ActEnum; }
        }

        public Guid PosterID { get; set; }
        public Guid TargetID { get; set; }

        public string Content { get; set; }
        public DateTime CreatTime { get; set; }
    }

    public class ReplyChildData {
        public string ID { get; set; }
        public int ActEnum { get; set; }
        public UserData Poster { get; set; }
        public string TargetID { get; set; }
        public string Content { get; set; }
        public DateTime CreatTime { get; set; }
    }
}
