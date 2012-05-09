using System;
using System.Collections.Generic;
using AutoMapper;
using System.ServiceModel;
using zkdao.Domain;
using zkdao.Repositories.EF;
using zic_dotnet;
using zic_dotnet.Domain;
using zic_dotnet.Repositories;
using zic_dotnet.Specifications;

namespace zkdao.Application {

    [ServiceContract(Namespace = "http://www.zkdao.com")]
    public interface IUserService {
        [OperationContract]
        UserData UserGetByID(Guid ID);
        [OperationContract]
        UserData UserGetByEmail(string email);
        [OperationContract]
        Pager<UserData> UserGetPager(int pageIndex, int pageSize);
        [OperationContract]
        Guid UserCreat(UserData dataObject);
        [OperationContract]
        bool UserValidate(string email, string password);
        [OperationContract]
        void UpdateCustomer(string email, UserData dataObject);
    }
}