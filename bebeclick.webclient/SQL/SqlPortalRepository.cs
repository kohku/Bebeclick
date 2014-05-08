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


        #region State Province
        public IEnumerable<StateProvince> GetStateProvinces(Guid? id, string name)
        {
            var provinces = new List<StateProvince>();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[this.ConnectionStringName].ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand("usp_GetStateProvinces", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                if (id.HasValue)
                {
                    command.Parameters.Add(new SqlParameter
                    {
                        Direction = ParameterDirection.Input,
                        IsNullable = true,
                        ParameterName = "@ID",
                        SqlDbType = SqlDbType.UniqueIdentifier,
                        SqlValue = id.Value
                    });
                }

                if (!string.IsNullOrEmpty(name))
                {
                    command.Parameters.Add(new SqlParameter
                    {
                        Direction = ParameterDirection.Input,
                        IsNullable = true,
                        ParameterName = "@Name",
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 200,
                        SqlValue = !name.Contains("%") ? name : "%" + name + "%"
                    });
                }

                IDataReader reader = command.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        var item = new StateProvince
                        {
                            ID = new Guid(reader["ID"].ToString()),
                            Name = reader["Name"].ToString(),
                            DateCreated = Convert.ToDateTime(reader["DateCreated"]),
                            CreatedBy = reader["CreatedBy"].ToString(),
                            LastUpdated = reader["LastUpdated"] != DBNull.Value ? Convert.ToDateTime(reader["LastUpdated"]) : default(DateTime?),
                            LastUpdatedBy = reader["LastUpdatedBy"] != DBNull.Value ? reader["LastUpdatedBy"].ToString() : null,
                            Visible = Convert.ToBoolean(reader["Visible"])
                        };

                        provinces.Add(item);
                    }
                }
                finally
                {
                    if (!reader.IsClosed)
                        reader.Close();
                }
            }

            return provinces;
        }

        public void UpdateStateProvince(StateProvince stateProvince)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[this.ConnectionStringName].ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand("usp_UpdateStateProvince", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
            }
        }

        public void InsertStateProvince(StateProvince stateProvince)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[this.ConnectionStringName].ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand("usp_InsertStateProvince", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
            }
        }

        public void DeleteStateProvince(StateProvince stateProvince)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[this.ConnectionStringName].ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand("usp_DeleteStateProvince", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
            }
        }

        #endregion

        #region Product
        public IEnumerable<Product> GetProducts(Guid? id, Guid? stateId, string name)
        {
            var products = new List<Product>();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[this.ConnectionStringName].ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand("usp_GetProducts", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                if (id.HasValue)
                {
                    command.Parameters.Add(new SqlParameter
                    {
                        Direction = ParameterDirection.Input,
                        IsNullable = true,
                        ParameterName = "@ID",
                        SqlDbType = SqlDbType.UniqueIdentifier,
                        SqlValue = id.Value
                    });
                }

                if (stateId.HasValue)
                {
                    command.Parameters.Add(new SqlParameter
                    {
                        Direction = ParameterDirection.Input,
                        IsNullable = true,
                        ParameterName = "@StateID",
                        SqlDbType = SqlDbType.UniqueIdentifier,
                        SqlValue = stateId.Value
                    });
                }

                if (!string.IsNullOrEmpty(name))
                {
                    command.Parameters.Add(new SqlParameter
                    {
                        Direction = ParameterDirection.Input,
                        IsNullable = true,
                        ParameterName = "@Name",
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 200,
                        SqlValue = !name.Contains("%") ? name : "%" + name + "%"
                    });
                }

                IDataReader reader = command.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        var item = new Product
                        {
                            ID = new Guid(reader["ID"].ToString()),
                            StateID = new Guid(reader["StateID"].ToString()),
                            Name = reader["Name"].ToString(),
                            DateCreated = Convert.ToDateTime(reader["DateCreated"]),
                            CreatedBy = reader["CreatedBy"].ToString(),
                            LastUpdated = reader["LastUpdated"] != DBNull.Value ? Convert.ToDateTime(reader["LastUpdated"]) : default(DateTime?),
                            LastUpdatedBy = reader["LastUpdatedBy"] != DBNull.Value ? reader["LastUpdatedBy"].ToString() : null,
                            Visible = Convert.ToBoolean(reader["Visible"])
                        };

                        products.Add(item);
                    }
                }
                finally
                {
                    if (!reader.IsClosed)
                        reader.Close();
                }
            }

            return products;
        }

        public void DeleteProduct(Product product)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[this.ConnectionStringName].ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand("usp_DeleteProduct", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
            }
        }

        public void InsertProduct(Product product)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[this.ConnectionStringName].ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand("usp_InsertProduct", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
            }
        }

        public void UpdateProduct(Product product)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[this.ConnectionStringName].ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand("usp_UpdateProduct", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
            }
        }

        #endregion

        #region Service

        public IEnumerable<Service> GetServices(Guid? id, Guid? productId, string name)
        {
            var services = new List<Service>();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[this.ConnectionStringName].ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand("usp_GetServices", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                if (id.HasValue)
                {
                    command.Parameters.Add(new SqlParameter
                    {
                        Direction = ParameterDirection.Input,
                        IsNullable = true,
                        ParameterName = "@ID",
                        SqlDbType = SqlDbType.UniqueIdentifier,
                        SqlValue = id.Value
                    });
                }

                if (productId.HasValue)
                {
                    command.Parameters.Add(new SqlParameter
                    {
                        Direction = ParameterDirection.Input,
                        IsNullable = true,
                        ParameterName = "@ProductID",
                        SqlDbType = SqlDbType.UniqueIdentifier,
                        SqlValue = productId.Value
                    });
                }

                if (!string.IsNullOrEmpty(name))
                {
                    command.Parameters.Add(new SqlParameter
                    {
                        Direction = ParameterDirection.Input,
                        IsNullable = true,
                        ParameterName = "@Name",
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 200,
                        SqlValue = !name.Contains("%") ? name : "%" + name + "%"
                    });
                }

                IDataReader reader = command.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        var item = new Service
                        {
                            ID = new Guid(reader["ID"].ToString()),
                            ProductID = new Guid(reader["ProductID"].ToString()),
                            Name = reader["Name"].ToString(),
                            DateCreated = Convert.ToDateTime(reader["DateCreated"]),
                            CreatedBy = reader["CreatedBy"].ToString(),
                            LastUpdated = reader["LastUpdated"] != DBNull.Value ? Convert.ToDateTime(reader["LastUpdated"]) : default(DateTime?),
                            LastUpdatedBy = reader["LastUpdatedBy"] != DBNull.Value ? reader["LastUpdatedBy"].ToString() : null,
                            Visible = Convert.ToBoolean(reader["Visible"])
                        };

                        services.Add(item);
                    }
                }
                finally
                {
                    if (!reader.IsClosed)
                        reader.Close();
                }
            }

            return services;
        }

        public void DeleteService(Service service)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[this.ConnectionStringName].ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand("usp_DeleteService", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
            }
        }

        public void InsertService(Service service)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[this.ConnectionStringName].ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand("usp_InsertService", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
            }
        }

        public void UpdateService(Service service)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[this.ConnectionStringName].ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand("usp_UpdateService", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
            }
        }

        #endregion
    }
}
