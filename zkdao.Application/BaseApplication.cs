using System.Linq;
using AutoMapper;
using Microsoft.Practices.Unity;
using zic_dotnet;
using zic_dotnet.Domain;
using zic_dotnet.Repositories;
using zkdao.Domain;
using zkdao.Repositories.EF;

namespace zkdao.Application {

    public abstract class BaseApplication {

        protected virtual void DispatchDomainEvent<TEvent>(TEvent evnt)
            where TEvent : IDomainEvent {
            // TODO: Implement the domain event dispatching logic here.
        }

        public static void Initialize() {
            IocLocator.Container.RegisterType<IUserService, UserApplication>();
            IocLocator.Container.RegisterType<IRepositoryContext, EFRepositoryContext>();
            IocLocator.Container.RegisterType<IRepository<User>, EntityFrameworkRepository<User>>();
            IocLocator.Container.RegisterType<IRepository<Info>, EntityFrameworkRepository<Info>>();
            IocLocator.Container.RegisterType<IRepository<Product>, EntityFrameworkRepository<Product>>();
            IocLocator.Container.RegisterType<IRepository<ReplyChild>, EntityFrameworkRepository<ReplyChild>>();

            Mapper.CreateMap<UserData, User>();
            Mapper.CreateMap<User, UserData>();
            Mapper.CreateMap<InfoData, Info>();
            Mapper.CreateMap<Info, InfoData>();
            Mapper.CreateMap<InfoReplyData, InfoReply>();
            Mapper.CreateMap<InfoReply, InfoReplyData>();
            Mapper.CreateMap<ProductData, Product>();
            Mapper.CreateMap<Product, ProductData>();
            Mapper.CreateMap<ProductReplyData, ProductReply>();
            Mapper.CreateMap<ProductReply, ProductReplyData>();
            Mapper.CreateMap<ReplyChildData, ReplyChild>();
            Mapper.CreateMap<ReplyChild, ReplyChildData>();
            Mapper.CreateMap<UserRelaInfoData, UserRelaInfo>();
            Mapper.CreateMap<UserRelaInfo, UserRelaInfoData>();
            Mapper.CreateMap<UserRelaProductData, UserRelaProduct>();
            Mapper.CreateMap<UserRelaProduct, UserRelaProductData>();
            Mapper.CreateMap<UserRelaReplyData, UserRelaReply>();
            Mapper.CreateMap<UserRelaReply, UserRelaReplyData>();
        }
    }
}