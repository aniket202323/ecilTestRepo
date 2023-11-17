using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BatchDataLayer
{
    public class UserData
    {
        /// <summary>
        /// AuthorizeUser
        /// </summary>
        /// <param name="_connectionString"></param>
        /// <param name="user"></param>
        /// <returns>boolean</returns>
        public bool AuthorizeUser(string _connectionString, string user)
        {


            bool result;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("spLocal_BDATUserAuthorization", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter IsActiveUser = new SqlParameter("@IsActiveUser", SqlDbType.Bit);
                IsActiveUser.Direction = ParameterDirection.Output;

                command.Parameters.Add(new SqlParameter("@UserName", user));
                command.Parameters.Add(IsActiveUser);
                command.ExecuteNonQuery();
                result = Convert.ToBoolean(command.Parameters["@IsActiveUser"].Value);
                conn.Close();
            }
            return result;
        }
    }
}