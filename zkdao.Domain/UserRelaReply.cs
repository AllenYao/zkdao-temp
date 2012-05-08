using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using zic_dotnet;
using zic_dotnet.Domain;

namespace zkdao.Domain {
    public class UserRelaReply : IEntity {
        [Key]
        public Guid ID { get; set; }

        public Guid UserID { get; set; }
        public User User { get; set; }
        public Guid TargetID { get; set; }

        public bool PlusOrMinus { get; set; }
    }

    public class UserRelaReplyData {
        public string ID { get; set; }
        public UserData User { get; set; }
        public string TargetID { get; set; }
        public bool PlusOrMinus { get; set; }
    }
}
