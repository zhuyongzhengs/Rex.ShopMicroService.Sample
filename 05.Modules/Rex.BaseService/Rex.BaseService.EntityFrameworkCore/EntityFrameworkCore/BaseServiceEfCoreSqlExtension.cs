using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Rex.BaseService.EntityFrameworkCore
{
    /// <summary>
    /// EF Core执行SQL语句扩展
    /// </summary>
    public static class BaseServiceEfCoreSqlExtension
    {
        /// <summary>
        /// 执行查询操作，返回DataTable
        /// </summary>
        /// <param name="dbContext">数据库上下文</param>
        /// <param name="commandText">命令语句</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="parameters">格式化参数集合</param>
        /// <returns>DataTable</returns>
        public static DataTable ExecuteQuery(this BaseServiceDbContext dbContext, string commandText, CommandType commandType, params DbParameter[] parameters)
        {
            DbConnection connection = dbContext.Database.GetDbConnection();
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            // 设置Command
            using DbCommand command = connection.CreateCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;
            if (parameters != null && parameters.Length > 0)
            {
                command.Parameters.AddRange(parameters);
            }

            // 查询数据
            try
            {
                //using SqlDataAdapter adapter = new SqlDataAdapter(command as SqlCommand);
                //DataTable dataTable = new DataTable();
                //adapter.Fill(dataTable);
                DbDataReader dbDataReader = command.ExecuteReader();
                DataTable dataTable = ConvertDataReaderToDataTable(dbDataReader);
                return dataTable;
            }
            catch
            {
                return null;
            }
            finally
            {
                command.Parameters.Clear();
                command.Dispose();
                connection.Close();
            }
        }

        /// <summary>
        /// 执行查询操作，返回DbDataReader
        /// </summary>
        /// <param name="dbContext">数据库上下文</param>
        /// <param name="commandText">命令语句</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="parameters">格式化参数集合</param>
        /// <returns>DbDataReader</returns>
        public static DbDataReader ExecuteReader(this DbContext dbContext, string commandText, CommandType commandType, params DbParameter[] parameters)
        {
            DbConnection connection = dbContext.Database.GetDbConnection();
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            // 设置Command
            using DbCommand command = connection.CreateCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;
            if (parameters != null && parameters.Length > 0)
            {
                command.Parameters.AddRange(parameters);
            }

            // 返回DataReader
            try
            {
                return command.ExecuteReader();
            }
            catch
            {
                return null;
            }
            finally
            {
                command.Parameters.Clear();
                command.Dispose();
                connection.Close();
            }
        }

        /// <summary>
        /// DataReaderToDataTable
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static DataTable ConvertDataReaderToDataTable(DbDataReader reader)
        {
            DataTable dt = new DataTable();
            // 获取查询结果的架构信息
            DataTable schemaTable = reader.GetSchemaTable();
            // 创建DataTable的列
            foreach (DataRow row in schemaTable.Rows)
            {
                string columnName = row["ColumnName"].ToString();
                Type dataType = (Type)row["DataType"];
                DataColumn column = new DataColumn(columnName, dataType);
                dt.Columns.Add(column);
            }
            // 添加查询结果的数据行
            while (reader.Read())
            {
                DataRow dataRow = dt.NewRow();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    dataRow[i] = reader.GetValue(i);
                }
                dt.Rows.Add(dataRow);
            }
            return dt;
        }

        /// <summary>
        /// 执行查询操作，返回第一行第一列
        /// </summary>
        /// <param name="dbContext">数据库上下文</param>
        /// <param name="commandText">命令语句</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="parameters">格式化参数集合</param>
        /// <returns>第一行第一列</returns>
        public static object ExecuteScalar(this DbContext dbContext, string commandText, CommandType commandType, params DbParameter[] parameters)
        {
            DbConnection connection = dbContext.Database.GetDbConnection();
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            // 设置Command
            using DbCommand command = connection.CreateCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;
            if (parameters != null && parameters.Length > 0)
            {
                command.Parameters.AddRange(parameters);
            }

            // 返回第一行第一列
            try
            {
                return command.ExecuteScalar();
            }
            catch
            {
                return null;
            }
            finally
            {
                command.Parameters.Clear();
                command.Dispose();
                connection.Close();
            }
        }

        /// <summary>
        /// 执行非查询操作，返回受影响的行数
        /// </summary>
        /// <param name="dbContext">数据库上下文</param>
        /// <param name="commandText">命令语句</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="parameters">格式化参数集合</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteNonQuery(this DbContext dbContext, string commandText, CommandType commandType, params DbParameter[] parameters)
        {
            DbConnection connection = dbContext.Database.GetDbConnection();
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            // 设置Command
            using DbCommand command = connection.CreateCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;
            if (parameters != null && parameters.Length > 0)
            {
                command.Parameters.AddRange(parameters);
            }

            // 返回受影响的行数
            try
            {
                return command.ExecuteNonQuery();
            }
            catch
            {
                return 0;
            }
            finally
            {
                command.Parameters.Clear();
                command.Dispose();
                connection.Close();
            }
        }

        /// <summary>
        /// 执行数据库事务，返回受影响的行数
        /// </summary>
        /// <param name="dbContext">数据库上下文</param>
        /// <param name="commands">命令集合</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteTransaction(this DbContext dbContext, List<SingleCommand> commands)
        {
            DbConnection connection = dbContext.Database.GetDbConnection();
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            // 开启事务
            using DbTransaction transaction = connection.BeginTransaction();
            try
            {
                foreach (var item in commands)
                {
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = item.CommandText;
                    command.CommandType = CommandType.Text;
                    command.Transaction = transaction;
                    if (item.Parameters.Count > 0)
                    {
                        command.Parameters.AddRange(item.Parameters.ToArray());
                    }
                    command.ExecuteNonQuery();
                }

                // 提交事务
                transaction.Commit();
                return 1;
            }
            catch
            {
                // 回滚事务
                transaction.Rollback();
                return 0;
            }
        }
    }

    /// <summary>
    /// 参数命令
    /// </summary>
    public class SingleCommand
    {
        /// <summary>
        /// 命令语句
        /// </summary>
        public string CommandText { get; set; }

        /// <summary>
        /// 格式化参数集合
        /// </summary>
        public List<DbParameter> Parameters { get; set; }
    }
}