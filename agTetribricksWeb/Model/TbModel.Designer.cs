﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

[assembly: EdmSchemaAttribute()]
namespace agTetribricksWeb.Model
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class tbscoresEntities : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new tbscoresEntities object using the connection string found in the 'tbscoresEntities' section of the application configuration file.
        /// </summary>
        public tbscoresEntities() : base("name=tbscoresEntities", "tbscoresEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new tbscoresEntities object.
        /// </summary>
        public tbscoresEntities(string connectionString) : base(connectionString, "tbscoresEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new tbscoresEntities object.
        /// </summary>
        public tbscoresEntities(EntityConnection connection) : base(connection, "tbscoresEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<TbScore> TbScores
        {
            get
            {
                if ((_TbScores == null))
                {
                    _TbScores = base.CreateObjectSet<TbScore>("TbScores");
                }
                return _TbScores;
            }
        }
        private ObjectSet<TbScore> _TbScores;

        #endregion

        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the TbScores EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToTbScores(TbScore tbScore)
        {
            base.AddObject("TbScores", tbScore);
        }

        #endregion

    }

    #endregion

    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="tbscoresModel", Name="TbScore")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class TbScore : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new TbScore object.
        /// </summary>
        /// <param name="id">Initial value of the ID property.</param>
        public static TbScore CreateTbScore(global::System.Guid id)
        {
            TbScore tbScore = new TbScore();
            tbScore.ID = id;
            return tbScore;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String EmailAddress
        {
            get
            {
                return _EmailAddress;
            }
            set
            {
                OnEmailAddressChanging(value);
                ReportPropertyChanging("EmailAddress");
                _EmailAddress = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("EmailAddress");
                OnEmailAddressChanged();
            }
        }
        private global::System.String _EmailAddress;
        partial void OnEmailAddressChanging(global::System.String value);
        partial void OnEmailAddressChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> ScoreDate
        {
            get
            {
                return _ScoreDate;
            }
            set
            {
                OnScoreDateChanging(value);
                ReportPropertyChanging("ScoreDate");
                _ScoreDate = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("ScoreDate");
                OnScoreDateChanged();
            }
        }
        private Nullable<global::System.DateTime> _ScoreDate;
        partial void OnScoreDateChanging(Nullable<global::System.DateTime> value);
        partial void OnScoreDateChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> ScoreValue
        {
            get
            {
                return _ScoreValue;
            }
            set
            {
                OnScoreValueChanging(value);
                ReportPropertyChanging("ScoreValue");
                _ScoreValue = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("ScoreValue");
                OnScoreValueChanged();
            }
        }
        private Nullable<global::System.Int32> _ScoreValue;
        partial void OnScoreValueChanging(Nullable<global::System.Int32> value);
        partial void OnScoreValueChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                OnUserNameChanging(value);
                ReportPropertyChanging("UserName");
                _UserName = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("UserName");
                OnUserNameChanged();
            }
        }
        private global::System.String _UserName;
        partial void OnUserNameChanging(global::System.String value);
        partial void OnUserNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Guid ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (_ID != value)
                {
                    OnIDChanging(value);
                    ReportPropertyChanging("ID");
                    _ID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("ID");
                    OnIDChanged();
                }
            }
        }
        private global::System.Guid _ID;
        partial void OnIDChanging(global::System.Guid value);
        partial void OnIDChanged();

        #endregion

    
    }

    #endregion

    
}
