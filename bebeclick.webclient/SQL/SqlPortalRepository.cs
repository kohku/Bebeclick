﻿using System;
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

        #region State
        public IEnumerable<StateEntity> GetStateEntities(Guid? id, string name)
        {
            var provinces = new List<StateEntity>();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[this.ConnectionStringName].ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand("usp_GetStateEntities", connection)
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
                        SqlValue = name.Contains("%") ? name : "%" + name + "%"
                    });
                }

                IDataReader reader = command.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        var item = new StateEntity
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

        public void UpdateStateEntity(StateEntity state)
        {
            if (state == null)
                throw new ArgumentNullException("state");

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[this.ConnectionStringName].ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand("usp_UpdateStateEntity", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                FillCommand(command, state);

                command.ExecuteNonQuery();
            }
        }

        private void FillCommand(SqlCommand command, StateEntity state)
        {
            command.Parameters.Add(new SqlParameter
            {
                Direction = ParameterDirection.Input,
                IsNullable = false,
                ParameterName = "@ID",
                SqlDbType = SqlDbType.UniqueIdentifier,
                SqlValue = state.ID
            });
            command.Parameters.Add(new SqlParameter
            {
                Direction = ParameterDirection.Input,
                IsNullable = false,
                ParameterName = "@Name",
                SqlDbType = SqlDbType.NVarChar,
                Size = 200,
                SqlValue = state.Name
            });
            command.Parameters.Add(new SqlParameter
            {
                Direction = ParameterDirection.Input,
                IsNullable = true,
                ParameterName = "@DateCreated",
                SqlDbType = SqlDbType.DateTime,
                SqlValue = state.DateCreated
            });
            command.Parameters.Add(new SqlParameter
            {
                Direction = ParameterDirection.Input,
                IsNullable = true,
                ParameterName = "@CreatedBy",
                SqlDbType = SqlDbType.NVarChar,
                Size = 512,
                SqlValue = state.CreatedBy
            });
            command.Parameters.Add(new SqlParameter
            {
                Direction = ParameterDirection.Input,
                IsNullable = true,
                ParameterName = "@LastUpdated",
                SqlDbType = SqlDbType.DateTime,
                SqlValue = state.LastUpdated
            });
            command.Parameters.Add(new SqlParameter
            {
                Direction = ParameterDirection.Input,
                IsNullable = true,
                ParameterName = "@LastUpdatedBy",
                SqlDbType = SqlDbType.NVarChar,
                Size = 512,
                SqlValue = state.LastUpdatedBy
            });
            command.Parameters.Add(new SqlParameter
            {
                Direction = ParameterDirection.Input,
                IsNullable = false,
                ParameterName = "@Visible",
                SqlDbType = SqlDbType.Bit,
                SqlValue = state.Visible
            });
        }

        public void InsertStateEntity(StateEntity state)
        {
            if (state == null)
                throw new ArgumentNullException("state");

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[this.ConnectionStringName].ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand("usp_InsertStateEntity", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                FillCommand(command, state);

                command.ExecuteNonQuery();
            }
        }

        public void DeleteStateEntity(StateEntity state)
        {
            if (state == null)
                throw new ArgumentNullException("state");

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[this.ConnectionStringName].ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand("usp_DeleteStateEntity", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.Add(new SqlParameter
                {
                    Direction = ParameterDirection.Input,
                    IsNullable = false,
                    ParameterName = "@ID",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    SqlValue = state.ID
                });

                command.ExecuteNonQuery();
            }
        }

        #endregion

        #region Province
        public IEnumerable<Province> GetProvinces(Guid? id, Guid? stateId, string name)
        {
            var provinces = new List<Province>();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[this.ConnectionStringName].ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand("usp_GetProvinces", connection)
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
                        SqlValue = name.Contains("%") ? name : "%" + name + "%"
                    });
                }

                IDataReader reader = command.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        var item = new Province
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

        public void UpdateProvince(Province province)
        {
            if (province == null)
                throw new ArgumentNullException("province");

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[this.ConnectionStringName].ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand("usp_UpdateProvince", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                FillCommand(command, province);

                command.ExecuteNonQuery();
            }
        }

        private void FillCommand(SqlCommand command, Province province)
        {
            command.Parameters.Add(new SqlParameter
            {
                Direction = ParameterDirection.Input,
                IsNullable = false,
                ParameterName = "@ID",
                SqlDbType = SqlDbType.UniqueIdentifier,
                SqlValue = province.ID
            });
            command.Parameters.Add(new SqlParameter
            {
                Direction = ParameterDirection.Input,
                IsNullable = false,
                ParameterName = "@StateID",
                SqlDbType = SqlDbType.UniqueIdentifier,
                SqlValue = province.StateID
            });
            command.Parameters.Add(new SqlParameter
            {
                Direction = ParameterDirection.Input,
                IsNullable = false,
                ParameterName = "@Name",
                SqlDbType = SqlDbType.NVarChar,
                Size = 200,
                SqlValue = province.Name
            });
            command.Parameters.Add(new SqlParameter
            {
                Direction = ParameterDirection.Input,
                IsNullable = true,
                ParameterName = "@DateCreated",
                SqlDbType = SqlDbType.DateTime,
                SqlValue = province.DateCreated
            });
            command.Parameters.Add(new SqlParameter
            {
                Direction = ParameterDirection.Input,
                IsNullable = true,
                ParameterName = "@CreatedBy",
                SqlDbType = SqlDbType.NVarChar,
                Size = 512,
                SqlValue = province.CreatedBy
            });
            command.Parameters.Add(new SqlParameter
            {
                Direction = ParameterDirection.Input,
                IsNullable = true,
                ParameterName = "@LastUpdated",
                SqlDbType = SqlDbType.DateTime,
                SqlValue = province.LastUpdated
            });
            command.Parameters.Add(new SqlParameter
            {
                Direction = ParameterDirection.Input,
                IsNullable = true,
                ParameterName = "@LastUpdatedBy",
                SqlDbType = SqlDbType.NVarChar,
                Size = 512,
                SqlValue = province.LastUpdatedBy
            });
            command.Parameters.Add(new SqlParameter
            {
                Direction = ParameterDirection.Input,
                IsNullable = false,
                ParameterName = "@Visible",
                SqlDbType = SqlDbType.Bit,
                SqlValue = province.Visible
            });
        }

        public void InsertProvince(Province province)
        {
            if (province == null)
                throw new ArgumentNullException("province");

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[this.ConnectionStringName].ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand("usp_InsertProvince", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                FillCommand(command, province);

                command.ExecuteNonQuery();
            }
        }

        public void DeleteProvince(Province province)
        {
            if (province == null)
                throw new ArgumentNullException("province");

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[this.ConnectionStringName].ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand("usp_DeleteProvince", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.Add(new SqlParameter
                {
                    Direction = ParameterDirection.Input,
                    IsNullable = true,
                    ParameterName = "@ID",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    SqlValue = province.ID
                });

                command.ExecuteNonQuery();
            }
        }

        #endregion

        #region Product
        public IEnumerable<Product> GetProducts(Guid? id, Guid? stateId, Guid? provinceId, string name)
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

                if (provinceId.HasValue)
                {
                    command.Parameters.Add(new SqlParameter
                    {
                        Direction = ParameterDirection.Input,
                        IsNullable = true,
                        ParameterName = "@ProvinceID",
                        SqlDbType = SqlDbType.UniqueIdentifier,
                        SqlValue = provinceId.Value
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
                        SqlValue = name.Contains("%") ? name : "%" + name + "%"
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
            if (product == null)
                throw new ArgumentNullException("product");

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[this.ConnectionStringName].ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand("usp_DeleteProduct", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.Add(new SqlParameter
                {
                    Direction = ParameterDirection.Input,
                    IsNullable = true,
                    ParameterName = "@ID",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    SqlValue = product.ID
                });

                command.ExecuteNonQuery();
            }
        }

        public void InsertProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[this.ConnectionStringName].ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand("usp_InsertProduct", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                FillCommand(command, product);

                command.ExecuteNonQuery();
            }
        }

        private void FillCommand(SqlCommand command, Product product)
        {
            command.Parameters.Add(new SqlParameter
            {
                Direction = ParameterDirection.Input,
                IsNullable = false,
                ParameterName = "@ID",
                SqlDbType = SqlDbType.UniqueIdentifier,
                SqlValue = product.ID
            });
            command.Parameters.Add(new SqlParameter
            {
                Direction = ParameterDirection.Input,
                IsNullable = false,
                ParameterName = "@Name",
                SqlDbType = SqlDbType.NVarChar,
                Size = 200,
                SqlValue = product.Name
            });
            command.Parameters.Add(new SqlParameter
            {
                Direction = ParameterDirection.Input,
                IsNullable = true,
                ParameterName = "@DateCreated",
                SqlDbType = SqlDbType.DateTime,
                SqlValue = product.DateCreated
            });
            command.Parameters.Add(new SqlParameter
            {
                Direction = ParameterDirection.Input,
                IsNullable = true,
                ParameterName = "@CreatedBy",
                SqlDbType = SqlDbType.NVarChar,
                Size = 512,
                SqlValue = product.CreatedBy
            });
            command.Parameters.Add(new SqlParameter
            {
                Direction = ParameterDirection.Input,
                IsNullable = true,
                ParameterName = "@LastUpdated",
                SqlDbType = SqlDbType.DateTime,
                SqlValue = product.LastUpdated
            });
            command.Parameters.Add(new SqlParameter
            {
                Direction = ParameterDirection.Input,
                IsNullable = true,
                ParameterName = "@LastUpdatedBy",
                SqlDbType = SqlDbType.NVarChar,
                Size = 512,
                SqlValue = product.LastUpdatedBy
            });
            command.Parameters.Add(new SqlParameter
            {
                Direction = ParameterDirection.Input,
                IsNullable = false,
                ParameterName = "@Visible",
                SqlDbType = SqlDbType.Bit,
                SqlValue = product.Visible
            });
        }

        public void UpdateProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[this.ConnectionStringName].ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand("usp_UpdateProduct", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                FillCommand(command, product);

                command.ExecuteNonQuery();
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
                        SqlValue = name.Contains("%") ? name : "%" + name + "%"
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
            if (service == null)
                throw new ArgumentNullException("service");

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[this.ConnectionStringName].ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand("usp_DeleteService", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.Add(new SqlParameter
                {
                    Direction = ParameterDirection.Input,
                    IsNullable = true,
                    ParameterName = "@ID",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    SqlValue = service.ID
                });

                command.ExecuteNonQuery();
            }
        }

        public void InsertService(Service service)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[this.ConnectionStringName].ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand("usp_InsertService", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                FillCommand(command, service);

                command.ExecuteNonQuery();
            }
        }

        private void FillCommand(SqlCommand command, Service service)
        {
            command.Parameters.Add(new SqlParameter
            {
                Direction = ParameterDirection.Input,
                IsNullable = false,
                ParameterName = "@ID",
                SqlDbType = SqlDbType.UniqueIdentifier,
                SqlValue = service.ID
            });
            command.Parameters.Add(new SqlParameter
            {
                Direction = ParameterDirection.Input,
                IsNullable = false,
                ParameterName = "@ProductID",
                SqlDbType = SqlDbType.UniqueIdentifier,
                SqlValue = service.ProductID
            });
            command.Parameters.Add(new SqlParameter
            {
                Direction = ParameterDirection.Input,
                IsNullable = false,
                ParameterName = "@Name",
                SqlDbType = SqlDbType.NVarChar,
                Size = 200,
                SqlValue = service.Name
            });
            command.Parameters.Add(new SqlParameter
            {
                Direction = ParameterDirection.Input,
                IsNullable = true,
                ParameterName = "@DateCreated",
                SqlDbType = SqlDbType.DateTime,
                SqlValue = service.DateCreated
            });
            command.Parameters.Add(new SqlParameter
            {
                Direction = ParameterDirection.Input,
                IsNullable = true,
                ParameterName = "@CreatedBy",
                SqlDbType = SqlDbType.NVarChar,
                Size = 512,
                SqlValue = service.CreatedBy
            });
            command.Parameters.Add(new SqlParameter
            {
                Direction = ParameterDirection.Input,
                IsNullable = true,
                ParameterName = "@LastUpdated",
                SqlDbType = SqlDbType.DateTime,
                SqlValue = service.LastUpdated
            });
            command.Parameters.Add(new SqlParameter
            {
                Direction = ParameterDirection.Input,
                IsNullable = true,
                ParameterName = "@LastUpdatedBy",
                SqlDbType = SqlDbType.NVarChar,
                Size = 512,
                SqlValue = service.LastUpdatedBy
            });
            command.Parameters.Add(new SqlParameter
            {
                Direction = ParameterDirection.Input,
                IsNullable = false,
                ParameterName = "@Visible",
                SqlDbType = SqlDbType.Bit,
                SqlValue = service.Visible
            });
        }

        public void UpdateService(Service service)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[this.ConnectionStringName].ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand("usp_UpdateService", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                FillCommand(command, service);

                command.ExecuteNonQuery();
            }
        }

        #endregion
    }
}
