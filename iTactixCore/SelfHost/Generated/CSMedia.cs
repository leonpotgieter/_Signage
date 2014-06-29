
/*
===============================================================================
                    EntitySpaces 2011 by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2011.1.1110.0
EntitySpaces Driver  : VistaDB4
Date Generated       : 2012/07/24 03:47:50 PM
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
	/// Encapsulates the 'CSMedia' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[XmlType("CSMedia")]
	public partial class CSMedia : esCSMedia
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new CSMedia();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Int64 id)
		{
			var obj = new CSMedia();
			obj.Id = id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Int64 id, esSqlAccessType sqlAccessType)
		{
			var obj = new CSMedia();
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
	[XmlType("CSMediaCollection")]
	public partial class CSMediaCollection : esCSMediaCollection, IEnumerable<CSMedia>
	{
		public CSMedia FindByPrimaryKey(System.Int64 id)
		{
			return this.SingleOrDefault(e => e.Id == id);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(CSMedia))]
		public class CSMediaCollectionWCFPacket : esCollectionWCFPacket<CSMediaCollection>
		{
			public static implicit operator CSMediaCollection(CSMediaCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator CSMediaCollectionWCFPacket(CSMediaCollection collection)
			{
				return new CSMediaCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class CSMediaQuery : esCSMediaQuery
	{
		public CSMediaQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "CSMediaQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(CSMediaQuery query)
		{
			return CSMediaQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator CSMediaQuery(string query)
		{
			return (CSMediaQuery)CSMediaQuery.SerializeHelper.FromXml(query, typeof(CSMediaQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esCSMedia : esEntity
	{
		public esCSMedia()
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
			CSMediaQuery query = new CSMediaQuery();
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
		/// Maps to CSMedia.id
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int64? Id
		{
			get
			{
				return base.GetSystemInt64(CSMediaMetadata.ColumnNames.Id);
			}
			
			set
			{
				if(base.SetSystemInt64(CSMediaMetadata.ColumnNames.Id, value))
				{
					OnPropertyChanged(CSMediaMetadata.PropertyNames.Id);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSMedia.mediatype
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Mediatype
		{
			get
			{
				return base.GetSystemString(CSMediaMetadata.ColumnNames.Mediatype);
			}
			
			set
			{
				if(base.SetSystemString(CSMediaMetadata.ColumnNames.Mediatype, value))
				{
					OnPropertyChanged(CSMediaMetadata.PropertyNames.Mediatype);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSMedia.isfile
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Byte? Isfile
		{
			get
			{
				return base.GetSystemByte(CSMediaMetadata.ColumnNames.Isfile);
			}
			
			set
			{
				if(base.SetSystemByte(CSMediaMetadata.ColumnNames.Isfile, value))
				{
					OnPropertyChanged(CSMediaMetadata.PropertyNames.Isfile);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSMedia.mediablob
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Byte[] Mediablob
		{
			get
			{
				return base.GetSystemByteArray(CSMediaMetadata.ColumnNames.Mediablob);
			}
			
			set
			{
				if(base.SetSystemByteArray(CSMediaMetadata.ColumnNames.Mediablob, value))
				{
					OnPropertyChanged(CSMediaMetadata.PropertyNames.Mediablob);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSMedia.mediafilename
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Mediafilename
		{
			get
			{
				return base.GetSystemString(CSMediaMetadata.ColumnNames.Mediafilename);
			}
			
			set
			{
				if(base.SetSystemString(CSMediaMetadata.ColumnNames.Mediafilename, value))
				{
					OnPropertyChanged(CSMediaMetadata.PropertyNames.Mediafilename);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSMedia.attributes1
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Attributes1
		{
			get
			{
				return base.GetSystemString(CSMediaMetadata.ColumnNames.Attributes1);
			}
			
			set
			{
				if(base.SetSystemString(CSMediaMetadata.ColumnNames.Attributes1, value))
				{
					OnPropertyChanged(CSMediaMetadata.PropertyNames.Attributes1);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSMedia.attributes2
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Attributes2
		{
			get
			{
				return base.GetSystemString(CSMediaMetadata.ColumnNames.Attributes2);
			}
			
			set
			{
				if(base.SetSystemString(CSMediaMetadata.ColumnNames.Attributes2, value))
				{
					OnPropertyChanged(CSMediaMetadata.PropertyNames.Attributes2);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSMedia.attributes3
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Attributes3
		{
			get
			{
				return base.GetSystemString(CSMediaMetadata.ColumnNames.Attributes3);
			}
			
			set
			{
				if(base.SetSystemString(CSMediaMetadata.ColumnNames.Attributes3, value))
				{
					OnPropertyChanged(CSMediaMetadata.PropertyNames.Attributes3);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSMedia.retainaspect
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Byte? Retainaspect
		{
			get
			{
				return base.GetSystemByte(CSMediaMetadata.ColumnNames.Retainaspect);
			}
			
			set
			{
				if(base.SetSystemByte(CSMediaMetadata.ColumnNames.Retainaspect, value))
				{
					OnPropertyChanged(CSMediaMetadata.PropertyNames.Retainaspect);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSMedia.datetimeimported
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.DateTime? Datetimeimported
		{
			get
			{
				return base.GetSystemDateTime(CSMediaMetadata.ColumnNames.Datetimeimported);
			}
			
			set
			{
				if(base.SetSystemDateTime(CSMediaMetadata.ColumnNames.Datetimeimported, value))
				{
					OnPropertyChanged(CSMediaMetadata.PropertyNames.Datetimeimported);
				}
			}
		}		
		
		/// <summary>
		/// Maps to CSMedia.importedbywho
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Importedbywho
		{
			get
			{
				return base.GetSystemString(CSMediaMetadata.ColumnNames.Importedbywho);
			}
			
			set
			{
				if(base.SetSystemString(CSMediaMetadata.ColumnNames.Importedbywho, value))
				{
					OnPropertyChanged(CSMediaMetadata.PropertyNames.Importedbywho);
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
						case "Mediatype": this.str().Mediatype = (string)value; break;							
						case "Isfile": this.str().Isfile = (string)value; break;							
						case "Mediafilename": this.str().Mediafilename = (string)value; break;							
						case "Attributes1": this.str().Attributes1 = (string)value; break;							
						case "Attributes2": this.str().Attributes2 = (string)value; break;							
						case "Attributes3": this.str().Attributes3 = (string)value; break;							
						case "Retainaspect": this.str().Retainaspect = (string)value; break;							
						case "Datetimeimported": this.str().Datetimeimported = (string)value; break;							
						case "Importedbywho": this.str().Importedbywho = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Id":
						
							if (value == null || value is System.Int64)
								this.Id = (System.Int64?)value;
								OnPropertyChanged(CSMediaMetadata.PropertyNames.Id);
							break;
						
						case "Isfile":
						
							if (value == null || value is System.Byte)
								this.Isfile = (System.Byte?)value;
								OnPropertyChanged(CSMediaMetadata.PropertyNames.Isfile);
							break;
						
						case "Mediablob":
						
							if (value == null || value is System.Byte[])
								this.Mediablob = (System.Byte[])value;
								OnPropertyChanged(CSMediaMetadata.PropertyNames.Mediablob);
							break;
						
						case "Retainaspect":
						
							if (value == null || value is System.Byte)
								this.Retainaspect = (System.Byte?)value;
								OnPropertyChanged(CSMediaMetadata.PropertyNames.Retainaspect);
							break;
						
						case "Datetimeimported":
						
							if (value == null || value is System.DateTime)
								this.Datetimeimported = (System.DateTime?)value;
								OnPropertyChanged(CSMediaMetadata.PropertyNames.Datetimeimported);
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
			public esStrings(esCSMedia entity)
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
				
			public System.String Isfile
			{
				get
				{
					System.Byte? data = entity.Isfile;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Isfile = null;
					else entity.Isfile = Convert.ToByte(value);
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
				
			public System.String Attributes1
			{
				get
				{
					System.String data = entity.Attributes1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Attributes1 = null;
					else entity.Attributes1 = Convert.ToString(value);
				}
			}
				
			public System.String Attributes2
			{
				get
				{
					System.String data = entity.Attributes2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Attributes2 = null;
					else entity.Attributes2 = Convert.ToString(value);
				}
			}
				
			public System.String Attributes3
			{
				get
				{
					System.String data = entity.Attributes3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Attributes3 = null;
					else entity.Attributes3 = Convert.ToString(value);
				}
			}
				
			public System.String Retainaspect
			{
				get
				{
					System.Byte? data = entity.Retainaspect;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Retainaspect = null;
					else entity.Retainaspect = Convert.ToByte(value);
				}
			}
				
			public System.String Datetimeimported
			{
				get
				{
					System.DateTime? data = entity.Datetimeimported;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Datetimeimported = null;
					else entity.Datetimeimported = Convert.ToDateTime(value);
				}
			}
				
			public System.String Importedbywho
			{
				get
				{
					System.String data = entity.Importedbywho;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Importedbywho = null;
					else entity.Importedbywho = Convert.ToString(value);
				}
			}
			

			private esCSMedia entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return CSMediaMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public CSMediaQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CSMediaQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(CSMediaQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(CSMediaQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private CSMediaQuery query;		
	}



	[Serializable]
	abstract public partial class esCSMediaCollection : esEntityCollection<CSMedia>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CSMediaMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "CSMediaCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public CSMediaQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CSMediaQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(CSMediaQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CSMediaQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(CSMediaQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((CSMediaQuery)query);
		}

		#endregion
		
		private CSMediaQuery query;
	}



	[Serializable]
	abstract public partial class esCSMediaQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return CSMediaMetadata.Meta();
			}
		}	
		
		#region esQueryItems

		public esQueryItem Id
		{
			get { return new esQueryItem(this, CSMediaMetadata.ColumnNames.Id, esSystemType.Int64); }
		} 
		
		public esQueryItem Mediatype
		{
			get { return new esQueryItem(this, CSMediaMetadata.ColumnNames.Mediatype, esSystemType.String); }
		} 
		
		public esQueryItem Isfile
		{
			get { return new esQueryItem(this, CSMediaMetadata.ColumnNames.Isfile, esSystemType.Byte); }
		} 
		
		public esQueryItem Mediablob
		{
			get { return new esQueryItem(this, CSMediaMetadata.ColumnNames.Mediablob, esSystemType.ByteArray); }
		} 
		
		public esQueryItem Mediafilename
		{
			get { return new esQueryItem(this, CSMediaMetadata.ColumnNames.Mediafilename, esSystemType.String); }
		} 
		
		public esQueryItem Attributes1
		{
			get { return new esQueryItem(this, CSMediaMetadata.ColumnNames.Attributes1, esSystemType.String); }
		} 
		
		public esQueryItem Attributes2
		{
			get { return new esQueryItem(this, CSMediaMetadata.ColumnNames.Attributes2, esSystemType.String); }
		} 
		
		public esQueryItem Attributes3
		{
			get { return new esQueryItem(this, CSMediaMetadata.ColumnNames.Attributes3, esSystemType.String); }
		} 
		
		public esQueryItem Retainaspect
		{
			get { return new esQueryItem(this, CSMediaMetadata.ColumnNames.Retainaspect, esSystemType.Byte); }
		} 
		
		public esQueryItem Datetimeimported
		{
			get { return new esQueryItem(this, CSMediaMetadata.ColumnNames.Datetimeimported, esSystemType.DateTime); }
		} 
		
		public esQueryItem Importedbywho
		{
			get { return new esQueryItem(this, CSMediaMetadata.ColumnNames.Importedbywho, esSystemType.String); }
		} 
		
		#endregion
		
	}


	
	public partial class CSMedia : esCSMedia
	{

		
		
	}
	



	[Serializable]
	public partial class CSMediaMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CSMediaMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CSMediaMetadata.ColumnNames.Id, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = CSMediaMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSMediaMetadata.ColumnNames.Mediatype, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = CSMediaMetadata.PropertyNames.Mediatype;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSMediaMetadata.ColumnNames.Isfile, 2, typeof(System.Byte), esSystemType.Byte);
			c.PropertyName = CSMediaMetadata.PropertyNames.Isfile;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSMediaMetadata.ColumnNames.Mediablob, 3, typeof(System.Byte[]), esSystemType.ByteArray);
			c.PropertyName = CSMediaMetadata.PropertyNames.Mediablob;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSMediaMetadata.ColumnNames.Mediafilename, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = CSMediaMetadata.PropertyNames.Mediafilename;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSMediaMetadata.ColumnNames.Attributes1, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = CSMediaMetadata.PropertyNames.Attributes1;
			c.CharacterMaxLength = 64;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSMediaMetadata.ColumnNames.Attributes2, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = CSMediaMetadata.PropertyNames.Attributes2;
			c.CharacterMaxLength = 64;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSMediaMetadata.ColumnNames.Attributes3, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = CSMediaMetadata.PropertyNames.Attributes3;
			c.CharacterMaxLength = 64;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSMediaMetadata.ColumnNames.Retainaspect, 8, typeof(System.Byte), esSystemType.Byte);
			c.PropertyName = CSMediaMetadata.PropertyNames.Retainaspect;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSMediaMetadata.ColumnNames.Datetimeimported, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CSMediaMetadata.PropertyNames.Datetimeimported;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CSMediaMetadata.ColumnNames.Importedbywho, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = CSMediaMetadata.PropertyNames.Importedbywho;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public CSMediaMetadata Meta()
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
			 public const string Mediatype = "mediatype";
			 public const string Isfile = "isfile";
			 public const string Mediablob = "mediablob";
			 public const string Mediafilename = "mediafilename";
			 public const string Attributes1 = "attributes1";
			 public const string Attributes2 = "attributes2";
			 public const string Attributes3 = "attributes3";
			 public const string Retainaspect = "retainaspect";
			 public const string Datetimeimported = "datetimeimported";
			 public const string Importedbywho = "importedbywho";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string Mediatype = "Mediatype";
			 public const string Isfile = "Isfile";
			 public const string Mediablob = "Mediablob";
			 public const string Mediafilename = "Mediafilename";
			 public const string Attributes1 = "Attributes1";
			 public const string Attributes2 = "Attributes2";
			 public const string Attributes3 = "Attributes3";
			 public const string Retainaspect = "Retainaspect";
			 public const string Datetimeimported = "Datetimeimported";
			 public const string Importedbywho = "Importedbywho";
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
			lock (typeof(CSMediaMetadata))
			{
				if(CSMediaMetadata.mapDelegates == null)
				{
					CSMediaMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (CSMediaMetadata.meta == null)
				{
					CSMediaMetadata.meta = new CSMediaMetadata();
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
				meta.AddTypeMap("Mediatype", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Isfile", new esTypeMap("TinyInt", "System.Byte"));
				meta.AddTypeMap("Mediablob", new esTypeMap("Image", "System.Byte[]"));
				meta.AddTypeMap("Mediafilename", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Attributes1", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Attributes2", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Attributes3", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Retainaspect", new esTypeMap("TinyInt", "System.Byte"));
				meta.AddTypeMap("Datetimeimported", new esTypeMap("DateTime", "System.DateTime"));
				meta.AddTypeMap("Importedbywho", new esTypeMap("VarChar", "System.String"));			
				
				
				
				meta.Source = "CSMedia";
				meta.Destination = "CSMedia";
				
				meta.spInsert = "proc_CSMediaInsert";				
				meta.spUpdate = "proc_CSMediaUpdate";		
				meta.spDelete = "proc_CSMediaDelete";
				meta.spLoadAll = "proc_CSMediaLoadAll";
				meta.spLoadByPrimaryKey = "proc_CSMediaLoadByPrimaryKey";
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CSMediaMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
