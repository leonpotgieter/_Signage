
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
	/// Encapsulates the 'Template' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[XmlType("Template")]
	public partial class Template : esTemplate
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new Template();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Int64 id)
		{
			var obj = new Template();
			obj.Id = id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Int64 id, esSqlAccessType sqlAccessType)
		{
			var obj = new Template();
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
	[XmlType("TemplateCollection")]
	public partial class TemplateCollection : esTemplateCollection, IEnumerable<Template>
	{
		public Template FindByPrimaryKey(System.Int64 id)
		{
			return this.SingleOrDefault(e => e.Id == id);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(Template))]
		public class TemplateCollectionWCFPacket : esCollectionWCFPacket<TemplateCollection>
		{
			public static implicit operator TemplateCollection(TemplateCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator TemplateCollectionWCFPacket(TemplateCollection collection)
			{
				return new TemplateCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class TemplateQuery : esTemplateQuery
	{
		public TemplateQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "TemplateQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(TemplateQuery query)
		{
			return TemplateQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator TemplateQuery(string query)
		{
			return (TemplateQuery)TemplateQuery.SerializeHelper.FromXml(query, typeof(TemplateQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esTemplate : esEntity
	{
		public esTemplate()
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
			TemplateQuery query = new TemplateQuery();
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
		/// Maps to Template.id
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int64? Id
		{
			get
			{
				return base.GetSystemInt64(TemplateMetadata.ColumnNames.Id);
			}
			
			set
			{
				if(base.SetSystemInt64(TemplateMetadata.ColumnNames.Id, value))
				{
					OnPropertyChanged(TemplateMetadata.PropertyNames.Id);
				}
			}
		}		
		
		/// <summary>
		/// Maps to Template.name
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Name
		{
			get
			{
				return base.GetSystemString(TemplateMetadata.ColumnNames.Name);
			}
			
			set
			{
				if(base.SetSystemString(TemplateMetadata.ColumnNames.Name, value))
				{
					OnPropertyChanged(TemplateMetadata.PropertyNames.Name);
				}
			}
		}		
		
		/// <summary>
		/// Maps to Template.description
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Description
		{
			get
			{
				return base.GetSystemString(TemplateMetadata.ColumnNames.Description);
			}
			
			set
			{
				if(base.SetSystemString(TemplateMetadata.ColumnNames.Description, value))
				{
					OnPropertyChanged(TemplateMetadata.PropertyNames.Description);
				}
			}
		}		
		
		/// <summary>
		/// Maps to Template.created
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.DateTime? Created
		{
			get
			{
				return base.GetSystemDateTime(TemplateMetadata.ColumnNames.Created);
			}
			
			set
			{
				if(base.SetSystemDateTime(TemplateMetadata.ColumnNames.Created, value))
				{
					OnPropertyChanged(TemplateMetadata.PropertyNames.Created);
				}
			}
		}		
		
		/// <summary>
		/// Maps to Template.zonename
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Zonename
		{
			get
			{
				return base.GetSystemString(TemplateMetadata.ColumnNames.Zonename);
			}
			
			set
			{
				if(base.SetSystemString(TemplateMetadata.ColumnNames.Zonename, value))
				{
					OnPropertyChanged(TemplateMetadata.PropertyNames.Zonename);
				}
			}
		}		
		
		/// <summary>
		/// Maps to Template.zonedescription
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Zonedescription
		{
			get
			{
				return base.GetSystemString(TemplateMetadata.ColumnNames.Zonedescription);
			}
			
			set
			{
				if(base.SetSystemString(TemplateMetadata.ColumnNames.Zonedescription, value))
				{
					OnPropertyChanged(TemplateMetadata.PropertyNames.Zonedescription);
				}
			}
		}		
		
		/// <summary>
		/// Maps to Template.x
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? X
		{
			get
			{
				return base.GetSystemInt32(TemplateMetadata.ColumnNames.X);
			}
			
			set
			{
				if(base.SetSystemInt32(TemplateMetadata.ColumnNames.X, value))
				{
					OnPropertyChanged(TemplateMetadata.PropertyNames.X);
				}
			}
		}		
		
		/// <summary>
		/// Maps to Template.y
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? Y
		{
			get
			{
				return base.GetSystemInt32(TemplateMetadata.ColumnNames.Y);
			}
			
			set
			{
				if(base.SetSystemInt32(TemplateMetadata.ColumnNames.Y, value))
				{
					OnPropertyChanged(TemplateMetadata.PropertyNames.Y);
				}
			}
		}		
		
		/// <summary>
		/// Maps to Template.width
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? Width
		{
			get
			{
				return base.GetSystemInt32(TemplateMetadata.ColumnNames.Width);
			}
			
			set
			{
				if(base.SetSystemInt32(TemplateMetadata.ColumnNames.Width, value))
				{
					OnPropertyChanged(TemplateMetadata.PropertyNames.Width);
				}
			}
		}		
		
		/// <summary>
		/// Maps to Template.height
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? Height
		{
			get
			{
				return base.GetSystemInt32(TemplateMetadata.ColumnNames.Height);
			}
			
			set
			{
				if(base.SetSystemInt32(TemplateMetadata.ColumnNames.Height, value))
				{
					OnPropertyChanged(TemplateMetadata.PropertyNames.Height);
				}
			}
		}		
		
		/// <summary>
		/// Maps to Template.opacity
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? Opacity
		{
			get
			{
				return base.GetSystemInt32(TemplateMetadata.ColumnNames.Opacity);
			}
			
			set
			{
				if(base.SetSystemInt32(TemplateMetadata.ColumnNames.Opacity, value))
				{
					OnPropertyChanged(TemplateMetadata.PropertyNames.Opacity);
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
						case "Zonename": this.str().Zonename = (string)value; break;							
						case "Zonedescription": this.str().Zonedescription = (string)value; break;							
						case "X": this.str().X = (string)value; break;							
						case "Y": this.str().Y = (string)value; break;							
						case "Width": this.str().Width = (string)value; break;							
						case "Height": this.str().Height = (string)value; break;							
						case "Opacity": this.str().Opacity = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Id":
						
							if (value == null || value is System.Int64)
								this.Id = (System.Int64?)value;
								OnPropertyChanged(TemplateMetadata.PropertyNames.Id);
							break;
						
						case "Created":
						
							if (value == null || value is System.DateTime)
								this.Created = (System.DateTime?)value;
								OnPropertyChanged(TemplateMetadata.PropertyNames.Created);
							break;
						
						case "X":
						
							if (value == null || value is System.Int32)
								this.X = (System.Int32?)value;
								OnPropertyChanged(TemplateMetadata.PropertyNames.X);
							break;
						
						case "Y":
						
							if (value == null || value is System.Int32)
								this.Y = (System.Int32?)value;
								OnPropertyChanged(TemplateMetadata.PropertyNames.Y);
							break;
						
						case "Width":
						
							if (value == null || value is System.Int32)
								this.Width = (System.Int32?)value;
								OnPropertyChanged(TemplateMetadata.PropertyNames.Width);
							break;
						
						case "Height":
						
							if (value == null || value is System.Int32)
								this.Height = (System.Int32?)value;
								OnPropertyChanged(TemplateMetadata.PropertyNames.Height);
							break;
						
						case "Opacity":
						
							if (value == null || value is System.Int32)
								this.Opacity = (System.Int32?)value;
								OnPropertyChanged(TemplateMetadata.PropertyNames.Opacity);
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
			public esStrings(esTemplate entity)
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
					System.DateTime? data = entity.Created;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Created = null;
					else entity.Created = Convert.ToDateTime(value);
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
				
			public System.String Zonedescription
			{
				get
				{
					System.String data = entity.Zonedescription;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Zonedescription = null;
					else entity.Zonedescription = Convert.ToString(value);
				}
			}
				
			public System.String X
			{
				get
				{
					System.Int32? data = entity.X;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.X = null;
					else entity.X = Convert.ToInt32(value);
				}
			}
				
			public System.String Y
			{
				get
				{
					System.Int32? data = entity.Y;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Y = null;
					else entity.Y = Convert.ToInt32(value);
				}
			}
				
			public System.String Width
			{
				get
				{
					System.Int32? data = entity.Width;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Width = null;
					else entity.Width = Convert.ToInt32(value);
				}
			}
				
			public System.String Height
			{
				get
				{
					System.Int32? data = entity.Height;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Height = null;
					else entity.Height = Convert.ToInt32(value);
				}
			}
				
			public System.String Opacity
			{
				get
				{
					System.Int32? data = entity.Opacity;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Opacity = null;
					else entity.Opacity = Convert.ToInt32(value);
				}
			}
			

			private esTemplate entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return TemplateMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public TemplateQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TemplateQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(TemplateQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(TemplateQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private TemplateQuery query;		
	}



	[Serializable]
	abstract public partial class esTemplateCollection : esEntityCollection<Template>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return TemplateMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "TemplateCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public TemplateQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TemplateQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(TemplateQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TemplateQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(TemplateQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((TemplateQuery)query);
		}

		#endregion
		
		private TemplateQuery query;
	}



	[Serializable]
	abstract public partial class esTemplateQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return TemplateMetadata.Meta();
			}
		}	
		
		#region esQueryItems

		public esQueryItem Id
		{
			get { return new esQueryItem(this, TemplateMetadata.ColumnNames.Id, esSystemType.Int64); }
		} 
		
		public esQueryItem Name
		{
			get { return new esQueryItem(this, TemplateMetadata.ColumnNames.Name, esSystemType.String); }
		} 
		
		public esQueryItem Description
		{
			get { return new esQueryItem(this, TemplateMetadata.ColumnNames.Description, esSystemType.String); }
		} 
		
		public esQueryItem Created
		{
			get { return new esQueryItem(this, TemplateMetadata.ColumnNames.Created, esSystemType.DateTime); }
		} 
		
		public esQueryItem Zonename
		{
			get { return new esQueryItem(this, TemplateMetadata.ColumnNames.Zonename, esSystemType.String); }
		} 
		
		public esQueryItem Zonedescription
		{
			get { return new esQueryItem(this, TemplateMetadata.ColumnNames.Zonedescription, esSystemType.String); }
		} 
		
		public esQueryItem X
		{
			get { return new esQueryItem(this, TemplateMetadata.ColumnNames.X, esSystemType.Int32); }
		} 
		
		public esQueryItem Y
		{
			get { return new esQueryItem(this, TemplateMetadata.ColumnNames.Y, esSystemType.Int32); }
		} 
		
		public esQueryItem Width
		{
			get { return new esQueryItem(this, TemplateMetadata.ColumnNames.Width, esSystemType.Int32); }
		} 
		
		public esQueryItem Height
		{
			get { return new esQueryItem(this, TemplateMetadata.ColumnNames.Height, esSystemType.Int32); }
		} 
		
		public esQueryItem Opacity
		{
			get { return new esQueryItem(this, TemplateMetadata.ColumnNames.Opacity, esSystemType.Int32); }
		} 
		
		#endregion
		
	}


	
	public partial class Template : esTemplate
	{

		
		
	}
	



	[Serializable]
	public partial class TemplateMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected TemplateMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(TemplateMetadata.ColumnNames.Id, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = TemplateMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(TemplateMetadata.ColumnNames.Name, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = TemplateMetadata.PropertyNames.Name;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(TemplateMetadata.ColumnNames.Description, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = TemplateMetadata.PropertyNames.Description;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(TemplateMetadata.ColumnNames.Created, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TemplateMetadata.PropertyNames.Created;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(TemplateMetadata.ColumnNames.Zonename, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = TemplateMetadata.PropertyNames.Zonename;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(TemplateMetadata.ColumnNames.Zonedescription, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = TemplateMetadata.PropertyNames.Zonedescription;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(TemplateMetadata.ColumnNames.X, 6, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = TemplateMetadata.PropertyNames.X;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(TemplateMetadata.ColumnNames.Y, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = TemplateMetadata.PropertyNames.Y;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(TemplateMetadata.ColumnNames.Width, 8, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = TemplateMetadata.PropertyNames.Width;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(TemplateMetadata.ColumnNames.Height, 9, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = TemplateMetadata.PropertyNames.Height;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(TemplateMetadata.ColumnNames.Opacity, 10, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = TemplateMetadata.PropertyNames.Opacity;
			c.IsNullable = true;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public TemplateMetadata Meta()
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
			 public const string Zonename = "zonename";
			 public const string Zonedescription = "zonedescription";
			 public const string X = "x";
			 public const string Y = "y";
			 public const string Width = "width";
			 public const string Height = "height";
			 public const string Opacity = "opacity";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string Name = "Name";
			 public const string Description = "Description";
			 public const string Created = "Created";
			 public const string Zonename = "Zonename";
			 public const string Zonedescription = "Zonedescription";
			 public const string X = "X";
			 public const string Y = "Y";
			 public const string Width = "Width";
			 public const string Height = "Height";
			 public const string Opacity = "Opacity";
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
			lock (typeof(TemplateMetadata))
			{
				if(TemplateMetadata.mapDelegates == null)
				{
					TemplateMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (TemplateMetadata.meta == null)
				{
					TemplateMetadata.meta = new TemplateMetadata();
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
				meta.AddTypeMap("Created", new esTypeMap("DateTime", "System.DateTime"));
				meta.AddTypeMap("Zonename", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Zonedescription", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("X", new esTypeMap("Int", "System.Int32"));
				meta.AddTypeMap("Y", new esTypeMap("Int", "System.Int32"));
				meta.AddTypeMap("Width", new esTypeMap("Int", "System.Int32"));
				meta.AddTypeMap("Height", new esTypeMap("Int", "System.Int32"));
				meta.AddTypeMap("Opacity", new esTypeMap("Int", "System.Int32"));			
				
				
				
				meta.Source = "Template";
				meta.Destination = "Template";
				
				meta.spInsert = "proc_TemplateInsert";				
				meta.spUpdate = "proc_TemplateUpdate";		
				meta.spDelete = "proc_TemplateDelete";
				meta.spLoadAll = "proc_TemplateLoadAll";
				meta.spLoadByPrimaryKey = "proc_TemplateLoadByPrimaryKey";
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private TemplateMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
