
/*
===============================================================================
                    EntitySpaces 2011 by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2011.1.1110.0
EntitySpaces Driver  : VistaDB4
Date Generated       : 2012/07/10 05:13:56 AM
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
	/// Encapsulates the 'CSTemplate' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[XmlType("CSTemplate")]
	public partial class CSTemplate : esCSTemplate
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new CSTemplate();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Int64 id)
		{
			var obj = new CSTemplate();
			obj.Id = id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Int64 id, esSqlAccessType sqlAccessType)
		{
			var obj = new CSTemplate();
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
	[XmlType("CSTemplateCollection")]
	public partial class CSTemplateCollection : esCSTemplateCollection, IEnumerable<CSTemplate>
	{
		public CSTemplate FindByPrimaryKey(System.Int64 id)
		{
			return this.SingleOrDefault(e => e.Id == id);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(CSTemplate))]
		public class CSTemplateCollectionWCFPacket : esCollectionWCFPacket<CSTemplateCollection>
		{
			public static implicit operator CSTemplateCollection(CSTemplateCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator CSTemplateCollectionWCFPacket(CSTemplateCollection collection)
			{
				return new CSTemplateCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class CSTemplateQuery : esCSTemplateQuery
	{
		public CSTemplateQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "CSTemplateQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(CSTemplateQuery query)
		{
			return CSTemplateQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator CSTemplateQuery(string query)
		{
			return (CSTemplateQuery)CSTemplateQuery.SerializeHelper.FromXml(query, typeof(CSTemplateQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esCSTemplate : esEntity
	{
		public esCSTemplate()
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
			CSTemplateQuery query = new CSTemplateQuery();
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
		/// Maps to CSTemplate.id
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int64? Id
		{
			get
			{
				return base.GetSystemInt64(CSTemplateMetadata.ColumnNames.Id);
			}
			
			set
			{
				if(base.SetSystemInt64(CSTemplateMetadata.ColumnNames.Id, value))
				{
					OnPropertyChanged(CSTemplateMetadata.PropertyNames.Id);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSTemplate.name
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Name
		{
			get
			{
				return base.GetSystemString(CSTemplateMetadata.ColumnNames.Name);
			}
			
			set
			{
				if(base.SetSystemString(CSTemplateMetadata.ColumnNames.Name, value))
				{
					OnPropertyChanged(CSTemplateMetadata.PropertyNames.Name);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSTemplate.description
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Description
		{
			get
			{
				return base.GetSystemString(CSTemplateMetadata.ColumnNames.Description);
			}
			
			set
			{
				if(base.SetSystemString(CSTemplateMetadata.ColumnNames.Description, value))
				{
					OnPropertyChanged(CSTemplateMetadata.PropertyNames.Description);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSTemplate.created
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int64? Created
		{
			get
			{
				return base.GetSystemInt64(CSTemplateMetadata.ColumnNames.Created);
			}
			
			set
			{
				if(base.SetSystemInt64(CSTemplateMetadata.ColumnNames.Created, value))
				{
					OnPropertyChanged(CSTemplateMetadata.PropertyNames.Created);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSTemplate.lastmodified
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Lastmodified
		{
			get
			{
				return base.GetSystemString(CSTemplateMetadata.ColumnNames.Lastmodified);
			}
			
			set
			{
				if(base.SetSystemString(CSTemplateMetadata.ColumnNames.Lastmodified, value))
				{
					OnPropertyChanged(CSTemplateMetadata.PropertyNames.Lastmodified);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSTemplate.createdby
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Createdby
		{
			get
			{
				return base.GetSystemString(CSTemplateMetadata.ColumnNames.Createdby);
			}
			
			set
			{
				if(base.SetSystemString(CSTemplateMetadata.ColumnNames.Createdby, value))
				{
					OnPropertyChanged(CSTemplateMetadata.PropertyNames.Createdby);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSTemplate.modifiedby
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Modifiedby
		{
			get
			{
				return base.GetSystemString(CSTemplateMetadata.ColumnNames.Modifiedby);
			}
			
			set
			{
				if(base.SetSystemString(CSTemplateMetadata.ColumnNames.Modifiedby, value))
				{
					OnPropertyChanged(CSTemplateMetadata.PropertyNames.Modifiedby);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSTemplate.active
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? Active
		{
			get
			{
				return base.GetSystemInt32(CSTemplateMetadata.ColumnNames.Active);
			}
			
			set
			{
				if(base.SetSystemInt32(CSTemplateMetadata.ColumnNames.Active, value))
				{
					OnPropertyChanged(CSTemplateMetadata.PropertyNames.Active);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSTemplate.thumb
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Byte[] Thumb
		{
			get
			{
				return base.GetSystemByteArray(CSTemplateMetadata.ColumnNames.Thumb);
			}
			
			set
			{
				if(base.SetSystemByteArray(CSTemplateMetadata.ColumnNames.Thumb, value))
				{
					OnPropertyChanged(CSTemplateMetadata.PropertyNames.Thumb);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSTemplate.template
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Template
		{
			get
			{
				return base.GetSystemString(CSTemplateMetadata.ColumnNames.Template);
			}
			
			set
			{
				if(base.SetSystemString(CSTemplateMetadata.ColumnNames.Template, value))
				{
					OnPropertyChanged(CSTemplateMetadata.PropertyNames.Template);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSTemplate.attribvaluetext
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Attribvaluetext
		{
			get
			{
				return base.GetSystemString(CSTemplateMetadata.ColumnNames.Attribvaluetext);
			}
			
			set
			{
				if(base.SetSystemString(CSTemplateMetadata.ColumnNames.Attribvaluetext, value))
				{
					OnPropertyChanged(CSTemplateMetadata.PropertyNames.Attribvaluetext);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSTemplate.attribvaluenumber
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int64? Attribvaluenumber
		{
			get
			{
				return base.GetSystemInt64(CSTemplateMetadata.ColumnNames.Attribvaluenumber);
			}
			
			set
			{
				if(base.SetSystemInt64(CSTemplateMetadata.ColumnNames.Attribvaluenumber, value))
				{
					OnPropertyChanged(CSTemplateMetadata.PropertyNames.Attribvaluenumber);
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
						case "Created": this.str().Created = (string)value; break;							
						case "Lastmodified": this.str().Lastmodified = (string)value; break;							
						case "Createdby": this.str().Createdby = (string)value; break;							
						case "Modifiedby": this.str().Modifiedby = (string)value; break;							
						case "Active": this.str().Active = (string)value; break;							
						case "Template": this.str().Template = (string)value; break;							
						case "Attribvaluetext": this.str().Attribvaluetext = (string)value; break;							
						case "Attribvaluenumber": this.str().Attribvaluenumber = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Id":
						
							if (value == null || value is System.Int64)
								this.Id = (System.Int64?)value;
								OnPropertyChanged(CSTemplateMetadata.PropertyNames.Id);
							break;
						
						case "Created":
						
							if (value == null || value is System.Int64)
								this.Created = (System.Int64?)value;
								OnPropertyChanged(CSTemplateMetadata.PropertyNames.Created);
							break;
						
						case "Active":
						
							if (value == null || value is System.Int32)
								this.Active = (System.Int32?)value;
								OnPropertyChanged(CSTemplateMetadata.PropertyNames.Active);
							break;
						
						case "Thumb":
						
							if (value == null || value is System.Byte[])
								this.Thumb = (System.Byte[])value;
								OnPropertyChanged(CSTemplateMetadata.PropertyNames.Thumb);
							break;
						
						case "Attribvaluenumber":
						
							if (value == null || value is System.Int64)
								this.Attribvaluenumber = (System.Int64?)value;
								OnPropertyChanged(CSTemplateMetadata.PropertyNames.Attribvaluenumber);
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
			public esStrings(esCSTemplate entity)
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
				
			public System.String Created
			{
				get
				{
					System.Int64? data = entity.Created;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Created = null;
					else entity.Created = Convert.ToInt64(value);
				}
			}
				
			public System.String Lastmodified
			{
				get
				{
					System.String data = entity.Lastmodified;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Lastmodified = null;
					else entity.Lastmodified = Convert.ToString(value);
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
				
			public System.String Modifiedby
			{
				get
				{
					System.String data = entity.Modifiedby;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Modifiedby = null;
					else entity.Modifiedby = Convert.ToString(value);
				}
			}
				
			public System.String Active
			{
				get
				{
					System.Int32? data = entity.Active;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Active = null;
					else entity.Active = Convert.ToInt32(value);
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
				
			public System.String Attribvaluetext
			{
				get
				{
					System.String data = entity.Attribvaluetext;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Attribvaluetext = null;
					else entity.Attribvaluetext = Convert.ToString(value);
				}
			}
				
			public System.String Attribvaluenumber
			{
				get
				{
					System.Int64? data = entity.Attribvaluenumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Attribvaluenumber = null;
					else entity.Attribvaluenumber = Convert.ToInt64(value);
				}
			}
			

			private esCSTemplate entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return CSTemplateMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public CSTemplateQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CSTemplateQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(CSTemplateQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(CSTemplateQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private CSTemplateQuery query;		
	}



	[Serializable]
	abstract public partial class esCSTemplateCollection : esEntityCollection<CSTemplate>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CSTemplateMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "CSTemplateCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public CSTemplateQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CSTemplateQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(CSTemplateQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CSTemplateQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(CSTemplateQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((CSTemplateQuery)query);
		}

		#endregion
		
		private CSTemplateQuery query;
	}



	[Serializable]
	abstract public partial class esCSTemplateQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return CSTemplateMetadata.Meta();
			}
		}	
		
		#region esQueryItems

		public esQueryItem Id
		{
			get { return new esQueryItem(this, CSTemplateMetadata.ColumnNames.Id, esSystemType.Int64); }
		} 
		
		public esQueryItem Name
		{
			get { return new esQueryItem(this, CSTemplateMetadata.ColumnNames.Name, esSystemType.String); }
		} 
		
		public esQueryItem Description
		{
			get { return new esQueryItem(this, CSTemplateMetadata.ColumnNames.Description, esSystemType.String); }
		} 
		
		public esQueryItem Created
		{
			get { return new esQueryItem(this, CSTemplateMetadata.ColumnNames.Created, esSystemType.Int64); }
		} 
		
		public esQueryItem Lastmodified
		{
			get { return new esQueryItem(this, CSTemplateMetadata.ColumnNames.Lastmodified, esSystemType.String); }
		} 
		
		public esQueryItem Createdby
		{
			get { return new esQueryItem(this, CSTemplateMetadata.ColumnNames.Createdby, esSystemType.String); }
		} 
		
		public esQueryItem Modifiedby
		{
			get { return new esQueryItem(this, CSTemplateMetadata.ColumnNames.Modifiedby, esSystemType.String); }
		} 
		
		public esQueryItem Active
		{
			get { return new esQueryItem(this, CSTemplateMetadata.ColumnNames.Active, esSystemType.Int32); }
		} 
		
		public esQueryItem Thumb
		{
			get { return new esQueryItem(this, CSTemplateMetadata.ColumnNames.Thumb, esSystemType.ByteArray); }
		} 
		
		public esQueryItem Template
		{
			get { return new esQueryItem(this, CSTemplateMetadata.ColumnNames.Template, esSystemType.String); }
		} 
		
		public esQueryItem Attribvaluetext
		{
			get { return new esQueryItem(this, CSTemplateMetadata.ColumnNames.Attribvaluetext, esSystemType.String); }
		} 
		
		public esQueryItem Attribvaluenumber
		{
			get { return new esQueryItem(this, CSTemplateMetadata.ColumnNames.Attribvaluenumber, esSystemType.Int64); }
		} 
		
		#endregion
		
	}


	
	public partial class CSTemplate : esCSTemplate
	{

		
		
	}
	



	[Serializable]
	public partial class CSTemplateMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CSTemplateMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CSTemplateMetadata.ColumnNames.Id, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = CSTemplateMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSTemplateMetadata.ColumnNames.Name, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = CSTemplateMetadata.PropertyNames.Name;
			c.CharacterMaxLength = 128;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSTemplateMetadata.ColumnNames.Description, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = CSTemplateMetadata.PropertyNames.Description;
			c.CharacterMaxLength = 4096;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSTemplateMetadata.ColumnNames.Created, 3, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = CSTemplateMetadata.PropertyNames.Created;
			c.IsComputed = true;
			c.IsConcurrency = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSTemplateMetadata.ColumnNames.Lastmodified, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = CSTemplateMetadata.PropertyNames.Lastmodified;
			c.CharacterMaxLength = 134213632;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSTemplateMetadata.ColumnNames.Createdby, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = CSTemplateMetadata.PropertyNames.Createdby;
			c.CharacterMaxLength = 128;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSTemplateMetadata.ColumnNames.Modifiedby, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = CSTemplateMetadata.PropertyNames.Modifiedby;
			c.CharacterMaxLength = 128;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSTemplateMetadata.ColumnNames.Active, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CSTemplateMetadata.PropertyNames.Active;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSTemplateMetadata.ColumnNames.Thumb, 8, typeof(System.Byte[]), esSystemType.ByteArray);
			c.PropertyName = CSTemplateMetadata.PropertyNames.Thumb;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSTemplateMetadata.ColumnNames.Template, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = CSTemplateMetadata.PropertyNames.Template;
			c.CharacterMaxLength = 128;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSTemplateMetadata.ColumnNames.Attribvaluetext, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = CSTemplateMetadata.PropertyNames.Attribvaluetext;
			c.CharacterMaxLength = 1024;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSTemplateMetadata.ColumnNames.Attribvaluenumber, 11, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = CSTemplateMetadata.PropertyNames.Attribvaluenumber;
			c.IsNullable = true;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public CSTemplateMetadata Meta()
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
			 public const string Created = "created";
			 public const string Lastmodified = "lastmodified";
			 public const string Createdby = "createdby";
			 public const string Modifiedby = "modifiedby";
			 public const string Active = "active";
			 public const string Thumb = "thumb";
			 public const string Template = "template";
			 public const string Attribvaluetext = "attribvaluetext";
			 public const string Attribvaluenumber = "attribvaluenumber";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string Name = "Name";
			 public const string Description = "Description";
			 public const string Created = "Created";
			 public const string Lastmodified = "Lastmodified";
			 public const string Createdby = "Createdby";
			 public const string Modifiedby = "Modifiedby";
			 public const string Active = "Active";
			 public const string Thumb = "Thumb";
			 public const string Template = "Template";
			 public const string Attribvaluetext = "Attribvaluetext";
			 public const string Attribvaluenumber = "Attribvaluenumber";
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
			lock (typeof(CSTemplateMetadata))
			{
				if(CSTemplateMetadata.mapDelegates == null)
				{
					CSTemplateMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (CSTemplateMetadata.meta == null)
				{
					CSTemplateMetadata.meta = new CSTemplateMetadata();
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
				meta.AddTypeMap("Created", new esTypeMap("Timestamp", "System.Int64"));
				meta.AddTypeMap("Lastmodified", new esTypeMap("Text", "System.String"));
				meta.AddTypeMap("Createdby", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Modifiedby", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Active", new esTypeMap("Int", "System.Int32"));
				meta.AddTypeMap("Thumb", new esTypeMap("Image", "System.Byte[]"));
				meta.AddTypeMap("Template", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Attribvaluetext", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Attribvaluenumber", new esTypeMap("BigInt", "System.Int64"));			
				
				
				
				meta.Source = "CSTemplate";
				meta.Destination = "CSTemplate";
				
				meta.spInsert = "proc_CSTemplateInsert";				
				meta.spUpdate = "proc_CSTemplateUpdate";		
				meta.spDelete = "proc_CSTemplateDelete";
				meta.spLoadAll = "proc_CSTemplateLoadAll";
				meta.spLoadByPrimaryKey = "proc_CSTemplateLoadByPrimaryKey";
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CSTemplateMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
