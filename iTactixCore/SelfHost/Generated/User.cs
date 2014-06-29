
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
	/// Encapsulates the 'User' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[XmlType("User")]
	public partial class User : esUser
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new User();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Int64 id)
		{
			var obj = new User();
			obj.Id = id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Int64 id, esSqlAccessType sqlAccessType)
		{
			var obj = new User();
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
	[XmlType("UserCollection")]
	public partial class UserCollection : esUserCollection, IEnumerable<User>
	{
		public User FindByPrimaryKey(System.Int64 id)
		{
			return this.SingleOrDefault(e => e.Id == id);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(User))]
		public class UserCollectionWCFPacket : esCollectionWCFPacket<UserCollection>
		{
			public static implicit operator UserCollection(UserCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator UserCollectionWCFPacket(UserCollection collection)
			{
				return new UserCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class UserQuery : esUserQuery
	{
		public UserQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "UserQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(UserQuery query)
		{
			return UserQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator UserQuery(string query)
		{
			return (UserQuery)UserQuery.SerializeHelper.FromXml(query, typeof(UserQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esUser : esEntity
	{
		public esUser()
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
			UserQuery query = new UserQuery();
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
		/// Maps to User.id
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int64? Id
		{
			get
			{
				return base.GetSystemInt64(UserMetadata.ColumnNames.Id);
			}
			
			set
			{
				if(base.SetSystemInt64(UserMetadata.ColumnNames.Id, value))
				{
					OnPropertyChanged(UserMetadata.PropertyNames.Id);
				}
			}
		}		
		
		/// <summary>
		/// Maps to User.loginid
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Loginid
		{
			get
			{
				return base.GetSystemString(UserMetadata.ColumnNames.Loginid);
			}
			
			set
			{
				if(base.SetSystemString(UserMetadata.ColumnNames.Loginid, value))
				{
					OnPropertyChanged(UserMetadata.PropertyNames.Loginid);
				}
			}
		}		
		
		/// <summary>
		/// Maps to User.password
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Password
		{
			get
			{
				return base.GetSystemString(UserMetadata.ColumnNames.Password);
			}
			
			set
			{
				if(base.SetSystemString(UserMetadata.ColumnNames.Password, value))
				{
					OnPropertyChanged(UserMetadata.PropertyNames.Password);
				}
			}
		}		
		
		/// <summary>
		/// Maps to User.fullname
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Fullname
		{
			get
			{
				return base.GetSystemString(UserMetadata.ColumnNames.Fullname);
			}
			
			set
			{
				if(base.SetSystemString(UserMetadata.ColumnNames.Fullname, value))
				{
					OnPropertyChanged(UserMetadata.PropertyNames.Fullname);
				}
			}
		}		
		
		/// <summary>
		/// Maps to User.enabled
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Boolean? Enabled
		{
			get
			{
				return base.GetSystemBoolean(UserMetadata.ColumnNames.Enabled);
			}
			
			set
			{
				if(base.SetSystemBoolean(UserMetadata.ColumnNames.Enabled, value))
				{
					OnPropertyChanged(UserMetadata.PropertyNames.Enabled);
				}
			}
		}		
		
		/// <summary>
		/// Maps to User.lastlogin
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.DateTime? Lastlogin
		{
			get
			{
				return base.GetSystemDateTime(UserMetadata.ColumnNames.Lastlogin);
			}
			
			set
			{
				if(base.SetSystemDateTime(UserMetadata.ColumnNames.Lastlogin, value))
				{
					OnPropertyChanged(UserMetadata.PropertyNames.Lastlogin);
				}
			}
		}		
		
		/// <summary>
		/// Maps to User.groupid
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Groupid
		{
			get
			{
				return base.GetSystemString(UserMetadata.ColumnNames.Groupid);
			}
			
			set
			{
				if(base.SetSystemString(UserMetadata.ColumnNames.Groupid, value))
				{
					OnPropertyChanged(UserMetadata.PropertyNames.Groupid);
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
						case "Loginid": this.str().Loginid = (string)value; break;							
						case "Password": this.str().Password = (string)value; break;							
						case "Fullname": this.str().Fullname = (string)value; break;							
						case "Enabled": this.str().Enabled = (string)value; break;							
						case "Lastlogin": this.str().Lastlogin = (string)value; break;							
						case "Groupid": this.str().Groupid = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Id":
						
							if (value == null || value is System.Int64)
								this.Id = (System.Int64?)value;
								OnPropertyChanged(UserMetadata.PropertyNames.Id);
							break;
						
						case "Enabled":
						
							if (value == null || value is System.Boolean)
								this.Enabled = (System.Boolean?)value;
								OnPropertyChanged(UserMetadata.PropertyNames.Enabled);
							break;
						
						case "Lastlogin":
						
							if (value == null || value is System.DateTime)
								this.Lastlogin = (System.DateTime?)value;
								OnPropertyChanged(UserMetadata.PropertyNames.Lastlogin);
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
			public esStrings(esUser entity)
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
				
			public System.String Loginid
			{
				get
				{
					System.String data = entity.Loginid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Loginid = null;
					else entity.Loginid = Convert.ToString(value);
				}
			}
				
			public System.String Password
			{
				get
				{
					System.String data = entity.Password;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Password = null;
					else entity.Password = Convert.ToString(value);
				}
			}
				
			public System.String Fullname
			{
				get
				{
					System.String data = entity.Fullname;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Fullname = null;
					else entity.Fullname = Convert.ToString(value);
				}
			}
				
			public System.String Enabled
			{
				get
				{
					System.Boolean? data = entity.Enabled;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Enabled = null;
					else entity.Enabled = Convert.ToBoolean(value);
				}
			}
				
			public System.String Lastlogin
			{
				get
				{
					System.DateTime? data = entity.Lastlogin;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Lastlogin = null;
					else entity.Lastlogin = Convert.ToDateTime(value);
				}
			}
				
			public System.String Groupid
			{
				get
				{
					System.String data = entity.Groupid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Groupid = null;
					else entity.Groupid = Convert.ToString(value);
				}
			}
			

			private esUser entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return UserMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public UserQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new UserQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(UserQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(UserQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private UserQuery query;		
	}



	[Serializable]
	abstract public partial class esUserCollection : esEntityCollection<User>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return UserMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "UserCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public UserQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new UserQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(UserQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new UserQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(UserQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((UserQuery)query);
		}

		#endregion
		
		private UserQuery query;
	}



	[Serializable]
	abstract public partial class esUserQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return UserMetadata.Meta();
			}
		}	
		
		#region esQueryItems

		public esQueryItem Id
		{
			get { return new esQueryItem(this, UserMetadata.ColumnNames.Id, esSystemType.Int64); }
		} 
		
		public esQueryItem Loginid
		{
			get { return new esQueryItem(this, UserMetadata.ColumnNames.Loginid, esSystemType.String); }
		} 
		
		public esQueryItem Password
		{
			get { return new esQueryItem(this, UserMetadata.ColumnNames.Password, esSystemType.String); }
		} 
		
		public esQueryItem Fullname
		{
			get { return new esQueryItem(this, UserMetadata.ColumnNames.Fullname, esSystemType.String); }
		} 
		
		public esQueryItem Enabled
		{
			get { return new esQueryItem(this, UserMetadata.ColumnNames.Enabled, esSystemType.Boolean); }
		} 
		
		public esQueryItem Lastlogin
		{
			get { return new esQueryItem(this, UserMetadata.ColumnNames.Lastlogin, esSystemType.DateTime); }
		} 
		
		public esQueryItem Groupid
		{
			get { return new esQueryItem(this, UserMetadata.ColumnNames.Groupid, esSystemType.String); }
		} 
		
		#endregion
		
	}


	
	public partial class User : esUser
	{

		
		
	}
	



	[Serializable]
	public partial class UserMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected UserMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(UserMetadata.ColumnNames.Id, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = UserMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(UserMetadata.ColumnNames.Loginid, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = UserMetadata.PropertyNames.Loginid;
			c.CharacterMaxLength = 32;
			m_columns.Add(c);
				
			c = new esColumnMetadata(UserMetadata.ColumnNames.Password, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = UserMetadata.PropertyNames.Password;
			c.CharacterMaxLength = 32;
			m_columns.Add(c);
				
			c = new esColumnMetadata(UserMetadata.ColumnNames.Fullname, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = UserMetadata.PropertyNames.Fullname;
			c.CharacterMaxLength = 64;
			m_columns.Add(c);
				
			c = new esColumnMetadata(UserMetadata.ColumnNames.Enabled, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = UserMetadata.PropertyNames.Enabled;
			c.HasDefault = true;
			c.Default = @"true";
			m_columns.Add(c);
				
			c = new esColumnMetadata(UserMetadata.ColumnNames.Lastlogin, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = UserMetadata.PropertyNames.Lastlogin;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(UserMetadata.ColumnNames.Groupid, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = UserMetadata.PropertyNames.Groupid;
			c.CharacterMaxLength = 32;
			c.IsNullable = true;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public UserMetadata Meta()
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
			 public const string Loginid = "loginid";
			 public const string Password = "password";
			 public const string Fullname = "fullname";
			 public const string Enabled = "enabled";
			 public const string Lastlogin = "lastlogin";
			 public const string Groupid = "groupid";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string Loginid = "Loginid";
			 public const string Password = "Password";
			 public const string Fullname = "Fullname";
			 public const string Enabled = "Enabled";
			 public const string Lastlogin = "Lastlogin";
			 public const string Groupid = "Groupid";
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
			lock (typeof(UserMetadata))
			{
				if(UserMetadata.mapDelegates == null)
				{
					UserMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (UserMetadata.meta == null)
				{
					UserMetadata.meta = new UserMetadata();
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
				meta.AddTypeMap("Loginid", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Password", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Fullname", new esTypeMap("VarChar", "System.String"));
				meta.AddTypeMap("Enabled", new esTypeMap("Bit", "System.Boolean"));
				meta.AddTypeMap("Lastlogin", new esTypeMap("DateTime", "System.DateTime"));
				meta.AddTypeMap("Groupid", new esTypeMap("VarChar", "System.String"));			
				
				
				
				meta.Source = "User";
				meta.Destination = "User";
				
				meta.spInsert = "proc_UserInsert";				
				meta.spUpdate = "proc_UserUpdate";		
				meta.spDelete = "proc_UserDelete";
				meta.spLoadAll = "proc_UserLoadAll";
				meta.spLoadByPrimaryKey = "proc_UserLoadByPrimaryKey";
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private UserMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
