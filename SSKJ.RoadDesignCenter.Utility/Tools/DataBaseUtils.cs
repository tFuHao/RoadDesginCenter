using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SSKJ.RoadDesignCenter.Utility.Tools
{
    public class DataBaseUtils
    {
        public async Task<bool> CreateDataBase(string dataBaseName)
        {
            try
            {
                var sql = "create database " + dataBaseName;
                var connStr = "Server=localhost;port=3306;user id=root;password=123456;";
                var con = new MySqlConnection(connStr);
                con.Open();
                var cmd = new MySqlCommand(sql, con);
                var result = cmd.ExecuteNonQuery();
                con.Close();
                return await CreateTable(dataBaseName);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private Task<bool> CreateTable(string dataBaseName)
        {
            throw new NotImplementedException();
        }
    }
}
