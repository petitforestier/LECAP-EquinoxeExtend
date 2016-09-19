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
        [ConnectionString("Provider=RemoteGroupProvider;Server=S2SQL01;Name=Equinoxe_SandBox")]
        [ExtendConnectionString("Server = S2SQL01; Database = Equinoxe_SandBox_Extend;User Id=sa;password=SQLequinoxe")]
        [SpecificationPrefix("7")]
        [DevelopperTeam("temRole_Developpeurs")]
        [Name("FR", "Equinoxe_SandBox")]
        [ProjectDirectory("Q:\\Masters_PréProd")]
        [ProjectDirectoryArchive("Q:\\Masters_Sandbox\\_Archives")]
        Sandbox = 10,

        [LoginPassword("Admin", "DWAequinoxe")]
        [ConnectionString("Provider=RemoteGroupProvider;Server=S2SQL01;Name=Equinoxe_Dev")]
        [ExtendConnectionString("Server = S2SQL01; Database = Equinoxe_Dev_Extend;User Id=sa;password=SQLequinoxe")]
        [SpecificationPrefix("7")]
        [DevelopperTeam("temRole_Developpeurs")]
        [Name("FR", "Equinoxe_Dev")]
        [ProjectDirectory("Q:\\Masters_Dev\\1-Projet")]
        [ProjectDirectoryArchive("Q:\\Masters_Dev\\1-Projet\\_Archives")]
        [PluginDirectory("Q:\\Masters_Dev\\0-Plugin")]
        [PluginDirectoryArchive("Q:\\Masters_Dev\\0-Plugin\\_Archives")]
        Developpement = 20,

        [LoginPassword("Admin", "DWAequinoxe")]
        [ConnectionString("Provider=RemoteGroupProvider;Server=S2SQL01;Name=Equinoxe_Préprod")]
        [ExtendConnectionString("Server = S2SQL01; Database = Equinoxe_Préprod_Extend;User Id=sa;password=SQLequinoxe")]
        [SpecificationPrefix("7")]
        [DevelopperTeam("temRole_Developpeurs")]
        [Name("FR", "Equinoxe_Préprod")]
        [ProjectDirectory("Q:\\Masters_PréProd\\1-Projet")]
        [ProjectDirectoryArchive("Q:\\Masters_PréProd\\1-Projet\\_Archives")]
        [PluginDirectory("Q:\\Masters_PréProd\\0-Plugin")]
        [PluginDirectoryArchive("Q:\\Masters_PréProd\\0-Plugin\\_Archives")]
        Staging = 30,

        [LoginPassword("Admin", "DWAequinoxe")]
        [ConnectionString("Provider=RemoteGroupProvider;Server=S2SQL01;Name=Equinoxe_Product")]
        [ExtendConnectionString("Server = S2SQL01; Database = Equinoxe_Product_Extend;User Id=sa;password=SQLequinoxe")]
        [SpecificationPrefix("5")]
        [DevelopperTeam("temRole_Developpeurs")]
        [Name("FR", "Equinoxe_Product")]
        [ProjectDirectory("Q:\\Masters_Product\\1-Projet")]
        [ProjectDirectoryArchive("Q:\\Masters_Product\\1-Projet\\_Archives")]
        [PluginDirectory("Q:\\Masters_Product\\0-Plugin")]
        [PluginDirectoryArchive("Q:\\Masters_Product\\0-Plugin\\_Archives")]
        Production = 40,

        [LoginPassword("Admin", "DWAequinoxe")]
        [ConnectionString("Provider=RemoteGroupProvider;Server=S2SQL01;Name=Equinoxe_Backup")]
        [ExtendConnectionString("Server = S2SQL01; Database = Equinoxe_Backup_Extend;User Id=sa;password=SQLequinoxe")]
        [SpecificationPrefix("7")]
        [DevelopperTeam("temRole_Developpeurs")]
        [Name("FR", "Equinoxe_Backup")]
        [ProjectDirectory("Q:\\Masters_Backup\\1-Projet")]
        [ProjectDirectoryArchive("Q:\\Masters_Backup\\1-Projet\\_Archives")]
        [PluginDirectory("Q:\\Masters_Backup\\0-Plugin")]
        [PluginDirectoryArchive("Q:\\Masters_Backup\\0-Plugin\\_Archives")]
        Backup = 50
    };
}
