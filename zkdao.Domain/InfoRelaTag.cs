using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using zic_dotnet;
using zic_dotnet.Domain;

namespace zkdao.Domain {
    public class InfoRelaTag {
        [Key, Column(Order = 0)]
        public Guid InfoID { get; set; }
        public Info Info { get; set; }

        [Key, Column(Order = 1)]
        public Guid TagID { get; set; }
        public Tag Tag { get; set; }
    }

    public class InfoRelaTagData {
        public string InfoID { get; set; }
        public string TagID { get; set; }
    }
}