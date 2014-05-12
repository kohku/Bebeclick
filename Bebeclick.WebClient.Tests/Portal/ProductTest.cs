using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bebeclick.WebClient.Utilities;

namespace Bebeclick.WebClient.Tests.Portal
{
    /// <summary>
    /// Summary description for Product
    /// </summary>
    [TestClass]
    public class ProductTest
    {
        public ProductTest()
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
        public void GetAllProducts()
        {
            var results = Bebeclick.WebClient.Product.GetAll();

            Assert.IsNotNull(results);

            if (results.Count() == 0)
                Assert.Inconclusive();

            Assert.IsTrue(results.Count() > 0);
        }

        [TestMethod]
        public void GetProductsByName()
        {
            var results = Bebeclick.WebClient.Product.GetAll();

            Assert.IsNotNull(results);

            if (results.Count() == 0)
            {
                Assert.Inconclusive("There are no products to load");
                return;
            }

            var item = results.FirstOrDefault();

            Assert.IsNotNull(item);

            // Exact search
            results = Bebeclick.WebClient.Product.GetProducts(item.Name);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count() > 0);

            item = results.FirstOrDefault();

            Assert.IsNotNull(item);

            Assert.IsTrue(!string.IsNullOrEmpty(item.Name));

            if (item.Name.Length < 2)
                Assert.Inconclusive();

            // keyword search
            results = Bebeclick.WebClient.Product.GetProducts(item.Name.Substring(0, item.Name.Length - 1));

            Assert.IsTrue(results.Count() > 0);

            item = results.Where(m => m.ID == item.ID).FirstOrDefault();

            Assert.IsNotNull(item);
        }

        [TestMethod]
        public void LoadProduct()
        {
            var products = Bebeclick.WebClient.Product.GetAll();

            if (products.Count() == 0)
            {
                Assert.Inconclusive("There are no products to load");
                return;
            }

            var item = products.FirstOrDefault();

            Assert.IsNotNull(item);

            var loadedById = Bebeclick.WebClient.Product.Load(item.ID);

            Compare(item, loadedById);

            var loadedByName = Bebeclick.WebClient.Product.GetProducts(loadedById.Name).FirstOrDefault();

            Assert.IsNotNull(loadedByName);

            Compare(loadedByName, loadedById);
        }

        private void Compare(Product first, Product second)
        {
            if (first == null)
                throw new ArgumentNullException("first");

            if (second == null)
                throw new ArgumentNullException("second");

            Assert.AreEqual(first, second);
            Assert.AreEqual(first.ID, second.ID);
            Assert.AreEqual(first.Name, second.Name);
            Assert.AreEqual(first.Visible, second.Visible);
            Assert.AreEqual(first.DateCreated.Truncate(TimeSpan.FromMilliseconds(100)), second.DateCreated.Truncate(TimeSpan.FromMilliseconds(100)));
            Assert.AreEqual(first.CreatedBy, second.CreatedBy);
            Assert.AreEqual(first.LastUpdated.HasValue ? first.LastUpdated.Value.Truncate(TimeSpan.FromMilliseconds(100)) : default(DateTime?),
                second.LastUpdated.HasValue ? second.LastUpdated.Value.Truncate(TimeSpan.FromMilliseconds(100)) : default(DateTime?));
            Assert.AreEqual(first.LastUpdatedBy, second.LastUpdatedBy);
        }

        [TestMethod]
        public void CRUDProduct()
        {
            // Insert
            var product = new Product
            {
            };

            Assert.IsTrue(!product.IsValid
                && !string.IsNullOrEmpty(product.ValidationMessage)
                && product.ValidationMessage.Contains(ResourceStringLoader.GetResourceString("Product_EmptyName")));

            Assert.IsTrue(!product.IsValid
                && !string.IsNullOrEmpty(product.ValidationMessage)
                && product.ValidationMessage.Contains(ResourceStringLoader.GetResourceString("Product_EmptyCreatedBy")));

            product.Name = "01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890";
            product.CreatedBy = "01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456";

            Assert.IsTrue(!product.IsValid
                && !string.IsNullOrEmpty(product.ValidationMessage)
                && product.ValidationMessage.Contains(ResourceStringLoader.GetResourceString("Product_MaxNameLength")));

            Assert.IsTrue(!product.IsValid
                && !string.IsNullOrEmpty(product.ValidationMessage)
                && product.ValidationMessage.Contains(ResourceStringLoader.GetResourceString("Product_MaxCreatedByLength")));

            product.Name = "New temporal product";
            product.CreatedBy = "dcruz";

            Assert.IsTrue(product.IsValid);
            Assert.IsTrue(product.IsNew);

            product.AcceptChanges();

            Assert.IsTrue(product.IsValid);
            Assert.IsTrue(!product.IsChanged);

            var loaded = Product.Load(product.ID);

            Assert.IsNotNull(loaded);
            Assert.IsTrue(!loaded.IsChanged);
            Assert.IsTrue(!loaded.IsNew);

            Compare(loaded, product);

            // Update

            loaded.Name = "01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890";

            Assert.IsTrue(!loaded.IsValid
                && !string.IsNullOrEmpty(loaded.ValidationMessage)
                && loaded.ValidationMessage.Contains(ResourceStringLoader.GetResourceString("Product_MaxNameLength")));

            Assert.IsTrue(!loaded.IsValid
                && !string.IsNullOrEmpty(loaded.ValidationMessage)
                && loaded.ValidationMessage.Contains(ResourceStringLoader.GetResourceString("Product_EmptyLastUpdatedBy")));

            var any = Product.GetAll().Where(m => m.ID != loaded.ID).FirstOrDefault();

            if (any == null)
                Assert.Inconclusive();

            loaded.Name = any.Name;

            Assert.IsTrue(!loaded.IsValid
                && !string.IsNullOrEmpty(loaded.ValidationMessage)
                && loaded.ValidationMessage.Contains(ResourceStringLoader.GetResourceString("Product_DuplicatedName", new { loaded.Name })));

            Assert.IsTrue(!loaded.IsValid
                && !string.IsNullOrEmpty(loaded.ValidationMessage)
                && loaded.ValidationMessage.Contains(ResourceStringLoader.GetResourceString("Product_EmptyLastUpdatedBy")));

            loaded.Name = "New temporal product 2";
            loaded.LastUpdatedBy = "01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456";

            Assert.IsTrue(!loaded.IsValid
                && !string.IsNullOrEmpty(loaded.ValidationMessage)
                && loaded.ValidationMessage.Contains(ResourceStringLoader.GetResourceString("Product_MaxLastUpdatedByLength")));

            loaded.LastUpdatedBy = "dcruz";

            Assert.IsTrue(loaded.IsChanged);
            Assert.IsTrue(loaded.IsValid);

            loaded.AcceptChanges();

            Assert.IsTrue(!loaded.IsChanged);
            Assert.IsTrue(loaded.IsValid);

            var updated = Product.Load(product.ID);

            Assert.IsNotNull(updated);

            Compare(updated, loaded);

            // Delete

            loaded = Product.Load(product.ID);
            loaded.Delete();

            Assert.IsTrue(loaded.IsValid);
            Assert.IsTrue(loaded.IsDeleted);

            loaded.AcceptChanges();

            loaded = Product.Load(product.ID);

            Assert.IsNull(loaded);
        }
    }
}
