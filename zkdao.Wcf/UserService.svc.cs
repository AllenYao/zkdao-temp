﻿ 

// ******************************************************
// DO NOT CHANGE THE CONTENT OF THIS FILE
// This file was generated by the T4 engine and the 
// contents of this source code will be changed after
// the custom tool was run.
// ******************************************************
using System;
using System.Collections.Generic;
using System.ServiceModel;
using zkdao.Application;
using zkdao.Domain;
using zic_dotnet;

namespace zkdao.Wcf {
	public class UserService : IUserService {
		private readonly IUserService userService = IocLocator.Instance.GetImple<IUserService>();
				public UserData UserGetByID(Guid ID) {
			try {
				return userService.UserGetByID(ID);
			}
			catch(Exception ex) {
				throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
			}
		}
		public UserData UserGetByKey(String userkey) {
			try {
				return userService.UserGetByKey(userkey);
			}
			catch(Exception ex) {
				throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
			}
		}
		public Pager<UserData> UserGetPager(Int32 pageIndex, Int32 pageSize) {
			try {
				return userService.UserGetPager(pageIndex, pageSize);
			}
			catch(Exception ex) {
				throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
			}
		}
		public Guid UserRegister(UserData dataObject) {
			try {
				return userService.UserRegister(dataObject);
			}
			catch(Exception ex) {
				throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
			}
		}
		public Boolean UserValidate(String userkey, String password) {
			try {
				return userService.UserValidate(userkey, password);
			}
			catch(Exception ex) {
				throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
			}
		}
		public void UserUpdate(String userkey, UserData dataObject) {
			try {
				 userService.UserUpdate(userkey, dataObject);
			}
			catch(Exception ex) {
				throw new FaultException<FaultData>(FaultData.CreateFromException(ex), FaultData.CreateFaultReason(ex));
			}
		}
	}
}


