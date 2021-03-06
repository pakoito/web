﻿//  Copyright 2012 
//  Name: Ryan Williams
//  URL: http://ryanmichaelwilliams.com | http://dasklub.com

//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at

//       http://www.apache.org/licenses/LICENSE-2.0

//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
using System;
using System.Data;
using System.Data.Common;
using BootBaronLib.Configs;
using BootBaronLib.Operational;

namespace BootBaronLib.DAL
{
    /// <summary>
    /// Generic data access functionality to be accessed from the business tier (CreateCommand,
    /// ExecuteSelectCommand, ExecuteNonQuery, ExecuteScalar, ExecuteMultipleTableSelectCommand)
    /// </summary>
    public class DbAct
    {
        #region constructor

        /// <summary>
        /// static constructor 
        /// </summary>
        static DbAct() { }

        #endregion

        #region Public Static methods

        #region core DB actions

        /// <summary>
        /// Returns a DataSet (best used for multiple tables in the result set)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <see cref=">http://msdn.microsoft.com/en-us/library/fks3666w%28VS.80%29.aspx"/>
        public static DataSet ExecuteMultipleTableSelectCommand(DbCommand command)
        {
            using (command)
            {
                DbProviderFactory factory = DbProviderFactories.GetFactory( DataBaseConfigs.DbProviderName);

                using (DbDataAdapter adapter = factory.CreateDataAdapter())
                {
                    adapter.SelectCommand = command;

                    using (DataSet ds = new DataSet())
                    {
                        try
                        {
                            adapter.Fill(ds);
                        }
                        catch (Exception ex)
                        {
                            if (command != null && command.CommandText != null)
                                Utilities.LogError("COMMAND: " + command.CommandText, ex);
                            else 
                                Utilities.LogError(ex);

                            return null;
                        }

                        command.Connection.Close();
                        return ds;
                    }
                }
            }

            
        }


        /// <summary>
        /// Execute a command and return the results as a DataTable object
        /// </summary>
        /// <param name="command">database command</param>
        public static DataTable ExecuteSelectCommand(DbCommand command)
        {
            // The DataTable to be returned 
            DataTable table = null;
            // Execute the command making sure the connection gets closed in the end
            using (command)
            {
                try
                {
                    // Open the data connection 
                    command.Connection.Open();

                    table = new DataTable();
                    // Execute the command and save the results in a DataTable
                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        table.Load(reader);
                        // Close the reader 
                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    // Log eventual errors and rethrow them
                    if (command != null && command.CommandText != null)
                        Utilities.LogError("COMMAND: " + command.CommandText, ex);
                    else
                        Utilities.LogError(ex);
                }
                finally
                {
                    command.Connection.Close();
                    // Close the connection
                    command.Parameters.Clear();
                }
                return table;
            }
        }

        /// <summary>
        /// Execute an update, delete, or insert command 
        /// and return the number of affected rows
        /// </summary>
        /// <param name="command">database command</param>
        public static int ExecuteNonQuery(DbCommand command)
        {
            // The number of affected rows 
            int affectedRows = -1;

            // Execute the command making sure the connection gets closed in the end
            using (command)
            {
                try
                {
                    // Open the connection of the command
                    command.Connection.Open();
                    // Execute the command and get the number of affected rows
                    affectedRows = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    if (command != null && command.CommandText != null)
                        Utilities.LogError("COMMAND: " + command.CommandText, ex);
                    else
                        Utilities.LogError(ex);
                }
                finally
                {
                    command.Parameters.Clear();
                    // Close the connection
                    command.Connection.Close();
                }
                // return the number of affected rows
                return affectedRows;
            }
        }

        /// <summary>
        /// Execute a select command and return a single result as a string
        /// </summary>
        /// <param name="command">database command</param>
        public static string ExecuteScalar(DbCommand command)
        {
            // The value to be returned 
            string value = string.Empty;
            // Execute the command making sure the connection gets closed in the end
            using (command)
            {
                try
                {
                   
                    // Open the connection of the command
                    command.Connection.Open();
                    // Execute the command and get the number of affected rows
                    object result = command.ExecuteScalar();
                    if (result != null) { value = Convert.ToString(result); }
                    else { value = string.Empty; }
                }
                catch (Exception ex)
                {
                    if (command != null && command.CommandText != null)
                        Utilities.LogError("COMMAND: " + command.CommandText, ex);
                    else
                        Utilities.LogError(ex);
                }
                finally
                {
                    command.Parameters.Clear();
                    // Close the connection
                    command.Connection.Close();
                }
                // return the result
                return value;
            }
        }

        #endregion

        #region DBCommands

        /// <summary>
        /// Creates and prepares a new DbCommand object 
        /// on a new connection using a StoredProcedure
        /// </summary>
        public static DbCommand CreateCommand()
        {
            // Obtain the database provider name
            string dataProviderName = DataBaseConfigs.DbProviderName;
            // Obtain the database connection string
            string connectionString = DataBaseConfigs.DbConnectionString;
            // Create a new data provider factory
            DbProviderFactory factory = DbProviderFactories.GetFactory(dataProviderName);
            // Obtain a database specific connection object
            DbConnection conn = factory.CreateConnection();
            // Set the connection string
            conn.ConnectionString = connectionString;
            // Create a database specific command object
            DbCommand comm = conn.CreateCommand();
            // Set the command type to stored procedure
            comm.CommandType = CommandType.StoredProcedure;
            // Return the initialized command object
            return comm;
        }

        /// <summary>
        /// Creates and prepares a new DbCommand object 
        /// on a new connection using choice
        /// </summary>
        /// <param name="text">true or false to use command type of text</param>
        /// <returns></returns>
        /// <remarks>allows the use of text or stored procedure for command type</remarks>
        public static DbCommand CreateCommand(bool isText)
        {
            // Obtain the database provider name
            string dataProviderName = DataBaseConfigs.DbProviderName;
            // Obtain the database connection string
            string connectionString = DataBaseConfigs.DbConnectionString;
            // Create a new data provider factory
            DbProviderFactory factory = DbProviderFactories.GetFactory(dataProviderName);
            // Obtain a database specific connection object
            DbConnection conn = factory.CreateConnection();
            // Set the connection string
            conn.ConnectionString = connectionString;
            // Create a database specific command object
            DbCommand comm = conn.CreateCommand();
            // Set the command type based on the paramter
            if (isText == true) { comm.CommandType = CommandType.Text; }
            else { comm.CommandType = CommandType.StoredProcedure; }
            // Return the initialized command object
            return comm;
        }

        /// <summary>
        /// Creates and prepares a new DbCommand object on a new 
        /// (other) connection using a StoredProcedure
        /// </summary>
        public static DbCommand CreateCommand2()
        {
            // Obtain the database provider name
            string dataProviderName2 = DataBaseConfigs.DbProviderName;
            // Obtain the database connection string
            string connectionString2 = DataBaseConfigs.DbConnectionString;
            // Create a new data provider factory
            DbProviderFactory factory = DbProviderFactories.GetFactory(dataProviderName2);
            // Obtain a database specific connection object
            DbConnection conn = factory.CreateConnection();
            // Set the connection string
            conn.ConnectionString = connectionString2;
            // Create a database specific command object
            DbCommand comm = conn.CreateCommand();
            // Set the command type to stored procedure
            comm.CommandType = CommandType.StoredProcedure;
            // Return the initialized command object
            return comm;
        }

        #endregion

        #endregion

    }
}