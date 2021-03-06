﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bebeclick.WebClient
{
    /// <summary>
    /// This is the base of business objects that could be loaded, modified and persisted on a data store.
    /// </summary>
    /// <typeparam name="TYPE">The element type of the business object.</typeparam>
    /// <typeparam name="KEY">The element type of the business's Identifier.</typeparam>
    [DataContract]
    [Serializable]
    public abstract class BusinessBase<TYPE, KEY> : INotifyPropertyChanging, INotifyPropertyChanged, IChangeTracking, IDisposable
        where TYPE : BusinessBase<TYPE, KEY>, new()
    {
        private static PropertyChangingEventArgs emptyChangingEventArgs;
        protected object syncRoot;

        /// <summary>
        /// Unique identifier.
        /// </summary>
        protected KEY _id;

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessBase(KEY)"/> class.
        /// </summary>
        /// <param name="id">Unique identifier</param>
        protected BusinessBase(KEY id)
        {
            _id = id;
            OnCreated();
        }

        /// <summary>
        /// On deserialization neither constructors nor the field initializers are called.
        /// This method handle all of the field initialization in there and it's fired before deserialization.
        /// </summary>
        protected virtual void OnCreated()
        {
            emptyChangingEventArgs =  new PropertyChangingEventArgs(String.Empty);
             syncRoot = new object();
            _dateCreated = DateTime.MinValue;
            _changedProperties = new StringCollection();
            _brokenRules = new StringDictionary();
            _isNew = true;
            _isChanged = true;
        }

        #region Properties

        /// <summary>
        /// Gets or set's the instance's ID.
        /// </summary>
        [DataMember]
        public KEY ID
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _id; }
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                if (!this._id.Equals(value))
                {
                    this.OnPropertyChanging("ID");
                    this.MarkChanged("ID");
                }
                this._id = value;
            }
        }

        private DateTime _dateCreated;
        /// <summary>
        /// The date on which the instance was created.
        /// </summary>
        [DataMember]
        public virtual DateTime DateCreated
        {
            get
            {
                if (_dateCreated == DateTime.MinValue)
                    return _dateCreated;

                return _dateCreated;
            }
            set
            {
                if (_dateCreated != value)
                {
                    this.OnPropertyChanging("DateCreated");
                    MarkChanged("DateCreated");
                }
                _dateCreated = value;
            }
        }

        private string _createdBy;

        [DataMember]
        public string CreatedBy
        {
            get
            {
                return _createdBy;
            }
            set
            {
                if (_createdBy != value)
                {
                    this.OnPropertyChanging("CreatedBy");
                    MarkChanged("CreatedBy");
                }
                _createdBy = value;
            }
        }

        private DateTime? _lastUpdated;
        /// <summary>
        /// The date on which the instance was modified.
        /// </summary>
        [DataMember]
        public virtual DateTime? LastUpdated
        {
            get { return _lastUpdated; }
            set
            {
                if (_lastUpdated != value)
                {
                    this.OnPropertyChanging("LastUpdated");
                    MarkChanged("LastUpdated");
                }
                _lastUpdated = value;
            }
        }

        private string _lastUpdatedBy;

        [DataMember]
        public string LastUpdatedBy
        {
            get
            {
                return _lastUpdatedBy;
            }
            set
            {
                if (_lastUpdatedBy != value)
                {
                    this.OnPropertyChanging("LastUpdatedBy");
                    MarkChanged("LastUpdatedBy");
                }
                _lastUpdatedBy = value;
            }
        }

        #endregion

        #region IsNew, IsDeleted, IsChanged

        private bool _isNew;
        /// <summary>
        /// Gets if this is a new object, False if it is a pre-existing object.
        /// </summary>
        public bool IsNew
        {
            get { return _isNew; }
        }

        private bool _isDeleted;
        /// <summary>
        /// Gets if this object is marked for deletion.
        /// </summary>
        public bool IsDeleted
        {
            get { return _isDeleted; }
        }

        /// <summary>
        /// Marks the object for deletion. It will then be 
        /// deleted when the object's Save() method is called.
        /// </summary>
        public void Delete()
        {
            _isDeleted = true;
            _isChanged = true;
        }

        private StringCollection _changedProperties;
        /// <summary>
        /// A collection of the properties that have 
        /// been marked as being dirty.
        /// </summary>
        protected virtual StringCollection ChangedProperties
        {
            get { return _changedProperties; }
        }

        /// <summary>
        /// Marks an object as being dirty, or changed.
        /// </summary>
        /// <param name="propertyName">The name of the property to mark dirty.</param>
        protected virtual void MarkChanged(string propertyName)
        {
            _isChanged = true;

            if (!_changedProperties.Contains(propertyName))
                _changedProperties.Add(propertyName);

            OnPropertyChanged(propertyName);
        }

        /// <summary>
        /// Marks the object as being an clean, 
        /// which means not dirty.
        /// </summary>
        public virtual void MarkOld()
        {
            _isChanged = false;
            _isNew = false;
            _changedProperties.Clear();
        }

        #endregion

        /// <summary>
        /// Method calle during deserialization.
        /// </summary>
        /// <param name="c"></param>
        [OnDeserializing]
        private void OnDeserializing(StreamingContext c)
        {
            OnCreated();
        }

        #region Events

        /// <summary>
        /// Occurs when the class is Saved
        /// </summary>
        public event EventHandler<SavedEventArgs> Saved;
        /// <summary>
        /// Raises the Saved event.
        /// </summary>
        protected void OnSaved(BusinessBase<TYPE, KEY> businessObject, SaveAction action)
        {
            if (Saved != null)
                Saved(businessObject, new SavedEventArgs(action));
        }

        /// <summary>
        /// Occurs when the class is Saved
        /// </summary>
        public event EventHandler<SavedEventArgs> Saving;
        /// <summary>
        /// Raises the Saving event
        /// </summary>
        protected void OnSaving(BusinessBase<TYPE, KEY> businessObject, SaveAction action)
        {
            if (Saving != null)
                Saving(businessObject, new SavedEventArgs(action));
        }

        #endregion

        #region Validation

        private StringDictionary _brokenRules;

        /// <summary>
        /// Add or remove a broken rule.
        /// </summary>
        /// <param name="ruleName">The name of the rule.</param>
        /// <param name="errorMessage">The description of the error</param>
        /// <param name="isBroken">True if the validation rule is broken.</param>
        protected virtual void AddRule(string ruleName, string errorMessage, bool isBroken)
        {
            if (isBroken)
            {
                _brokenRules[ruleName] = errorMessage;
            }
            else
            {
                if (_brokenRules.ContainsKey(ruleName))
                    _brokenRules.Remove(ruleName);
            }
        }

        /// <summary>
        /// Reinforces the business rules by adding additional rules to the broken rules collection.
        /// </summary>
        protected abstract void ValidationRules();

        /// <summary>
        /// Gets whether the object is valid or not.
        /// </summary>
        public bool IsValid
        {
            get
            {
                ValidationRules();
                return this._brokenRules.Count == 0;
            }
        }

        /// /// <summary>
        /// If the object has broken business rules, use this property to get access
        /// to the different validation messages.
        /// </summary>
        public virtual string ValidationMessage
        {
            get
            {
                // We must assure not to call the function twice on a call to the save member function.
                if (this._brokenRules.Count > 0 || !IsValid)
                {
                    StringBuilder sb = new StringBuilder();

                    foreach (string messages in this._brokenRules.Values)
                        sb.AppendLine(messages);

                    return sb.ToString();
                }

                return string.Empty;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads an instance of the object based on the Id.
        /// </summary>
        /// <param name="id">The unique identifier of the object</param>
        public static TYPE Load(KEY id)
        {
            TYPE instance = new TYPE();

            instance = instance.DataSelect(id);

            if (instance != null)
            {
                instance.ID = id;
                instance.MarkOld();
                return instance;
            }

            return default(TYPE);
        }

        /// <summary>
        /// Saves the object to the data store (inserts, updates or deletes).
        /// </summary>
        virtual public SaveAction Save()
        {
            lock (syncRoot)
            {
                if (!IsValid && !IsDeleted)
                    throw new InvalidOperationException(ValidationMessage);

                if (IsDisposed && !IsDeleted)
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture,
                        "You cannot save a disposed {0}", this.GetType().Name));

                if (IsChanged)
                    return Update();

                return SaveAction.None;
            }
        }

        /// <summary>
        /// Is called by the save method when the object is old and dirty.
        /// </summary>
        private SaveAction Update()
        {
            SaveAction action = SaveAction.None;

            if (this.IsDeleted)
            {
                if (!this.IsNew)
                {
                    action = SaveAction.Delete;
                    OnSaving(this, action);
                    DataDelete();
                }
            }
            else
            {
                if (this.IsNew)
                {
                    if (this.DateCreated.Equals(DateTime.MinValue))
                        this.DateCreated = DateTime.UtcNow;

                    action = SaveAction.Insert;
                    OnSaving(this, action);
                    DataInsert();
                }
                else
                {
                    // This allows to save DateTime.UtcNow if required
                    if (!this.ChangedProperties.Contains("LastUpdated"))
                        this.LastUpdated = DateTime.UtcNow;

                    action = SaveAction.Update;
                    OnSaving(this, action);
                    DataUpdate();
                }

                MarkOld();
            }

            OnSaved(this, action);
            return action;
        }

        #endregion

        #region Data access

        /// <summary>
        /// Retrieves the object from the data store and populates it.
        /// </summary>
        /// <param name="id">The unique identifier of the object.</param>
        /// <returns>The objects' instance represented by ID.</returns>
        protected abstract TYPE DataSelect(KEY id);

        /// <summary>
        /// Updates the object in its data store.
        /// </summary>
        protected abstract void DataUpdate();

        /// <summary>
        /// Inserts a new object to the data store.
        /// </summary>
        protected abstract void DataInsert();

        /// <summary>
        /// Deletes the object from the data store.
        /// </summary>
        protected abstract void DataDelete();

        #endregion

        #region Equality overrides

        /// <summary>
        /// A uniquely key to identify this particullar instance of the class
        /// </summary>
        /// <returns>A unique integer value</returns>
        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>True if the specified objecdt is equal to the corrent objectd; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() == this.GetType())
                return obj.GetHashCode() == this.GetHashCode();

            return false;
        }

        /// <summary>
        /// Checks to see if two business objects are the same.
        /// </summary>
        public static bool operator ==(BusinessBase<TYPE, KEY> first, BusinessBase<TYPE, KEY> second)
        {
            if (Object.ReferenceEquals(first, second))
            {
                return true;
            }

            if ((object)first == null || (object)second == null)
            {
                return false;
            }

            return first.GetHashCode() == second.GetHashCode();
        }

        /// <summary>
        /// Checks to see if two business objects are different.
        /// </summary>
        public static bool operator !=(BusinessBase<TYPE, KEY> first, BusinessBase<TYPE, KEY> second)
        {
            return !(first == second);
        }

        #endregion

        #region INotifyPropertyChanging

        /// <summary>
        /// Occurs when a property value is changing.
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging;

        /// <summary>
        /// Raise the property changing event safely.
        /// </summary>
        /// <param name="propertyName">Property changing.</param>
        protected virtual void OnPropertyChanging(string propertyName)
        {
            if (this.PropertyChanging != null)
                this.PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
        }

        #endregion

        #region INotifyPropertyChanged

        /// <summary>
        /// Occurs when a property value changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event safely.
        /// </summary>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region IChangeTracking Members

        /// <summary>
        /// Resets the object’s state to unchanged by accepting the modifications.
        /// </summary>
        public void AcceptChanges()
        {
            Save();
        }

        private bool _isChanged;
        /// <summary>
        /// Gets if this object's data has been changed.
        /// </summary>
        public virtual bool IsChanged
        {
            get { return _isChanged; }
        }

        #endregion

        #region IDisposable Members

        private bool _IsDisposed;
        /// <summary>
        /// Gets or sets if the object has been disposed.
        /// <remarks>
        /// If the objects is disposed, it must not be disposed a second
        /// time. The IsDisposed property is set the first time the object
        /// is disposed. If the IsDisposed property is true, then the Dispose()
        /// method will not dispose again. This help not to prolong the object's
        /// life if the Garbage Collector.
        /// </remarks>
        /// </summary>
        protected bool IsDisposed
        {
            get { return _IsDisposed; }
        }

        /// <summary>
        /// Disposes the object and frees ressources for the Garbage Collector.
        /// </summary>
        /// <param name="disposing">If true, the object gets disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.IsDisposed)
                return;

            if (disposing)
            {
                _changedProperties.Clear();
                _brokenRules.Clear();
                _IsDisposed = true;
            }
        }

        /// <summary>
        /// Disposes the object and frees ressources for the Garbage Collector.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
