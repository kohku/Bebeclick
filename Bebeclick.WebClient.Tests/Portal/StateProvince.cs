using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bebeclick.WebClient.Tests.Portal
{
    /// <summary>
    /// Summary description for StateProvince
    /// </summary>
    [TestClass]
    public class StateProvince
    {
        public StateProvince()
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
        public void GetAll()
        {
            var results = Bebeclick.WebClient.StateProvince.GetAll();

            Assert.IsNotNull(results);

            if (results == null)
                return;

            Assert.IsTrue(results.Count() >= 0);
        }

        [TestMethod]
        public void LoadStateProvince()
        {
            var provinces = Bebeclick.WebClient.StateProvince.GetAll();

            if (provinces.Count() == 0)
            {
                Assert.Inconclusive("There are no provinces to load");
                return;
            }

            var item = provinces.First();

            Assert.IsNotNull(item);

            if (item == null)
                return;

            var loadedById = Bebeclick.WebClient.StateProvince.Load(item.ID);

            Assert.AreEqual(item, loadedById);
            Assert.AreEqual(item.ID, loadedById.ID);
            Assert.AreEqual(item.Name, loadedById.Name);
            Assert.AreEqual(item.Visible, loadedById.Visible);
            Assert.AreEqual(item.DateCreated, loadedById.DateCreated);
            Assert.AreEqual(item.CreatedBy, loadedById.CreatedBy);
            Assert.AreEqual(item.LastUpdated, loadedById.LastUpdated);
            Assert.AreEqual(item.LastUpdatedBy, loadedById.LastUpdatedBy);

            var loadedByName = Bebeclick.WebClient.StateProvince.GetStateProvinces(loadedById.Name).FirstOrDefault();

            Assert.IsNotNull(loadedByName);

            if (loadedByName == null)
                return;

            Assert.AreEqual(loadedByName, loadedById);
            Assert.AreEqual(loadedByName.ID, loadedById.ID);
            Assert.AreEqual(loadedByName.Name, loadedById.Name);
            Assert.AreEqual(loadedByName.Visible, loadedById.Visible);
            Assert.AreEqual(loadedByName.DateCreated, loadedById.DateCreated);
            Assert.AreEqual(loadedByName.CreatedBy, loadedById.CreatedBy);
            Assert.AreEqual(loadedByName.LastUpdated, loadedById.LastUpdated);
            Assert.AreEqual(loadedByName.LastUpdatedBy, loadedById.LastUpdatedBy);
        }
    }
}
