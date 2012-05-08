using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using ByteartRetail.Domain.Repositories;
using ByteartRetail.Domain.Model;
using ByteartRetail.Domain.Specifications;

namespace ByteartRetail.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
         //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            ByteartRetailDbContextInitailizer.Initialize();
        }

        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestEFRepository()
        {
            Guid salesOrderID = Guid.Empty;
            using (RepositoryContextManager mgr = new RepositoryContextManager())
            {
                Customer cust = new Customer();
                cust.UserName = "daxnet";
                cust.Password = "123";
                cust.Contact = "daxnet";
                cust.Email = "daxnet@live.com";
                cust.Address = new Address("China", "Shanghai", "", "", "");

                SalesLine salesLine = new SalesLine();
                Laptop laptop = new Laptop();
                laptop.Name = "laptop";
                laptop.Brand = "HP";
                laptop.Description = "dv1606tn";
                laptop.Configuration = new LaptopConfiguration("AMD 1600", "Kingston 4GB x1", "Intel", "Samsung 320GB", "ATI Radeon 7500");
                salesLine.Laptop = laptop;
                salesLine.Quantity = 2;

                SalesOrder salesOrder = new SalesOrder();
                salesOrder.SalesLines.Add(salesLine);
                salesOrder.Customer = cust;
                salesOrder.DateCreated = DateTime.Now;

                salesLine.SalesOrder = salesOrder;

                IRepository<SalesOrder> salesOrderRepository = mgr.GetRepository<SalesOrder>();
                salesOrderRepository.Add(salesOrder);

                mgr.Commit();

                salesOrderID = salesOrder.ID;

                SalesOrder justAdded = salesOrderRepository.Get(Specification<SalesOrder>.Eval(s => s.ID == salesOrderID));
                justAdded.Customer.Contact = "sunny chen";
                salesOrderRepository.Update(justAdded);
                mgr.Commit();
            }
        }

        [TestMethod]
        public void TestTemp()
        {
            Guid salesOrderID = Guid.Empty;
            using (RepositoryContextManager mgr = new RepositoryContextManager())
            {
                Customer cust = new Customer();
                var custUserName = "daxnet" + Guid.NewGuid().ToString().Substring(0, 5);
                cust.UserName = custUserName;
                cust.Password = "123";
                cust.Contact = "daxnet";
                cust.Email = "daxnet234@live.com" + Guid.NewGuid().ToString().Substring(0, 5);
                cust.Address = new Address("China", "Shanghai", "", "", "");

                SalesLine salesLine = new SalesLine();
                Laptop laptop = new Laptop();
                laptop.Name = "laptopsdf" + Guid.NewGuid().ToString().Substring(0, 5);
                laptop.Brand = "HP";
                laptop.Description = "dv1606tn";
                laptop.Configuration = new LaptopConfiguration("AMD 1600", "Kingston 4GB x1", "Intel", "Samsung 320GB", "ATI Radeon 7500");
                salesLine.Laptop = laptop;
                salesLine.Quantity = 2;

                SalesOrder salesOrder = new SalesOrder();
                salesOrder.SalesLines.Add(salesLine);
                salesOrder.Customer = cust;
                salesOrder.DateCreated = DateTime.Now;

                salesLine.SalesOrder = salesOrder;

                IRepository<SalesOrder> salesOrderRepository = mgr.GetRepository<SalesOrder>();
                salesOrderRepository.Add(salesOrder);

                mgr.Commit();

                Guid customerID = cust.ID;
                var salesOrder2 = salesOrderRepository.Get(Specification<SalesOrder>.Eval(p => p.Customer.UserName == custUserName));

            }
        }

        [TestMethod]
        public void TestMultiple()
        {
            Guid salesOrderID = Guid.Empty;
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    using (RepositoryContextManager mgr = new RepositoryContextManager())
                    {
                        Customer cust = new Customer();
                        cust.UserName = "daxnet";
                        cust.Password = "123";
                        cust.Contact = "daxnet";
                        cust.Email = "daxnet@live.com";
                        cust.Address = new Address("China", "Shanghai", "", "", "");
                        IRepository<Customer> rep = mgr.GetRepository<Customer>();
                        rep.Add(cust);
                        mgr.Commit();
                    }
                }
                catch
                {
                    continue;
                }
            }
        }
    }
}
