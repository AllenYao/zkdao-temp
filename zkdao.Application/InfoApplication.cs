using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using zic_dotnet;
using zic_dotnet.Repositories;
using zic_dotnet.Specifications;
using zkdao.Domain;

namespace zkdao.Application {

    public class InfoApplication : BaseApplication, IInfoService {

        public InfoData InfoGetByID(Guid ID) {
            using (IRepositoryContext context = IocLocator.Instance.GetImple<IRepositoryContext>()) {
                var infoRepository = context.GetRepository<Info>();
                var info = infoRepository.GetByKey(ID);
                if (info == null)
                    return null;
                var infoData = Mapper.Map<Info, InfoData>(info);
                return infoData;
            }
        }

        public Pager<InfoData> InfoGetPager(int pageIndex, int pageSize) {
            using (IRepositoryContext context = IocLocator.Instance.GetImple<IRepositoryContext>()) {
                var infoRepository = context.GetRepository<Info>();
                var infos = infoRepository.FindAll(pageIndex, pageSize);
                if (infos == null)
                    return null;
                var infoDatas = new List<InfoData>();
                foreach (var info in infos) {
                    infoDatas.Add(Mapper.Map<Info, InfoData>(info));
                }
                Pager<InfoData> pager = new Pager<InfoData>();
                pager.Count = infoDatas.Count;
                pager.Result = infoDatas;
                return pager;
            }
        }

        public InfoData InfoSubmit(InfoData dataObject, bool force) {
            if (dataObject == null)
                throw new ArgumentNullException("infoDataObject");
            using (IRepositoryContext context = IocLocator.Instance.GetImple<IRepositoryContext>()) {
                IRepository<Info> infoRepository = context.GetRepository<Info>();
                Info info = Mapper.Map<InfoData, Info>(dataObject);
                info.CreatTime = DateTime.Now;
                info.ActEnum = force ? (int)eAct.Normal : (int)eAct.unApproved;
                infoRepository.Add(info);
                context.Commit();
                return Mapper.Map<Info, InfoData>(info);
            }
        }

        public void InfoApproved(Guid ID) {
            using (IRepositoryContext context = IocLocator.Instance.GetImple<IRepositoryContext>()) {
                var infoRepository = context.GetRepository<Info>();
                var info = infoRepository.GetByKey(ID);
                if (info == null)
                    throw new ArgumentNullException("infoDataID");
                if (info.Act == eAct.unApproved) {
                    info.ActEnum = (int)eAct.Normal;
                    infoRepository.Update(info);
                } else {
                    throw new Exception("InfoIsNotUnApproved");
                }
            }
        }

        public void InfoUpdate(InfoData dataObject) {
            if (string.IsNullOrEmpty(dataObject.ID))
                throw new ArgumentNullException("ID");
            Info info = Mapper.Map<InfoData, Info>(dataObject);
            using (IRepositoryContext context = IocLocator.Instance.GetImple<IRepositoryContext>()) {
                var infoRepository = context.GetRepository<Info>();
                var upInfo = infoRepository.Get(Specification<Info>.Eval(c => c.ID.ToString() == dataObject.ID));
                if (!string.IsNullOrEmpty(dataObject.Title))
                    upInfo.Title = dataObject.Title;
                if (!string.IsNullOrEmpty(dataObject.ContentRich))
                    upInfo.ContentRich = dataObject.ContentRich;
                if (dataObject.EndTime.HasValue)
                    upInfo.EndTime = dataObject.EndTime;
                if (dataObject.StartTime.HasValue)
                    upInfo.EndTime = dataObject.StartTime;
                if (!string.IsNullOrEmpty(dataObject.LinkUrl))
                    upInfo.LinkUrl = dataObject.LinkUrl;
                if (!string.IsNullOrEmpty(dataObject.LinkImage))
                    upInfo.LinkImage = dataObject.LinkImage;
                if (dataObject.GradeAverage != 0)
                    upInfo.GradeAverage = dataObject.GradeAverage;
                if (dataObject.ActEnum != 0)
                    upInfo.ActEnum = dataObject.ActEnum;
                infoRepository.Update(upInfo);
            }
        }
    }
}