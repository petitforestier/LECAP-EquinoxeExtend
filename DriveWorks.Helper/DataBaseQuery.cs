using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Library.Tools.Extensions;

namespace DriveWorks.Helper
{
    public class DataBaseQuery
    {
        #region Public CONSTRUCTORS

        public DataBaseQuery(string iConnectionString)
        {
            _ConnectionString = iConnectionString;
        }

        #endregion

        #region Public METHODS

        public List<DriveWorks.Helper.Object.Specification> GetSpecificationsByCreatorId(string iCreatorId)
        {
            var result = new List<DriveWorks.Helper.Object.Specification>();

            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                var queryString = "SELECT[Id],[CreatorId],[Name],[StateName] FROM [dbo].[Specifications] WHERE[CreatorId]= @CreatorId";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@CreatorId", iCreatorId);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var newSpecification = new DriveWorks.Helper.Object.Specification();
                            newSpecification.CreatorID = reader["CreatorId"].ToString();
                            newSpecification.Id = (int)reader["Id"];
                            newSpecification.Name = reader["Name"].ToString();
                            newSpecification.StateName = reader["StateName"].ToString();
                            result.Add(newSpecification);
                        }
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            return result;
        }

        public List<DriveWorks.Helper.Object.Specification> GetSpecificationsByStateName(string iStateName)
        {
            var result = new List<DriveWorks.Helper.Object.Specification>();

            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                var queryString = "SELECT [Id],[CreatorId],[Name],[StateName] FROM [dbo].[Specifications] WHERE[StateName]= @StateName";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@StateName", iStateName);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var newSpecification = new DriveWorks.Helper.Object.Specification();
                            newSpecification.CreatorID = reader["CreatorId"].ToString();
                            newSpecification.Id = (int)reader["Id"];
                            newSpecification.Name = reader["Name"].ToString();
                            newSpecification.StateName = reader["StateName"].ToString();
                            result.Add(newSpecification);
                        }
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            return result;
        }

        public List<string> GetStateNameListOfProject(string iProjectId)
        {
            var result = new List<string>();

            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                var queryString = "SELECT DISTINCT [StateName] FROM [dbo].[Specifications] WHERE[ProjectId]= @ProjectId";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@ProjectId", iProjectId);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var newSpecification = new DriveWorks.Helper.Object.Specification();
                            result.Add(reader["StateName"].ToString());
                        }
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return result.Enum().OrderBy(x => x).Enum().ToList();
        }

        #endregion

        #region Private FIELDS

        private string _ConnectionString;

        #endregion
    }
}