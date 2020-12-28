using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test1.Interface;
using Test1.Models;

namespace Test1.Services
{
    public class UserService : IUserService
    {
        private readonly string _connectionString;

        public UserService(IConfiguration _configuration)
        {
            _connectionString = _configuration.GetConnectionString("OracleDBConnection");
        }

        public User AddUser(User user)
        {
            try
            {
                using (OracleConnection con = new OracleConnection(_connectionString))
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = con;
                        con.Open();
                        cmd.CommandText = $"insert into LoginUser(" +
                            $"Id, Password,Name, Birth, Phone,Age) " +
                            $"Values(" +
                            $"'{user.Id}','{user.Password}','{user.Name}','{user.Birth}','{user.Phone}',{user.Age})";
                        cmd.ExecuteNonQuery();


                    }
                }
                return user;

            }
            catch (Exception e)
            {

                throw(e);
            }
        }

        public IEnumerable<User> GetAllUser()
        {
            List<User> users = new List<User>();
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.BindByName = true;
                    cmd.CommandText = "select * from LoginUser";
                    OracleDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        User user = new User
                        {
                            Id = rdr["Id"].ToString(),
                            Password = rdr["Password"].ToString(),
                            Name = rdr["Name"].ToString(),
                            //Birth = Convert.ToDateTime(rdr["Birth"]),
                            Birth = rdr["Birth"].ToString(),
                            Phone = rdr["Phone"].ToString(),
                            Age = Convert.ToInt32(rdr["Age"])

                        };
                        users.Add(user);
                    }
                }
            }
            return users;
        }

        public User GetUserById(string id)
        {
            User user = new User();
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.BindByName = true;
                    cmd.CommandText = $"select *from LoginUser where id='{id}'";
                    OracleDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        user.Id = rdr["Id"].ToString();
                        user.Password = rdr["Password"].ToString();
                        user.Name = rdr["Name"].ToString();
                        //user.Birth = Convert.ToDateTime(rdr["Birth"]);
                        user.Birth = rdr["Birth"].ToString();
                        user.Phone = rdr["Phone"].ToString();
                        user.Age = Convert.ToInt32(rdr["Age"]);
                    }
                }
            }
            return user;
        }

        public void RemoveUser(string id)
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

        public User UpdateUser(User user)
        {
            try
            {
                using (OracleConnection con = new OracleConnection(_connectionString))
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = con;
                        con.Open();
                        cmd.CommandText = $"update LoginUser Set Name='" +
                            $"{user.Name}',password='{user.Password}',age='{user.Age}',phone='{user.Phone}',birth='{user.Birth}' where id='{user.Id}'";
                        cmd.ExecuteNonQuery();
                    }
                }
                return user;
            }
            catch (Exception e)
            {

                throw(e);
            }
        }
    }
}
