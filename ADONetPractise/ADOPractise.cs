using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ADONetPractise
{
    public class ADOPractise
    {
        public void Execute()
        {
            string sql = "sp_db";
            try
            {
                string con = ConfigurationManager.ConnectionStrings["ADONETConn"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(con))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@minAge", 50));
                        cmd.Parameters.Add("@count", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@message", SqlDbType.VarChar, 30).Direction = ParameterDirection.Output;
                        cmd.CommandType = CommandType.StoredProcedure;

                        conn.Open();
                        var queryResult = cmd.ExecuteScalar();
                        Console.WriteLine((int)cmd.Parameters["@count"].Value);
                        Console.WriteLine((string)cmd.Parameters["@message"].Value);
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
    }
}
