using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using Npgsql;
using Npgsql.Logging;
using System.Data;

namespace DB_Lab6
{
    public class PostgresDatabase
    {
        private string _connection_string;
        private NpgsqlConnection _connection = default(NpgsqlConnection);

        private NpgsqlDataAdapter _adapter;
        private NpgsqlCommandBuilder _cmd_builder;

        private PostgresDatabase()
        {
            _adapter = new NpgsqlDataAdapter();
            _cmd_builder = new NpgsqlCommandBuilder();
        }

        /// <summary>
        /// Makes a connection and tries to connect.
        /// </summary>
        /// <param name="db_ip">Hostname</param>
        /// <param name="db_port">Port</param>
        /// <param name="db_name">Database name</param>
        /// <param name="db_username">Username to log in</param>
        /// <param name="db_password">Password according to username</param>
        public PostgresDatabase(string db_ip = "localhost", string db_port = "5432", string db_name = "lab_db", string db_username = "postgres", string db_password = "62548466") : this()
        {
            _connection_string = string.Format("Host={0};Port={1};Database={2};Username={3};Password={4}", db_ip, db_port, db_name, db_username, db_password);
            _connection = new NpgsqlConnection(_connection_string);
            try
            {
                _connection.Open();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);

                if (_connection != null)
                {
                    _connection.Close();
                }
            }
        }

        ~PostgresDatabase()
        {
            if (_connection != default(NpgsqlConnection))
            {
                _connection.Close();
            }
        }

        public void UpdateData(DataTable table)
        {
            try
            {
                _cmd_builder.DataAdapter = _adapter;
                _adapter.Update(table);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
            }
        }

        public string QueryString(string sql)
        {
            string request_result = default(string);
            // Making a request
            NpgsqlDataReader data_reader = default(NpgsqlDataReader);
            try
            {
                _connection.Open();
                data_reader = (new NpgsqlCommand(sql, _connection)).ExecuteReader();

                while (data_reader.Read())
                {
                    request_result = data_reader[0].ToString();
                }

                data_reader.Close();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

            return request_result;
        }

        public DataTable QueryDataTable(string sql)
        {
            DataSet _set = new DataSet();
            DataTable _table = new DataTable();
            try
            {
                _connection.Open();
                _adapter = new NpgsqlDataAdapter(sql, _connection);

                _set.Reset();
                _adapter.Fill(_set);
                // check one table to exist
                if (_set.Tables.Count > 0)
                {
                    _table.Clear();
                    _table = _set.Tables[0];
                }
                _connection.Close();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
            }

            return _table;
        }

        public List<List<string>> QueryList(string sql)
        {
            List<List<string>> request_result = new List<List<string>>();
            // Making a request
            NpgsqlDataReader data_reader = default(NpgsqlDataReader);
            try
            {
                _connection.Open();

                data_reader = (new NpgsqlCommand(sql, _connection)).ExecuteReader();

                bool is_column_headers_added = false;
                while (data_reader.Read())
                {
                    if (!data_reader.HasRows)
                    {
                        continue;
                    }

                    // If there is something in answer.
                    List<string> cortege = new List<string>();
                    if (!is_column_headers_added)
                    {
                        for (int column_index = 0; column_index < data_reader.FieldCount; column_index++)
                        {
                            cortege.Add(data_reader.GetName(column_index));
                        }
                        request_result.Add(cortege);
                        cortege = new List<string>();
                        is_column_headers_added = true;
                    }

                    for (int field_index = 0; field_index < data_reader.FieldCount; field_index++)
                    {
                        cortege.Add(data_reader[field_index].ToString());
                    }

                    // Adding next cortege to list
                    request_result.Add(cortege);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                data_reader.Close();
                _connection.Close();
            }

            return request_result;
        }

        public void Exec(string sql)
        {
            try
            {
                _connection.Open();

                (new NpgsqlCommand(sql, _connection)).ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _connection.Close();
            }
        }

        public string SecureString(string str)
        {
            str = str.Replace('\'', '^');
            str = str.Replace('"', '^');
            str = str.TrimStart(' ', '\n', '\r', '\t');
            str = str.TrimEnd(' ', '\n', '\r', '\t');

            return str;
        }
    }
}
