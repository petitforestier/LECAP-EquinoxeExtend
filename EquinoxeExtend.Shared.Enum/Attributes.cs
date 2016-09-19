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

        public static string GetConnectionString(this EnvironmentEnum iValue)
        {
            Type type = iValue.GetType();
            FieldInfo fieldInfo = type.GetField(iValue.ToString());
            ConnectionStringAttribute[] attrs = fieldInfo.GetCustomAttributes(typeof(ConnectionStringAttribute), false) as ConnectionStringAttribute[];
            ConnectionStringAttribute value = (ConnectionStringAttribute)attrs.FirstOrDefault();
            return value != null ? value.ConnectionString : null;
        }

        public static string GetExtendConnectionString(this EnvironmentEnum iValue)
        {
            Type type = iValue.GetType();
            FieldInfo fieldInfo = type.GetField(iValue.ToString());
            ExtendConnectionStringAttribute[] attrs = fieldInfo.GetCustomAttributes(typeof(ExtendConnectionStringAttribute), false) as ExtendConnectionStringAttribute[];
            ExtendConnectionStringAttribute value = (ExtendConnectionStringAttribute)attrs.FirstOrDefault();
            return value != null ? value.ExtendConnectionString : null;
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

    public class ConnectionStringAttribute : Attribute
    {
        #region Public PROPERTIES

        public string ConnectionString { get; protected set; }

        #endregion

        #region Public CONSTRUCTORS

        public ConnectionStringAttribute(string iValue)
        {
            this.ConnectionString = iValue;
        }

        #endregion
    }

    public class ExtendConnectionStringAttribute : Attribute
    {
        #region Public PROPERTIES

        public string ExtendConnectionString { get; protected set; }

        #endregion

        #region Public CONSTRUCTORS

        public ExtendConnectionStringAttribute(string iValue)
        {
            var entityConnectionStringBuilder = new EntityConnectionStringBuilder();
            entityConnectionStringBuilder.Provider = "System.Data.SqlClient";
            entityConnectionStringBuilder.ProviderConnectionString = iValue;
            entityConnectionStringBuilder.Metadata = "res://*";
            this.ExtendConnectionString = entityConnectionStringBuilder.ToString();
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