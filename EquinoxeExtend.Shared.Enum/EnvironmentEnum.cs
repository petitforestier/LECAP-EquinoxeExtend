using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Tools.Attributes;

namespace EquinoxeExtend.Shared.Enum
{
    public enum EnvironmentEnum
    {
        [LoginPassword("Admin","")]
        [DWConnectionString("Provider=RemoteGroupProvider;Server=S2SQL01;Name=Equinoxe_SandBox")]
        [SQLConnectionString("Server = S2SQL01; Database = Equinoxe_SandBox;User Id=sa;password=SQLequinoxe")]
        [SQLExtendConnectionString("Server = S2SQL01; Database = Equinoxe_SandBox_Extend;User Id=sa;password=SQLequinoxe")]
        [SpecificationPrefix("7")]
        [DevelopperTeam("temRole_Developpeurs")]
        [Name("FR", "Equinoxe_SandBox")]
        [ProjectDirectory("\\s2sbe01\\Equinoxe\\Masters_PréProd\\")]
        [ProjectDirectoryArchive("\\s2sbe01\\Equinoxe\\Masters_Sandbox\\_Archives\\")]
        Sandbox = 10,

        [LoginPassword("Admin", "DWAequinoxe")]
        [DWConnectionString("Provider=RemoteGroupProvider;Server=S2SQL01;Name=Equinoxe_Dev")]
        [SQLConnectionString("Server = S2SQL01; Database = Equinoxe_Dev;User Id=sa;password=SQLequinoxe")]
        [SQLExtendConnectionString("Server = S2SQL01; Database = Equinoxe_Dev_Extend;User Id=sa;password=SQLequinoxe")]
        [SpecificationPrefix("7")]
        [DevelopperTeam("temRole_Developpeurs")]
        [Name("FR", "Equinoxe_Dev")]
        [ProjectDirectory("\\s2sbe01\\Equinoxe\\Masters_Dev\\1-Projet\\")]
        [ProjectDirectoryArchive("\\s2sbe01\\Equinoxe\\Masters_Dev\\1-Projet\\_Archives\\")]
        [PluginDirectory("\\s2sbe01\\Equinoxe\\Masters_Dev\\0-Plugin\\")]
        [PluginDirectoryArchive("\\s2sbe01\\Equinoxe\\Masters_Dev\\0-Plugin\\_Archives\\")]
        Developpement = 20,

        [LoginPassword("Admin", "DWAequinoxe")]
        [DWConnectionString("Provider=RemoteGroupProvider;Server=S2SQL01;Name=Equinoxe_Preprod")]
        [SQLConnectionString("Server = S2SQL01; Database = Equinoxe_Preprod;User Id=sa;password=SQLequinoxe")]
        [SQLExtendConnectionString("Server = S2SQL01; Database = Equinoxe_Preprod_Extend;User Id=sa;password=SQLequinoxe")]
        [SpecificationPrefix("7")]
        [DevelopperTeam("temRole_Developpeurs")]
        [Name("FR", "Equinoxe_PreProd")]
        [ProjectDirectory("\\s2sbe01\\Equinoxe\\Masters_PreProd\\1-Projet\\")]
        [ProjectDirectoryArchive("\\s2sbe01\\Equinoxe\\Masters_PreProd\\1-Projet\\_Archives\\")]
        [PluginDirectory("\\s2sbe01\\Equinoxe\\Masters_PreProd\\0-Plugin\\")]
        [PluginDirectoryArchive("\\s2sbe01\\Equinoxe\\Masters_PreProd\\0-Plugin\\_Archives\\")]
        Staging = 30,

        [LoginPassword("Admin", "DWAequinoxe")]
        [DWConnectionString("Provider=RemoteGroupProvider;Server=S2SQL01;Name=Equinoxe_Product")]
        [SQLConnectionString("Server = S2SQL01; Database = Equinoxe_Product;User Id=sa;password=SQLequinoxe")]
        [SQLExtendConnectionString("Server = S2SQL01; Database = Equinoxe_Product_Extend;User Id=sa;password=SQLequinoxe")]
        [SpecificationPrefix("5")]
        [DevelopperTeam("temRole_Developpeurs")]
        [Name("FR", "Equinoxe_Product")]
        [ProjectDirectory("\\s2sbe01\\Equinoxe\\Masters_Product\\1-Projet\\")]
        [ProjectDirectoryArchive("\\s2sbe01\\Equinoxe\\Masters_Product\\1-Projet\\_Archives\\")]
        [PluginDirectory("\\s2sbe01\\Equinoxe\\Masters_Product\\0-Plugin\\")]
        [PluginDirectoryArchive("\\s2sbe01\\Equinoxe\\Masters_Product\\0-Plugin\\_Archives\\")]
        Production = 40,

        [LoginPassword("Admin", "")]
        [DWConnectionString("Provider=RemoteGroupProvider;Server=S2SQL01;Name=Equinoxe_Backup")]
        [SQLConnectionString("Server = S2SQL01; Database = Equinoxe_Backup;User Id=sa;password=SQLequinoxe")]
        [SQLExtendConnectionString("Server = S2SQL01; Database = Equinoxe_Backup_Extend;User Id=sa;password=SQLequinoxe")]
        [SpecificationPrefix("7")]
        [DevelopperTeam("temRole_Developpeurs")]
        [Name("FR", "Equinoxe_Backup")]
        [ProjectDirectory("\\s2sbe01\\Equinoxe\\Masters_Backup\\1-Projet\\")]
        [ProjectDirectoryArchive("\\s2sbe01\\Equinoxe\\Masters_Backup\\1-Projet\\_Archives\\")]
        [PluginDirectory("\\s2sbe01\\Equinoxe\\Masters_Backup\\0-Plugin\\")]
        [PluginDirectoryArchive("\\s2sbe01\\Equinoxe\\Masters_Backup\\0-Plugin\\_Archives\\")]
        Backup = 50
    };
}
