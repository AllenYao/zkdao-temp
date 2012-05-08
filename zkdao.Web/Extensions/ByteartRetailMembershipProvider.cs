using System;
using System.Web.Security;
using zkdao.Web.CustomerServiceReference;

namespace zkdao.Web.Extensions
{
    public class zkdaoMembershipProvider : MembershipProvider
    {
        private string applicationName;
        private bool enablePasswordReset;
        private bool enablePasswordRetrieval = false;
        private bool requiresQuestionAndAnswer = false;
        private bool requiresUniqueEmail = true;
        private int maxInvalidPasswordAttempts;
        private int passwordAttemptWindow;
        private int minRequiredPasswordLength;
        private int minRequiredNonalphanumericCharacters;
        private string passwordStrengthRegularExpression;
        private MembershipPasswordFormat passwordFormat = MembershipPasswordFormat.Clear;

        private MembershipUser ConvertFrom(CustomerDataObject userObj)
        {
            if (userObj == null)
                return null;

            MembershipUser user = new MembershipUser("zkdaoMembershipProvider",
                userObj.UserName, userObj.ID, userObj.Email, "", "", true, false,
                DateTime.MinValue, DateTime.MinValue, DateTime.MinValue,
                DateTime.Now, DateTime.Now);
            return user;
        }

        private string GetConfigValue(string configValue, string defaultValue)
        {
            if (string.IsNullOrEmpty(configValue))
                return defaultValue;

            return configValue;
        }

        public override string ApplicationName
        {
            get
            {
                return applicationName;
            }
            set
            {
                applicationName = value;
            }
        }

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            if (name == null || name.Length == 0)
                name = "zkdaoMembershipProvider";

            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Membership Provider for zkdao");
            }

            base.Initialize(name, config);

            applicationName = GetConfigValue(config["applicationName"], System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            maxInvalidPasswordAttempts = Convert.ToInt32(GetConfigValue(config["maxInvalidPasswordAttempts"], "5"));
            passwordAttemptWindow = Convert.ToInt32(GetConfigValue(config["passwordAttemptWindow"], "10"));
            minRequiredNonalphanumericCharacters = Convert.ToInt32(GetConfigValue(config["minRequiredNonalphanumericCharacters"], "1"));
            minRequiredPasswordLength = Convert.ToInt32(GetConfigValue(config["minRequiredPasswordLength"], "6"));
            enablePasswordReset = Convert.ToBoolean(GetConfigValue(config["enablePasswordReset"], "true"));
            passwordStrengthRegularExpression = Convert.ToString(GetConfigValue(config["passwordStrengthRegularExpression"], ""));
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotSupportedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            return false;
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, password, true);
            OnValidatingPassword(args);
            if (args.Cancel)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }
            if (RequiresUniqueEmail && !string.IsNullOrEmpty(GetUserNameByEmail(email)))
            {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }
            MembershipUser user = GetUser(username, true);
            if (user == null)
            {
                RegisterModelAdditionalData data = (RegisterModelAdditionalData)providerUserKey;
                using (CustomerServiceClient svc = new CustomerServiceClient())
                {
                    CustomerDataObject dataObject = new CustomerDataObject
                    {
                        UserName = username,
                        Password = password,
                        Email = email,
                        Contact = data.Contact,
                        Address = new AddressDataObject
                        {
                            City = data.AddressCity,
                            Country = data.AddressCountry,
                            State = data.AddressState,
                            Street = data.AddressStreet,
                            Zip = data.AddressZip
                        }
                    };
                    svc.CreateCustomer(dataObject);
                }
                status = MembershipCreateStatus.Success;
                return GetUser(username, true);
            }
            else
            {
                status = MembershipCreateStatus.DuplicateUserName;
                return null;
            }
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotSupportedException();
        }

        public override bool EnablePasswordReset
        {
            get { return this.enablePasswordReset; }
        }

        public override bool EnablePasswordRetrieval
        {
            get { return this.enablePasswordRetrieval; }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            MembershipUserCollection col = new MembershipUserCollection();
            using (CustomerServiceClient client = new CustomerServiceClient())
            {
                var dataObject = client.GetCustomerByEmail(emailToMatch);
                totalRecords = 1;
                col.Add(this.ConvertFrom(dataObject));
                client.Close();
                return col;
            }
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            MembershipUserCollection col = new MembershipUserCollection();
            using (CustomerServiceClient client = new CustomerServiceClient())
            {
                var dataObject = client.GetCustomerByUserName(usernameToMatch);
                totalRecords = 1;
                col.Add(this.ConvertFrom(dataObject));
                client.Close();
                return col;
            }
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            MembershipUserCollection col = new MembershipUserCollection();
            using (CustomerServiceClient client = new CustomerServiceClient())
            {
                var dataObjects = client.GetCustomers();
                if (dataObjects != null)
                {
                    totalRecords = dataObjects.Length;
                    foreach (var dataObject in dataObjects)
                        col.Add(this.ConvertFrom(dataObject));
                }
                else
                {
                    totalRecords = 0;
                }
                client.Close();
                return col;
            }
        }

        public override int GetNumberOfUsersOnline()
        {
            return 0;
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotSupportedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            using (CustomerServiceClient client = new CustomerServiceClient())
            {
                var dataObject = client.GetCustomerByUserName(username);
                client.Close();
                if (dataObject == null)
                    return null;
                return ConvertFrom(dataObject);
            }
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            using (CustomerServiceClient client = new CustomerServiceClient())
            {
                var dataObject = client.GetCustomerByKey((Guid)providerUserKey);
                client.Close();
                if (dataObject == null)
                    return null;
                return ConvertFrom(dataObject);
            }
        }

        public override string GetUserNameByEmail(string email)
        {
            using (CustomerServiceClient client = new CustomerServiceClient())
            {
                var dataObject = client.GetCustomerByEmail(email);
                client.Close();
                if (dataObject == null)
                    return null;
                return dataObject.UserName;
            }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return this.maxInvalidPasswordAttempts; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return this.minRequiredNonalphanumericCharacters; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return this.minRequiredPasswordLength; }
        }

        public override int PasswordAttemptWindow
        {
            get { return this.passwordAttemptWindow; }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { return this.passwordFormat; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { return this.passwordStrengthRegularExpression; }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return this.requiresQuestionAndAnswer; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return this.requiresUniqueEmail; }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotSupportedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotSupportedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotSupportedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            using (CustomerServiceClient client = new CustomerServiceClient())
            {
                return client.ValidateCustomer(username, password);
            }
        }
    }
}
