using System;
using System.ComponentModel.DataAnnotations;
using zic_dotnet.Domain;

namespace zkdao.Domain {

    public class UserRelaProduct : IEntity {

        [Key]
        public Guid ID { get; set; }

        public Guid UserID { get; set; }

        public User User { get; set; }

        public Guid ProductID { get; set; }

        public Product Product { get; set; }

        public int UserToGoodsEnum { get; set; }

        public eUserToGoods Status {
            get { return (eUserToGoods)UserToGoodsEnum; }
        }

        public bool IsEager { get; set; }

        public int GiveGrade { get; set; }

        //必须为Hold或Eager状态才能给予Grade
        public bool CouldGiveGrade() {
            return Status == eUserToGoods.Hold || Status == eUserToGoods.Ever;
        }
    }

    public class UserRelaProductData {

        public string ID { get; set; }

        public UserData User { get; set; }

        public string ProductID { get; set; }

        public int UserToGoodsEnum { get; set; }

        public bool IsEager { get; set; }

        public int GiveGrade { get; set; }
    }
}