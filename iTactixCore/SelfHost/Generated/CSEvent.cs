
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
	/// Encapsulates the 'CSEvent' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[XmlType("CSEvent")]
	public partial class CSEvent : esCSEvent
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new CSEvent();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Int64 id)
		{
			var obj = new CSEvent();
			obj.Id = id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Int64 id, esSqlAccessType sqlAccessType)
		{
			var obj = new CSEvent();
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
	[XmlType("CSEventCollection")]
	public partial class CSEventCollection : esCSEventCollection, IEnumerable<CSEvent>
	{
		public CSEvent FindByPrimaryKey(System.Int64 id)
		{
			return this.SingleOrDefault(e => e.Id == id);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(CSEvent))]
		public class CSEventCollectionWCFPacket : esCollectionWCFPacket<CSEventCollection>
		{
			public static implicit operator CSEventCollection(CSEventCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator CSEventCollectionWCFPacket(CSEventCollection collection)
			{
				return new CSEventCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class CSEventQuery : esCSEventQuery
	{
		public CSEventQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "CSEventQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(CSEventQuery query)
		{
			return CSEventQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator CSEventQuery(string query)
		{
			return (CSEventQuery)CSEventQuery.SerializeHelper.FromXml(query, typeof(CSEventQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esCSEvent : esEntity
	{
		public esCSEvent()
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
			CSEventQuery query = new CSEventQuery();
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
		/// Maps to CSEvent.id
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int64? Id
		{
			get
			{
				return base.GetSystemInt64(CSEventMetadata.ColumnNames.Id);
			}
			
			set
			{
				if(base.SetSystemInt64(CSEventMetadata.ColumnNames.Id, value))
				{
					OnPropertyChanged(CSEventMetadata.PropertyNames.Id);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSEvent.screenlocation
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Screenlocation
		{
			get
			{
				return base.GetSystemString(CSEventMetadata.ColumnNames.Screenlocation);
			}
			
			set
			{
				if(base.SetSystemString(CSEventMetadata.ColumnNames.Screenlocation, value))
				{
					OnPropertyChanged(CSEventMetadata.PropertyNames.Screenlocation);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSEvent.title
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Title
		{
			get
			{
				return base.GetSystemString(CSEventMetadata.ColumnNames.Title);
			}
			
			set
			{
				if(base.SetSystemString(CSEventMetadata.ColumnNames.Title, value))
				{
					OnPropertyChanged(CSEventMetadata.PropertyNames.Title);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSEvent.description
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Description
		{
			get
			{
				return base.GetSystemString(CSEventMetadata.ColumnNames.Description);
			}
			
			set
			{
				if(base.SetSystemString(CSEventMetadata.ColumnNames.Description, value))
				{
					OnPropertyChanged(CSEventMetadata.PropertyNames.Description);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSEvent.customticker
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Customticker
		{
			get
			{
				return base.GetSystemString(CSEventMetadata.ColumnNames.Customticker);
			}
			
			set
			{
				if(base.SetSystemString(CSEventMetadata.ColumnNames.Customticker, value))
				{
					OnPropertyChanged(CSEventMetadata.PropertyNames.Customticker);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSEvent.eventstart
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.DateTime? Eventstart
		{
			get
			{
				return base.GetSystemDateTime(CSEventMetadata.ColumnNames.Eventstart);
			}
			
			set
			{
				if(base.SetSystemDateTime(CSEventMetadata.ColumnNames.Eventstart, value))
				{
					OnPropertyChanged(CSEventMetadata.PropertyNames.Eventstart);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSEvent.eventend
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.DateTime? Eventend
		{
			get
			{
				return base.GetSystemDateTime(CSEventMetadata.ColumnNames.Eventend);
			}
			
			set
			{
				if(base.SetSystemDateTime(CSEventMetadata.ColumnNames.Eventend, value))
				{
					OnPropertyChanged(CSEventMetadata.PropertyNames.Eventend);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSEvent.template
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Template
		{
			get
			{
				return base.GetSystemString(CSEventMetadata.ColumnNames.Template);
			}
			
			set
			{
				if(base.SetSystemString(CSEventMetadata.ColumnNames.Template, value))
				{
					OnPropertyChanged(CSEventMetadata.PropertyNames.Template);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSEvent.welcome
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Welcome
		{
			get
			{
				return base.GetSystemString(CSEventMetadata.ColumnNames.Welcome);
			}
			
			set
			{
				if(base.SetSystemString(CSEventMetadata.ColumnNames.Welcome, value))
				{
					OnPropertyChanged(CSEventMetadata.PropertyNames.Welcome);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSEvent.datetimecreated
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.DateTime? Datetimecreated
		{
			get
			{
				return base.GetSystemDateTime(CSEventMetadata.ColumnNames.Datetimecreated);
			}
			
			set
			{
				if(base.SetSystemDateTime(CSEventMetadata.ColumnNames.Datetimecreated, value))
				{
					OnPropertyChanged(CSEventMetadata.PropertyNames.Datetimecreated);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSEvent.createdby
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Createdby
		{
			get
			{
				return base.GetSystemString(CSEventMetadata.ColumnNames.Createdby);
			}
			
			set
			{
				if(base.SetSystemString(CSEventMetadata.ColumnNames.Createdby, value))
				{
					OnPropertyChanged(CSEventMetadata.PropertyNames.Createdby);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSEvent.eventbackground
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Eventbackground
		{
			get
			{
				return base.GetSystemString(CSEventMetadata.ColumnNames.Eventbackground);
			}
			
			set
			{
				if(base.SetSystemString(CSEventMetadata.ColumnNames.Eventbackground, value))
				{
					OnPropertyChanged(CSEventMetadata.PropertyNames.Eventbackground);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSEvent.eventlogofile
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Eventlogofile
		{
			get
			{
				return base.GetSystemString(CSEventMetadata.ColumnNames.Eventlogofile);
			}
			
			set
			{
				if(base.SetSystemString(CSEventMetadata.ColumnNames.Eventlogofile, value))
				{
					OnPropertyChanged(CSEventMetadata.PropertyNames.Eventlogofile);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSEvent.eventlogoblob
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Byte[] Eventlogoblob
		{
			get
			{
				return base.GetSystemByteArray(CSEventMetadata.ColumnNames.Eventlogoblob);
			}
			
			set
			{
				if(base.SetSystemByteArray(CSEventMetadata.ColumnNames.Eventlogoblob, value))
				{
					OnPropertyChanged(CSEventMetadata.PropertyNames.Eventlogoblob);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSEvent.backgroundmedialoop
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Backgroundmedialoop
		{
			get
			{
				return base.GetSystemString(CSEventMetadata.ColumnNames.Backgroundmedialoop);
			}
			
			set
			{
				if(base.SetSystemString(CSEventMetadata.ColumnNames.Backgroundmedialoop, value))
				{
					OnPropertyChanged(CSEventMetadata.PropertyNames.Backgroundmedialoop);
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
						case "Screenlocation": this.str().Screenlocation = (string)value; break;							
						case "Title": this.str().Title = (string)value; break;							
						case "Description": this.str().Description = (string)value; break;							
						case "Customticker": this.str().Customticker = (string)value; break;							
						case "Eventstart": this.str().Eventstart = (string)value; break;							
						case "Eventend": this.str().Eventend = (string)value; break;							
						case "Template": this.str().Template = (string)value; break;							
						case "Welcome": this.str().Welcome = (string)value; break;							
						case "Datetimecreated": this.str().Datetimecreated = (string)value; break;							
						case "Createdby": this.str().Createdby = (string)value; break;							
						case "Eventbackground": this.str().Eventbackground = (string)value; break;							
						case "Eventlogofile": this.str().Eventlogofile = (string)value; break;							
						case "Backgroundmedialoop": this.str().Backgroundmedialoop = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Id":
						
							if (value == null || value is System.Int64)
								this.Id = (System.Int64?)value;
								OnPropertyChanged(CSEventMetadata.PropertyNames.Id);
							break;
						
						case "Eventstart":
						
							if (value == null || value is System.DateTime)
								this.Eventstart = (System.DateTime?)value;
								OnPropertyChanged(CSEventMetadata.PropertyNames.Eventstart);
							break;
						
						case "Eventend":
						
							if (value == null || value is System.DateTime)
								this.Eventend = (System.DateTime?)value;
								OnPropertyChanged(CSEventMetadata.PropertyNames.Eventend);
							break;
						
						case "Datetimecreated":
						
							if (value == null || value is System.DateTime)
								this.Datetimecreated = (System.DateTime?)value;
								OnPropertyChanged(CSEventMetadata.PropertyNames.Datetimecreated);
							break;
						
						case "Eventlogoblob":
						
							if (value == null || value is System.Byte[])
								this.Eventlogoblob = (System.Byte[])value;
								OnPropertyChanged(CSEventMetadata.PropertyNames.Eventlogoblob);
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
			public esStrings(esCSEvent entity)
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
				
			public System.String Title
			{
				get
				{
					System.String data = entity.Title;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Title = null;
					else entity.Title = Convert.ToString(value);
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
				
			public System.String Customticker
			{
				get
				{
					System.String data = entity.Customticker;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Customticker = null;
					else entity.Customticker = Convert.ToString(value);
				}
			}
				
			public System.String Eventstart
			{
				get
				{
					System.DateTime? data = entity.Eventstart;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Eventstart = null;
					else entity.Eventstart = Convert.ToDateTime(value);
				}
			}
				
			public System.String Eventend
			{
				get
				{
					System.DateTime? data = entity.Eventend;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Eventend = null;
					else entity.Eventend = Convert.ToDateTime(value);
				}
			}
				
			public System.String Template
			{
				get
				{
					System.String data = entity.Template;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Template = null;
					else entity.Template = Convert.ToString(value);
				}
			}
				
			public System.String Welcome
			{
				get
				{
					System.String data = entity.Welcome;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Welcome = null;
					else entity.Welcome = Convert.ToString(value);
				}
			}
				
			public System.String Datetimecreated
			{
				get
				{
					System.DateTime? data = entity.Datetimecreated;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Datetimecreated = null;
					else entity.Datetimecreated = Convert.ToDateTime(value);
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
				
			public System.String Eventbackground
			{
				get
				{
					System.String data = entity.Eventbackground;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Eventbackground = null;
					else entity.Eventbackground = Convert.ToString(value);
				}
			}
				
			public System.String Eventlogofile
			{
				get
				{
					System.String data = entity.Eventlogofile;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Eventlogofile = null;
					else entity.Eventlogofile = Convert.ToString(value);
				}
			}
				
			public System.String Backgroundmedialoop
			{
				get
				{
					System.String data = entity.Backgroundmedialoop;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Backgroundmedialoop = null;
					else entity.Backgroundmedialoop = Convert.ToString(value);
				}
			}
			

			private esCSEvent entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return CSEventMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public CSEventQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CSEventQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(CSEventQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(CSEventQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private CSEventQuery query;		
	}



	[Serializable]
	abstract public partial class esCSEventCollection : esEntityCollection<CSEvent>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CSEventMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "CSEventCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public CSEventQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CSEventQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(CSEventQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CSEventQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(CSEventQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((CSEventQuery)query);
		}

		#endregion
		
		private CSEventQuery query;
	}



	[Serializable]
	abstract public partial class esCSEventQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return CSEventMetadata.Meta();
			}
		}	
		
		#region esQueryItems

		public esQueryItem Id
		{
			get { return new esQueryItem(this, CSEventMetadata.ColumnNames.Id, esSystemType.Int64); }
		} 
		
		public esQueryItem Screenlocation
		{
			get { return new esQueryItem(this, CSEventMetadata.ColumnNames.Screenlocation, esSystemType.String); }
		} 
		
		public esQueryItem Title
		{
			get { return new esQueryItem(this, CSEventMetadata.ColumnNames.Title, esSystemType.String); }
		} 
		
		public esQueryItem Description
		{
			get { return new esQueryItem(this, CSEventMetadata.ColumnNames.Description, esSystemType.String); }
		} 
		
		public esQueryItem Customticker
		{
			get { return new esQueryItem(this, CSEventMetadata.ColumnNames.Customticker, esSystemType.String); }
		} 
		
		public esQueryItem Eventstart
		{
			get { return new esQueryItem(this, CSEventMetadata.ColumnNames.Eventstart, esSystemType.DateTime); }
		} 
		
		public esQueryItem Eventend
		{
			get { return new esQueryItem(this, CSEventMetadata.ColumnNames.Eventend, esSystemType.DateTime); }
		} 
		
		public esQueryItem Template
		{
			get { return new esQueryItem(this, CSEventMetadata.ColumnNames.Template, esSystemType.String); }
		} 
		
		public esQueryItem Welcome
		{
			get { return new esQueryItem(this, CSEventMetadata.ColumnNames.Welcome, esSystemType.String); }
		} 
		
		public esQueryItem Datetimecreated
		{
			get { return new esQueryItem(this, CSEventMetadata.ColumnNames.Datetimecreated, esSystemType.DateTime); }
		} 
		
		public esQueryItem Createdby
		{
			get { return new esQueryItem(this, CSEventMetadata.ColumnNames.Createdby, esSystemType.String); }
		} 
		
		public esQueryItem Eventbackground
		{
			get { return new esQueryItem(this, CSEventMetadata.ColumnNames.Eventbackground, esSystemType.String); }
		} 
		
		public esQueryItem Eventlogofile
		{
			get { return new esQueryItem(this, CSEventMetadata.ColumnNames.Eventlogofile, esSystemType.String); }
		} 
		
		public esQueryItem Eventlogoblob
		{
			get { return new esQueryItem(this, CSEventMetadata.ColumnNames.Eventlogoblob, esSystemType.ByteArray); }
		} 
		
		public esQueryItem Backgroundmedialoop
		{
			get { return new esQueryItem(this, CSEventMetadata.ColumnNames.Backgroundmedialoop, esSystemType.String); }
		} 
		
		#endregion
		
	}


	
	public partial class CSEvent : esCSEvent
	{

		
		
	}
	



	[Serializable]
	public partial class CSEventMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CSEventMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CSEventMetadata.ColumnNames.Id, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = CSEventMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSEventMetadata.ColumnNames.Screenlocation, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = CSEventMetadata.PropertyNames.Screenlocation;
			c.CharacterMaxLength = 128;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSEventMetadata.ColumnNames.Title, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = CSEventMetadata.PropertyNames.Title;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSEventMetadata.ColumnNames.Description, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = CSEventMetadata.PropertyNames.Description;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSEventMetadata.ColumnNames.Customticker, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = CSEventMetadata.PropertyNames.Customticker;
			c.CharacterMaxLength = 1024;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSEventMetadata.ColumnNames.Eventstart, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CSEventMetadata.PropertyNames.Eventstart;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSEventMetadata.ColumnNames.Eventend, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CSEventMetadata.PropertyNames.Eventend;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSEventMetadata.ColumnNames.Template, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = CSEventMetadata.PropertyNames.Template;
			c.CharacterMaxLength = 128;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSEventMetadata.ColumnNames.Welcome, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = CSEventMetadata.PropertyNames.Welcome;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSEventMetadata.ColumnNames.Datetimecreated, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CSEventMetadata.PropertyNames.Datetimecreated;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSEventMetadata.ColumnNames.Createdby, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = CSEventMetadata.PropertyNames.Createdby;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSEventMetadata.ColumnNames.Eventbackground, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = CSEventMetadata.PropertyNames.Eventbackground;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSEventMetadata.ColumnNames.Eventlogofile, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = CSEventMetadata.PropertyNames.Eventlogofile;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSEventMetadata.ColumnNames.Eventlogoblob, 13, typeof(System.Byte[]), esSystemType.ByteArray);
			c.PropertyName = CSEventMetadata.PropertyNames.Eventlogoblob;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSEventMetadata.ColumnNames.Backgroundmedialoop, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = CSEventMetadata.PropertyNames.Backgroundmedialoop;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public CSEventMetadata Meta()
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
			 public const string Screenlocation = "screenlocation";
			 public const string Title = "title";
			 public const string Description = "description";
			 public const string Customticker = "customticker";
			 public const string Eventstart = "eventstart";
			 public const string Eventend = "eventend";
			 public const string Template = "template";
			 public const string Welcome = "welcome";
			 public const string Datetimecreated = "datetimecreated";
			 public const string Createdby = "createdby";
			 public const string Eventbackground = "eventbackground";
			 public const string Eventlogofile = "eventlogofile";
			 public const string Eventlogoblob = "eventlogoblob";
			 public const string Backgroundmedialoop = "backgroundmedialoop";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string Screenlocation = "Screenlocation";
			 public const string Title = "Title";
			 public const string Description = "Description";
			 public const string Customticker = "Customticker";
			 public const string Eventstart = "Eventstart";
			 public const string Eventend = "Eventend";
			 public const string Template = "Template";
			 public const string Welcome = "Welcome";
			 public const string Datetimecreated = "Datetimecreated";
			 public const string Createdby = "Createdby";
			 public const string Eventbackground = "Eventbackground";
			 public const string Eventlogofile = "Eventlogofile";
			 public const string Eventlogoblob = "Eventlogoblob";
			 public const string Backgroundmedialoop = "Backgroundmedialoop";
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
			lock (typeof(CSEventMetadata))
			{
				if(CSEventMetadata.mapDelegates == null)
				{
					CSEventMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (CSEventMetadata.meta == null)
				{
					CSEventMetadata.meta = new CSEventMetadata();
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
				meta.AddTypeMap("Screenlocation", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Title", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Description", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Customticker", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Eventstart", new esTypeMap("DateTime", "System.DateTime"));
				meta.AddTypeMap("Eventend", new esTypeMap("DateTime", "System.DateTime"));
				meta.AddTypeMap("Template", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Welcome", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Datetimecreated", new esTypeMap("DateTime", "System.DateTime"));
				meta.AddTypeMap("Createdby", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Eventbackground", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Eventlogofile", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Eventlogoblob", new esTypeMap("Image", "System.Byte[]"));
				meta.AddTypeMap("Backgroundmedialoop", new esTypeMap("VarChar", "System.String"));			
				
				
				
				meta.Source = "CSEvent";
				meta.Destination = "CSEvent";
				
				meta.spInsert = "proc_CSEventInsert";				
				meta.spUpdate = "proc_CSEventUpdate";		
				meta.spDelete = "proc_CSEventDelete";
				meta.spLoadAll = "proc_CSEventLoadAll";
				meta.spLoadByPrimaryKey = "proc_CSEventLoadByPrimaryKey";
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CSEventMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
