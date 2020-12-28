using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // 오라클 연결 문자열        
            string strConn = "Data Source=DMP_WINNERS;User Id=TEST_USER;Password=TEST1234;";
            // 네트워크 연결 정보 직접 대입
            //string s = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=ORCL)));User Id=hr;Password=hr;";

            try
            {
                using (OracleConnection con = new OracleConnection(strConn))
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = con;
                        con.Open();
                        cmd.CommandText = "select * from LoginUser";
                        OracleDataReader rdr = cmd.ExecuteReader();
                        List<User> users = new List<User>();
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
            }
            catch(Exception exp)
            {

            }

        }
    }

    public class User
    {
        public string Id { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Birth { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
    }
}
