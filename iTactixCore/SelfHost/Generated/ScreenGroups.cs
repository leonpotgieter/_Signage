
/*
===============================================================================
                    EntitySpaces 2011 by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2011.1.0530.0
EntitySpaces Driver  : VistaDB4
Date Generated       : 2011/09/01 09:38:10 AM
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
	/// Encapsulates the 'ScreenGroups' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[XmlType("ScreenGroups")]
	public partial class ScreenGroups : esScreenGroups
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new ScreenGroups();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Int64 id)
		{
			var obj = new ScreenGroups();
			obj.Id = id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Int64 id, esSqlAccessType sqlAccessType)
		{
			var obj = new ScreenGroups();
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
	[XmlType("ScreenGroupsCollection")]
	public partial class ScreenGroupsCollection : esScreenGroupsCollection, IEnumerable<ScreenGroups>
	{
		public ScreenGroups FindByPrimaryKey(System.Int64 id)
		{
			return this.SingleOrDefault(e => e.Id == id);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(ScreenGroups))]
		public class ScreenGroupsCollectionWCFPacket : esCollectionWCFPacket<ScreenGroupsCollection>
		{
			public static implicit operator ScreenGroupsCollection(ScreenGroupsCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator ScreenGroupsCollectionWCFPacket(ScreenGroupsCollection collection)
			{
				return new ScreenGroupsCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class ScreenGroupsQuery : esScreenGroupsQuery
	{
		public ScreenGroupsQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "ScreenGroupsQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(ScreenGroupsQuery query)
		{
			return ScreenGroupsQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator ScreenGroupsQuery(string query)
		{
			return (ScreenGroupsQuery)ScreenGroupsQuery.SerializeHelper.FromXml(query, typeof(ScreenGroupsQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esScreenGroups : esEntity
	{
		public esScreenGroups()
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
			ScreenGroupsQuery query = new ScreenGroupsQuery();
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
		/// Maps to ScreenGroups.id
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int64? Id
		{
			get
			{
				return base.GetSystemInt64(ScreenGroupsMetadata.ColumnNames.Id);
			}
			
			set
			{
				if(base.SetSystemInt64(ScreenGroupsMetadata.ColumnNames.Id, value))
				{
					OnPropertyChanged(ScreenGroupsMetadata.PropertyNames.Id);
				}
			}
		}		
		
		/// <summary>
		/// Maps to ScreenGroups.name
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Name
		{
			get
			{
				return base.GetSystemString(ScreenGroupsMetadata.ColumnNames.Name);
			}
			
			set
			{
				if(base.SetSystemString(ScreenGroupsMetadata.ColumnNames.Name, value))
				{
					OnPropertyChanged(ScreenGroupsMetadata.PropertyNames.Name);
				}
			}
		}		
		
		/// <summary>
		/// Maps to ScreenGroups.description
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Description
		{
			get
			{
				return base.GetSystemString(ScreenGroupsMetadata.ColumnNames.Description);
			}
			
			set
			{
				if(base.SetSystemString(ScreenGroupsMetadata.ColumnNames.Description, value))
				{
					OnPropertyChanged(ScreenGroupsMetadata.PropertyNames.Description);
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
					}
				}
				else
				{
					switch (name)
					{	
						case "Id":
						
							if (value == null || value is System.Int64)
								this.Id = (System.Int64?)value;
								OnPropertyChanged(ScreenGroupsMetadata.PropertyNames.Id);
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
			public esStrings(esScreenGroups entity)
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
			

			private esScreenGroups entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return ScreenGroupsMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public ScreenGroupsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ScreenGroupsQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(ScreenGroupsQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(ScreenGroupsQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private ScreenGroupsQuery query;		
	}



	[Serializable]
	abstract public partial class esScreenGroupsCollection : esEntityCollection<ScreenGroups>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ScreenGroupsMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "ScreenGroupsCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public ScreenGroupsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ScreenGroupsQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(ScreenGroupsQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ScreenGroupsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(ScreenGroupsQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((ScreenGroupsQuery)query);
		}

		#endregion
		
		private ScreenGroupsQuery query;
	}



	[Serializable]
	abstract public partial class esScreenGroupsQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ScreenGroupsMetadata.Meta();
			}
		}	
		
		#region esQueryItems

		public esQueryItem Id
		{
			get { return new esQueryItem(this, ScreenGroupsMetadata.ColumnNames.Id, esSystemType.Int64); }
		} 
		
		public esQueryItem Name
		{
			get { return new esQueryItem(this, ScreenGroupsMetadata.ColumnNames.Name, esSystemType.String); }
		} 
		
		public esQueryItem Description
		{
			get { return new esQueryItem(this, ScreenGroupsMetadata.ColumnNames.Description, esSystemType.String); }
		} 
		
		#endregion
		
	}


	
	public partial class ScreenGroups : esScreenGroups
	{

		
		
	}
	



	[Serializable]
	public partial class ScreenGroupsMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ScreenGroupsMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ScreenGroupsMetadata.ColumnNames.Id, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = ScreenGroupsMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ScreenGroupsMetadata.ColumnNames.Name, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ScreenGroupsMetadata.PropertyNames.Name;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ScreenGroupsMetadata.ColumnNames.Description, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ScreenGroupsMetadata.PropertyNames.Description;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public ScreenGroupsMetadata Meta()
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
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string Name = "Name";
			 public const string Description = "Description";
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
			lock (typeof(ScreenGroupsMetadata))
			{
				if(ScreenGroupsMetadata.mapDelegates == null)
				{
					ScreenGroupsMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ScreenGroupsMetadata.meta == null)
				{
					ScreenGroupsMetadata.meta = new ScreenGroupsMetadata();
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
				
				
				
				meta.Source = "ScreenGroups";
				meta.Destination = "ScreenGroups";
				
				meta.spInsert = "proc_ScreenGroupsInsert";				
				meta.spUpdate = "proc_ScreenGroupsUpdate";		
				meta.spDelete = "proc_ScreenGroupsDelete";
				meta.spLoadAll = "proc_ScreenGroupsLoadAll";
				meta.spLoadByPrimaryKey = "proc_ScreenGroupsLoadByPrimaryKey";
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ScreenGroupsMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
