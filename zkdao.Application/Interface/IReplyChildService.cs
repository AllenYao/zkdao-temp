using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using zkdao.Domain;
using zic_dotnet;

namespace zkdao.Application {

    [ServiceContract(Namespace = "www.zkdao.com")]
    public interface IReplyChildService {
        [OperationContract]
        ReplyChildData ReplyChildGetByID(Guid ID);

        [OperationContract]
        Pager<ReplyChildData> ReplyChildGetPager(int pageIndex, int pageSize);

        [OperationContract]
        ReplyChildData ReplyChildCreat(ReplyChildData replyChild);

        [OperationContract]
        void ReplyChildUpdate(ReplyChildData dataObject);
    }
}