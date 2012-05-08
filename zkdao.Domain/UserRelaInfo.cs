using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using zic_dotnet;
using zic_dotnet.Domain;

namespace zkdao.Domain {
    public class UserRelaInfo : IEntity {
        [Key]
        public Guid ID { get; set; }

        public Guid UserID { get; set; }
        public User User { get; set; }

        public Guid InfoID { get; set; }
        public Info Info { get; set; }

        public int GiveGrade { get; set; }
    }

    public class UserRelaInfoData {
        public string ID { get; set; }
        public UserData User { get; set; }
        public string InfoID { get; set; }
        public int GiveGrade { get; set; }
    }
}
