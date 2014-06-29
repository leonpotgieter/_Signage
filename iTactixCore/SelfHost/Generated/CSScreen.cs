
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
	/// Encapsulates the 'CSScreen' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[XmlType("CSScreen")]
	public partial class CSScreen : esCSScreen
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new CSScreen();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Int64 id)
		{
			var obj = new CSScreen();
			obj.Id = id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Int64 id, esSqlAccessType sqlAccessType)
		{
			var obj = new CSScreen();
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
	[XmlType("CSScreenCollection")]
	public partial class CSScreenCollection : esCSScreenCollection, IEnumerable<CSScreen>
	{
		public CSScreen FindByPrimaryKey(System.Int64 id)
		{
			return this.SingleOrDefault(e => e.Id == id);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(CSScreen))]
		public class CSScreenCollectionWCFPacket : esCollectionWCFPacket<CSScreenCollection>
		{
			public static implicit operator CSScreenCollection(CSScreenCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator CSScreenCollectionWCFPacket(CSScreenCollection collection)
			{
				return new CSScreenCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class CSScreenQuery : esCSScreenQuery
	{
		public CSScreenQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "CSScreenQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(CSScreenQuery query)
		{
			return CSScreenQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator CSScreenQuery(string query)
		{
			return (CSScreenQuery)CSScreenQuery.SerializeHelper.FromXml(query, typeof(CSScreenQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esCSScreen : esEntity
	{
		public esCSScreen()
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
			CSScreenQuery query = new CSScreenQuery();
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
		/// Maps to CSScreen.id
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int64? Id
		{
			get
			{
				return base.GetSystemInt64(CSScreenMetadata.ColumnNames.Id);
			}
			
			set
			{
				if(base.SetSystemInt64(CSScreenMetadata.ColumnNames.Id, value))
				{
					OnPropertyChanged(CSScreenMetadata.PropertyNames.Id);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSScreen.computername
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Computername
		{
			get
			{
				return base.GetSystemString(CSScreenMetadata.ColumnNames.Computername);
			}
			
			set
			{
				if(base.SetSystemString(CSScreenMetadata.ColumnNames.Computername, value))
				{
					OnPropertyChanged(CSScreenMetadata.PropertyNames.Computername);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSScreen.computerlocation
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Computerlocation
		{
			get
			{
				return base.GetSystemString(CSScreenMetadata.ColumnNames.Computerlocation);
			}
			
			set
			{
				if(base.SetSystemString(CSScreenMetadata.ColumnNames.Computerlocation, value))
				{
					OnPropertyChanged(CSScreenMetadata.PropertyNames.Computerlocation);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSScreen.screennumber
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Screennumber
		{
			get
			{
				return base.GetSystemString(CSScreenMetadata.ColumnNames.Screennumber);
			}
			
			set
			{
				if(base.SetSystemString(CSScreenMetadata.ColumnNames.Screennumber, value))
				{
					OnPropertyChanged(CSScreenMetadata.PropertyNames.Screennumber);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSScreen.screenlocation
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Screenlocation
		{
			get
			{
				return base.GetSystemString(CSScreenMetadata.ColumnNames.Screenlocation);
			}
			
			set
			{
				if(base.SetSystemString(CSScreenMetadata.ColumnNames.Screenlocation, value))
				{
					OnPropertyChanged(CSScreenMetadata.PropertyNames.Screenlocation);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSScreen.isdirectional
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? Isdirectional
		{
			get
			{
				return base.GetSystemInt32(CSScreenMetadata.ColumnNames.Isdirectional);
			}
			
			set
			{
				if(base.SetSystemInt32(CSScreenMetadata.ColumnNames.Isdirectional, value))
				{
					OnPropertyChanged(CSScreenMetadata.PropertyNames.Isdirectional);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSScreen.lastqueriedcore
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.DateTime? Lastqueriedcore
		{
			get
			{
				return base.GetSystemDateTime(CSScreenMetadata.ColumnNames.Lastqueriedcore);
			}
			
			set
			{
				if(base.SetSystemDateTime(CSScreenMetadata.ColumnNames.Lastqueriedcore, value))
				{
					OnPropertyChanged(CSScreenMetadata.PropertyNames.Lastqueriedcore);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSScreen.defaulttemplate
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int64? Defaulttemplate
		{
			get
			{
				return base.GetSystemInt64(CSScreenMetadata.ColumnNames.Defaulttemplate);
			}
			
			set
			{
				if(base.SetSystemInt64(CSScreenMetadata.ColumnNames.Defaulttemplate, value))
				{
					OnPropertyChanged(CSScreenMetadata.PropertyNames.Defaulttemplate);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSScreen.thumbnail
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Byte[] Thumbnail
		{
			get
			{
				return base.GetSystemByteArray(CSScreenMetadata.ColumnNames.Thumbnail);
			}
			
			set
			{
				if(base.SetSystemByteArray(CSScreenMetadata.ColumnNames.Thumbnail, value))
				{
					OnPropertyChanged(CSScreenMetadata.PropertyNames.Thumbnail);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSScreen.thumbnaildatetime
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.DateTime? Thumbnaildatetime
		{
			get
			{
				return base.GetSystemDateTime(CSScreenMetadata.ColumnNames.Thumbnaildatetime);
			}
			
			set
			{
				if(base.SetSystemDateTime(CSScreenMetadata.ColumnNames.Thumbnaildatetime, value))
				{
					OnPropertyChanged(CSScreenMetadata.PropertyNames.Thumbnaildatetime);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSScreen.enabled
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? Enabled
		{
			get
			{
				return base.GetSystemInt32(CSScreenMetadata.ColumnNames.Enabled);
			}
			
			set
			{
				if(base.SetSystemInt32(CSScreenMetadata.ColumnNames.Enabled, value))
				{
					OnPropertyChanged(CSScreenMetadata.PropertyNames.Enabled);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSScreen.hardwareid
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Hardwareid
		{
			get
			{
				return base.GetSystemString(CSScreenMetadata.ColumnNames.Hardwareid);
			}
			
			set
			{
				if(base.SetSystemString(CSScreenMetadata.ColumnNames.Hardwareid, value))
				{
					OnPropertyChanged(CSScreenMetadata.PropertyNames.Hardwareid);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSScreen.idlemode
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Idlemode
		{
			get
			{
				return base.GetSystemString(CSScreenMetadata.ColumnNames.Idlemode);
			}
			
			set
			{
				if(base.SetSystemString(CSScreenMetadata.ColumnNames.Idlemode, value))
				{
					OnPropertyChanged(CSScreenMetadata.PropertyNames.Idlemode);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSScreen.medialoop
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Medialoop
		{
			get
			{
				return base.GetSystemString(CSScreenMetadata.ColumnNames.Medialoop);
			}
			
			set
			{
				if(base.SetSystemString(CSScreenMetadata.ColumnNames.Medialoop, value))
				{
					OnPropertyChanged(CSScreenMetadata.PropertyNames.Medialoop);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSScreen.volume
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? Volume
		{
			get
			{
				return base.GetSystemInt32(CSScreenMetadata.ColumnNames.Volume);
			}
			
			set
			{
				if(base.SetSystemInt32(CSScreenMetadata.ColumnNames.Volume, value))
				{
					OnPropertyChanged(CSScreenMetadata.PropertyNames.Volume);
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
						case "Computername": this.str().Computername = (string)value; break;							
						case "Computerlocation": this.str().Computerlocation = (string)value; break;							
						case "Screennumber": this.str().Screennumber = (string)value; break;							
						case "Screenlocation": this.str().Screenlocation = (string)value; break;							
						case "Isdirectional": this.str().Isdirectional = (string)value; break;							
						case "Lastqueriedcore": this.str().Lastqueriedcore = (string)value; break;							
						case "Defaulttemplate": this.str().Defaulttemplate = (string)value; break;							
						case "Thumbnaildatetime": this.str().Thumbnaildatetime = (string)value; break;							
						case "Enabled": this.str().Enabled = (string)value; break;							
						case "Hardwareid": this.str().Hardwareid = (string)value; break;							
						case "Idlemode": this.str().Idlemode = (string)value; break;							
						case "Medialoop": this.str().Medialoop = (string)value; break;							
						case "Volume": this.str().Volume = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Id":
						
							if (value == null || value is System.Int64)
								this.Id = (System.Int64?)value;
								OnPropertyChanged(CSScreenMetadata.PropertyNames.Id);
							break;
						
						case "Isdirectional":
						
							if (value == null || value is System.Int32)
								this.Isdirectional = (System.Int32?)value;
								OnPropertyChanged(CSScreenMetadata.PropertyNames.Isdirectional);
							break;
						
						case "Lastqueriedcore":
						
							if (value == null || value is System.DateTime)
								this.Lastqueriedcore = (System.DateTime?)value;
								OnPropertyChanged(CSScreenMetadata.PropertyNames.Lastqueriedcore);
							break;
						
						case "Defaulttemplate":
						
							if (value == null || value is System.Int64)
								this.Defaulttemplate = (System.Int64?)value;
								OnPropertyChanged(CSScreenMetadata.PropertyNames.Defaulttemplate);
							break;
						
						case "Thumbnail":
						
							if (value == null || value is System.Byte[])
								this.Thumbnail = (System.Byte[])value;
								OnPropertyChanged(CSScreenMetadata.PropertyNames.Thumbnail);
							break;
						
						case "Thumbnaildatetime":
						
							if (value == null || value is System.DateTime)
								this.Thumbnaildatetime = (System.DateTime?)value;
								OnPropertyChanged(CSScreenMetadata.PropertyNames.Thumbnaildatetime);
							break;
						
						case "Enabled":
						
							if (value == null || value is System.Int32)
								this.Enabled = (System.Int32?)value;
								OnPropertyChanged(CSScreenMetadata.PropertyNames.Enabled);
							break;
						
						case "Volume":
						
							if (value == null || value is System.Int32)
								this.Volume = (System.Int32?)value;
								OnPropertyChanged(CSScreenMetadata.PropertyNames.Volume);
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
			public esStrings(esCSScreen entity)
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
				
			public System.String Computername
			{
				get
				{
					System.String data = entity.Computername;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Computername = null;
					else entity.Computername = Convert.ToString(value);
				}
			}
				
			public System.String Computerlocation
			{
				get
				{
					System.String data = entity.Computerlocation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Computerlocation = null;
					else entity.Computerlocation = Convert.ToString(value);
				}
			}
				
			public System.String Screennumber
			{
				get
				{
					System.String data = entity.Screennumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Screennumber = null;
					else entity.Screennumber = Convert.ToString(value);
				}
			}
				
			public System.String Screenlocation
			{
				get
				{
					System.String data = entity.Screenlocation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Screenlocation = null;
					else entity.Screenlocation = Convert.ToString(value);
				}
			}
				
			public System.String Isdirectional
			{
				get
				{
					System.Int32? data = entity.Isdirectional;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Isdirectional = null;
					else entity.Isdirectional = Convert.ToInt32(value);
				}
			}
				
			public System.String Lastqueriedcore
			{
				get
				{
					System.DateTime? data = entity.Lastqueriedcore;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Lastqueriedcore = null;
					else entity.Lastqueriedcore = Convert.ToDateTime(value);
				}
			}
				
			public System.String Defaulttemplate
			{
				get
				{
					System.Int64? data = entity.Defaulttemplate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Defaulttemplate = null;
					else entity.Defaulttemplate = Convert.ToInt64(value);
				}
			}
				
			public System.String Thumbnaildatetime
			{
				get
				{
					System.DateTime? data = entity.Thumbnaildatetime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Thumbnaildatetime = null;
					else entity.Thumbnaildatetime = Convert.ToDateTime(value);
				}
			}
				
			public System.String Enabled
			{
				get
				{
					System.Int32? data = entity.Enabled;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Enabled = null;
					else entity.Enabled = Convert.ToInt32(value);
				}
			}
				
			public System.String Hardwareid
			{
				get
				{
					System.String data = entity.Hardwareid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Hardwareid = null;
					else entity.Hardwareid = Convert.ToString(value);
				}
			}
				
			public System.String Idlemode
			{
				get
				{
					System.String data = entity.Idlemode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Idlemode = null;
					else entity.Idlemode = Convert.ToString(value);
				}
			}
				
			public System.String Medialoop
			{
				get
				{
					System.String data = entity.Medialoop;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Medialoop = null;
					else entity.Medialoop = Convert.ToString(value);
				}
			}
				
			public System.String Volume
			{
				get
				{
					System.Int32? data = entity.Volume;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Volume = null;
					else entity.Volume = Convert.ToInt32(value);
				}
			}
			

			private esCSScreen entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return CSScreenMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public CSScreenQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CSScreenQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(CSScreenQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(CSScreenQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private CSScreenQuery query;		
	}



	[Serializable]
	abstract public partial class esCSScreenCollection : esEntityCollection<CSScreen>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CSScreenMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "CSScreenCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public CSScreenQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CSScreenQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(CSScreenQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CSScreenQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(CSScreenQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((CSScreenQuery)query);
		}

		#endregion
		
		private CSScreenQuery query;
	}



	[Serializable]
	abstract public partial class esCSScreenQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return CSScreenMetadata.Meta();
			}
		}	
		
		#region esQueryItems

		public esQueryItem Id
		{
			get { return new esQueryItem(this, CSScreenMetadata.ColumnNames.Id, esSystemType.Int64); }
		} 
		
		public esQueryItem Computername
		{
			get { return new esQueryItem(this, CSScreenMetadata.ColumnNames.Computername, esSystemType.String); }
		} 
		
		public esQueryItem Computerlocation
		{
			get { return new esQueryItem(this, CSScreenMetadata.ColumnNames.Computerlocation, esSystemType.String); }
		} 
		
		public esQueryItem Screennumber
		{
			get { return new esQueryItem(this, CSScreenMetadata.ColumnNames.Screennumber, esSystemType.String); }
		} 
		
		public esQueryItem Screenlocation
		{
			get { return new esQueryItem(this, CSScreenMetadata.ColumnNames.Screenlocation, esSystemType.String); }
		} 
		
		public esQueryItem Isdirectional
		{
			get { return new esQueryItem(this, CSScreenMetadata.ColumnNames.Isdirectional, esSystemType.Int32); }
		} 
		
		public esQueryItem Lastqueriedcore
		{
			get { return new esQueryItem(this, CSScreenMetadata.ColumnNames.Lastqueriedcore, esSystemType.DateTime); }
		} 
		
		public esQueryItem Defaulttemplate
		{
			get { return new esQueryItem(this, CSScreenMetadata.ColumnNames.Defaulttemplate, esSystemType.Int64); }
		} 
		
		public esQueryItem Thumbnail
		{
			get { return new esQueryItem(this, CSScreenMetadata.ColumnNames.Thumbnail, esSystemType.ByteArray); }
		} 
		
		public esQueryItem Thumbnaildatetime
		{
			get { return new esQueryItem(this, CSScreenMetadata.ColumnNames.Thumbnaildatetime, esSystemType.DateTime); }
		} 
		
		public esQueryItem Enabled
		{
			get { return new esQueryItem(this, CSScreenMetadata.ColumnNames.Enabled, esSystemType.Int32); }
		} 
		
		public esQueryItem Hardwareid
		{
			get { return new esQueryItem(this, CSScreenMetadata.ColumnNames.Hardwareid, esSystemType.String); }
		} 
		
		public esQueryItem Idlemode
		{
			get { return new esQueryItem(this, CSScreenMetadata.ColumnNames.Idlemode, esSystemType.String); }
		} 
		
		public esQueryItem Medialoop
		{
			get { return new esQueryItem(this, CSScreenMetadata.ColumnNames.Medialoop, esSystemType.String); }
		} 
		
		public esQueryItem Volume
		{
			get { return new esQueryItem(this, CSScreenMetadata.ColumnNames.Volume, esSystemType.Int32); }
		} 
		
		#endregion
		
	}


	
	public partial class CSScreen : esCSScreen
	{

		
		
	}
	



	[Serializable]
	public partial class CSScreenMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CSScreenMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CSScreenMetadata.ColumnNames.Id, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = CSScreenMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSScreenMetadata.ColumnNames.Computername, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = CSScreenMetadata.PropertyNames.Computername;
			c.CharacterMaxLength = 64;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSScreenMetadata.ColumnNames.Computerlocation, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = CSScreenMetadata.PropertyNames.Computerlocation;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSScreenMetadata.ColumnNames.Screennumber, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = CSScreenMetadata.PropertyNames.Screennumber;
			c.CharacterMaxLength = 16;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSScreenMetadata.ColumnNames.Screenlocation, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = CSScreenMetadata.PropertyNames.Screenlocation;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSScreenMetadata.ColumnNames.Isdirectional, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CSScreenMetadata.PropertyNames.Isdirectional;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSScreenMetadata.ColumnNames.Lastqueriedcore, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CSScreenMetadata.PropertyNames.Lastqueriedcore;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSScreenMetadata.ColumnNames.Defaulttemplate, 7, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = CSScreenMetadata.PropertyNames.Defaulttemplate;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSScreenMetadata.ColumnNames.Thumbnail, 8, typeof(System.Byte[]), esSystemType.ByteArray);
			c.PropertyName = CSScreenMetadata.PropertyNames.Thumbnail;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSScreenMetadata.ColumnNames.Thumbnaildatetime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CSScreenMetadata.PropertyNames.Thumbnaildatetime;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSScreenMetadata.ColumnNames.Enabled, 10, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CSScreenMetadata.PropertyNames.Enabled;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSScreenMetadata.ColumnNames.Hardwareid, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = CSScreenMetadata.PropertyNames.Hardwareid;
			c.CharacterMaxLength = 128;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSScreenMetadata.ColumnNames.Idlemode, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = CSScreenMetadata.PropertyNames.Idlemode;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSScreenMetadata.ColumnNames.Medialoop, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = CSScreenMetadata.PropertyNames.Medialoop;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSScreenMetadata.ColumnNames.Volume, 14, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CSScreenMetadata.PropertyNames.Volume;
			c.IsNullable = true;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public CSScreenMetadata Meta()
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
			 public const string Computername = "computername";
			 public const string Computerlocation = "computerlocation";
			 public const string Screennumber = "screennumber";
			 public const string Screenlocation = "screenlocation";
			 public const string Isdirectional = "isdirectional";
			 public const string Lastqueriedcore = "lastqueriedcore";
			 public const string Defaulttemplate = "defaulttemplate";
			 public const string Thumbnail = "thumbnail";
			 public const string Thumbnaildatetime = "thumbnaildatetime";
			 public const string Enabled = "enabled";
			 public const string Hardwareid = "hardwareid";
			 public const string Idlemode = "idlemode";
			 public const string Medialoop = "medialoop";
			 public const string Volume = "volume";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string Computername = "Computername";
			 public const string Computerlocation = "Computerlocation";
			 public const string Screennumber = "Screennumber";
			 public const string Screenlocation = "Screenlocation";
			 public const string Isdirectional = "Isdirectional";
			 public const string Lastqueriedcore = "Lastqueriedcore";
			 public const string Defaulttemplate = "Defaulttemplate";
			 public const string Thumbnail = "Thumbnail";
			 public const string Thumbnaildatetime = "Thumbnaildatetime";
			 public const string Enabled = "Enabled";
			 public const string Hardwareid = "Hardwareid";
			 public const string Idlemode = "Idlemode";
			 public const string Medialoop = "Medialoop";
			 public const string Volume = "Volume";
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
			lock (typeof(CSScreenMetadata))
			{
				if(CSScreenMetadata.mapDelegates == null)
				{
					CSScreenMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (CSScreenMetadata.meta == null)
				{
					CSScreenMetadata.meta = new CSScreenMetadata();
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
				meta.AddTypeMap("Computername", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Computerlocation", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Screennumber", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Screenlocation", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Isdirectional", new esTypeMap("Int", "System.Int32"));
				meta.AddTypeMap("Lastqueriedcore", new esTypeMap("DateTime", "System.DateTime"));
				meta.AddTypeMap("Defaulttemplate", new esTypeMap("BigInt", "System.Int64"));
				meta.AddTypeMap("Thumbnail", new esTypeMap("Image", "System.Byte[]"));
				meta.AddTypeMap("Thumbnaildatetime", new esTypeMap("DateTime", "System.DateTime"));
				meta.AddTypeMap("Enabled", new esTypeMap("Int", "System.Int32"));
				meta.AddTypeMap("Hardwareid", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Idlemode", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Medialoop", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Volume", new esTypeMap("Int", "System.Int32"));			
				
				
				
				meta.Source = "CSScreen";
				meta.Destination = "CSScreen";
				
				meta.spInsert = "proc_CSScreenInsert";				
				meta.spUpdate = "proc_CSScreenUpdate";		
				meta.spDelete = "proc_CSScreenDelete";
				meta.spLoadAll = "proc_CSScreenLoadAll";
				meta.spLoadByPrimaryKey = "proc_CSScreenLoadByPrimaryKey";
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CSScreenMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
