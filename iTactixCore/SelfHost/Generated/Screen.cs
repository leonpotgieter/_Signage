
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
	/// Encapsulates the 'Screen' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[XmlType("Screen")]
	public partial class Screen : esScreen
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new Screen();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Int64 id)
		{
			var obj = new Screen();
			obj.Id = id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Int64 id, esSqlAccessType sqlAccessType)
		{
			var obj = new Screen();
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
	[XmlType("ScreenCollection")]
	public partial class ScreenCollection : esScreenCollection, IEnumerable<Screen>
	{
		public Screen FindByPrimaryKey(System.Int64 id)
		{
			return this.SingleOrDefault(e => e.Id == id);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(Screen))]
		public class ScreenCollectionWCFPacket : esCollectionWCFPacket<ScreenCollection>
		{
			public static implicit operator ScreenCollection(ScreenCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator ScreenCollectionWCFPacket(ScreenCollection collection)
			{
				return new ScreenCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class ScreenQuery : esScreenQuery
	{
		public ScreenQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "ScreenQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(ScreenQuery query)
		{
			return ScreenQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator ScreenQuery(string query)
		{
			return (ScreenQuery)ScreenQuery.SerializeHelper.FromXml(query, typeof(ScreenQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esScreen : esEntity
	{
		public esScreen()
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
			ScreenQuery query = new ScreenQuery();
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
		/// Maps to Screen.id
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int64? Id
		{
			get
			{
				return base.GetSystemInt64(ScreenMetadata.ColumnNames.Id);
			}
			
			set
			{
				if(base.SetSystemInt64(ScreenMetadata.ColumnNames.Id, value))
				{
					OnPropertyChanged(ScreenMetadata.PropertyNames.Id);
				}
			}
		}		
		
		/// <summary>
		/// Maps to Screen.screenname
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Screenname
		{
			get
			{
				return base.GetSystemString(ScreenMetadata.ColumnNames.Screenname);
			}
			
			set
			{
				if(base.SetSystemString(ScreenMetadata.ColumnNames.Screenname, value))
				{
					OnPropertyChanged(ScreenMetadata.PropertyNames.Screenname);
				}
			}
		}		
		
		/// <summary>
		/// Maps to Screen.description
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Description
		{
			get
			{
				return base.GetSystemString(ScreenMetadata.ColumnNames.Description);
			}
			
			set
			{
				if(base.SetSystemString(ScreenMetadata.ColumnNames.Description, value))
				{
					OnPropertyChanged(ScreenMetadata.PropertyNames.Description);
				}
			}
		}		
		
		/// <summary>
		/// Maps to Screen.location
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Location
		{
			get
			{
				return base.GetSystemString(ScreenMetadata.ColumnNames.Location);
			}
			
			set
			{
				if(base.SetSystemString(ScreenMetadata.ColumnNames.Location, value))
				{
					OnPropertyChanged(ScreenMetadata.PropertyNames.Location);
				}
			}
		}		
		
		/// <summary>
		/// Maps to Screen.groupid
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Groupid
		{
			get
			{
				return base.GetSystemString(ScreenMetadata.ColumnNames.Groupid);
			}
			
			set
			{
				if(base.SetSystemString(ScreenMetadata.ColumnNames.Groupid, value))
				{
					OnPropertyChanged(ScreenMetadata.PropertyNames.Groupid);
				}
			}
		}		
		
		/// <summary>
		/// Maps to Screen.ip
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Ip
		{
			get
			{
				return base.GetSystemString(ScreenMetadata.ColumnNames.Ip);
			}
			
			set
			{
				if(base.SetSystemString(ScreenMetadata.ColumnNames.Ip, value))
				{
					OnPropertyChanged(ScreenMetadata.PropertyNames.Ip);
				}
			}
		}		
		
		/// <summary>
		/// Maps to Screen.lastactive
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.DateTime? Lastactive
		{
			get
			{
				return base.GetSystemDateTime(ScreenMetadata.ColumnNames.Lastactive);
			}
			
			set
			{
				if(base.SetSystemDateTime(ScreenMetadata.ColumnNames.Lastactive, value))
				{
					OnPropertyChanged(ScreenMetadata.PropertyNames.Lastactive);
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
						case "Screenname": this.str().Screenname = (string)value; break;							
						case "Description": this.str().Description = (string)value; break;							
						case "Location": this.str().Location = (string)value; break;							
						case "Groupid": this.str().Groupid = (string)value; break;							
						case "Ip": this.str().Ip = (string)value; break;							
						case "Lastactive": this.str().Lastactive = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Id":
						
							if (value == null || value is System.Int64)
								this.Id = (System.Int64?)value;
								OnPropertyChanged(ScreenMetadata.PropertyNames.Id);
							break;
						
						case "Lastactive":
						
							if (value == null || value is System.DateTime)
								this.Lastactive = (System.DateTime?)value;
								OnPropertyChanged(ScreenMetadata.PropertyNames.Lastactive);
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
			public esStrings(esScreen entity)
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
				
			public System.String Screenname
			{
				get
				{
					System.String data = entity.Screenname;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Screenname = null;
					else entity.Screenname = Convert.ToString(value);
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
				
			public System.String Location
			{
				get
				{
					System.String data = entity.Location;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Location = null;
					else entity.Location = Convert.ToString(value);
				}
			}
				
			public System.String Groupid
			{
				get
				{
					System.String data = entity.Groupid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Groupid = null;
					else entity.Groupid = Convert.ToString(value);
				}
			}
				
			public System.String Ip
			{
				get
				{
					System.String data = entity.Ip;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Ip = null;
					else entity.Ip = Convert.ToString(value);
				}
			}
				
			public System.String Lastactive
			{
				get
				{
					System.DateTime? data = entity.Lastactive;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Lastactive = null;
					else entity.Lastactive = Convert.ToDateTime(value);
				}
			}
			

			private esScreen entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return ScreenMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public ScreenQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ScreenQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(ScreenQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(ScreenQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private ScreenQuery query;		
	}



	[Serializable]
	abstract public partial class esScreenCollection : esEntityCollection<Screen>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ScreenMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "ScreenCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public ScreenQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ScreenQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(ScreenQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ScreenQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(ScreenQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((ScreenQuery)query);
		}

		#endregion
		
		private ScreenQuery query;
	}



	[Serializable]
	abstract public partial class esScreenQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ScreenMetadata.Meta();
			}
		}	
		
		#region esQueryItems

		public esQueryItem Id
		{
			get { return new esQueryItem(this, ScreenMetadata.ColumnNames.Id, esSystemType.Int64); }
		} 
		
		public esQueryItem Screenname
		{
			get { return new esQueryItem(this, ScreenMetadata.ColumnNames.Screenname, esSystemType.String); }
		} 
		
		public esQueryItem Description
		{
			get { return new esQueryItem(this, ScreenMetadata.ColumnNames.Description, esSystemType.String); }
		} 
		
		public esQueryItem Location
		{
			get { return new esQueryItem(this, ScreenMetadata.ColumnNames.Location, esSystemType.String); }
		} 
		
		public esQueryItem Groupid
		{
			get { return new esQueryItem(this, ScreenMetadata.ColumnNames.Groupid, esSystemType.String); }
		} 
		
		public esQueryItem Ip
		{
			get { return new esQueryItem(this, ScreenMetadata.ColumnNames.Ip, esSystemType.String); }
		} 
		
		public esQueryItem Lastactive
		{
			get { return new esQueryItem(this, ScreenMetadata.ColumnNames.Lastactive, esSystemType.DateTime); }
		} 
		
		#endregion
		
	}


	
	public partial class Screen : esScreen
	{

		
		
	}
	



	[Serializable]
	public partial class ScreenMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ScreenMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ScreenMetadata.ColumnNames.Id, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = ScreenMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ScreenMetadata.ColumnNames.Screenname, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ScreenMetadata.PropertyNames.Screenname;
			c.CharacterMaxLength = 64;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ScreenMetadata.ColumnNames.Description, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ScreenMetadata.PropertyNames.Description;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ScreenMetadata.ColumnNames.Location, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ScreenMetadata.PropertyNames.Location;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ScreenMetadata.ColumnNames.Groupid, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ScreenMetadata.PropertyNames.Groupid;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ScreenMetadata.ColumnNames.Ip, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ScreenMetadata.PropertyNames.Ip;
			c.CharacterMaxLength = 32;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ScreenMetadata.ColumnNames.Lastactive, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ScreenMetadata.PropertyNames.Lastactive;
			c.IsNullable = true;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public ScreenMetadata Meta()
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
			 public const string Screenname = "screenname";
			 public const string Description = "description";
			 public const string Location = "location";
			 public const string Groupid = "groupid";
			 public const string Ip = "ip";
			 public const string Lastactive = "lastactive";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string Screenname = "Screenname";
			 public const string Description = "Description";
			 public const string Location = "Location";
			 public const string Groupid = "Groupid";
			 public const string Ip = "Ip";
			 public const string Lastactive = "Lastactive";
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
			lock (typeof(ScreenMetadata))
			{
				if(ScreenMetadata.mapDelegates == null)
				{
					ScreenMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ScreenMetadata.meta == null)
				{
					ScreenMetadata.meta = new ScreenMetadata();
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
				meta.AddTypeMap("Screenname", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Description", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Location", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Groupid", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Ip", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Lastactive", new esTypeMap("DateTime", "System.DateTime"));			
				
				
				
				meta.Source = "Screen";
				meta.Destination = "Screen";
				
				meta.spInsert = "proc_ScreenInsert";				
				meta.spUpdate = "proc_ScreenUpdate";		
				meta.spDelete = "proc_ScreenDelete";
				meta.spLoadAll = "proc_ScreenLoadAll";
				meta.spLoadByPrimaryKey = "proc_ScreenLoadByPrimaryKey";
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ScreenMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
