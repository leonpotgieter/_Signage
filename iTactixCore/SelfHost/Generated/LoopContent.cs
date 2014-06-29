
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
	/// Encapsulates the 'LoopContent' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[XmlType("LoopContent")]
	public partial class LoopContent : esLoopContent
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new LoopContent();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Int64 id)
		{
			var obj = new LoopContent();
			obj.Id = id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Int64 id, esSqlAccessType sqlAccessType)
		{
			var obj = new LoopContent();
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
	[XmlType("LoopContentCollection")]
	public partial class LoopContentCollection : esLoopContentCollection, IEnumerable<LoopContent>
	{
		public LoopContent FindByPrimaryKey(System.Int64 id)
		{
			return this.SingleOrDefault(e => e.Id == id);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(LoopContent))]
		public class LoopContentCollectionWCFPacket : esCollectionWCFPacket<LoopContentCollection>
		{
			public static implicit operator LoopContentCollection(LoopContentCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator LoopContentCollectionWCFPacket(LoopContentCollection collection)
			{
				return new LoopContentCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class LoopContentQuery : esLoopContentQuery
	{
		public LoopContentQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "LoopContentQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(LoopContentQuery query)
		{
			return LoopContentQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator LoopContentQuery(string query)
		{
			return (LoopContentQuery)LoopContentQuery.SerializeHelper.FromXml(query, typeof(LoopContentQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esLoopContent : esEntity
	{
		public esLoopContent()
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
			LoopContentQuery query = new LoopContentQuery();
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
		/// Maps to LoopContent.id
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int64? Id
		{
			get
			{
				return base.GetSystemInt64(LoopContentMetadata.ColumnNames.Id);
			}
			
			set
			{
				if(base.SetSystemInt64(LoopContentMetadata.ColumnNames.Id, value))
				{
					OnPropertyChanged(LoopContentMetadata.PropertyNames.Id);
				}
			}
		}		
		
		/// <summary>
		/// Maps to LoopContent.loopid
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int64? Loopid
		{
			get
			{
				return base.GetSystemInt64(LoopContentMetadata.ColumnNames.Loopid);
			}
			
			set
			{
				if(base.SetSystemInt64(LoopContentMetadata.ColumnNames.Loopid, value))
				{
					OnPropertyChanged(LoopContentMetadata.PropertyNames.Loopid);
				}
			}
		}		
		
		/// <summary>
		/// Maps to LoopContent.loopname
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Loopname
		{
			get
			{
				return base.GetSystemString(LoopContentMetadata.ColumnNames.Loopname);
			}
			
			set
			{
				if(base.SetSystemString(LoopContentMetadata.ColumnNames.Loopname, value))
				{
					OnPropertyChanged(LoopContentMetadata.PropertyNames.Loopname);
				}
			}
		}		
		
		/// <summary>
		/// Maps to LoopContent.mediaid
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int64? Mediaid
		{
			get
			{
				return base.GetSystemInt64(LoopContentMetadata.ColumnNames.Mediaid);
			}
			
			set
			{
				if(base.SetSystemInt64(LoopContentMetadata.ColumnNames.Mediaid, value))
				{
					OnPropertyChanged(LoopContentMetadata.PropertyNames.Mediaid);
				}
			}
		}		
		
		/// <summary>
		/// Maps to LoopContent.medianame
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Medianame
		{
			get
			{
				return base.GetSystemString(LoopContentMetadata.ColumnNames.Medianame);
			}
			
			set
			{
				if(base.SetSystemString(LoopContentMetadata.ColumnNames.Medianame, value))
				{
					OnPropertyChanged(LoopContentMetadata.PropertyNames.Medianame);
				}
			}
		}		
		
		/// <summary>
		/// Maps to LoopContent.templateid
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int64? Templateid
		{
			get
			{
				return base.GetSystemInt64(LoopContentMetadata.ColumnNames.Templateid);
			}
			
			set
			{
				if(base.SetSystemInt64(LoopContentMetadata.ColumnNames.Templateid, value))
				{
					OnPropertyChanged(LoopContentMetadata.PropertyNames.Templateid);
				}
			}
		}		
		
		/// <summary>
		/// Maps to LoopContent.templatename
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Templatename
		{
			get
			{
				return base.GetSystemString(LoopContentMetadata.ColumnNames.Templatename);
			}
			
			set
			{
				if(base.SetSystemString(LoopContentMetadata.ColumnNames.Templatename, value))
				{
					OnPropertyChanged(LoopContentMetadata.PropertyNames.Templatename);
				}
			}
		}		
		
		/// <summary>
		/// Maps to LoopContent.zoneid
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int64? Zoneid
		{
			get
			{
				return base.GetSystemInt64(LoopContentMetadata.ColumnNames.Zoneid);
			}
			
			set
			{
				if(base.SetSystemInt64(LoopContentMetadata.ColumnNames.Zoneid, value))
				{
					OnPropertyChanged(LoopContentMetadata.PropertyNames.Zoneid);
				}
			}
		}		
		
		/// <summary>
		/// Maps to LoopContent.zonename
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Zonename
		{
			get
			{
				return base.GetSystemString(LoopContentMetadata.ColumnNames.Zonename);
			}
			
			set
			{
				if(base.SetSystemString(LoopContentMetadata.ColumnNames.Zonename, value))
				{
					OnPropertyChanged(LoopContentMetadata.PropertyNames.Zonename);
				}
			}
		}		
		
		/// <summary>
		/// Maps to LoopContent.order
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? Order
		{
			get
			{
				return base.GetSystemInt32(LoopContentMetadata.ColumnNames.Order);
			}
			
			set
			{
				if(base.SetSystemInt32(LoopContentMetadata.ColumnNames.Order, value))
				{
					OnPropertyChanged(LoopContentMetadata.PropertyNames.Order);
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
						case "Loopid": this.str().Loopid = (string)value; break;							
						case "Loopname": this.str().Loopname = (string)value; break;							
						case "Mediaid": this.str().Mediaid = (string)value; break;							
						case "Medianame": this.str().Medianame = (string)value; break;							
						case "Templateid": this.str().Templateid = (string)value; break;							
						case "Templatename": this.str().Templatename = (string)value; break;							
						case "Zoneid": this.str().Zoneid = (string)value; break;							
						case "Zonename": this.str().Zonename = (string)value; break;							
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
								OnPropertyChanged(LoopContentMetadata.PropertyNames.Id);
							break;
						
						case "Loopid":
						
							if (value == null || value is System.Int64)
								this.Loopid = (System.Int64?)value;
								OnPropertyChanged(LoopContentMetadata.PropertyNames.Loopid);
							break;
						
						case "Mediaid":
						
							if (value == null || value is System.Int64)
								this.Mediaid = (System.Int64?)value;
								OnPropertyChanged(LoopContentMetadata.PropertyNames.Mediaid);
							break;
						
						case "Templateid":
						
							if (value == null || value is System.Int64)
								this.Templateid = (System.Int64?)value;
								OnPropertyChanged(LoopContentMetadata.PropertyNames.Templateid);
							break;
						
						case "Zoneid":
						
							if (value == null || value is System.Int64)
								this.Zoneid = (System.Int64?)value;
								OnPropertyChanged(LoopContentMetadata.PropertyNames.Zoneid);
							break;
						
						case "Order":
						
							if (value == null || value is System.Int32)
								this.Order = (System.Int32?)value;
								OnPropertyChanged(LoopContentMetadata.PropertyNames.Order);
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
			public esStrings(esLoopContent entity)
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
				
			public System.String Loopid
			{
				get
				{
					System.Int64? data = entity.Loopid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Loopid = null;
					else entity.Loopid = Convert.ToInt64(value);
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
				
			public System.String Mediaid
			{
				get
				{
					System.Int64? data = entity.Mediaid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Mediaid = null;
					else entity.Mediaid = Convert.ToInt64(value);
				}
			}
				
			public System.String Medianame
			{
				get
				{
					System.String data = entity.Medianame;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Medianame = null;
					else entity.Medianame = Convert.ToString(value);
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
				
			public System.String Zoneid
			{
				get
				{
					System.Int64? data = entity.Zoneid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Zoneid = null;
					else entity.Zoneid = Convert.ToInt64(value);
				}
			}
				
			public System.String Zonename
			{
				get
				{
					System.String data = entity.Zonename;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Zonename = null;
					else entity.Zonename = Convert.ToString(value);
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
			

			private esLoopContent entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return LoopContentMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public LoopContentQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LoopContentQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(LoopContentQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(LoopContentQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private LoopContentQuery query;		
	}



	[Serializable]
	abstract public partial class esLoopContentCollection : esEntityCollection<LoopContent>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return LoopContentMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "LoopContentCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public LoopContentQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LoopContentQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(LoopContentQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LoopContentQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(LoopContentQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((LoopContentQuery)query);
		}

		#endregion
		
		private LoopContentQuery query;
	}



	[Serializable]
	abstract public partial class esLoopContentQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return LoopContentMetadata.Meta();
			}
		}	
		
		#region esQueryItems

		public esQueryItem Id
		{
			get { return new esQueryItem(this, LoopContentMetadata.ColumnNames.Id, esSystemType.Int64); }
		} 
		
		public esQueryItem Loopid
		{
			get { return new esQueryItem(this, LoopContentMetadata.ColumnNames.Loopid, esSystemType.Int64); }
		} 
		
		public esQueryItem Loopname
		{
			get { return new esQueryItem(this, LoopContentMetadata.ColumnNames.Loopname, esSystemType.String); }
		} 
		
		public esQueryItem Mediaid
		{
			get { return new esQueryItem(this, LoopContentMetadata.ColumnNames.Mediaid, esSystemType.Int64); }
		} 
		
		public esQueryItem Medianame
		{
			get { return new esQueryItem(this, LoopContentMetadata.ColumnNames.Medianame, esSystemType.String); }
		} 
		
		public esQueryItem Templateid
		{
			get { return new esQueryItem(this, LoopContentMetadata.ColumnNames.Templateid, esSystemType.Int64); }
		} 
		
		public esQueryItem Templatename
		{
			get { return new esQueryItem(this, LoopContentMetadata.ColumnNames.Templatename, esSystemType.String); }
		} 
		
		public esQueryItem Zoneid
		{
			get { return new esQueryItem(this, LoopContentMetadata.ColumnNames.Zoneid, esSystemType.Int64); }
		} 
		
		public esQueryItem Zonename
		{
			get { return new esQueryItem(this, LoopContentMetadata.ColumnNames.Zonename, esSystemType.String); }
		} 
		
		public esQueryItem Order
		{
			get { return new esQueryItem(this, LoopContentMetadata.ColumnNames.Order, esSystemType.Int32); }
		} 
		
		#endregion
		
	}


	
	public partial class LoopContent : esLoopContent
	{

		
		
	}
	



	[Serializable]
	public partial class LoopContentMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected LoopContentMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(LoopContentMetadata.ColumnNames.Id, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = LoopContentMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(LoopContentMetadata.ColumnNames.Loopid, 1, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = LoopContentMetadata.PropertyNames.Loopid;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(LoopContentMetadata.ColumnNames.Loopname, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = LoopContentMetadata.PropertyNames.Loopname;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(LoopContentMetadata.ColumnNames.Mediaid, 3, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = LoopContentMetadata.PropertyNames.Mediaid;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(LoopContentMetadata.ColumnNames.Medianame, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = LoopContentMetadata.PropertyNames.Medianame;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(LoopContentMetadata.ColumnNames.Templateid, 5, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = LoopContentMetadata.PropertyNames.Templateid;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(LoopContentMetadata.ColumnNames.Templatename, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = LoopContentMetadata.PropertyNames.Templatename;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(LoopContentMetadata.ColumnNames.Zoneid, 7, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = LoopContentMetadata.PropertyNames.Zoneid;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(LoopContentMetadata.ColumnNames.Zonename, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = LoopContentMetadata.PropertyNames.Zonename;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(LoopContentMetadata.ColumnNames.Order, 9, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = LoopContentMetadata.PropertyNames.Order;
			c.IsNullable = true;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public LoopContentMetadata Meta()
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
			 public const string Loopid = "loopid";
			 public const string Loopname = "loopname";
			 public const string Mediaid = "mediaid";
			 public const string Medianame = "medianame";
			 public const string Templateid = "templateid";
			 public const string Templatename = "templatename";
			 public const string Zoneid = "zoneid";
			 public const string Zonename = "zonename";
			 public const string Order = "order";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string Loopid = "Loopid";
			 public const string Loopname = "Loopname";
			 public const string Mediaid = "Mediaid";
			 public const string Medianame = "Medianame";
			 public const string Templateid = "Templateid";
			 public const string Templatename = "Templatename";
			 public const string Zoneid = "Zoneid";
			 public const string Zonename = "Zonename";
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
			lock (typeof(LoopContentMetadata))
			{
				if(LoopContentMetadata.mapDelegates == null)
				{
					LoopContentMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (LoopContentMetadata.meta == null)
				{
					LoopContentMetadata.meta = new LoopContentMetadata();
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
				meta.AddTypeMap("Loopid", new esTypeMap("BigInt", "System.Int64"));
				meta.AddTypeMap("Loopname", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Mediaid", new esTypeMap("BigInt", "System.Int64"));
				meta.AddTypeMap("Medianame", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Templateid", new esTypeMap("BigInt", "System.Int64"));
				meta.AddTypeMap("Templatename", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Zoneid", new esTypeMap("BigInt", "System.Int64"));
				meta.AddTypeMap("Zonename", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Order", new esTypeMap("Int", "System.Int32"));			
				
				
				
				meta.Source = "LoopContent";
				meta.Destination = "LoopContent";
				
				meta.spInsert = "proc_LoopContentInsert";				
				meta.spUpdate = "proc_LoopContentUpdate";		
				meta.spDelete = "proc_LoopContentDelete";
				meta.spLoadAll = "proc_LoopContentLoadAll";
				meta.spLoadByPrimaryKey = "proc_LoopContentLoadByPrimaryKey";
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private LoopContentMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
