namespace zkdao.Web.Extensions {

    public enum eAct {
        Normal = 1,
        Delete = 2,
        Freeze = 3,
        unApproved = 4,
        Reported = 5
    }

    public enum eRole {
        Normal = 1,
        Cooperator = 2,
        Manager = 3
    }

    public enum eTagGroup {
        Goods = 1,
        Humanity = 2,
        Price = 3,
        Source = 4,
        Area = 5,
        Article = 6
    }

    public enum eUserToGoods {
        Normal = 1,
        Watch = 2,
        Eager = 3,
        Hold = 4,
        Ever = 5
    }
}