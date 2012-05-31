using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using zkdao.Domain;
using zic_dotnet;

namespace zkdao.Application {
    [ServiceContract(Namespace = "www.zkdao.com")]
    public interface IInfoService {
        [OperationContract]
        InfoData InfoGetByID(Guid ID);

        [OperationContract]
        Pager<InfoData> InfoGetPager(int pageIndex, int pageSize);

        [OperationContract]
        InfoData InfoSubmit(InfoData subInfo, bool force);

        [OperationContract]
        void InfoApproved(Guid ID);

        [OperationContract]
        void InfoUpdate(UserData dataObject);
    }
}
