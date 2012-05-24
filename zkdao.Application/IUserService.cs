using System;
using System.ServiceModel;
using zic_dotnet;
using zkdao.Domain;

namespace zkdao.Application {

    [ServiceContract(Namespace = "http://www.zkdao.com")]
    public interface IUserService {

        [OperationContract]
        UserData UserGetByID(Guid ID);

        [OperationContract]
        UserData UserGetByKey(string userkey);

        [OperationContract]
        Pager<UserData> UserGetPager(int pageIndex, int pageSize);

        [OperationContract]
        UserData UserRegister(UserData dataObject);

        [OperationContract]
        bool UserApproved(string userkey, string approvedID);

        [OperationContract]
        void UserRequestApproved(string userkey);

        [OperationContract]
        bool UserValidate(string userkey, string password);

        [OperationContract]
        void UserUpdate(string userkey, UserData dataObject);
    }
}