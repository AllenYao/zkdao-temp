using System;
using System.Collections.Generic;
using AutoMapper;
using zkdao.Domain;
using zkdao.Repositories.EF;
using zic_dotnet;
using zic_dotnet.Domain;
using zic_dotnet.Repositories;
using zic_dotnet.Specifications;

namespace zkdao.Application {
    public interface IUserApplication {
        UserData UserGetByID(Guid ID);
        UserData UserGetByEmail(string email);
        Pager<UserData> UserGetPager(int pageNumber, int pageSize);
        Guid UserCreat(UserData dataObject);
        bool UserValidate(string email, string password);
        void UpdateCustomer(string email, UserData dataObject);
    }
}