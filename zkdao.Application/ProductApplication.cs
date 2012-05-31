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

    public class ProductApplication : BaseApplication, IProductService {

        public ProductData ProductGetByID(Guid ID) {
            using (IRepositoryContext context = IocLocator.Instance.GetImple<IRepositoryContext>()) {
                var productRepository = context.GetRepository<Product>();
                var product = productRepository.GetByKey(ID);
                if (product == null)
                    return null;
                var productData = Mapper.Map<Product, ProductData>(product);
                return productData;
            }
        }

        public Pager<ProductData> ProductGetPager(int pageIndex, int pageSize) {
            using (IRepositoryContext context = IocLocator.Instance.GetImple<IRepositoryContext>()) {
                var productRepository = context.GetRepository<Product>();
                var products = productRepository.FindAll(pageIndex, pageSize);
                if (products == null)
                    return null;
                var productDatas = new List<ProductData>();
                foreach (var product in products) {
                    productDatas.Add(Mapper.Map<Product, ProductData>(product));
                }
                Pager<ProductData> pager = new Pager<ProductData>();
                pager.Count = productDatas.Count;
                pager.Result = productDatas;
                return pager;
            }
        }

        public ProductData ProductSubmit(ProductData dataObject, bool force) {
            if (dataObject == null)
                throw new ArgumentNullException("productDataObject");
            using (IRepositoryContext context = IocLocator.Instance.GetImple<IRepositoryContext>()) {
                var productRepository = context.GetRepository<Product>();
                Product product = Mapper.Map<ProductData, Product>(dataObject);
                product.ActEnum = force ? (int)eAct.Normal : (int)eAct.unApproved;
                productRepository.Add(product);
                context.Commit();
                return Mapper.Map<Product, ProductData>(product);
            }
        }

        public void ProductApproved(Guid ID) {
            using (IRepositoryContext context = IocLocator.Instance.GetImple<IRepositoryContext>()) {
                var productRepository = context.GetRepository<Product>();
                var product = productRepository.GetByKey(ID);
                if (product == null)
                    throw new ArgumentNullException("productDataID");
                if (product.Act == eAct.unApproved) {
                    product.ActEnum = (int)eAct.Normal;
                    productRepository.Update(product);
                } else {
                    throw new Exception("ProductIsNotUnApproved");
                }
            }
        }

        public void ProductUpdate(ProductData dataObject) {
            if (string.IsNullOrEmpty(dataObject.ID))
                throw new ArgumentNullException("ID");
            Product product = Mapper.Map<ProductData, Product>(dataObject);
            using (IRepositoryContext context = IocLocator.Instance.GetImple<IRepositoryContext>()) {
                var productRepository = context.GetRepository<Product>();
                var upInfo = productRepository.Get(Specification<Product>.Eval(c => c.ID.ToString() == dataObject.ID));
                if (!string.IsNullOrEmpty(dataObject.Name))
                    upInfo.Name = dataObject.Name;
                if (!string.IsNullOrEmpty(dataObject.Link))
                    upInfo.Link = dataObject.Link;
                if (!string.IsNullOrEmpty(dataObject.PriceHistoryLink))
                    upInfo.PriceHistoryLink = dataObject.PriceHistoryLink;
                if (!string.IsNullOrEmpty(dataObject.PriceHistory))
                    upInfo.PriceHistory = dataObject.PriceHistory;
                if (!string.IsNullOrEmpty(dataObject.Image))
                    upInfo.Image = dataObject.Image;
                if (!string.IsNullOrEmpty(dataObject.EagerHistory))
                    upInfo.EagerHistory = dataObject.EagerHistory;
                if (!string.IsNullOrEmpty(dataObject.GradeHistory))
                    upInfo.GradeHistory = dataObject.GradeHistory;
                if (dataObject.EagerAmount != 0)
                    upInfo.EagerAmount = dataObject.EagerAmount;
                if (dataObject.GradeAverage != 0)
                    upInfo.GradeAverage = dataObject.GradeAverage;
                if (dataObject.ActEnum != 0)
                    upInfo.ActEnum = dataObject.ActEnum;
                productRepository.Update(upInfo);
            }
        }
    }
}