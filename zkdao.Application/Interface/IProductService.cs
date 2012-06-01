using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using zic_dotnet;
using zkdao.Domain;

namespace zkdao.Application {

    [ServiceContract(Namespace = "www.zkdao.com")]
    public interface IProductService {

        [OperationContract]
        ProductData ProductGetByID(Guid ID);

        [OperationContract]
        Pager<ProductData> ProductGetPager(int pageIndex, int pageSize);

        [OperationContract]
        ProductData ProductSubmit(ProductData subInfo, bool force);

        [OperationContract]
        void ProductApproved(Guid ID);

        [OperationContract]
        void ProductUpdate(ProductData dataObject);
    }
}