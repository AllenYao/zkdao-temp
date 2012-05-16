using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using zic_dotnet;
using zic_dotnet.Domain;

namespace zkdao.Domain {
    public class Product : IAggregateRoot {
        [Key]
        public Guid ID { get; set; }
        public int ActEnum { get; set; }
        public eAct Act {
            get { return (eAct)ActEnum; }
        }
        [Timestamp]
        public Byte[] Timestamp { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required]
        [MaxLength(200)]
        public string Link { get; set; } //来源不确定
        [MaxLength(200)]
        public string PriceHistoryLink { get; set; } //基本固定为淘宝的历史价格

        public string PriceHistory { get; set; }
        public string Image { get; set; }

        public int EagerAmount { get; set; }
        public string EagerHistory { get; set; }
        public int GradeAverage { get; set; }
        public string GradeHistory { get; set; }

        public virtual ICollection<ProductReply> Replys { get; set; }
    }

    public class ProductData {
        public string ID { get; set; }
        public int ActEnum { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string PriceHistoryLink { get; set; }
        public string Image { get; set; }
        public int EagerAmount { get; set; }
        public string EagerHistory { get; set; }
        public int GradeAverage { get; set; }
        public string GradeHistory { get; set; }
        public virtual ICollection<ProductReplyData> Replys { get; set; }
    }
}
