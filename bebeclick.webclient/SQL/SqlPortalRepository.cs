using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebeclick.WebClient.SQL
{
    public class SqlPortalRepository : IPortalRepository
    {
        private string _connectionStringName;

        public SqlPortalRepository(string connectionStringName)
        {
            this._connectionStringName = connectionStringName;
        }

        public string ConnectionStringName
        {
            get { return this._connectionStringName; }
        }

        public IEnumerable<StateProvince> GetAllStateProvinces()
        {
            var provinces = new List<StateProvince>();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[this.ConnectionStringName].ConnectionString))
            {
                connection.Open();

                try
                {
                    var command = new SqlCommand("usp_GetStateProvince", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };

                    IDataReader reader = command.ExecuteReader();

                    try
                    {
                        while (reader.Read())
                        {
                            var province = new StateProvince
                            {
                                ID = new Guid(reader["ID"].ToString()),
                                Name = reader["Name"].ToString(),
                                DateCreated = Convert.ToDateTime(reader["DateCreated"]),
                                CreatedBy = reader["CreatedBy"].ToString(),
                                LastUpdated = reader["LastUpdated"] != DBNull.Value ? Convert.ToDateTime(reader["LastUpdated"]) : default(DateTime?),
                                LastUpdatedBy = reader["LastUpdatedBy"]!= DBNull.Value ? reader["LastUpdatedBy"].ToString() : null,
                                Visible = Convert.ToBoolean(reader["Visible"])
                            };

                            provinces.Add(province);
                        }
                    }
                    finally 
                    {
                        if (!reader.IsClosed)
                            reader.Close();
                    }

                }
                finally
                {
                    connection.Close();
                }
            }

            return provinces;
        }
    }
}
