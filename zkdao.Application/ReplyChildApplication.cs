using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using zkdao.Domain;
using zic_dotnet;
using zic_dotnet.Repositories;
using AutoMapper;
using zic_dotnet.Specifications;

namespace zkdao.Application {

    public class ReplyChildApplication : BaseApplication, IReplyChildService {
        public ReplyChildData ReplyChildGetByID(Guid ID) {
            using (IRepositoryContext context = IocLocator.Instance.GetImple<IRepositoryContext>()) {
                var replyChildRepository = context.GetRepository<ReplyChild>();
                var replyChild = replyChildRepository.GetByKey(ID);
                if (replyChild == null)
                    return null;
                var replyChildData = Mapper.Map<ReplyChild, ReplyChildData>(replyChild);
                return replyChildData;
            }
        }

        public Pager<ReplyChildData> ReplyChildGetPager(int pageIndex, int pageSize) {
            using (IRepositoryContext context = IocLocator.Instance.GetImple<IRepositoryContext>()) {
                var replyChildRepository = context.GetRepository<ReplyChild>();
                var replyChilds = replyChildRepository.FindAll(pageIndex, pageSize);
                if (replyChilds == null)
                    return null;
                var replyChildDatas = new List<ReplyChildData>();
                foreach (var replyChild in replyChilds) {
                    replyChildDatas.Add(Mapper.Map<ReplyChild, ReplyChildData>(replyChild));
                }
                Pager<ReplyChildData> pager = new Pager<ReplyChildData>();
                pager.Count = replyChildDatas.Count;
                pager.Result = replyChildDatas;
                return pager;
            }
        }

        public ReplyChildData ReplyChildCreat(ReplyChildData dataObject) {
            if (dataObject == null)
                throw new ArgumentNullException("replyChildDataObject");
            using (IRepositoryContext context = IocLocator.Instance.GetImple<IRepositoryContext>()) {
                var replyChildRepository = context.GetRepository<ReplyChild>();
                ReplyChild replyChild = Mapper.Map<ReplyChildData, ReplyChild>(dataObject);
                replyChild.CreatTime = DateTime.Now;
                replyChildRepository.Add(replyChild);
                context.Commit();
                return Mapper.Map<ReplyChild, ReplyChildData>(replyChild);
            }
        }

        public void ReplyChildUpdate(ReplyChildData dataObject) {
            if (string.IsNullOrEmpty(dataObject.ID))
                throw new ArgumentNullException("ID");
            ReplyChild replyChild = Mapper.Map<ReplyChildData, ReplyChild>(dataObject);
            using (IRepositoryContext context = IocLocator.Instance.GetImple<IRepositoryContext>()) {
                var replyChildRepository = context.GetRepository<ReplyChild>();
                var upInfo = replyChildRepository.Get(Specification<ReplyChild>.Eval(c => c.ID.ToString() == dataObject.ID));
                if (!string.IsNullOrEmpty(dataObject.Content))
                    upInfo.Content = dataObject.Content;
                if (dataObject.ActEnum != 0)
                    upInfo.ActEnum = dataObject.ActEnum;
                replyChildRepository.Update(upInfo);
            }
        }
    }
}