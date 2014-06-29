
/*
===============================================================================
                    EntitySpaces 2011 by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2011.1.1110.0
EntitySpaces Driver  : VistaDB4
Date Generated       : 2012/07/03 11:09:42 AM
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
	/// Encapsulates the 'CSDirection' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[XmlType("CSDirection")]
	public partial class CSDirection : esCSDirection
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new CSDirection();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Int64 id)
		{
			var obj = new CSDirection();
			obj.Id = id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Int64 id, esSqlAccessType sqlAccessType)
		{
			var obj = new CSDirection();
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
	[XmlType("CSDirectionCollection")]
	public partial class CSDirectionCollection : esCSDirectionCollection, IEnumerable<CSDirection>
	{
		public CSDirection FindByPrimaryKey(System.Int64 id)
		{
			return this.SingleOrDefault(e => e.Id == id);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(CSDirection))]
		public class CSDirectionCollectionWCFPacket : esCollectionWCFPacket<CSDirectionCollection>
		{
			public static implicit operator CSDirectionCollection(CSDirectionCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator CSDirectionCollectionWCFPacket(CSDirectionCollection collection)
			{
				return new CSDirectionCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class CSDirectionQuery : esCSDirectionQuery
	{
		public CSDirectionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "CSDirectionQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(CSDirectionQuery query)
		{
			return CSDirectionQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator CSDirectionQuery(string query)
		{
			return (CSDirectionQuery)CSDirectionQuery.SerializeHelper.FromXml(query, typeof(CSDirectionQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esCSDirection : esEntity
	{
		public esCSDirection()
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
			CSDirectionQuery query = new CSDirectionQuery();
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
		/// Maps to CSDirection.id
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int64? Id
		{
			get
			{
				return base.GetSystemInt64(CSDirectionMetadata.ColumnNames.Id);
			}
			
			set
			{
				if(base.SetSystemInt64(CSDirectionMetadata.ColumnNames.Id, value))
				{
					OnPropertyChanged(CSDirectionMetadata.PropertyNames.Id);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSDirection.directionalscreenname
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Directionalscreenname
		{
			get
			{
				return base.GetSystemString(CSDirectionMetadata.ColumnNames.Directionalscreenname);
			}
			
			set
			{
				if(base.SetSystemString(CSDirectionMetadata.ColumnNames.Directionalscreenname, value))
				{
					OnPropertyChanged(CSDirectionMetadata.PropertyNames.Directionalscreenname);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSDirection.meetingroomname
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Meetingroomname
		{
			get
			{
				return base.GetSystemString(CSDirectionMetadata.ColumnNames.Meetingroomname);
			}
			
			set
			{
				if(base.SetSystemString(CSDirectionMetadata.ColumnNames.Meetingroomname, value))
				{
					OnPropertyChanged(CSDirectionMetadata.PropertyNames.Meetingroomname);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSDirection.directionimageblob
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Byte[] Directionimageblob
		{
			get
			{
				return base.GetSystemByteArray(CSDirectionMetadata.ColumnNames.Directionimageblob);
			}
			
			set
			{
				if(base.SetSystemByteArray(CSDirectionMetadata.ColumnNames.Directionimageblob, value))
				{
					OnPropertyChanged(CSDirectionMetadata.PropertyNames.Directionimageblob);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSDirection.directionimagefile
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Directionimagefile
		{
			get
			{
				return base.GetSystemString(CSDirectionMetadata.ColumnNames.Directionimagefile);
			}
			
			set
			{
				if(base.SetSystemString(CSDirectionMetadata.ColumnNames.Directionimagefile, value))
				{
					OnPropertyChanged(CSDirectionMetadata.PropertyNames.Directionimagefile);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSDirection.directiontext
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Directiontext
		{
			get
			{
				return base.GetSystemString(CSDirectionMetadata.ColumnNames.Directiontext);
			}
			
			set
			{
				if(base.SetSystemString(CSDirectionMetadata.ColumnNames.Directiontext, value))
				{
					OnPropertyChanged(CSDirectionMetadata.PropertyNames.Directiontext);
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
						case "Directionalscreenname": this.str().Directionalscreenname = (string)value; break;							
						case "Meetingroomname": this.str().Meetingroomname = (string)value; break;							
						case "Directionimagefile": this.str().Directionimagefile = (string)value; break;							
						case "Directiontext": this.str().Directiontext = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Id":
						
							if (value == null || value is System.Int64)
								this.Id = (System.Int64?)value;
								OnPropertyChanged(CSDirectionMetadata.PropertyNames.Id);
							break;
						
						case "Directionimageblob":
						
							if (value == null || value is System.Byte[])
								this.Directionimageblob = (System.Byte[])value;
								OnPropertyChanged(CSDirectionMetadata.PropertyNames.Directionimageblob);
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
			public esStrings(esCSDirection entity)
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
				
			public System.String Directionalscreenname
			{
				get
				{
					System.String data = entity.Directionalscreenname;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Directionalscreenname = null;
					else entity.Directionalscreenname = Convert.ToString(value);
				}
			}
				
			public System.String Meetingroomname
			{
				get
				{
					System.String data = entity.Meetingroomname;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Meetingroomname = null;
					else entity.Meetingroomname = Convert.ToString(value);
				}
			}
				
			public System.String Directionimagefile
			{
				get
				{
					System.String data = entity.Directionimagefile;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Directionimagefile = null;
					else entity.Directionimagefile = Convert.ToString(value);
				}
			}
				
			public System.String Directiontext
			{
				get
				{
					System.String data = entity.Directiontext;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Directiontext = null;
					else entity.Directiontext = Convert.ToString(value);
				}
			}
			

			private esCSDirection entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return CSDirectionMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public CSDirectionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CSDirectionQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(CSDirectionQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(CSDirectionQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private CSDirectionQuery query;		
	}



	[Serializable]
	abstract public partial class esCSDirectionCollection : esEntityCollection<CSDirection>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CSDirectionMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "CSDirectionCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public CSDirectionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CSDirectionQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(CSDirectionQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CSDirectionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(CSDirectionQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((CSDirectionQuery)query);
		}

		#endregion
		
		private CSDirectionQuery query;
	}



	[Serializable]
	abstract public partial class esCSDirectionQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return CSDirectionMetadata.Meta();
			}
		}	
		
		#region esQueryItems

		public esQueryItem Id
		{
			get { return new esQueryItem(this, CSDirectionMetadata.ColumnNames.Id, esSystemType.Int64); }
		} 
		
		public esQueryItem Directionalscreenname
		{
			get { return new esQueryItem(this, CSDirectionMetadata.ColumnNames.Directionalscreenname, esSystemType.String); }
		} 
		
		public esQueryItem Meetingroomname
		{
			get { return new esQueryItem(this, CSDirectionMetadata.ColumnNames.Meetingroomname, esSystemType.String); }
		} 
		
		public esQueryItem Directionimageblob
		{
			get { return new esQueryItem(this, CSDirectionMetadata.ColumnNames.Directionimageblob, esSystemType.ByteArray); }
		} 
		
		public esQueryItem Directionimagefile
		{
			get { return new esQueryItem(this, CSDirectionMetadata.ColumnNames.Directionimagefile, esSystemType.String); }
		} 
		
		public esQueryItem Directiontext
		{
			get { return new esQueryItem(this, CSDirectionMetadata.ColumnNames.Directiontext, esSystemType.String); }
		} 
		
		#endregion
		
	}


	
	public partial class CSDirection : esCSDirection
	{

		
		
	}
	



	[Serializable]
	public partial class CSDirectionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CSDirectionMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CSDirectionMetadata.ColumnNames.Id, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = CSDirectionMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSDirectionMetadata.ColumnNames.Directionalscreenname, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = CSDirectionMetadata.PropertyNames.Directionalscreenname;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSDirectionMetadata.ColumnNames.Meetingroomname, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = CSDirectionMetadata.PropertyNames.Meetingroomname;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSDirectionMetadata.ColumnNames.Directionimageblob, 3, typeof(System.Byte[]), esSystemType.ByteArray);
			c.PropertyName = CSDirectionMetadata.PropertyNames.Directionimageblob;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSDirectionMetadata.ColumnNames.Directionimagefile, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = CSDirectionMetadata.PropertyNames.Directionimagefile;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSDirectionMetadata.ColumnNames.Directiontext, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = CSDirectionMetadata.PropertyNames.Directiontext;
			c.CharacterMaxLength = 1024;
			c.IsNullable = true;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public CSDirectionMetadata Meta()
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
			 public const string Directionalscreenname = "directionalscreenname";
			 public const string Meetingroomname = "meetingroomname";
			 public const string Directionimageblob = "directionimageblob";
			 public const string Directionimagefile = "directionimagefile";
			 public const string Directiontext = "directiontext";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string Directionalscreenname = "Directionalscreenname";
			 public const string Meetingroomname = "Meetingroomname";
			 public const string Directionimageblob = "Directionimageblob";
			 public const string Directionimagefile = "Directionimagefile";
			 public const string Directiontext = "Directiontext";
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
			lock (typeof(CSDirectionMetadata))
			{
				if(CSDirectionMetadata.mapDelegates == null)
				{
					CSDirectionMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (CSDirectionMetadata.meta == null)
				{
					CSDirectionMetadata.meta = new CSDirectionMetadata();
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
				meta.AddTypeMap("Directionalscreenname", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Meetingroomname", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Directionimageblob", new esTypeMap("Image", "System.Byte[]"));
				meta.AddTypeMap("Directionimagefile", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Directiontext", new esTypeMap("VarChar", "System.String"));			
				
				
				
				meta.Source = "CSDirection";
				meta.Destination = "CSDirection";
				
				meta.spInsert = "proc_CSDirectionInsert";				
				meta.spUpdate = "proc_CSDirectionUpdate";		
				meta.spDelete = "proc_CSDirectionDelete";
				meta.spLoadAll = "proc_CSDirectionLoadAll";
				meta.spLoadByPrimaryKey = "proc_CSDirectionLoadByPrimaryKey";
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CSDirectionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
