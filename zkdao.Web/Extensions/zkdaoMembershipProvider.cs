using System;
using System.Web.Security;
using zic_dotnet;
using zkdao.Web.UserServiceReference;

namespace zkdao.Web.Models {

    public class zkdaoMembershipProvider : MembershipProvider {
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

        private MembershipUser ConvertToMem(UserData userObj) {
            if (userObj == null)
                return null;
            MembershipUser user = new MembershipUser("zkdaoMembershipProvider",
                userObj.Email, userObj.ID, userObj.Email, "", "",
                userObj.ActEnum != (int)eAct.unApproved, userObj.ActEnum == (int)eAct.Freeze,
                userObj.DateCreated, userObj.DateLastLogin ?? DateTime.MinValue, DateTime.MinValue,
                userObj.DateLastPasswordChange ?? DateTime.MinValue, DateTime.Now);
            return user;
        }

        private UserData ConvertToData(MembershipUser memUser) {
            if (memUser == null)
                return null;
            UserData user = new UserData {
                DateCreated = memUser.CreationDate,
                DateLastLogin = memUser.LastLoginDate,
                DateLastPasswordChange = memUser.LastPasswordChangedDate,
                Email = memUser.Email,
                Name = memUser.UserName
            };
            return user;
        }

        private string GetConfigValue(string configValue, string defaultValue) {
            if (string.IsNullOrEmpty(configValue))
                return defaultValue;
            return configValue;
        }

        public override string ApplicationName {
            get {
                return applicationName;
            }
            set {
                applicationName = value;
            }
        }

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config) {
            if (config == null)
                throw new ArgumentNullException("config");

            if (name == null || name.Length == 0)
                name = "zkdaoMembershipProvider";

            if (String.IsNullOrEmpty(config["description"])) {
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

        public override bool ChangePassword(string userKey, string oldPassword, string newPassword) {
            if (ValidateUser(userKey, oldPassword)) {
                UserData userData = new UserData {
                    PasswordHash = UserData.GetHashPassword(newPassword),
                    Email = userKey
                };
                using (UserServiceClient client = new UserServiceClient()) {
                    client.UserUpdate(userData.Email, userData);
                }
                return true;
            } else {
                return false;
            }
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer) {
            throw new NotSupportedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status) {
            ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, password, true);
            OnValidatingPassword(args);
            if (args.Cancel) {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }
            if (RequiresUniqueEmail && !string.IsNullOrEmpty(GetUserNameByEmail(email))) {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }
            MembershipUser user = GetUser(username, true);
            if (user == null) {
                using (UserServiceClient svc = new UserServiceClient()) {
                    UserData dataObject = new UserData {
                        Name = email,
                        PasswordHash = UserData.GetHashPassword(password),
                        Email = email
                    };
                    svc.UserCreat(dataObject);
                }
                status = MembershipCreateStatus.Success;
                return GetUser(username, true);
            } else {
                status = MembershipCreateStatus.DuplicateUserName;
                return null;
            }
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData) {
            throw new NotSupportedException();
        }

        public override bool EnablePasswordReset {
            get { return this.enablePasswordReset; }
        }

        public override bool EnablePasswordRetrieval {
            get { return this.enablePasswordRetrieval; }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords) {
            MembershipUserCollection col = new MembershipUserCollection();
            using (UserServiceClient client = new UserServiceClient()) {
                var dataObject = client.UserGetByKey(emailToMatch);
                totalRecords = 1;
                col.Add(this.ConvertToMem(dataObject));
                client.Close();
                return col;
            }
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords) {
            MembershipUserCollection col = new MembershipUserCollection();
            using (UserServiceClient client = new UserServiceClient()) {
                var dataObject = client.UserGetByKey(usernameToMatch);
                totalRecords = 1;
                col.Add(this.ConvertToMem(dataObject));
                client.Close();
                return col;
            }
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords) {
            MembershipUserCollection col = new MembershipUserCollection();
            using (UserServiceClient client = new UserServiceClient()) {
                Pager<UserData> dataObjects = client.UserGetPager(pageIndex, pageSize);
                if (dataObjects != null) {
                    totalRecords = dataObjects.Count;
                    foreach (var dataObject in dataObjects.Result)
                        col.Add(this.ConvertToMem(dataObject));
                } else {
                    totalRecords = 0;
                }
                client.Close();
                return col;
            }
        }

        public override int GetNumberOfUsersOnline() {
            return 0;
        }

        public override string GetPassword(string username, string answer) {
            throw new NotSupportedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline) {
            using (UserServiceClient client = new UserServiceClient()) {
                var dataObject = client.UserGetByKey(username);
                client.Close();
                if (dataObject == null)
                    return null;
                return ConvertToMem(dataObject);
            }
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline) {
            using (UserServiceClient client = new UserServiceClient()) {
                var dataObject = client.UserGetByID((Guid)providerUserKey);
                client.Close();
                if (dataObject == null)
                    return null;
                return ConvertToMem(dataObject);
            }
        }

        public override string GetUserNameByEmail(string email) {
            using (UserServiceClient client = new UserServiceClient()) {
                var dataObject = client.UserGetByKey(email);
                client.Close();
                if (dataObject == null)
                    return null;
                return dataObject.Name;
            }
        }

        public override int MaxInvalidPasswordAttempts {
            get { return this.maxInvalidPasswordAttempts; }
        }

        public override int MinRequiredNonAlphanumericCharacters {
            get { return this.minRequiredNonalphanumericCharacters; }
        }

        public override int MinRequiredPasswordLength {
            get { return this.minRequiredPasswordLength; }
        }

        public override int PasswordAttemptWindow {
            get { return this.passwordAttemptWindow; }
        }

        public override MembershipPasswordFormat PasswordFormat {
            get { return this.passwordFormat; }
        }

        public override string PasswordStrengthRegularExpression {
            get { return this.passwordStrengthRegularExpression; }
        }

        public override bool RequiresQuestionAndAnswer {
            get { return this.requiresQuestionAndAnswer; }
        }

        public override bool RequiresUniqueEmail {
            get { return this.requiresUniqueEmail; }
        }

        public override string ResetPassword(string username, string answer) {
            throw new NotSupportedException();
        }

        public override bool UnlockUser(string userName) {
            throw new NotSupportedException();
        }

        public override void UpdateUser(MembershipUser userData) {
            throw new NotSupportedException();
        }

        public override bool ValidateUser(string username, string password) {
            using (UserServiceClient client = new UserServiceClient()) {
                return client.UserValidate(username, UserData.GetHashPassword(password));
            }
        }
    }
}