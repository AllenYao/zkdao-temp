using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using zkdao.Domain;
using zic_dotnet.Repositories;
using zic_dotnet;
using zic_dotnet.Specifications;
using AutoMapper;

namespace zkdao.Application {

    public class TagApplication : BaseApplication, ITagService {
        public TagData TagGetByKey(string name) {
            using (IRepositoryContext context = IocLocator.Instance.GetImple<IRepositoryContext>()) {
                var tagRepository = context.GetRepository<Tag>();
                var tag = tagRepository.Find(Specification<Tag>.Eval(c => c.Name == name));
                if (tag == null)
                    return null;
                var tagData = Mapper.Map<Tag, TagData>(tag);
                return tagData;
            }
        }
        public IList<TagData> TagGet() {
            using (IRepositoryContext context = IocLocator.Instance.GetImple<IRepositoryContext>()) {
                var tagRepository = context.GetRepository<Tag>();
                var tags = tagRepository.FindAll();
                if (tags == null)
                    return null;
                var tagDatas = new List<TagData>();
                foreach (var user in tags) {
                    tagDatas.Add(Mapper.Map<Tag, TagData>(user));
                }
                return tagDatas;
            }
        }
        public TagData TagCreat(TagData dataObject) {
            if (dataObject == null)
                throw new ArgumentNullException("tagDataObject");
            using (IRepositoryContext context = IocLocator.Instance.GetImple<IRepositoryContext>()) {
                var tagRepository = context.GetRepository<Tag>();
                Tag tag = Mapper.Map<TagData, Tag>(dataObject);
                tagRepository.Add(tag);
                context.Commit();
                return Mapper.Map<Tag, TagData>(tag);
            }
        }
        public void TagUpdate(TagData dataObject) {
            if (string.IsNullOrEmpty(dataObject.ID))
                throw new ArgumentNullException("ID");
            Tag replyChild = Mapper.Map<TagData, Tag>(dataObject);
            using (IRepositoryContext context = IocLocator.Instance.GetImple<IRepositoryContext>()) {
                var tagRepository = context.GetRepository<Tag>();
                var upTag = tagRepository.Get(Specification<Tag>.Eval(c => c.ID.ToString() == dataObject.ID));
                if (!string.IsNullOrEmpty(dataObject.Name))
                    upTag.Name = dataObject.Name;
                if (dataObject.GroupEnum != 0)
                    upTag.GroupEnum = dataObject.GroupEnum;
                if (dataObject.ActEnum != 0)
                    upTag.ActEnum = dataObject.ActEnum;
                if (dataObject.IsNecessary)
                    upTag.IsNecessary = dataObject.IsNecessary;
                tagRepository.Update(upTag);
            }
        }
    }
}