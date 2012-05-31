using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using zic_dotnet;
using zkdao.Domain;

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
        void InfoUpdate(InfoData dataObject);
    }
}