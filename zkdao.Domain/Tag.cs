using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using zic_dotnet;
using zic_dotnet.Domain;

namespace zkdao.Domain {
    public class Tag : IAggregateRoot {
        [Key]
        public Guid ID { get; set; }
        public int ActEnum { get; set; }
        public eAct Act {
            get { return (eAct)ActEnum; }
        }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public int GroupEnum { get; set; }
        public eTagGroup Group {
            get { return (eTagGroup)GroupEnum; }
        }

        public bool IsNecessary { get; set; }

        public virtual ICollection<InfoRelaTag> Infos { get; set; }
    }

    public class TagData {
        public string ID { get; set; }
        public int ActEnum { get; set; }
        public string Name { get; set; }
        public int GroupEnum { get; set; }
        public bool IsNecessary { get; set; }
        public virtual ICollection<InfoRelaTagData> Infos { get; set; }
    }
}
