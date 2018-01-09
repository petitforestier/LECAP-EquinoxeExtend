using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Data.EntityClient;

namespace EquinoxeExtend.Shared.Enum
{
    public static class Extension
    {
        #region Public METHODS

        public static Tuple<string, string> GetLoginPassword(this EnvironmentEnum iValue)
        {
            Type type = iValue.GetType();
            FieldInfo fieldInfo = type.GetField(iValue.ToString());
            LoginPasswordAttribute[] attrs = fieldInfo.GetCustomAttributes(typeof(LoginPasswordAttribute), false) as LoginPasswordAttribute[];
            LoginPasswordAttribute value = (LoginPasswordAttribute)attrs.FirstOrDefault();
            return value != null ? new Tuple<string, string>(value.Login, value.Password) : null;
        }

        public static string GetDWConnectionString(this EnvironmentEnum iValue)
        {
            Type type = iValue.GetType();
            FieldInfo fieldInfo = type.GetField(iValue.ToString());
            DWConnectionStringAttribute[] attrs = fieldInfo.GetCustomAttributes(typeof(DWConnectionStringAttribute), false) as DWConnectionStringAttribute[];
            DWConnectionStringAttribute value = (DWConnectionStringAttribute)attrs.FirstOrDefault();
            return value != null ? value.DWConnectionString : null;
        }

        public static string GetSQLConnectionString(this EnvironmentEnum iValue)
        {
            Type type = iValue.GetType();
            FieldInfo fieldInfo = type.GetField(iValue.ToString());
            SQLConnectionStringAttribute[] attrs = fieldInfo.GetCustomAttributes(typeof(SQLConnectionStringAttribute), false) as SQLConnectionStringAttribute[];
            SQLConnectionStringAttribute value = (SQLConnectionStringAttribute)attrs.FirstOrDefault();
            return value != null ? value.SQLConnectionString : null;
        }

        public static string GetSQLExtendConnectionString(this EnvironmentEnum iValue)
        {
            Type type = iValue.GetType();
            FieldInfo fieldInfo = type.GetField(iValue.ToString());
            SQLExtendConnectionStringAttribute[] attrs = fieldInfo.GetCustomAttributes(typeof(SQLExtendConnectionStringAttribute), false) as SQLExtendConnectionStringAttribute[];
            SQLExtendConnectionStringAttribute value = (SQLExtendConnectionStringAttribute)attrs.FirstOrDefault();
            return value != null ? value.SQLExtendConnectionString : null;
        }

        public static string GetSpecificationPrefix(this EnvironmentEnum iValue)
        {
            Type type = iValue.GetType();
            FieldInfo fieldInfo = type.GetField(iValue.ToString());
            SpecificationPrefixAttribute[] attrs = fieldInfo.GetCustomAttributes(typeof(SpecificationPrefixAttribute), false) as SpecificationPrefixAttribute[];
            SpecificationPrefixAttribute value = (SpecificationPrefixAttribute)attrs.FirstOrDefault();
            return value != null ? value.SpecificationPrefix : null;
        }

        public static string GetDevelopperTeam(this EnvironmentEnum iValue)
        {
            Type type = iValue.GetType();
            FieldInfo fieldInfo = type.GetField(iValue.ToString());
            DevelopperTeamAttribute[] attrs = fieldInfo.GetCustomAttributes(typeof(DevelopperTeamAttribute), false) as DevelopperTeamAttribute[];
            DevelopperTeamAttribute value = (DevelopperTeamAttribute)attrs.FirstOrDefault();
            return value != null ? value.DevelopperTeam : null;
        }

        public static string GetProjectDirectory(this EnvironmentEnum iValue)
        {
            Type type = iValue.GetType();
            FieldInfo fieldInfo = type.GetField(iValue.ToString());
            ProjectDirectoryAttribute[] attrs = fieldInfo.GetCustomAttributes(typeof(ProjectDirectoryAttribute), false) as ProjectDirectoryAttribute[];
            ProjectDirectoryAttribute value = (ProjectDirectoryAttribute)attrs.FirstOrDefault();
            return value != null ? value.ProjectDirectory : null;
        }

        public static string GetProjectDirectoryArchive(this EnvironmentEnum iValue)
        {
            Type type = iValue.GetType();
            FieldInfo fieldInfo = type.GetField(iValue.ToString());
            ProjectDirectoryArchiveAttribute[] attrs = fieldInfo.GetCustomAttributes(typeof(ProjectDirectoryArchiveAttribute), false) as ProjectDirectoryArchiveAttribute[];
            ProjectDirectoryArchiveAttribute value = (ProjectDirectoryArchiveAttribute)attrs.FirstOrDefault();
            return value != null ? value.ProjectDirectoryArchive : null;
        }

        public static string GetPluginDirectory(this EnvironmentEnum iValue)
        {
            Type type = iValue.GetType();
            FieldInfo fieldInfo = type.GetField(iValue.ToString());
            PluginDirectoryAttribute[] attrs = fieldInfo.GetCustomAttributes(typeof(PluginDirectoryAttribute), false) as PluginDirectoryAttribute[];
            PluginDirectoryAttribute value = (PluginDirectoryAttribute)attrs.FirstOrDefault();
            return value != null ? value.PluginDirectory : null;
        }

        public static string GetPluginDirectoryArchive(this EnvironmentEnum iValue)
        {
            Type type = iValue.GetType();
            FieldInfo fieldInfo = type.GetField(iValue.ToString());
            PluginDirectoryArchiveAttribute[] attrs = fieldInfo.GetCustomAttributes(typeof(PluginDirectoryArchiveAttribute), false) as PluginDirectoryArchiveAttribute[];
            PluginDirectoryArchiveAttribute value = (PluginDirectoryArchiveAttribute)attrs.FirstOrDefault();
            return value != null ? value.PluginDirectoryArchive : null;
        }

        #endregion
    }

    public class LoginPasswordAttribute : Attribute
    {
        #region Public PROPERTIES

        public string Login { get; protected set; }
        public string Password { get; protected set; }

        #endregion

        #region Public CONSTRUCTORS

        public LoginPasswordAttribute(string iLogin, string iPassword)
        {
            this.Login = iLogin;
            this.Password = iPassword;
        }

        #endregion
    }

    public class SQLConnectionStringAttribute : Attribute
    {
        #region Public PROPERTIES

        public string SQLConnectionString { get; protected set; }

        #endregion

        #region Public CONSTRUCTORS

        public SQLConnectionStringAttribute(string iValue)
        {
            this.SQLConnectionString = iValue;
            //var entityConnectionStringBuilder = new EntityConnectionStringBuilder();
            //entityConnectionStringBuilder.Provider = "System.Data.SqlClient";
            //entityConnectionStringBuilder.ProviderConnectionString = iValue;
            //entityConnectionStringBuilder.Metadata = "res://*";
            //this.SQLConnectionString = entityConnectionStringBuilder.ToString();
        }

        #endregion
    }

    public class DWConnectionStringAttribute : Attribute
    {
        #region Public PROPERTIES

        public string DWConnectionString { get; protected set; }

        #endregion

        #region Public CONSTRUCTORS

        public DWConnectionStringAttribute(string iValue)
        {
            this.DWConnectionString = iValue;
        }

        #endregion
    }

    public class SQLExtendConnectionStringAttribute : Attribute
    {
        #region Public PROPERTIES

        public string SQLExtendConnectionString { get; protected set; }

        #endregion

        #region Public CONSTRUCTORS

        public SQLExtendConnectionStringAttribute(string iValue)
        {
            var entityConnectionStringBuilder = new EntityConnectionStringBuilder();
            entityConnectionStringBuilder.Provider = "System.Data.SqlClient";
            entityConnectionStringBuilder.ProviderConnectionString = iValue;
            entityConnectionStringBuilder.Metadata = "res://*";
            this.SQLExtendConnectionString = entityConnectionStringBuilder.ToString();
        }

        #endregion
    }

    public class SpecificationPrefixAttribute : Attribute
    {
        #region Public PROPERTIES

        public string SpecificationPrefix { get; protected set; }

        #endregion

        #region Public CONSTRUCTORS

        public SpecificationPrefixAttribute(string iValue)
        {
            this.SpecificationPrefix = iValue;
        }

        #endregion
    }

    public class DevelopperTeamAttribute : Attribute
    {
        #region Public PROPERTIES

        public string DevelopperTeam { get; protected set; }

        #endregion

        #region Public CONSTRUCTORS

        public DevelopperTeamAttribute(string iValue)
        {
            this.DevelopperTeam = iValue;
        }

        #endregion
    }

    public class ProjectDirectoryAttribute : Attribute
    {
        #region Public PROPERTIES

        public string ProjectDirectory { get; protected set; }

        #endregion

        #region Public CONSTRUCTORS

        public ProjectDirectoryAttribute(string iValue)
        {
            this.ProjectDirectory = iValue;
        }

        #endregion
    }

    public class ProjectDirectoryArchiveAttribute : Attribute
    {
        #region Public PROPERTIES

        public string ProjectDirectoryArchive { get; protected set; }

        #endregion

        #region Public CONSTRUCTORS

        public ProjectDirectoryArchiveAttribute(string iValue)
        {
            this.ProjectDirectoryArchive = iValue;
        }

        #endregion
    }

    public class PluginDirectoryAttribute : Attribute
    {
        #region Public PROPERTIES

        public string PluginDirectory { get; protected set; }

        #endregion

        #region Public CONSTRUCTORS

        public PluginDirectoryAttribute(string iValue)
        {
            this.PluginDirectory = iValue;
        }

        #endregion
    }

    public class PluginDirectoryArchiveAttribute : Attribute
    {
        #region Public PROPERTIES

        public string PluginDirectoryArchive { get; protected set; }

        #endregion

        #region Public CONSTRUCTORS

        public PluginDirectoryArchiveAttribute(string iValue)
        {
            this.PluginDirectoryArchive = iValue;
        }

        #endregion
    }

}