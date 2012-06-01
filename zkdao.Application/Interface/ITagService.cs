using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using zkdao.Domain;

namespace zkdao.Application {

    [ServiceContract(Namespace = "www.zkdao.com")]
    public interface ITagService {
        [OperationContract]
        TagData TagGetByKey(string name);
        [OperationContract]
        IList<TagData> TagGet();
        [OperationContract]
        TagData TagCreat(TagData dataObject);
        [OperationContract]
        void TagUpdate(TagData dataObject);
    }
}