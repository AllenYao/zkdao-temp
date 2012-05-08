﻿using System.Linq;
using AutoMapper;
using Microsoft.Practices.Unity;
using zkdao.Domain;
using zkdao.Repositories.EF;
using zic_dotnet;
using zic_dotnet.Domain;
using zic_dotnet.Repositories;

namespace zkdao.Application {
    public abstract class BaseApplication {
        protected virtual void DispatchDomainEvent<TEvent>(TEvent evnt)
            where TEvent : IDomainEvent {
            // TODO: Implement the domain event dispatching logic here.
        }

        public static void Initialize() {
            IocLocator.Container.RegisterType<IRepositoryContext, EFRepositoryContext>();
            IocLocator.Container.RegisterType<IRepository<User>, EntityFrameworkRepository<User>>();
            IocLocator.Container.RegisterType<IRepository<Info>, EntityFrameworkRepository<Info>>();
            IocLocator.Container.RegisterType<IRepository<Product>, EntityFrameworkRepository<Product>>();
            IocLocator.Container.RegisterType<IRepository<ReplyChild>, EntityFrameworkRepository<ReplyChild>>();

            Mapper.CreateMap<UserData, User>();
            Mapper.CreateMap<InfoData, Info>();
            Mapper.CreateMap<InfoReplyData, InfoReply>();
            Mapper.CreateMap<ProductData, Product>();
            Mapper.CreateMap<ProductReplyData, ProductReply>();
            Mapper.CreateMap<ReplyChildData, ReplyChild>();
            Mapper.CreateMap<UserRelaInfoData, UserRelaInfo>();
            Mapper.CreateMap<UserRelaProductData, UserRelaProduct>();
            Mapper.CreateMap<UserRelaReplyData, UserRelaReply>();
        }
    }
}
