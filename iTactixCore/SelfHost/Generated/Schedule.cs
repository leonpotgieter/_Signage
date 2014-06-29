
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
	/// Encapsulates the 'schedule' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[XmlType("Schedule")]
	public partial class Schedule : esSchedule
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new Schedule();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Int64 id)
		{
			var obj = new Schedule();
			obj.Id = id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Int64 id, esSqlAccessType sqlAccessType)
		{
			var obj = new Schedule();
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
	[XmlType("ScheduleCollection")]
	public partial class ScheduleCollection : esScheduleCollection, IEnumerable<Schedule>
	{
		public Schedule FindByPrimaryKey(System.Int64 id)
		{
			return this.SingleOrDefault(e => e.Id == id);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(Schedule))]
		public class ScheduleCollectionWCFPacket : esCollectionWCFPacket<ScheduleCollection>
		{
			public static implicit operator ScheduleCollection(ScheduleCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator ScheduleCollectionWCFPacket(ScheduleCollection collection)
			{
				return new ScheduleCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class ScheduleQuery : esScheduleQuery
	{
		public ScheduleQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "ScheduleQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(ScheduleQuery query)
		{
			return ScheduleQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator ScheduleQuery(string query)
		{
			return (ScheduleQuery)ScheduleQuery.SerializeHelper.FromXml(query, typeof(ScheduleQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esSchedule : esEntity
	{
		public esSchedule()
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
			ScheduleQuery query = new ScheduleQuery();
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
		/// Maps to schedule.id
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int64? Id
		{
			get
			{
				return base.GetSystemInt64(ScheduleMetadata.ColumnNames.Id);
			}
			
			set
			{
				if(base.SetSystemInt64(ScheduleMetadata.ColumnNames.Id, value))
				{
					OnPropertyChanged(ScheduleMetadata.PropertyNames.Id);
				}
			}
		}		
		
		/// <summary>
		/// Maps to schedule.loopid
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int64? Loopid
		{
			get
			{
				return base.GetSystemInt64(ScheduleMetadata.ColumnNames.Loopid);
			}
			
			set
			{
				if(base.SetSystemInt64(ScheduleMetadata.ColumnNames.Loopid, value))
				{
					OnPropertyChanged(ScheduleMetadata.PropertyNames.Loopid);
				}
			}
		}		
		
		/// <summary>
		/// Maps to schedule.loopname
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Loopname
		{
			get
			{
				return base.GetSystemString(ScheduleMetadata.ColumnNames.Loopname);
			}
			
			set
			{
				if(base.SetSystemString(ScheduleMetadata.ColumnNames.Loopname, value))
				{
					OnPropertyChanged(ScheduleMetadata.PropertyNames.Loopname);
				}
			}
		}		
		
		/// <summary>
		/// Maps to schedule.groupid
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int64? Groupid
		{
			get
			{
				return base.GetSystemInt64(ScheduleMetadata.ColumnNames.Groupid);
			}
			
			set
			{
				if(base.SetSystemInt64(ScheduleMetadata.ColumnNames.Groupid, value))
				{
					OnPropertyChanged(ScheduleMetadata.PropertyNames.Groupid);
				}
			}
		}		
		
		/// <summary>
		/// Maps to schedule.groupname
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Groupname
		{
			get
			{
				return base.GetSystemString(ScheduleMetadata.ColumnNames.Groupname);
			}
			
			set
			{
				if(base.SetSystemString(ScheduleMetadata.ColumnNames.Groupname, value))
				{
					OnPropertyChanged(ScheduleMetadata.PropertyNames.Groupname);
				}
			}
		}		
		
		/// <summary>
		/// Maps to schedule.screenid
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int64? Screenid
		{
			get
			{
				return base.GetSystemInt64(ScheduleMetadata.ColumnNames.Screenid);
			}
			
			set
			{
				if(base.SetSystemInt64(ScheduleMetadata.ColumnNames.Screenid, value))
				{
					OnPropertyChanged(ScheduleMetadata.PropertyNames.Screenid);
				}
			}
		}		
		
		/// <summary>
		/// Maps to schedule.screenname
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Screenname
		{
			get
			{
				return base.GetSystemString(ScheduleMetadata.ColumnNames.Screenname);
			}
			
			set
			{
				if(base.SetSystemString(ScheduleMetadata.ColumnNames.Screenname, value))
				{
					OnPropertyChanged(ScheduleMetadata.PropertyNames.Screenname);
				}
			}
		}		
		
		/// <summary>
		/// Maps to schedule.loopttype
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Loopttype
		{
			get
			{
				return base.GetSystemString(ScheduleMetadata.ColumnNames.Loopttype);
			}
			
			set
			{
				if(base.SetSystemString(ScheduleMetadata.ColumnNames.Loopttype, value))
				{
					OnPropertyChanged(ScheduleMetadata.PropertyNames.Loopttype);
				}
			}
		}		
		
		/// <summary>
		/// Maps to schedule.loopstart
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.DateTime? Loopstart
		{
			get
			{
				return base.GetSystemDateTime(ScheduleMetadata.ColumnNames.Loopstart);
			}
			
			set
			{
				if(base.SetSystemDateTime(ScheduleMetadata.ColumnNames.Loopstart, value))
				{
					OnPropertyChanged(ScheduleMetadata.PropertyNames.Loopstart);
				}
			}
		}		
		
		/// <summary>
		/// Maps to schedule.loopend
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.DateTime? Loopend
		{
			get
			{
				return base.GetSystemDateTime(ScheduleMetadata.ColumnNames.Loopend);
			}
			
			set
			{
				if(base.SetSystemDateTime(ScheduleMetadata.ColumnNames.Loopend, value))
				{
					OnPropertyChanged(ScheduleMetadata.PropertyNames.Loopend);
				}
			}
		}		
		
		/// <summary>
		/// Maps to schedule.active
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Boolean? Active
		{
			get
			{
				return base.GetSystemBoolean(ScheduleMetadata.ColumnNames.Active);
			}
			
			set
			{
				if(base.SetSystemBoolean(ScheduleMetadata.ColumnNames.Active, value))
				{
					OnPropertyChanged(ScheduleMetadata.PropertyNames.Active);
				}
			}
		}		
		
		/// <summary>
		/// Maps to schedule.createdby
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Createdby
		{
			get
			{
				return base.GetSystemString(ScheduleMetadata.ColumnNames.Createdby);
			}
			
			set
			{
				if(base.SetSystemString(ScheduleMetadata.ColumnNames.Createdby, value))
				{
					OnPropertyChanged(ScheduleMetadata.PropertyNames.Createdby);
				}
			}
		}		
		
		/// <summary>
		/// Maps to schedule.createdon
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.DateTime? Createdon
		{
			get
			{
				return base.GetSystemDateTime(ScheduleMetadata.ColumnNames.Createdon);
			}
			
			set
			{
				if(base.SetSystemDateTime(ScheduleMetadata.ColumnNames.Createdon, value))
				{
					OnPropertyChanged(ScheduleMetadata.PropertyNames.Createdon);
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
						case "Groupid": this.str().Groupid = (string)value; break;							
						case "Groupname": this.str().Groupname = (string)value; break;							
						case "Screenid": this.str().Screenid = (string)value; break;							
						case "Screenname": this.str().Screenname = (string)value; break;							
						case "Loopttype": this.str().Loopttype = (string)value; break;							
						case "Loopstart": this.str().Loopstart = (string)value; break;							
						case "Loopend": this.str().Loopend = (string)value; break;							
						case "Active": this.str().Active = (string)value; break;							
						case "Createdby": this.str().Createdby = (string)value; break;							
						case "Createdon": this.str().Createdon = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Id":
						
							if (value == null || value is System.Int64)
								this.Id = (System.Int64?)value;
								OnPropertyChanged(ScheduleMetadata.PropertyNames.Id);
							break;
						
						case "Loopid":
						
							if (value == null || value is System.Int64)
								this.Loopid = (System.Int64?)value;
								OnPropertyChanged(ScheduleMetadata.PropertyNames.Loopid);
							break;
						
						case "Groupid":
						
							if (value == null || value is System.Int64)
								this.Groupid = (System.Int64?)value;
								OnPropertyChanged(ScheduleMetadata.PropertyNames.Groupid);
							break;
						
						case "Screenid":
						
							if (value == null || value is System.Int64)
								this.Screenid = (System.Int64?)value;
								OnPropertyChanged(ScheduleMetadata.PropertyNames.Screenid);
							break;
						
						case "Loopstart":
						
							if (value == null || value is System.DateTime)
								this.Loopstart = (System.DateTime?)value;
								OnPropertyChanged(ScheduleMetadata.PropertyNames.Loopstart);
							break;
						
						case "Loopend":
						
							if (value == null || value is System.DateTime)
								this.Loopend = (System.DateTime?)value;
								OnPropertyChanged(ScheduleMetadata.PropertyNames.Loopend);
							break;
						
						case "Active":
						
							if (value == null || value is System.Boolean)
								this.Active = (System.Boolean?)value;
								OnPropertyChanged(ScheduleMetadata.PropertyNames.Active);
							break;
						
						case "Createdon":
						
							if (value == null || value is System.DateTime)
								this.Createdon = (System.DateTime?)value;
								OnPropertyChanged(ScheduleMetadata.PropertyNames.Createdon);
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
			public esStrings(esSchedule entity)
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
				
			public System.String Groupid
			{
				get
				{
					System.Int64? data = entity.Groupid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Groupid = null;
					else entity.Groupid = Convert.ToInt64(value);
				}
			}
				
			public System.String Groupname
			{
				get
				{
					System.String data = entity.Groupname;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Groupname = null;
					else entity.Groupname = Convert.ToString(value);
				}
			}
				
			public System.String Screenid
			{
				get
				{
					System.Int64? data = entity.Screenid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Screenid = null;
					else entity.Screenid = Convert.ToInt64(value);
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
				
			public System.String Loopttype
			{
				get
				{
					System.String data = entity.Loopttype;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Loopttype = null;
					else entity.Loopttype = Convert.ToString(value);
				}
			}
				
			public System.String Loopstart
			{
				get
				{
					System.DateTime? data = entity.Loopstart;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Loopstart = null;
					else entity.Loopstart = Convert.ToDateTime(value);
				}
			}
				
			public System.String Loopend
			{
				get
				{
					System.DateTime? data = entity.Loopend;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Loopend = null;
					else entity.Loopend = Convert.ToDateTime(value);
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
			

			private esSchedule entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return ScheduleMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public ScheduleQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ScheduleQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(ScheduleQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(ScheduleQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private ScheduleQuery query;		
	}



	[Serializable]
	abstract public partial class esScheduleCollection : esEntityCollection<Schedule>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ScheduleMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "ScheduleCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public ScheduleQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ScheduleQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(ScheduleQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ScheduleQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(ScheduleQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((ScheduleQuery)query);
		}

		#endregion
		
		private ScheduleQuery query;
	}



	[Serializable]
	abstract public partial class esScheduleQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ScheduleMetadata.Meta();
			}
		}	
		
		#region esQueryItems

		public esQueryItem Id
		{
			get { return new esQueryItem(this, ScheduleMetadata.ColumnNames.Id, esSystemType.Int64); }
		} 
		
		public esQueryItem Loopid
		{
			get { return new esQueryItem(this, ScheduleMetadata.ColumnNames.Loopid, esSystemType.Int64); }
		} 
		
		public esQueryItem Loopname
		{
			get { return new esQueryItem(this, ScheduleMetadata.ColumnNames.Loopname, esSystemType.String); }
		} 
		
		public esQueryItem Groupid
		{
			get { return new esQueryItem(this, ScheduleMetadata.ColumnNames.Groupid, esSystemType.Int64); }
		} 
		
		public esQueryItem Groupname
		{
			get { return new esQueryItem(this, ScheduleMetadata.ColumnNames.Groupname, esSystemType.String); }
		} 
		
		public esQueryItem Screenid
		{
			get { return new esQueryItem(this, ScheduleMetadata.ColumnNames.Screenid, esSystemType.Int64); }
		} 
		
		public esQueryItem Screenname
		{
			get { return new esQueryItem(this, ScheduleMetadata.ColumnNames.Screenname, esSystemType.String); }
		} 
		
		public esQueryItem Loopttype
		{
			get { return new esQueryItem(this, ScheduleMetadata.ColumnNames.Loopttype, esSystemType.String); }
		} 
		
		public esQueryItem Loopstart
		{
			get { return new esQueryItem(this, ScheduleMetadata.ColumnNames.Loopstart, esSystemType.DateTime); }
		} 
		
		public esQueryItem Loopend
		{
			get { return new esQueryItem(this, ScheduleMetadata.ColumnNames.Loopend, esSystemType.DateTime); }
		} 
		
		public esQueryItem Active
		{
			get { return new esQueryItem(this, ScheduleMetadata.ColumnNames.Active, esSystemType.Boolean); }
		} 
		
		public esQueryItem Createdby
		{
			get { return new esQueryItem(this, ScheduleMetadata.ColumnNames.Createdby, esSystemType.String); }
		} 
		
		public esQueryItem Createdon
		{
			get { return new esQueryItem(this, ScheduleMetadata.ColumnNames.Createdon, esSystemType.DateTime); }
		} 
		
		#endregion
		
	}


	
	public partial class Schedule : esSchedule
	{

		
		
	}
	



	[Serializable]
	public partial class ScheduleMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ScheduleMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ScheduleMetadata.ColumnNames.Id, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = ScheduleMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ScheduleMetadata.ColumnNames.Loopid, 1, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = ScheduleMetadata.PropertyNames.Loopid;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ScheduleMetadata.ColumnNames.Loopname, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ScheduleMetadata.PropertyNames.Loopname;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ScheduleMetadata.ColumnNames.Groupid, 3, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = ScheduleMetadata.PropertyNames.Groupid;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ScheduleMetadata.ColumnNames.Groupname, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ScheduleMetadata.PropertyNames.Groupname;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ScheduleMetadata.ColumnNames.Screenid, 5, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = ScheduleMetadata.PropertyNames.Screenid;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ScheduleMetadata.ColumnNames.Screenname, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = ScheduleMetadata.PropertyNames.Screenname;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ScheduleMetadata.ColumnNames.Loopttype, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = ScheduleMetadata.PropertyNames.Loopttype;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ScheduleMetadata.ColumnNames.Loopstart, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ScheduleMetadata.PropertyNames.Loopstart;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ScheduleMetadata.ColumnNames.Loopend, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ScheduleMetadata.PropertyNames.Loopend;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ScheduleMetadata.ColumnNames.Active, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ScheduleMetadata.PropertyNames.Active;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ScheduleMetadata.ColumnNames.Createdby, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = ScheduleMetadata.PropertyNames.Createdby;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ScheduleMetadata.ColumnNames.Createdon, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ScheduleMetadata.PropertyNames.Createdon;
			c.IsNullable = true;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public ScheduleMetadata Meta()
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
			 public const string Groupid = "groupid";
			 public const string Groupname = "groupname";
			 public const string Screenid = "screenid";
			 public const string Screenname = "screenname";
			 public const string Loopttype = "loopttype";
			 public const string Loopstart = "loopstart";
			 public const string Loopend = "loopend";
			 public const string Active = "active";
			 public const string Createdby = "createdby";
			 public const string Createdon = "createdon";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string Loopid = "Loopid";
			 public const string Loopname = "Loopname";
			 public const string Groupid = "Groupid";
			 public const string Groupname = "Groupname";
			 public const string Screenid = "Screenid";
			 public const string Screenname = "Screenname";
			 public const string Loopttype = "Loopttype";
			 public const string Loopstart = "Loopstart";
			 public const string Loopend = "Loopend";
			 public const string Active = "Active";
			 public const string Createdby = "Createdby";
			 public const string Createdon = "Createdon";
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
			lock (typeof(ScheduleMetadata))
			{
				if(ScheduleMetadata.mapDelegates == null)
				{
					ScheduleMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ScheduleMetadata.meta == null)
				{
					ScheduleMetadata.meta = new ScheduleMetadata();
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
				meta.AddTypeMap("Groupid", new esTypeMap("BigInt", "System.Int64"));
				meta.AddTypeMap("Groupname", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Screenid", new esTypeMap("BigInt", "System.Int64"));
				meta.AddTypeMap("Screenname", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Loopttype", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Loopstart", new esTypeMap("DateTime", "System.DateTime"));
				meta.AddTypeMap("Loopend", new esTypeMap("DateTime", "System.DateTime"));
				meta.AddTypeMap("Active", new esTypeMap("Bit", "System.Boolean"));
				meta.AddTypeMap("Createdby", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Createdon", new esTypeMap("DateTime", "System.DateTime"));			
				
				
				
				meta.Source = "schedule";
				meta.Destination = "schedule";
				
				meta.spInsert = "proc_scheduleInsert";				
				meta.spUpdate = "proc_scheduleUpdate";		
				meta.spDelete = "proc_scheduleDelete";
				meta.spLoadAll = "proc_scheduleLoadAll";
				meta.spLoadByPrimaryKey = "proc_scheduleLoadByPrimaryKey";
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ScheduleMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
