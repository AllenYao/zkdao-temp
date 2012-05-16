using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using zic_dotnet;
using zic_dotnet.Domain;

namespace zkdao.Domain {

    public class User : IAggregateRoot {

        [Key]
        public Guid ID { get; set; }

        public int ActEnum { get; set; }

        public eAct Act {
            get { return (eAct)ActEnum; }
        }

        public int RoleEnum { get; set; }

        public eRole Role {
            get { return (eRole)Role; }
        }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [Column("Password")]
        [MaxLength(128)]
        public string PasswordHash { get; set; }

        public static string GetHashPassword(string value) {
            return Encrypt.EncryptUserPassword(value);
        }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Link { get; set; }

        [MaxLength(50)]
        public string QQ { get; set; }

        [MaxLength(50)]
        public string Weibo { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateLastLogin { get; set; }

        public DateTime? DateLastPasswordChange { get; set; }

        public virtual ICollection<Info> PostInfos { get; set; }

        public virtual ICollection<InfoReply> PostInfoReplys { get; set; }

        public virtual ICollection<ProductReply> PostProductReplys { get; set; }

        public virtual ICollection<ReplyChild> PostReplyChilds { get; set; }

        public virtual ICollection<UserRelaInfo> UserRelaInfos { get; set; }

        public virtual ICollection<UserRelaProduct> UserRelaProducts { get; set; }

        public virtual ICollection<UserRelaReply> UserRelaReplys { get; set; }
    }

    public class UserData {

        public string ID { get; set; }

        public int ActEnum { get; set; }

        public int RoleEnum { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string Name { get; set; }

        public string Link { get; set; }

        public string QQ { get; set; }

        public string Weibo { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateLastLogin { get; set; }

        public DateTime? DateLastPasswordChange { get; set; }

        public virtual ICollection<InfoData> PostInfos { get; set; }

        public virtual ICollection<InfoReplyData> PostInfoReplys { get; set; }

        public virtual ICollection<ProductReplyData> PostProductReplys { get; set; }

        public virtual ICollection<ReplyChildData> PostReplyChilds { get; set; }

        public virtual ICollection<UserRelaInfoData> UserRelaInfos { get; set; }

        public virtual ICollection<UserRelaProductData> UserRelaProducts { get; set; }

        public virtual ICollection<UserRelaReplyData> UserRelaReplys { get; set; }
    }
}