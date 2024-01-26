using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Linq;

using System.Linq.Expressions;

namespace PartyLib.Helpers
{
    public static class DatabaseHelper
    {
        /// <summary>
        /// Verifies the primary application database exists.
        /// </summary>
        public static SQLiteConnection VerifyDatabaseIntegrity()
        {
            if (!File.Exists("./partylib.sqlite"))
            {
                CreateDatabase("partylib");
            }

            return CreateConnection("partylib");
        }

        /// <summary>
        /// Creates a database file.
        /// </summary>
        /// <param name="databaseName">Name for the new database</param>
        /// <returns>The path of the new database</returns>
        public static string CreateDatabase(string databaseName)
        {
            SQLiteConnection.CreateFile($"{databaseName}.sqlite");
            return databaseName + ".sqlite";
        }

        /// <summary>
        /// Opens a connection to an SQLite database. Does not create one and will fail if the
        /// database does not exist.
        /// </summary>
        /// <param name="databaseName">Name of the database file to read</param>
        /// <param name="Version">Database version (default: 3)</param>
        /// <param name="New">Database new flag (default: true)</param>
        /// <param name="Compress">Database compression flag (default: true)</param>
        /// <returns></returns>
        public static SQLiteConnection? CreateConnection(string databaseName, int Version = 3, bool New = true, bool Compress = true)
        {
            SQLiteConnection sqlite_conn;
            sqlite_conn = new SQLiteConnection($"Data Source={databaseName}.sqlite; Version = {Version.ToString()}; New = {New}; Compress = {Compress}; ");
            try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {
                return null;
            }
            return sqlite_conn;
        }

        /// <summary>
        /// Creates a table in the database with optional columns.
        /// </summary>
        /// <param name="conn">SQLite database connection</param>
        /// <param name="tableName">Name of new table</param>
        /// <param name="columns">A list of column names to create inside the table</param>
        /// <returns>A boolean representing the success of the operation</returns>
        public static bool CreateTable(SQLiteConnection conn, string tableName, List<string>? columns = null)
        {
            SQLiteCommand sqlite_cmd;
            string columnsStr = String.Empty;
            if (columns != null)
            {
                columnsStr = $"({String.Join(", ", columns)})";
            }
            string Createsql = $"CREATE TABLE {tableName} {columnsStr}";
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = Createsql;
            try
            {
                sqlite_cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Insert data into a column(s) residing in a table.
        /// </summary>
        /// <param name="conn">SQLite database connection</param>
        /// <param name="tableName">Name of table to modify data from</param>
        /// <param name="columns">Name of columns inside table</param>
        /// <param name="values">Values to insert into according columns</param>
        /// <returns>A boolean representing the success of the operation</returns>
        public static bool InsertData(SQLiteConnection conn, string tableName, List<string> columns, List<object> values)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = $"INSERT INTO {tableName} ({String.Join(", ", columns)}) VALUES({String.Join(", ", values)}); ";
            try
            {
                sqlite_cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Reads values from a series of columns, or fetches all values inside all columns if none
        /// are provided.
        /// </summary>
        /// <param name="conn">SQLite database connection</param>
        /// <param name="tableName">Name of table to read data from</param>
        /// <param name="columns">A list of columns to read data from (optional)</param>
        /// <returns>A list of strings corresponding to values read</returns>
        public static List<string> ReadData(SQLiteConnection conn, string tableName, List<string> columns = null)
        {
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();

            if (columns != null)
            {
                // Select data from columns
                sqlite_cmd.CommandText = $"SELECT {String.Join(", ", columns)} FROM {tableName}";
            }
            else
            {
                // Select all data
                sqlite_cmd.CommandText = $"SELECT * FROM {tableName}";
            }

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            List<string> data = new List<string>();
            while (sqlite_datareader.Read())
            {
                string myreader = sqlite_datareader.GetString(0);
                data.Add(myreader);
            }
            return data;
        }

        /// <summary>
        /// Closes an SQLite database connection.
        /// </summary>
        /// <param name="conn">SQLite database connection</param>
        public static void CloseConnection(SQLiteConnection conn)
        {
            conn.Close();
        }
    }
}