using System;
using System.Collections.Generic;
using AutoMapper;
using zic_dotnet;
using zic_dotnet.Domain;
using zic_dotnet.Repositories;
using zic_dotnet.Specifications;
using zkdao.Domain;
using zkdao.Repositories.EF;

namespace zkdao.Application {

    public class UserApplication : BaseApplication, IUserService {

        public UserData UserGetByID(Guid ID) {
            using (IRepositoryContext context = IocLocator.Instance.GetService<IRepositoryContext>()) {
                var userRepository = context.GetRepository<User>();
                var user = userRepository.GetByKey(ID);
                if (user == null)
                    return null;
                var userData = Mapper.Map<User, UserData>(user);
                return userData;
            }
        }

        public UserData UserGetByEmail(string email) {
            using (IRepositoryContext context = IocLocator.Instance.GetService<IRepositoryContext>()) {
                var userRepository = context.GetRepository<User>();
                var customer = userRepository.Find(Specification<User>.Eval(c => c.Email == email));
                if (customer == null)
                    return null;
                var userData = Mapper.Map<User, UserData>(customer);
                return userData;
            }
        }

        public Pager<UserData> UserGetPager(int pageIndex, int pageSize) {
            using (IRepositoryContext context = IocLocator.Instance.GetService<IRepositoryContext>()) {
                var customerRepository = context.GetRepository<User>();
                var users = customerRepository.FindAll(pageIndex, pageSize);
                if (users == null)
                    return null;
                var userDatas = new List<UserData>();
                foreach (var user in users) {
                    userDatas.Add(Mapper.Map<User, UserData>(user));
                }
                Pager<UserData> pager = new Pager<UserData>();
                pager.Count = userDatas.Count;
                pager.Result = userDatas;
                return pager;
            }
        }

        public Guid UserCreat(UserData dataObject) {
            if (dataObject == null)
                throw new ArgumentNullException("userDataObject");

            using (IRepositoryContext context = IocLocator.Instance.GetService<IRepositoryContext>()) {
                IRepository<User> userRepository = context.GetRepository<User>();
                if (userRepository.Exists(Specification<User>.Eval(c => c.Email == dataObject.Email)))
                    throw new DomainException("Customer with the Email of '{0}' already exists.", dataObject.Email);
                //dataObject.DateCreated = DateTime.Now;
                User user = Mapper.Map<UserData, User>(dataObject);
                userRepository.Add(user);
                context.Commit();
                return user.ID;
            }
        }

        public bool UserValidate(string email, string password) {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException("email");
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException("password");
            using (IRepositoryContext context = IocLocator.Instance.GetService<IRepositoryContext>()) {
                var userRepository = context.GetRepository<User>();
                return userRepository.Exists(Specification<User>.Eval(c => c.Email == email && c.PasswordHash == User.GetHashPassword(password)));
            }
        }

        public void UpdateCustomer(string email, UserData dataObject) {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException("email");
            if (dataObject == null)
                throw new ArgumentNullException("dataObject");

            using (IRepositoryContext context = IocLocator.Instance.GetService<IRepositoryContext>()) {
                var userRepository = context.GetRepository<User>();
                var user = userRepository.Get(Specification<User>.Eval(c => c.Email == email));
                if (!string.IsNullOrEmpty(dataObject.PasswordHash))
                    user.PasswordHash = dataObject.PasswordHash;
                if (!string.IsNullOrEmpty(dataObject.Name))
                    user.Name = dataObject.Name;
                if (!string.IsNullOrEmpty(dataObject.Link))
                    user.Link = dataObject.Link;
                if (!string.IsNullOrEmpty(dataObject.QQ))
                    user.QQ = dataObject.QQ;
                if (!string.IsNullOrEmpty(dataObject.Weibo))
                    user.Weibo = dataObject.Weibo;
                if (dataObject.RoleEnum != 0)
                    user.RoleEnum = dataObject.RoleEnum;
                if (dataObject.ActEnum != 0)
                    user.ActEnum = dataObject.ActEnum;
                if (dataObject.DateLastLogin.HasValue)
                    user.DateLastLogin = dataObject.DateLastLogin;
                if (dataObject.DateLastPasswordChange.HasValue)
                    user.DateLastPasswordChange = dataObject.DateLastPasswordChange;
                userRepository.Update(user);
            }
        }
    }
}