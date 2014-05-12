using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bebeclick.WebClient.Utilities;

namespace Bebeclick.WebClient.Tests.Portal
{
    /// <summary>
    /// Summary description for StateEntity
    /// </summary>
    [TestClass]
    public class StateEntityTest
    {
        public StateEntityTest()
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
        public void GetAllStates()
        {
            var results = Bebeclick.WebClient.StateEntity.GetAll();

            Assert.IsNotNull(results);

            if (results.Count() == 0)
                Assert.Inconclusive();

            Assert.IsTrue(results.Count() > 0);
        }

        [TestMethod]
        public void GetStateEntitiesByName()
        {
            var results = Bebeclick.WebClient.StateEntity.GetAll();

            Assert.IsNotNull(results);

            if (results.Count() == 0)
            {
                Assert.Inconclusive("There are no states to load");
                return;
            }

            var item = results.FirstOrDefault();

            Assert.IsNotNull(item);

            // Exact search
            results = Bebeclick.WebClient.StateEntity.GetStateEntities(item.Name);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count() > 0);

            item = results.FirstOrDefault();

            Assert.IsNotNull(item);

            Assert.IsTrue(!string.IsNullOrEmpty(item.Name));

            if (item.Name.Length < 2)
                Assert.Inconclusive();

            // keyword search
            results = Bebeclick.WebClient.StateEntity.GetStateEntities(item.Name.Substring(0, item.Name.Length - 1));

            Assert.IsTrue(results.Count() > 0);

            item = results.Where(m => m.ID == item.ID).FirstOrDefault();

            Assert.IsNotNull(item);
        }

        [TestMethod]
        public void LoadStateEntity()
        {
            var states = Bebeclick.WebClient.StateEntity.GetAll();

            if (states.Count() == 0)
            {
                Assert.Inconclusive("There are no states to load");
                return;
            }

            var item = states.FirstOrDefault();

            Assert.IsNotNull(item);

            var loadedById = Bebeclick.WebClient.StateEntity.Load(item.ID);

            Compare(item, loadedById);

            var loadedByName = Bebeclick.WebClient.StateEntity.GetStateEntities(loadedById.Name).FirstOrDefault();

            Assert.IsNotNull(loadedByName);

            Compare(loadedByName, loadedById);
        }

        private void Compare(StateEntity first, StateEntity second)
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
        public void CRUDStateEntity()
        {
            // Insert
            var state = new StateEntity
            {
            };

            Assert.IsTrue(!state.IsValid
                && !string.IsNullOrEmpty(state.ValidationMessage)
                && state.ValidationMessage.Contains(ResourceStringLoader.GetResourceString("StateEntity_EmptyName")));

            Assert.IsTrue(!state.IsValid
                && !string.IsNullOrEmpty(state.ValidationMessage)
                && state.ValidationMessage.Contains(ResourceStringLoader.GetResourceString("StateEntity_EmptyCreatedBy")));

            state.Name = "01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890";
            state.CreatedBy = "01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456";

            Assert.IsTrue(!state.IsValid
                && !string.IsNullOrEmpty(state.ValidationMessage)
                && state.ValidationMessage.Contains(ResourceStringLoader.GetResourceString("StateEntity_MaxNameLength")));

            Assert.IsTrue(!state.IsValid
                && !string.IsNullOrEmpty(state.ValidationMessage)
                && state.ValidationMessage.Contains(ResourceStringLoader.GetResourceString("StateEntity_MaxCreatedByLength")));

            state.Name = "New temporal state";
            state.CreatedBy = "dcruz";

            Assert.IsTrue(state.IsValid);
            Assert.IsTrue(state.IsNew);

            state.AcceptChanges();

            Assert.IsTrue(state.IsValid);
            Assert.IsTrue(!state.IsChanged);

            var loaded = StateEntity.Load(state.ID);

            Assert.IsNotNull(loaded);
            Assert.IsTrue(!loaded.IsChanged);
            Assert.IsTrue(!loaded.IsNew);

            Compare(loaded, state);

            // Update

            loaded.Name = "01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890";

            Assert.IsTrue(!loaded.IsValid
                && !string.IsNullOrEmpty(loaded.ValidationMessage)
                && loaded.ValidationMessage.Contains(ResourceStringLoader.GetResourceString("StateEntity_MaxNameLength")));

            Assert.IsTrue(!loaded.IsValid
                && !string.IsNullOrEmpty(loaded.ValidationMessage)
                && loaded.ValidationMessage.Contains(ResourceStringLoader.GetResourceString("StateEntity_EmptyLastUpdatedBy")));

            var any = StateEntity.GetAll().Where(m => m.ID != loaded.ID).FirstOrDefault();

            if (any == null)
                Assert.Inconclusive();

            loaded.Name = any.Name;

            Assert.IsTrue(!loaded.IsValid
                && !string.IsNullOrEmpty(loaded.ValidationMessage)
                && loaded.ValidationMessage.Contains(ResourceStringLoader.GetResourceString("StateEntity_DuplicatedName", new { loaded.Name })));

            Assert.IsTrue(!loaded.IsValid
                && !string.IsNullOrEmpty(loaded.ValidationMessage)
                && loaded.ValidationMessage.Contains(ResourceStringLoader.GetResourceString("StateEntity_EmptyLastUpdatedBy")));

            loaded.Name = "New temporal state 2";
            loaded.LastUpdatedBy = "01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456";

            Assert.IsTrue(!loaded.IsValid
                && !string.IsNullOrEmpty(loaded.ValidationMessage)
                && loaded.ValidationMessage.Contains(ResourceStringLoader.GetResourceString("StateEntity_MaxLastUpdatedByLength")));

            loaded.LastUpdatedBy = "dcruz";

            Assert.IsTrue(loaded.IsChanged);
            Assert.IsTrue(loaded.IsValid);

            loaded.AcceptChanges();

            Assert.IsTrue(!loaded.IsChanged);
            Assert.IsTrue(loaded.IsValid);

            var updated = StateEntity.Load(state.ID);

            Assert.IsNotNull(updated);

            Compare(updated, loaded);

            // Delete

            loaded = StateEntity.Load(state.ID);
            loaded.Delete();

            Assert.IsTrue(loaded.IsValid);
            Assert.IsTrue(loaded.IsDeleted);

            loaded.AcceptChanges();

            loaded = StateEntity.Load(state.ID);

            Assert.IsNull(loaded);
        }
    }
}
