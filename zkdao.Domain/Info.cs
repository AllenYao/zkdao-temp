using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using zic_dotnet.Domain;

namespace zkdao.Domain {

    public class Info : IAggregateRoot {

        [Key]
        public Guid ID { get; set; }

        public int ActEnum { get; set; }

        public eAct Act {
            get { return (eAct)ActEnum; }
        }

        public Guid PosterID { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        public string ContentRich { get; set; }

        [MaxLength(200)]
        public string LinkUrl { get; set; }

        [MaxLength(200)]
        public string LinkImage { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public int GradeAverage { get; set; }

        public Guid ProductID { get; set; }

        public virtual ICollection<InfoReply> Replys { get; set; }

        public virtual ICollection<InfoRelaTag> Tags { get; set; }
    }

    public class InfoData {

        public string ID { get; set; }

        public int ActEnum { get; set; }

        public UserData Poster { get; set; }

        public string Title { get; set; }

        public string ContentRich { get; set; }

        public string LinkUrl { get; set; }

        public string LinkImage { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public int GradeAverage { get; set; }

        public ProductData Product { get; set; }

        public virtual ICollection<InfoReplyData> Replys { get; set; }

        public virtual ICollection<InfoRelaTagData> Tags { get; set; }
    }
}