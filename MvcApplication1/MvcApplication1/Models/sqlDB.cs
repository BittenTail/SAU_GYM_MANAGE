using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace MvcApplication1.Models
{
    public class SqlDB
    {
        protected SqlConnection conn;

        //打开数据库连接
        public void OpenConnection()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["defaultconnection"].ConnectionString);
            try{
                if (conn.State.ToString() != "Open"){
                    conn.Open();
                }
            }catch(SqlException ex){
                throw ex;
            }

        }
        //关闭数据库
        public void CloseConnection(){
            try{
                conn.Close();
            }catch(SqlException ex){
                throw ex;
            }
        }
        //insert 插入数据
        public int Insert(String sql){
            int i = 0;
            try{
                if(conn.State.ToString() == "Open"){
                    SqlCommand cmd = new SqlCommand(sql,conn);
                    i = cmd.ExecuteNonQuery();
                }
                return i;
            }catch(SqlException ex){
                return -1;
            }
        }
        //search 搜索数据库
        public DataSet Search(string sql){
            try{
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand(sql, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }catch(SqlException ex){
                throw ex;
            }
        }

        //update修改
        public int Update(String sql)
        {
            int i = 0;
            try
            {
                if (conn.State.ToString() == "Open")
                {
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    i = cmd.ExecuteNonQuery();
                }
                return i;
            }
            catch (SqlException ex)
            {
                return -1;
            }
        }
        //delete 删除
        public int Delete(string sql){
            try{
                int result = 0;
                SqlCommand cmd = new SqlCommand(sql, conn);
                result = cmd.ExecuteNonQuery();
                return result;
            }catch (SqlException ex){
                throw ex;
            }
        } 
    }
}