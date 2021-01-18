﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace VivuDuyKhang.Database
{
    public class Database
    {
        public static DataTable Read_Table(string StoredProcedureName, Dictionary<string, object> dic_param = null)
        {
            string SQLconnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            DataTable result = new DataTable();
            using (SqlConnection conn = new SqlConnection(SQLconnectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                SqlTransaction transaction;
                // Start a local transaction.
                transaction = conn.BeginTransaction("SampleTransaction");
                cmd.Connection = conn;
                cmd.CommandText = StoredProcedureName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = transaction;
                if (dic_param != null)
                {
                    foreach (KeyValuePair<string, object> data in dic_param)
                    {
                        if (data.Value == null)
                        {
                            cmd.Parameters.AddWithValue("@" + data.Key, DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@" + data.Key, data.Value);
                        }
                    }
                }
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(result);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
            return result;
        }

        public static object Exec_Scala(string StoredProcedureName, Dictionary<string, object> dic_param = null)
        {
            string SQLconnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            object result = null;
            using (SqlConnection conn = new SqlConnection(SQLconnectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                SqlTransaction transaction;
                // Start a local transaction.
                transaction = conn.BeginTransaction("SampleTransaction");
                cmd.Connection = conn;
                cmd.CommandText = StoredProcedureName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = transaction;
                if (dic_param != null)
                {
                    foreach (KeyValuePair<string, object> data in dic_param)
                    {
                        if (data.Value == null)
                        {
                            cmd.Parameters.AddWithValue("@" + data.Key, DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@" + data.Key, data.Value);
                        }
                    }
                }
                try
                {
                    result = cmd.ExecuteScalar();
                    // Attempt to commit the transaction.
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    result = null;
                }
            }
            return result;
        }

        public static object Exec_Command(string StoredProcedureName, Dictionary<string, object> dic_param = null)
        {
            string SQLconnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            object result = null;
            using (SqlConnection conn = new SqlConnection(SQLconnectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                SqlTransaction transaction;
                // Start a local transaction.
                transaction = conn.BeginTransaction("SampleTransaction");
                cmd.Connection = conn;
                cmd.CommandText = StoredProcedureName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = transaction;
                if (dic_param != null)
                {
                    foreach (KeyValuePair<string, object> data in dic_param)
                    {
                        if (data.Value == null)
                        {
                            cmd.Parameters.AddWithValue("@" + data.Key, DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@" + data.Key, data.Value);
                        }
                    }
                }
                cmd.Parameters.Add("@CurrentID", SqlDbType.Int).Direction = ParameterDirection.Output;
                try
                {
                    cmd.ExecuteNonQuery();
                    result = cmd.Parameters["@CurrentID"].Value;
                    // Attempt to commit the transaction.
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    result = null;
                }

            }
            return result;
        }
    }
}