
/*
===============================================================================
                    EntitySpaces 2011 by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2011.1.0530.0
EntitySpaces Driver  : VistaDB4
Date Generated       : 2011/09/01 09:38:09 AM
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
	/// Encapsulates the 'Content' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[XmlType("Content")]
	public partial class Content : esContent
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new Content();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Int64 id)
		{
			var obj = new Content();
			obj.Id = id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Int64 id, esSqlAccessType sqlAccessType)
		{
			var obj = new Content();
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
	[XmlType("ContentCollection")]
	public partial class ContentCollection : esContentCollection, IEnumerable<Content>
	{
		public Content FindByPrimaryKey(System.Int64 id)
		{
			return this.SingleOrDefault(e => e.Id == id);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(Content))]
		public class ContentCollectionWCFPacket : esCollectionWCFPacket<ContentCollection>
		{
			public static implicit operator ContentCollection(ContentCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator ContentCollectionWCFPacket(ContentCollection collection)
			{
				return new ContentCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class ContentQuery : esContentQuery
	{
		public ContentQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "ContentQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(ContentQuery query)
		{
			return ContentQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator ContentQuery(string query)
		{
			return (ContentQuery)ContentQuery.SerializeHelper.FromXml(query, typeof(ContentQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esContent : esEntity
	{
		public esContent()
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
			ContentQuery query = new ContentQuery();
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
		/// Maps to Content.id
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int64? Id
		{
			get
			{
				return base.GetSystemInt64(ContentMetadata.ColumnNames.Id);
			}
			
			set
			{
				if(base.SetSystemInt64(ContentMetadata.ColumnNames.Id, value))
				{
					OnPropertyChanged(ContentMetadata.PropertyNames.Id);
				}
			}
		}		
		
		/// <summary>
		/// Maps to Content.contenttype
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Contenttype
		{
			get
			{
				return base.GetSystemString(ContentMetadata.ColumnNames.Contenttype);
			}
			
			set
			{
				if(base.SetSystemString(ContentMetadata.ColumnNames.Contenttype, value))
				{
					OnPropertyChanged(ContentMetadata.PropertyNames.Contenttype);
				}
			}
		}		
		
		/// <summary>
		/// Maps to Content.name
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Name
		{
			get
			{
				return base.GetSystemString(ContentMetadata.ColumnNames.Name);
			}
			
			set
			{
				if(base.SetSystemString(ContentMetadata.ColumnNames.Name, value))
				{
					OnPropertyChanged(ContentMetadata.PropertyNames.Name);
				}
			}
		}		
		
		/// <summary>
		/// Maps to Content.description
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Description
		{
			get
			{
				return base.GetSystemString(ContentMetadata.ColumnNames.Description);
			}
			
			set
			{
				if(base.SetSystemString(ContentMetadata.ColumnNames.Description, value))
				{
					OnPropertyChanged(ContentMetadata.PropertyNames.Description);
				}
			}
		}		
		
		/// <summary>
		/// Maps to Content.filelocation
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Filelocation
		{
			get
			{
				return base.GetSystemString(ContentMetadata.ColumnNames.Filelocation);
			}
			
			set
			{
				if(base.SetSystemString(ContentMetadata.ColumnNames.Filelocation, value))
				{
					OnPropertyChanged(ContentMetadata.PropertyNames.Filelocation);
				}
			}
		}		
		
		/// <summary>
		/// Maps to Content.filesize
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int64? Filesize
		{
			get
			{
				return base.GetSystemInt64(ContentMetadata.ColumnNames.Filesize);
			}
			
			set
			{
				if(base.SetSystemInt64(ContentMetadata.ColumnNames.Filesize, value))
				{
					OnPropertyChanged(ContentMetadata.PropertyNames.Filesize);
				}
			}
		}		
		
		/// <summary>
		/// Maps to Content.expiry
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.DateTime? Expiry
		{
			get
			{
				return base.GetSystemDateTime(ContentMetadata.ColumnNames.Expiry);
			}
			
			set
			{
				if(base.SetSystemDateTime(ContentMetadata.ColumnNames.Expiry, value))
				{
					OnPropertyChanged(ContentMetadata.PropertyNames.Expiry);
				}
			}
		}		
		
		/// <summary>
		/// Maps to Content.importdate
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.DateTime? Importdate
		{
			get
			{
				return base.GetSystemDateTime(ContentMetadata.ColumnNames.Importdate);
			}
			
			set
			{
				if(base.SetSystemDateTime(ContentMetadata.ColumnNames.Importdate, value))
				{
					OnPropertyChanged(ContentMetadata.PropertyNames.Importdate);
				}
			}
		}		
		
		/// <summary>
		/// Maps to Content.metadata1
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Metadata1
		{
			get
			{
				return base.GetSystemString(ContentMetadata.ColumnNames.Metadata1);
			}
			
			set
			{
				if(base.SetSystemString(ContentMetadata.ColumnNames.Metadata1, value))
				{
					OnPropertyChanged(ContentMetadata.PropertyNames.Metadata1);
				}
			}
		}		
		
		/// <summary>
		/// Maps to Content.metadata2
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Metadata2
		{
			get
			{
				return base.GetSystemString(ContentMetadata.ColumnNames.Metadata2);
			}
			
			set
			{
				if(base.SetSystemString(ContentMetadata.ColumnNames.Metadata2, value))
				{
					OnPropertyChanged(ContentMetadata.PropertyNames.Metadata2);
				}
			}
		}		
		
		/// <summary>
		/// Maps to Content.metadata3
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Metadata3
		{
			get
			{
				return base.GetSystemString(ContentMetadata.ColumnNames.Metadata3);
			}
			
			set
			{
				if(base.SetSystemString(ContentMetadata.ColumnNames.Metadata3, value))
				{
					OnPropertyChanged(ContentMetadata.PropertyNames.Metadata3);
				}
			}
		}		
		
		/// <summary>
		/// Maps to Content.tags
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Tags
		{
			get
			{
				return base.GetSystemString(ContentMetadata.ColumnNames.Tags);
			}
			
			set
			{
				if(base.SetSystemString(ContentMetadata.ColumnNames.Tags, value))
				{
					OnPropertyChanged(ContentMetadata.PropertyNames.Tags);
				}
			}
		}		
		
		/// <summary>
		/// Maps to Content.snapshot
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Byte[] Snapshot
		{
			get
			{
				return base.GetSystemByteArray(ContentMetadata.ColumnNames.Snapshot);
			}
			
			set
			{
				if(base.SetSystemByteArray(ContentMetadata.ColumnNames.Snapshot, value))
				{
					OnPropertyChanged(ContentMetadata.PropertyNames.Snapshot);
				}
			}
		}		
		
		/// <summary>
		/// Maps to Content.uploadstatus
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? Uploadstatus
		{
			get
			{
				return base.GetSystemInt32(ContentMetadata.ColumnNames.Uploadstatus);
			}
			
			set
			{
				if(base.SetSystemInt32(ContentMetadata.ColumnNames.Uploadstatus, value))
				{
					OnPropertyChanged(ContentMetadata.PropertyNames.Uploadstatus);
				}
			}
		}		
		
		/// <summary>
		/// Maps to Content.metadata4
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Metadata4
		{
			get
			{
				return base.GetSystemString(ContentMetadata.ColumnNames.Metadata4);
			}
			
			set
			{
				if(base.SetSystemString(ContentMetadata.ColumnNames.Metadata4, value))
				{
					OnPropertyChanged(ContentMetadata.PropertyNames.Metadata4);
				}
			}
		}		
		
		/// <summary>
		/// Maps to Content.metadata5
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Metadata5
		{
			get
			{
				return base.GetSystemString(ContentMetadata.ColumnNames.Metadata5);
			}
			
			set
			{
				if(base.SetSystemString(ContentMetadata.ColumnNames.Metadata5, value))
				{
					OnPropertyChanged(ContentMetadata.PropertyNames.Metadata5);
				}
			}
		}		
		
		/// <summary>
		/// Maps to Content.metadata6
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Metadata6
		{
			get
			{
				return base.GetSystemString(ContentMetadata.ColumnNames.Metadata6);
			}
			
			set
			{
				if(base.SetSystemString(ContentMetadata.ColumnNames.Metadata6, value))
				{
					OnPropertyChanged(ContentMetadata.PropertyNames.Metadata6);
				}
			}
		}		
		
		/// <summary>
		/// Maps to Content.metadata7
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Metadata7
		{
			get
			{
				return base.GetSystemString(ContentMetadata.ColumnNames.Metadata7);
			}
			
			set
			{
				if(base.SetSystemString(ContentMetadata.ColumnNames.Metadata7, value))
				{
					OnPropertyChanged(ContentMetadata.PropertyNames.Metadata7);
				}
			}
		}		
		
		/// <summary>
		/// Maps to Content.metadata8
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Metadata8
		{
			get
			{
				return base.GetSystemString(ContentMetadata.ColumnNames.Metadata8);
			}
			
			set
			{
				if(base.SetSystemString(ContentMetadata.ColumnNames.Metadata8, value))
				{
					OnPropertyChanged(ContentMetadata.PropertyNames.Metadata8);
				}
			}
		}		
		
		/// <summary>
		/// Maps to Content.metadata9
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Metadata9
		{
			get
			{
				return base.GetSystemString(ContentMetadata.ColumnNames.Metadata9);
			}
			
			set
			{
				if(base.SetSystemString(ContentMetadata.ColumnNames.Metadata9, value))
				{
					OnPropertyChanged(ContentMetadata.PropertyNames.Metadata9);
				}
			}
		}		
		
		/// <summary>
		/// Maps to Content.active
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Boolean? Active
		{
			get
			{
				return base.GetSystemBoolean(ContentMetadata.ColumnNames.Active);
			}
			
			set
			{
				if(base.SetSystemBoolean(ContentMetadata.ColumnNames.Active, value))
				{
					OnPropertyChanged(ContentMetadata.PropertyNames.Active);
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
						case "Contenttype": this.str().Contenttype = (string)value; break;							
						case "Name": this.str().Name = (string)value; break;							
						case "Description": this.str().Description = (string)value; break;							
						case "Filelocation": this.str().Filelocation = (string)value; break;							
						case "Filesize": this.str().Filesize = (string)value; break;							
						case "Expiry": this.str().Expiry = (string)value; break;							
						case "Importdate": this.str().Importdate = (string)value; break;							
						case "Metadata1": this.str().Metadata1 = (string)value; break;							
						case "Metadata2": this.str().Metadata2 = (string)value; break;							
						case "Metadata3": this.str().Metadata3 = (string)value; break;							
						case "Tags": this.str().Tags = (string)value; break;							
						case "Uploadstatus": this.str().Uploadstatus = (string)value; break;							
						case "Metadata4": this.str().Metadata4 = (string)value; break;							
						case "Metadata5": this.str().Metadata5 = (string)value; break;							
						case "Metadata6": this.str().Metadata6 = (string)value; break;							
						case "Metadata7": this.str().Metadata7 = (string)value; break;							
						case "Metadata8": this.str().Metadata8 = (string)value; break;							
						case "Metadata9": this.str().Metadata9 = (string)value; break;							
						case "Active": this.str().Active = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Id":
						
							if (value == null || value is System.Int64)
								this.Id = (System.Int64?)value;
								OnPropertyChanged(ContentMetadata.PropertyNames.Id);
							break;
						
						case "Filesize":
						
							if (value == null || value is System.Int64)
								this.Filesize = (System.Int64?)value;
								OnPropertyChanged(ContentMetadata.PropertyNames.Filesize);
							break;
						
						case "Expiry":
						
							if (value == null || value is System.DateTime)
								this.Expiry = (System.DateTime?)value;
								OnPropertyChanged(ContentMetadata.PropertyNames.Expiry);
							break;
						
						case "Importdate":
						
							if (value == null || value is System.DateTime)
								this.Importdate = (System.DateTime?)value;
								OnPropertyChanged(ContentMetadata.PropertyNames.Importdate);
							break;
						
						case "Snapshot":
						
							if (value == null || value is System.Byte[])
								this.Snapshot = (System.Byte[])value;
								OnPropertyChanged(ContentMetadata.PropertyNames.Snapshot);
							break;
						
						case "Uploadstatus":
						
							if (value == null || value is System.Int32)
								this.Uploadstatus = (System.Int32?)value;
								OnPropertyChanged(ContentMetadata.PropertyNames.Uploadstatus);
							break;
						
						case "Active":
						
							if (value == null || value is System.Boolean)
								this.Active = (System.Boolean?)value;
								OnPropertyChanged(ContentMetadata.PropertyNames.Active);
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
			public esStrings(esContent entity)
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
				
			public System.String Contenttype
			{
				get
				{
					System.String data = entity.Contenttype;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Contenttype = null;
					else entity.Contenttype = Convert.ToString(value);
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
				
			public System.String Filelocation
			{
				get
				{
					System.String data = entity.Filelocation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Filelocation = null;
					else entity.Filelocation = Convert.ToString(value);
				}
			}
				
			public System.String Filesize
			{
				get
				{
					System.Int64? data = entity.Filesize;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Filesize = null;
					else entity.Filesize = Convert.ToInt64(value);
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
				
			public System.String Importdate
			{
				get
				{
					System.DateTime? data = entity.Importdate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Importdate = null;
					else entity.Importdate = Convert.ToDateTime(value);
				}
			}
				
			public System.String Metadata1
			{
				get
				{
					System.String data = entity.Metadata1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Metadata1 = null;
					else entity.Metadata1 = Convert.ToString(value);
				}
			}
				
			public System.String Metadata2
			{
				get
				{
					System.String data = entity.Metadata2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Metadata2 = null;
					else entity.Metadata2 = Convert.ToString(value);
				}
			}
				
			public System.String Metadata3
			{
				get
				{
					System.String data = entity.Metadata3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Metadata3 = null;
					else entity.Metadata3 = Convert.ToString(value);
				}
			}
				
			public System.String Tags
			{
				get
				{
					System.String data = entity.Tags;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Tags = null;
					else entity.Tags = Convert.ToString(value);
				}
			}
				
			public System.String Uploadstatus
			{
				get
				{
					System.Int32? data = entity.Uploadstatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Uploadstatus = null;
					else entity.Uploadstatus = Convert.ToInt32(value);
				}
			}
				
			public System.String Metadata4
			{
				get
				{
					System.String data = entity.Metadata4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Metadata4 = null;
					else entity.Metadata4 = Convert.ToString(value);
				}
			}
				
			public System.String Metadata5
			{
				get
				{
					System.String data = entity.Metadata5;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Metadata5 = null;
					else entity.Metadata5 = Convert.ToString(value);
				}
			}
				
			public System.String Metadata6
			{
				get
				{
					System.String data = entity.Metadata6;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Metadata6 = null;
					else entity.Metadata6 = Convert.ToString(value);
				}
			}
				
			public System.String Metadata7
			{
				get
				{
					System.String data = entity.Metadata7;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Metadata7 = null;
					else entity.Metadata7 = Convert.ToString(value);
				}
			}
				
			public System.String Metadata8
			{
				get
				{
					System.String data = entity.Metadata8;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Metadata8 = null;
					else entity.Metadata8 = Convert.ToString(value);
				}
			}
				
			public System.String Metadata9
			{
				get
				{
					System.String data = entity.Metadata9;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Metadata9 = null;
					else entity.Metadata9 = Convert.ToString(value);
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
			

			private esContent entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return ContentMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public ContentQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ContentQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(ContentQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(ContentQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private ContentQuery query;		
	}



	[Serializable]
	abstract public partial class esContentCollection : esEntityCollection<Content>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ContentMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "ContentCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public ContentQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ContentQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(ContentQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ContentQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(ContentQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((ContentQuery)query);
		}

		#endregion
		
		private ContentQuery query;
	}



	[Serializable]
	abstract public partial class esContentQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ContentMetadata.Meta();
			}
		}	
		
		#region esQueryItems

		public esQueryItem Id
		{
			get { return new esQueryItem(this, ContentMetadata.ColumnNames.Id, esSystemType.Int64); }
		} 
		
		public esQueryItem Contenttype
		{
			get { return new esQueryItem(this, ContentMetadata.ColumnNames.Contenttype, esSystemType.String); }
		} 
		
		public esQueryItem Name
		{
			get { return new esQueryItem(this, ContentMetadata.ColumnNames.Name, esSystemType.String); }
		} 
		
		public esQueryItem Description
		{
			get { return new esQueryItem(this, ContentMetadata.ColumnNames.Description, esSystemType.String); }
		} 
		
		public esQueryItem Filelocation
		{
			get { return new esQueryItem(this, ContentMetadata.ColumnNames.Filelocation, esSystemType.String); }
		} 
		
		public esQueryItem Filesize
		{
			get { return new esQueryItem(this, ContentMetadata.ColumnNames.Filesize, esSystemType.Int64); }
		} 
		
		public esQueryItem Expiry
		{
			get { return new esQueryItem(this, ContentMetadata.ColumnNames.Expiry, esSystemType.DateTime); }
		} 
		
		public esQueryItem Importdate
		{
			get { return new esQueryItem(this, ContentMetadata.ColumnNames.Importdate, esSystemType.DateTime); }
		} 
		
		public esQueryItem Metadata1
		{
			get { return new esQueryItem(this, ContentMetadata.ColumnNames.Metadata1, esSystemType.String); }
		} 
		
		public esQueryItem Metadata2
		{
			get { return new esQueryItem(this, ContentMetadata.ColumnNames.Metadata2, esSystemType.String); }
		} 
		
		public esQueryItem Metadata3
		{
			get { return new esQueryItem(this, ContentMetadata.ColumnNames.Metadata3, esSystemType.String); }
		} 
		
		public esQueryItem Tags
		{
			get { return new esQueryItem(this, ContentMetadata.ColumnNames.Tags, esSystemType.String); }
		} 
		
		public esQueryItem Snapshot
		{
			get { return new esQueryItem(this, ContentMetadata.ColumnNames.Snapshot, esSystemType.ByteArray); }
		} 
		
		public esQueryItem Uploadstatus
		{
			get { return new esQueryItem(this, ContentMetadata.ColumnNames.Uploadstatus, esSystemType.Int32); }
		} 
		
		public esQueryItem Metadata4
		{
			get { return new esQueryItem(this, ContentMetadata.ColumnNames.Metadata4, esSystemType.String); }
		} 
		
		public esQueryItem Metadata5
		{
			get { return new esQueryItem(this, ContentMetadata.ColumnNames.Metadata5, esSystemType.String); }
		} 
		
		public esQueryItem Metadata6
		{
			get { return new esQueryItem(this, ContentMetadata.ColumnNames.Metadata6, esSystemType.String); }
		} 
		
		public esQueryItem Metadata7
		{
			get { return new esQueryItem(this, ContentMetadata.ColumnNames.Metadata7, esSystemType.String); }
		} 
		
		public esQueryItem Metadata8
		{
			get { return new esQueryItem(this, ContentMetadata.ColumnNames.Metadata8, esSystemType.String); }
		} 
		
		public esQueryItem Metadata9
		{
			get { return new esQueryItem(this, ContentMetadata.ColumnNames.Metadata9, esSystemType.String); }
		} 
		
		public esQueryItem Active
		{
			get { return new esQueryItem(this, ContentMetadata.ColumnNames.Active, esSystemType.Boolean); }
		} 
		
		#endregion
		
	}


	
	public partial class Content : esContent
	{

		
		
	}
	



	[Serializable]
	public partial class ContentMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ContentMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ContentMetadata.ColumnNames.Id, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = ContentMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ContentMetadata.ColumnNames.Contenttype, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ContentMetadata.PropertyNames.Contenttype;
			c.CharacterMaxLength = 32;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ContentMetadata.ColumnNames.Name, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ContentMetadata.PropertyNames.Name;
			c.CharacterMaxLength = 64;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ContentMetadata.ColumnNames.Description, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ContentMetadata.PropertyNames.Description;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ContentMetadata.ColumnNames.Filelocation, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ContentMetadata.PropertyNames.Filelocation;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ContentMetadata.ColumnNames.Filesize, 5, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = ContentMetadata.PropertyNames.Filesize;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ContentMetadata.ColumnNames.Expiry, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ContentMetadata.PropertyNames.Expiry;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ContentMetadata.ColumnNames.Importdate, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ContentMetadata.PropertyNames.Importdate;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ContentMetadata.ColumnNames.Metadata1, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = ContentMetadata.PropertyNames.Metadata1;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ContentMetadata.ColumnNames.Metadata2, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = ContentMetadata.PropertyNames.Metadata2;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ContentMetadata.ColumnNames.Metadata3, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = ContentMetadata.PropertyNames.Metadata3;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ContentMetadata.ColumnNames.Tags, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = ContentMetadata.PropertyNames.Tags;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ContentMetadata.ColumnNames.Snapshot, 12, typeof(System.Byte[]), esSystemType.ByteArray);
			c.PropertyName = ContentMetadata.PropertyNames.Snapshot;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ContentMetadata.ColumnNames.Uploadstatus, 13, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ContentMetadata.PropertyNames.Uploadstatus;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ContentMetadata.ColumnNames.Metadata4, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = ContentMetadata.PropertyNames.Metadata4;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ContentMetadata.ColumnNames.Metadata5, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = ContentMetadata.PropertyNames.Metadata5;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ContentMetadata.ColumnNames.Metadata6, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = ContentMetadata.PropertyNames.Metadata6;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ContentMetadata.ColumnNames.Metadata7, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = ContentMetadata.PropertyNames.Metadata7;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ContentMetadata.ColumnNames.Metadata8, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = ContentMetadata.PropertyNames.Metadata8;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ContentMetadata.ColumnNames.Metadata9, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = ContentMetadata.PropertyNames.Metadata9;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ContentMetadata.ColumnNames.Active, 20, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ContentMetadata.PropertyNames.Active;
			c.IsNullable = true;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public ContentMetadata Meta()
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
			 public const string Contenttype = "contenttype";
			 public const string Name = "name";
			 public const string Description = "description";
			 public const string Filelocation = "filelocation";
			 public const string Filesize = "filesize";
			 public const string Expiry = "expiry";
			 public const string Importdate = "importdate";
			 public const string Metadata1 = "metadata1";
			 public const string Metadata2 = "metadata2";
			 public const string Metadata3 = "metadata3";
			 public const string Tags = "tags";
			 public const string Snapshot = "snapshot";
			 public const string Uploadstatus = "uploadstatus";
			 public const string Metadata4 = "metadata4";
			 public const string Metadata5 = "metadata5";
			 public const string Metadata6 = "metadata6";
			 public const string Metadata7 = "metadata7";
			 public const string Metadata8 = "metadata8";
			 public const string Metadata9 = "metadata9";
			 public const string Active = "active";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string Contenttype = "Contenttype";
			 public const string Name = "Name";
			 public const string Description = "Description";
			 public const string Filelocation = "Filelocation";
			 public const string Filesize = "Filesize";
			 public const string Expiry = "Expiry";
			 public const string Importdate = "Importdate";
			 public const string Metadata1 = "Metadata1";
			 public const string Metadata2 = "Metadata2";
			 public const string Metadata3 = "Metadata3";
			 public const string Tags = "Tags";
			 public const string Snapshot = "Snapshot";
			 public const string Uploadstatus = "Uploadstatus";
			 public const string Metadata4 = "Metadata4";
			 public const string Metadata5 = "Metadata5";
			 public const string Metadata6 = "Metadata6";
			 public const string Metadata7 = "Metadata7";
			 public const string Metadata8 = "Metadata8";
			 public const string Metadata9 = "Metadata9";
			 public const string Active = "Active";
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
			lock (typeof(ContentMetadata))
			{
				if(ContentMetadata.mapDelegates == null)
				{
					ContentMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ContentMetadata.meta == null)
				{
					ContentMetadata.meta = new ContentMetadata();
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
				meta.AddTypeMap("Contenttype", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Name", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Description", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Filelocation", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Filesize", new esTypeMap("BigInt", "System.Int64"));
				meta.AddTypeMap("Expiry", new esTypeMap("DateTime", "System.DateTime"));
				meta.AddTypeMap("Importdate", new esTypeMap("DateTime", "System.DateTime"));
				meta.AddTypeMap("Metadata1", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Metadata2", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Metadata3", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Tags", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Snapshot", new esTypeMap("Image", "System.Byte[]"));
				meta.AddTypeMap("Uploadstatus", new esTypeMap("Int", "System.Int32"));
				meta.AddTypeMap("Metadata4", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Metadata5", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Metadata6", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Metadata7", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Metadata8", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Metadata9", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Active", new esTypeMap("Bit", "System.Boolean"));			
				
				
				
				meta.Source = "Content";
				meta.Destination = "Content";
				
				meta.spInsert = "proc_ContentInsert";				
				meta.spUpdate = "proc_ContentUpdate";		
				meta.spDelete = "proc_ContentDelete";
				meta.spLoadAll = "proc_ContentLoadAll";
				meta.spLoadByPrimaryKey = "proc_ContentLoadByPrimaryKey";
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ContentMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
