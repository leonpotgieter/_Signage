
/*
===============================================================================
                    EntitySpaces 2011 by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2011.1.1110.0
EntitySpaces Driver  : VistaDB4
Date Generated       : 2012/07/03 11:09:44 AM
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
	/// Encapsulates the 'CSMedialoop' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[XmlType("CSMedialoop")]
	public partial class CSMedialoop : esCSMedialoop
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new CSMedialoop();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Int64 id)
		{
			var obj = new CSMedialoop();
			obj.Id = id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Int64 id, esSqlAccessType sqlAccessType)
		{
			var obj = new CSMedialoop();
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
	[XmlType("CSMedialoopCollection")]
	public partial class CSMedialoopCollection : esCSMedialoopCollection, IEnumerable<CSMedialoop>
	{
		public CSMedialoop FindByPrimaryKey(System.Int64 id)
		{
			return this.SingleOrDefault(e => e.Id == id);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(CSMedialoop))]
		public class CSMedialoopCollectionWCFPacket : esCollectionWCFPacket<CSMedialoopCollection>
		{
			public static implicit operator CSMedialoopCollection(CSMedialoopCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator CSMedialoopCollectionWCFPacket(CSMedialoopCollection collection)
			{
				return new CSMedialoopCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class CSMedialoopQuery : esCSMedialoopQuery
	{
		public CSMedialoopQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "CSMedialoopQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(CSMedialoopQuery query)
		{
			return CSMedialoopQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator CSMedialoopQuery(string query)
		{
			return (CSMedialoopQuery)CSMedialoopQuery.SerializeHelper.FromXml(query, typeof(CSMedialoopQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esCSMedialoop : esEntity
	{
		public esCSMedialoop()
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
			CSMedialoopQuery query = new CSMedialoopQuery();
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
		/// Maps to CSMedialoop.id
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int64? Id
		{
			get
			{
				return base.GetSystemInt64(CSMedialoopMetadata.ColumnNames.Id);
			}
			
			set
			{
				if(base.SetSystemInt64(CSMedialoopMetadata.ColumnNames.Id, value))
				{
					OnPropertyChanged(CSMedialoopMetadata.PropertyNames.Id);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSMedialoop.loopname
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Loopname
		{
			get
			{
				return base.GetSystemString(CSMedialoopMetadata.ColumnNames.Loopname);
			}
			
			set
			{
				if(base.SetSystemString(CSMedialoopMetadata.ColumnNames.Loopname, value))
				{
					OnPropertyChanged(CSMedialoopMetadata.PropertyNames.Loopname);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSMedialoop.mediatype
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Mediatype
		{
			get
			{
				return base.GetSystemString(CSMedialoopMetadata.ColumnNames.Mediatype);
			}
			
			set
			{
				if(base.SetSystemString(CSMedialoopMetadata.ColumnNames.Mediatype, value))
				{
					OnPropertyChanged(CSMedialoopMetadata.PropertyNames.Mediatype);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSMedialoop.mediafilename
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Mediafilename
		{
			get
			{
				return base.GetSystemString(CSMedialoopMetadata.ColumnNames.Mediafilename);
			}
			
			set
			{
				if(base.SetSystemString(CSMedialoopMetadata.ColumnNames.Mediafilename, value))
				{
					OnPropertyChanged(CSMedialoopMetadata.PropertyNames.Mediafilename);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSMedialoop.order
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? Order
		{
			get
			{
				return base.GetSystemInt32(CSMedialoopMetadata.ColumnNames.Order);
			}
			
			set
			{
				if(base.SetSystemInt32(CSMedialoopMetadata.ColumnNames.Order, value))
				{
					OnPropertyChanged(CSMedialoopMetadata.PropertyNames.Order);
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
						case "Loopname": this.str().Loopname = (string)value; break;							
						case "Mediatype": this.str().Mediatype = (string)value; break;							
						case "Mediafilename": this.str().Mediafilename = (string)value; break;							
						case "Order": this.str().Order = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Id":
						
							if (value == null || value is System.Int64)
								this.Id = (System.Int64?)value;
								OnPropertyChanged(CSMedialoopMetadata.PropertyNames.Id);
							break;
						
						case "Order":
						
							if (value == null || value is System.Int32)
								this.Order = (System.Int32?)value;
								OnPropertyChanged(CSMedialoopMetadata.PropertyNames.Order);
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
			public esStrings(esCSMedialoop entity)
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
				
			public System.String Loopname
			{
				get
				{
					System.String data = entity.Loopname;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Loopname = null;
					else entity.Loopname = Convert.ToString(value);
				}
			}
				
			public System.String Mediatype
			{
				get
				{
					System.String data = entity.Mediatype;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Mediatype = null;
					else entity.Mediatype = Convert.ToString(value);
				}
			}
				
			public System.String Mediafilename
			{
				get
				{
					System.String data = entity.Mediafilename;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Mediafilename = null;
					else entity.Mediafilename = Convert.ToString(value);
				}
			}
				
			public System.String Order
			{
				get
				{
					System.Int32? data = entity.Order;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Order = null;
					else entity.Order = Convert.ToInt32(value);
				}
			}
			

			private esCSMedialoop entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return CSMedialoopMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public CSMedialoopQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CSMedialoopQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(CSMedialoopQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(CSMedialoopQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private CSMedialoopQuery query;		
	}



	[Serializable]
	abstract public partial class esCSMedialoopCollection : esEntityCollection<CSMedialoop>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CSMedialoopMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "CSMedialoopCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public CSMedialoopQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CSMedialoopQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(CSMedialoopQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CSMedialoopQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(CSMedialoopQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((CSMedialoopQuery)query);
		}

		#endregion
		
		private CSMedialoopQuery query;
	}



	[Serializable]
	abstract public partial class esCSMedialoopQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return CSMedialoopMetadata.Meta();
			}
		}	
		
		#region esQueryItems

		public esQueryItem Id
		{
			get { return new esQueryItem(this, CSMedialoopMetadata.ColumnNames.Id, esSystemType.Int64); }
		} 
		
		public esQueryItem Loopname
		{
			get { return new esQueryItem(this, CSMedialoopMetadata.ColumnNames.Loopname, esSystemType.String); }
		} 
		
		public esQueryItem Mediatype
		{
			get { return new esQueryItem(this, CSMedialoopMetadata.ColumnNames.Mediatype, esSystemType.String); }
		} 
		
		public esQueryItem Mediafilename
		{
			get { return new esQueryItem(this, CSMedialoopMetadata.ColumnNames.Mediafilename, esSystemType.String); }
		} 
		
		public esQueryItem Order
		{
			get { return new esQueryItem(this, CSMedialoopMetadata.ColumnNames.Order, esSystemType.Int32); }
		} 
		
		#endregion
		
	}


	
	public partial class CSMedialoop : esCSMedialoop
	{

		
		
	}
	



	[Serializable]
	public partial class CSMedialoopMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CSMedialoopMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CSMedialoopMetadata.ColumnNames.Id, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = CSMedialoopMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSMedialoopMetadata.ColumnNames.Loopname, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = CSMedialoopMetadata.PropertyNames.Loopname;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSMedialoopMetadata.ColumnNames.Mediatype, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = CSMedialoopMetadata.PropertyNames.Mediatype;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSMedialoopMetadata.ColumnNames.Mediafilename, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = CSMedialoopMetadata.PropertyNames.Mediafilename;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSMedialoopMetadata.ColumnNames.Order, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CSMedialoopMetadata.PropertyNames.Order;
			c.IsNullable = true;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public CSMedialoopMetadata Meta()
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
			 public const string Loopname = "loopname";
			 public const string Mediatype = "mediatype";
			 public const string Mediafilename = "mediafilename";
			 public const string Order = "order";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string Loopname = "Loopname";
			 public const string Mediatype = "Mediatype";
			 public const string Mediafilename = "Mediafilename";
			 public const string Order = "Order";
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
			lock (typeof(CSMedialoopMetadata))
			{
				if(CSMedialoopMetadata.mapDelegates == null)
				{
					CSMedialoopMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (CSMedialoopMetadata.meta == null)
				{
					CSMedialoopMetadata.meta = new CSMedialoopMetadata();
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
				meta.AddTypeMap("Loopname", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Mediatype", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Mediafilename", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Order", new esTypeMap("Int", "System.Int32"));			
				
				
				
				meta.Source = "CSMedialoop";
				meta.Destination = "CSMedialoop";
				
				meta.spInsert = "proc_CSMedialoopInsert";				
				meta.spUpdate = "proc_CSMedialoopUpdate";		
				meta.spDelete = "proc_CSMedialoopDelete";
				meta.spLoadAll = "proc_CSMedialoopLoadAll";
				meta.spLoadByPrimaryKey = "proc_CSMedialoopLoadByPrimaryKey";
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CSMedialoopMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
