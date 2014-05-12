using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bebeclick.WebClient.Utilities;

namespace Bebeclick.WebClient.Tests.Portal
{
    /// <summary>
    /// Summary description for Service
    /// </summary>
    [TestClass]
    public class ServiceTest
    {
        public ServiceTest()
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
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
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
        public void GetAllServices()
        {
            var results = Bebeclick.WebClient.Service.GetAll();

            Assert.IsNotNull(results);

            if (results.Count() == 0)
                Assert.Inconclusive();

            Assert.IsTrue(results.Count() > 0);
        }

        [TestMethod]
        public void GetServicesByName()
        {
            var results = Bebeclick.WebClient.Service.GetAll();

            Assert.IsNotNull(results);

            if (results.Count() == 0)
            {
                Assert.Inconclusive("There are no services to load");
                return;
            }

            var item = results.FirstOrDefault();

            Assert.IsNotNull(item);

            // Exact search
            results = Bebeclick.WebClient.Service.GetServices(item.ProductID, item.Name);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count() > 0);

            item = results.FirstOrDefault();

            Assert.IsNotNull(item);

            Assert.IsTrue(!string.IsNullOrEmpty(item.Name));

            if (item.Name.Length < 2)
                Assert.Inconclusive();

            // keyword search
            results = Bebeclick.WebClient.Service.GetServices(item.ProductID, item.Name.Substring(0, item.Name.Length - 1));

            Assert.IsTrue(results.Count() > 0);

            item = results.Where(m => m.ID == item.ID).FirstOrDefault();

            Assert.IsNotNull(item);
        }

        [TestMethod]
        public void LoadService()
        {
            var services = Bebeclick.WebClient.Service.GetAll();

            if (services.Count() == 0)
            {
                Assert.Inconclusive("There are no services to load");
                return;
            }

            var item = services.FirstOrDefault();

            Assert.IsNotNull(item);

            var loadedById = Bebeclick.WebClient.Service.Load(item.ID);

            Compare(item, loadedById);

            var loadedByName = Bebeclick.WebClient.Service.GetServices(loadedById.ProductID, loadedById.Name).FirstOrDefault();

            Assert.IsNotNull(loadedByName);

            Compare(loadedByName, loadedById);
        }

        private void Compare(Service first, Service second)
        {
            if (first == null)
                throw new ArgumentNullException("first");

            if (second == null)
                throw new ArgumentNullException("second");

            Assert.AreEqual(first, second);
            Assert.AreEqual(first.ID, second.ID);
            Assert.AreEqual(first.ProductID, second.ProductID);
            Assert.AreEqual(first.Name, second.Name);
            Assert.AreEqual(first.Visible, second.Visible);
            Assert.AreEqual(first.DateCreated.Truncate(TimeSpan.FromMilliseconds(100)), second.DateCreated.Truncate(TimeSpan.FromMilliseconds(100)));
            Assert.AreEqual(first.CreatedBy, second.CreatedBy);
            Assert.AreEqual(first.LastUpdated.HasValue ? first.LastUpdated.Value.Truncate(TimeSpan.FromMilliseconds(100)) : default(DateTime?),
                second.LastUpdated.HasValue ? second.LastUpdated.Value.Truncate(TimeSpan.FromMilliseconds(100)) : default(DateTime?));
            Assert.AreEqual(first.LastUpdatedBy, second.LastUpdatedBy);
        }

        [TestMethod]
        public void CRUDService()
        {
            // Insert
            var service = new Service
            {
            };

            Assert.IsTrue(!service.IsValid
                && !string.IsNullOrEmpty(service.ValidationMessage)
                && service.ValidationMessage.Contains(ResourceStringLoader.GetResourceString("Service_EmptyName")));

            Assert.IsTrue(!service.IsValid
                && !string.IsNullOrEmpty(service.ValidationMessage)
                && service.ValidationMessage.Contains(ResourceStringLoader.GetResourceString("Service_EmptyCreatedBy")));

            Assert.IsTrue(!service.IsValid
                && !string.IsNullOrEmpty(service.ValidationMessage)
                && service.ValidationMessage.Contains(ResourceStringLoader.GetResourceString("Service_EmptyProductID")));

            service.Name = "01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890";
            service.CreatedBy = "01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456";

            var product = Product.GetAll().FirstOrDefault();

            if (product == null)
                Assert.Inconclusive();

            service.ProductID = product.ID;

            Assert.IsTrue(!service.IsValid
                && !string.IsNullOrEmpty(service.ValidationMessage)
                && service.ValidationMessage.Contains(ResourceStringLoader.GetResourceString("Service_MaxNameLength")));

            Assert.IsTrue(!service.IsValid
                && !string.IsNullOrEmpty(service.ValidationMessage)
                && service.ValidationMessage.Contains(ResourceStringLoader.GetResourceString("Service_MaxCreatedByLength")));

            service.Name = "New temporal service";
            service.CreatedBy = "dcruz";

            Assert.IsTrue(service.IsValid);
            Assert.IsTrue(service.IsNew);

            service.AcceptChanges();

            Assert.IsTrue(service.IsValid);
            Assert.IsTrue(!service.IsChanged);

            var loaded = Service.Load(service.ID);

            Assert.IsNotNull(loaded);
            Assert.IsTrue(!loaded.IsChanged);
            Assert.IsTrue(!loaded.IsNew);

            Compare(loaded, service);

            // Update

            loaded.Name = "01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890";

            Assert.IsTrue(!loaded.IsValid
                && !string.IsNullOrEmpty(loaded.ValidationMessage)
                && loaded.ValidationMessage.Contains(ResourceStringLoader.GetResourceString("Service_MaxNameLength")));

            Assert.IsTrue(!loaded.IsValid
                && !string.IsNullOrEmpty(loaded.ValidationMessage)
                && loaded.ValidationMessage.Contains(ResourceStringLoader.GetResourceString("Service_EmptyLastUpdatedBy")));

            var any = Service.GetServices(loaded.ProductID).Where(m => m.ID != loaded.ID).FirstOrDefault();

            if (any == null)
                Assert.Inconclusive();

            loaded.Name = any.Name;

            Assert.IsTrue(!loaded.IsValid
                && !string.IsNullOrEmpty(loaded.ValidationMessage)
                && loaded.ValidationMessage.Contains(ResourceStringLoader.GetResourceString("Service_DuplicatedName", new { loaded.Name })));

            Assert.IsTrue(!loaded.IsValid
                && !string.IsNullOrEmpty(loaded.ValidationMessage)
                && loaded.ValidationMessage.Contains(ResourceStringLoader.GetResourceString("Service_EmptyLastUpdatedBy")));

            loaded.Name = "New temporal state 2";
            loaded.LastUpdatedBy = "01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456";

            Assert.IsTrue(!loaded.IsValid
                && !string.IsNullOrEmpty(loaded.ValidationMessage)
                && loaded.ValidationMessage.Contains(ResourceStringLoader.GetResourceString("Service_MaxLastUpdatedByLength")));

            loaded.LastUpdatedBy = "dcruz";

            Assert.IsTrue(loaded.IsChanged);
            Assert.IsTrue(loaded.IsValid);

            loaded.AcceptChanges();

            Assert.IsTrue(!loaded.IsChanged);
            Assert.IsTrue(loaded.IsValid);

            var updated = Service.Load(service.ID);

            Assert.IsNotNull(updated);

            Compare(updated, loaded);

            // Delete

            loaded = Service.Load(service.ID);
            loaded.Delete();

            Assert.IsTrue(loaded.IsValid);
            Assert.IsTrue(loaded.IsDeleted);

            loaded.AcceptChanges();

            loaded = Service.Load(service.ID);

            Assert.IsNull(loaded);
        }
    }
}
