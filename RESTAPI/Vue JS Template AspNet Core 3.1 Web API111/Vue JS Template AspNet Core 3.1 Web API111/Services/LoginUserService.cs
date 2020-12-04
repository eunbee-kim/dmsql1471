using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vue_JS_Template_AspNet_Core_3._1_Web_API111.Interface;
using Vue_JS_Template_AspNet_Core_3._1_Web_API111.Models;

namespace Vue_JS_Template_AspNet_Core_3._1_Web_API111.Services
{
    public class LoginUserService : ILoginUserService
    {
        private readonly string _connectionString;

        public LoginUserService(IConfiguration _configuration)
        {
            _connectionString = _configuration.GetConnectionString("OracleDBConnection");
        }

        public IEnumerable<LoginUser> GetAllLoginUser()
        {
            List<LoginUser> loginUsers = new List<LoginUser>();

            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.BindByName = true;
                    cmd.CommandText = "Select Id,Password,Name from LoginUser";
                    OracleDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        LoginUser loginUser = new LoginUser
                        {
                            //Id = Convert.ToInt32(rdr["Id"]),
                            //Password = Convert.ToInt32(rdr["Password"])
                            Id = rdr["Id"].ToString(),
                            Password = rdr["Password"].ToString(),
                            Name = rdr["Name"].ToString()
                        };
                        loginUsers.Add(loginUser);
                    }
                }
            }

            return loginUsers;
        }

        public LoginUser GetLoginUserById(string loginid)
        {
            LoginUser loginUser = new LoginUser();
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.BindByName = true;
                    cmd.CommandText = $"Select * from LoginUser where id='{loginid}'";
                    OracleDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        loginUser.Id = rdr["Id"].ToString();
                        loginUser.Password = rdr["Password"].ToString();
                        loginUser.Name = rdr["Name"].ToString();
                    }
                }
            }

            return loginUser;
        }


        public LoginUser AddLoginUser(LoginUser user)
        {
            try
            {
                using (OracleConnection con = new OracleConnection(_connectionString))
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = con;
                        con.Open();
                        cmd.CommandText = $"Insert into LoginUser(Id,Password,Name) Values('{user.Id}','{user.Password}','{user.Name}')";
                        cmd.ExecuteNonQuery();
                    }
                }
                return user;
            }
            
            catch (Exception)
            {

                throw;
            }
        }

        public LoginUser UpdateLoginUser(LoginUser user)
        {
            try
            {
                using (OracleConnection con = new OracleConnection(_connectionString))
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = con;
                        con.Open();
                        cmd.CommandText = $"update LoginUser Set Name='{user.Name}',password='{user.Password}' where id='{user.Id}'";
                        cmd.ExecuteNonQuery();
                    }
                }
                return user;
            }

            catch (Exception)
            {

                throw;
            }
        }

        public void Remove(string id)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.CommandText = $"delete from LoginUser where id='{id}'";
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
