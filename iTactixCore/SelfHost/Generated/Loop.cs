
/*
===============================================================================
                    EntitySpaces 2011 by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2011.1.0530.0
EntitySpaces Driver  : VistaDB4
Date Generated       : 2011/09/01 11:04:45 AM
===============================================================================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;
using System.Data;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;

using EntitySpaces.Core;
using EntitySpaces.Interfaces;
using EntitySpaces.DynamicQuery;



namespace BusinessObjects
{
	/// <summary>
	/// Encapsulates the 'Loop' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[XmlType("Loop")]
	public partial class Loop : esLoop
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new Loop();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Int64 id)
		{
			var obj = new Loop();
			obj.Id = id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Int64 id, esSqlAccessType sqlAccessType)
		{
			var obj = new Loop();
			obj.Id = id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save(sqlAccessType);
		}
		#endregion

		
					
		
	
	}



    [DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[CollectionDataContract]
	[XmlType("LoopCollection")]
	public partial class LoopCollection : esLoopCollection, IEnumerable<Loop>
	{
		public Loop FindByPrimaryKey(System.Int64 id)
		{
			return this.SingleOrDefault(e => e.Id == id);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(Loop))]
		public class LoopCollectionWCFPacket : esCollectionWCFPacket<LoopCollection>
		{
			public static implicit operator LoopCollection(LoopCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator LoopCollectionWCFPacket(LoopCollection collection)
			{
				return new LoopCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class LoopQuery : esLoopQuery
	{
		public LoopQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "LoopQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(LoopQuery query)
		{
			return LoopQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator LoopQuery(string query)
		{
			return (LoopQuery)LoopQuery.SerializeHelper.FromXml(query, typeof(LoopQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esLoop : esEntity
	{
		public esLoop()
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int64 id)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(id);
			else
				return LoadByPrimaryKeyStoredProcedure(id);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int64 id)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(id);
			else
				return LoadByPrimaryKeyStoredProcedure(id);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int64 id)
		{
			LoopQuery query = new LoopQuery();
			query.Where(query.Id == id);
			return this.Load(query);
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int64 id)
		{
			esParameters parms = new esParameters();
			parms.Add("Id", id);
			return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
		}
		#endregion
		
		#region Properties
		
		
		
		/// <summary>
		/// Maps to Loop.id
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int64? Id
		{
			get
			{
				return base.GetSystemInt64(LoopMetadata.ColumnNames.Id);
			}
			
			set
			{
				if(base.SetSystemInt64(LoopMetadata.ColumnNames.Id, value))
				{
					OnPropertyChanged(LoopMetadata.PropertyNames.Id);
				}
			}
		}		
		
		/// <summary>
		/// Maps to Loop.name
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Name
		{
			get
			{
				return base.GetSystemString(LoopMetadata.ColumnNames.Name);
			}
			
			set
			{
				if(base.SetSystemString(LoopMetadata.ColumnNames.Name, value))
				{
					OnPropertyChanged(LoopMetadata.PropertyNames.Name);
				}
			}
		}		
		
		/// <summary>
		/// Maps to Loop.description
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Description
		{
			get
			{
				return base.GetSystemString(LoopMetadata.ColumnNames.Description);
			}
			
			set
			{
				if(base.SetSystemString(LoopMetadata.ColumnNames.Description, value))
				{
					OnPropertyChanged(LoopMetadata.PropertyNames.Description);
				}
			}
		}		
		
		/// <summary>
		/// Maps to Loop.createdon
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.DateTime? Createdon
		{
			get
			{
				return base.GetSystemDateTime(LoopMetadata.ColumnNames.Createdon);
			}
			
			set
			{
				if(base.SetSystemDateTime(LoopMetadata.ColumnNames.Createdon, value))
				{
					OnPropertyChanged(LoopMetadata.PropertyNames.Createdon);
				}
			}
		}		
		
		/// <summary>
		/// Maps to Loop.createdby
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Createdby
		{
			get
			{
				return base.GetSystemString(LoopMetadata.ColumnNames.Createdby);
			}
			
			set
			{
				if(base.SetSystemString(LoopMetadata.ColumnNames.Createdby, value))
				{
					OnPropertyChanged(LoopMetadata.PropertyNames.Createdby);
				}
			}
		}		
		
		/// <summary>
		/// Maps to Loop.expiry
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.DateTime? Expiry
		{
			get
			{
				return base.GetSystemDateTime(LoopMetadata.ColumnNames.Expiry);
			}
			
			set
			{
				if(base.SetSystemDateTime(LoopMetadata.ColumnNames.Expiry, value))
				{
					OnPropertyChanged(LoopMetadata.PropertyNames.Expiry);
				}
			}
		}		
		
		/// <summary>
		/// Maps to Loop.active
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Boolean? Active
		{
			get
			{
				return base.GetSystemBoolean(LoopMetadata.ColumnNames.Active);
			}
			
			set
			{
				if(base.SetSystemBoolean(LoopMetadata.ColumnNames.Active, value))
				{
					OnPropertyChanged(LoopMetadata.PropertyNames.Active);
				}
			}
		}		
		
		/// <summary>
		/// Maps to Loop.templateid
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int64? Templateid
		{
			get
			{
				return base.GetSystemInt64(LoopMetadata.ColumnNames.Templateid);
			}
			
			set
			{
				if(base.SetSystemInt64(LoopMetadata.ColumnNames.Templateid, value))
				{
					OnPropertyChanged(LoopMetadata.PropertyNames.Templateid);
				}
			}
		}		
		
		/// <summary>
		/// Maps to Loop.templatename
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Templatename
		{
			get
			{
				return base.GetSystemString(LoopMetadata.ColumnNames.Templatename);
			}
			
			set
			{
				if(base.SetSystemString(LoopMetadata.ColumnNames.Templatename, value))
				{
					OnPropertyChanged(LoopMetadata.PropertyNames.Templatename);
				}
			}
		}		
		
		#endregion	

		#region .str() Properties
		
		public override void SetProperties(IDictionary values)
		{
			foreach (string propertyName in values.Keys)
			{
				this.SetProperty(propertyName, values[propertyName]);
			}
		}
		
		public override void SetProperty(string name, object value)
		{
			esColumnMetadata col = this.Meta.Columns.FindByPropertyName(name);
			if (col != null)
			{
				if(value == null || value is System.String)
				{				
					// Use the strongly typed property
					switch (name)
					{							
						case "Id": this.str().Id = (string)value; break;							
						case "Name": this.str().Name = (string)value; break;							
						case "Description": this.str().Description = (string)value; break;							
						case "Createdon": this.str().Createdon = (string)value; break;							
						case "Createdby": this.str().Createdby = (string)value; break;							
						case "Expiry": this.str().Expiry = (string)value; break;							
						case "Active": this.str().Active = (string)value; break;							
						case "Templateid": this.str().Templateid = (string)value; break;							
						case "Templatename": this.str().Templatename = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Id":
						
							if (value == null || value is System.Int64)
								this.Id = (System.Int64?)value;
								OnPropertyChanged(LoopMetadata.PropertyNames.Id);
							break;
						
						case "Createdon":
						
							if (value == null || value is System.DateTime)
								this.Createdon = (System.DateTime?)value;
								OnPropertyChanged(LoopMetadata.PropertyNames.Createdon);
							break;
						
						case "Expiry":
						
							if (value == null || value is System.DateTime)
								this.Expiry = (System.DateTime?)value;
								OnPropertyChanged(LoopMetadata.PropertyNames.Expiry);
							break;
						
						case "Active":
						
							if (value == null || value is System.Boolean)
								this.Active = (System.Boolean?)value;
								OnPropertyChanged(LoopMetadata.PropertyNames.Active);
							break;
						
						case "Templateid":
						
							if (value == null || value is System.Int64)
								this.Templateid = (System.Int64?)value;
								OnPropertyChanged(LoopMetadata.PropertyNames.Templateid);
							break;
					

						default:
							break;
					}
				}
			}
            else if (this.ContainsColumn(name))
            {
                this.SetColumn(name, value);
            }
			else
			{
				throw new Exception("SetProperty Error: '" + name + "' not found");
			}
		}		

		public esStrings str()
		{
			if (esstrings == null)
			{
				esstrings = new esStrings(this);
			}
			return esstrings;
		}

		sealed public class esStrings
		{
			public esStrings(esLoop entity)
			{
				this.entity = entity;
			}
			
	
			public System.String Id
			{
				get
				{
					System.Int64? data = entity.Id;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Id = null;
					else entity.Id = Convert.ToInt64(value);
				}
			}
				
			public System.String Name
			{
				get
				{
					System.String data = entity.Name;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Name = null;
					else entity.Name = Convert.ToString(value);
				}
			}
				
			public System.String Description
			{
				get
				{
					System.String data = entity.Description;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Description = null;
					else entity.Description = Convert.ToString(value);
				}
			}
				
			public System.String Createdon
			{
				get
				{
					System.DateTime? data = entity.Createdon;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Createdon = null;
					else entity.Createdon = Convert.ToDateTime(value);
				}
			}
				
			public System.String Createdby
			{
				get
				{
					System.String data = entity.Createdby;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Createdby = null;
					else entity.Createdby = Convert.ToString(value);
				}
			}
				
			public System.String Expiry
			{
				get
				{
					System.DateTime? data = entity.Expiry;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Expiry = null;
					else entity.Expiry = Convert.ToDateTime(value);
				}
			}
				
			public System.String Active
			{
				get
				{
					System.Boolean? data = entity.Active;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Active = null;
					else entity.Active = Convert.ToBoolean(value);
				}
			}
				
			public System.String Templateid
			{
				get
				{
					System.Int64? data = entity.Templateid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Templateid = null;
					else entity.Templateid = Convert.ToInt64(value);
				}
			}
				
			public System.String Templatename
			{
				get
				{
					System.String data = entity.Templatename;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Templatename = null;
					else entity.Templatename = Convert.ToString(value);
				}
			}
			

			private esLoop entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return LoopMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public LoopQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LoopQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(LoopQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(LoopQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private LoopQuery query;		
	}



	[Serializable]
	abstract public partial class esLoopCollection : esEntityCollection<Loop>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return LoopMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "LoopCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public LoopQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LoopQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(LoopQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LoopQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(LoopQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((LoopQuery)query);
		}

		#endregion
		
		private LoopQuery query;
	}



	[Serializable]
	abstract public partial class esLoopQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return LoopMetadata.Meta();
			}
		}	
		
		#region esQueryItems

		public esQueryItem Id
		{
			get { return new esQueryItem(this, LoopMetadata.ColumnNames.Id, esSystemType.Int64); }
		} 
		
		public esQueryItem Name
		{
			get { return new esQueryItem(this, LoopMetadata.ColumnNames.Name, esSystemType.String); }
		} 
		
		public esQueryItem Description
		{
			get { return new esQueryItem(this, LoopMetadata.ColumnNames.Description, esSystemType.String); }
		} 
		
		public esQueryItem Createdon
		{
			get { return new esQueryItem(this, LoopMetadata.ColumnNames.Createdon, esSystemType.DateTime); }
		} 
		
		public esQueryItem Createdby
		{
			get { return new esQueryItem(this, LoopMetadata.ColumnNames.Createdby, esSystemType.String); }
		} 
		
		public esQueryItem Expiry
		{
			get { return new esQueryItem(this, LoopMetadata.ColumnNames.Expiry, esSystemType.DateTime); }
		} 
		
		public esQueryItem Active
		{
			get { return new esQueryItem(this, LoopMetadata.ColumnNames.Active, esSystemType.Boolean); }
		} 
		
		public esQueryItem Templateid
		{
			get { return new esQueryItem(this, LoopMetadata.ColumnNames.Templateid, esSystemType.Int64); }
		} 
		
		public esQueryItem Templatename
		{
			get { return new esQueryItem(this, LoopMetadata.ColumnNames.Templatename, esSystemType.String); }
		} 
		
		#endregion
		
	}


	
	public partial class Loop : esLoop
	{

		
		
	}
	



	[Serializable]
	public partial class LoopMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected LoopMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(LoopMetadata.ColumnNames.Id, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = LoopMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(LoopMetadata.ColumnNames.Name, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = LoopMetadata.PropertyNames.Name;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(LoopMetadata.ColumnNames.Description, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = LoopMetadata.PropertyNames.Description;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(LoopMetadata.ColumnNames.Createdon, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LoopMetadata.PropertyNames.Createdon;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(LoopMetadata.ColumnNames.Createdby, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = LoopMetadata.PropertyNames.Createdby;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(LoopMetadata.ColumnNames.Expiry, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LoopMetadata.PropertyNames.Expiry;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(LoopMetadata.ColumnNames.Active, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = LoopMetadata.PropertyNames.Active;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(LoopMetadata.ColumnNames.Templateid, 7, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = LoopMetadata.PropertyNames.Templateid;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(LoopMetadata.ColumnNames.Templatename, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = LoopMetadata.PropertyNames.Templatename;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public LoopMetadata Meta()
		{
			return meta;
		}	
		
		public Guid DataID
		{
			get { return base.m_dataID; }
		}	
		
		public bool MultiProviderMode
		{
			get { return false; }
		}		

		public esColumnMetadataCollection Columns
		{
			get	{ return base.m_columns; }
		}
		
		#region ColumnNames
		public class ColumnNames
		{ 
			 public const string Id = "id";
			 public const string Name = "name";
			 public const string Description = "description";
			 public const string Createdon = "createdon";
			 public const string Createdby = "createdby";
			 public const string Expiry = "expiry";
			 public const string Active = "active";
			 public const string Templateid = "templateid";
			 public const string Templatename = "templatename";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string Name = "Name";
			 public const string Description = "Description";
			 public const string Createdon = "Createdon";
			 public const string Createdby = "Createdby";
			 public const string Expiry = "Expiry";
			 public const string Active = "Active";
			 public const string Templateid = "Templateid";
			 public const string Templatename = "Templatename";
		}
		#endregion	

		public esProviderSpecificMetadata GetProviderMetadata(string mapName)
		{
			MapToMeta mapMethod = mapDelegates[mapName];

			if (mapMethod != null)
				return mapMethod(mapName);
			else
				return null;
		}
		
		#region MAP esDefault
		
		static private int RegisterDelegateesDefault()
		{
			// This is only executed once per the life of the application
			lock (typeof(LoopMetadata))
			{
				if(LoopMetadata.mapDelegates == null)
				{
					LoopMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (LoopMetadata.meta == null)
				{
					LoopMetadata.meta = new LoopMetadata();
				}
				
				MapToMeta mapMethod = new MapToMeta(meta.esDefault);
				mapDelegates.Add("esDefault", mapMethod);
				mapMethod("esDefault");
			}
			return 0;
		}			

		private esProviderSpecificMetadata esDefault(string mapName)
		{
			if(!m_providerMetadataMaps.ContainsKey(mapName))
			{
				esProviderSpecificMetadata meta = new esProviderSpecificMetadata();			


				meta.AddTypeMap("Id", new esTypeMap("BigInt", "System.Int64"));
				meta.AddTypeMap("Name", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Description", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Createdon", new esTypeMap("DateTime", "System.DateTime"));
				meta.AddTypeMap("Createdby", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Expiry", new esTypeMap("DateTime", "System.DateTime"));
				meta.AddTypeMap("Active", new esTypeMap("Bit", "System.Boolean"));
				meta.AddTypeMap("Templateid", new esTypeMap("BigInt", "System.Int64"));
				meta.AddTypeMap("Templatename", new esTypeMap("VarChar", "System.String"));			
				
				
				
				meta.Source = "Loop";
				meta.Destination = "Loop";
				
				meta.spInsert = "proc_LoopInsert";				
				meta.spUpdate = "proc_LoopUpdate";		
				meta.spDelete = "proc_LoopDelete";
				meta.spLoadAll = "proc_LoopLoadAll";
				meta.spLoadByPrimaryKey = "proc_LoopLoadByPrimaryKey";
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private LoopMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
