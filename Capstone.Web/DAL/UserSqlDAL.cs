using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Net.Mail;
using Capstone.Web.Models.Data;

namespace Capstone.Web.DAL
{
    public class UserSqlDal : IUserDal
    {
        private readonly string databaseConnectionString;

        public UserSqlDal(string connectionString)
        {
            databaseConnectionString = connectionString;
        }
        public bool ChangePassword(string username, string newPassword)
        {
            try
            {
                string sql = $"UPDATE Login SET password = '{newPassword}' WHERE User_Name = '{username}'";

                using (SqlConnection conn = new SqlConnection(databaseConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    int result = cmd.ExecuteNonQuery();

                    return result > 0;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public bool CreateUser(User newUser)
        {
            return true;

            try
            {
                string sql = $"INSERT INTO Login VALUES (@Username, @Password, @User_Role);";

                using (SqlConnection conn = new SqlConnection(databaseConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Username", newUser.Username);
                    cmd.Parameters.AddWithValue("@Password", newUser.Password);
                    cmd.Parameters.AddWithValue("@User_Role", newUser.User_Role);


                    int result = cmd.ExecuteNonQuery();

                    return result > 0;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public List<string> GetUsernames(string startsWith)
        {
            List<string> usernames = new List<string>();

            //usernames.Add("Ahmad");
            //return usernames;

            try
            {
                string sql = $"SELECT User_Name FROM Login WHERE User_Name LIKE '{startsWith}%';";

                using (SqlConnection conn = new SqlConnection(databaseConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        usernames.Add(Convert.ToString(reader["Username"]));
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return usernames;
        }

        public User GetUser(string username)
        {
             User user = null;


            //User user = new User
            //{
            //    Username = "a",
            //    Password = "a",
            //    User_Role = 3

            //};
            try
            {
                string sql = $"SELECT TOP 1 * FROM Login WHERE  User_Name= '{username}'";

                using (SqlConnection conn = new SqlConnection(databaseConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        user = new User
                        {
                            Username = Convert.ToString(reader["User_Name"]),
                            Password = Convert.ToString(reader["Password"]),
                            User_Role = Convert.ToString(reader["User_Role"])
                        };
                    }

                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return user;
        }

        public User GetUser(string username, string password)
        {
            User user = null;


            //User user = new User
            //{
            //    Username = "a",
            //    Password = "a",
            //    User_Role = 1

            //};

            //return user;


            try
            {
                string sql = $"SELECT TOP 1 * FROM Login WHERE User_Name = '{username}' AND Password = '{password}'";

                using (SqlConnection conn = new SqlConnection(databaseConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        user = new User
                        {
                            Username = Convert.ToString(reader["User_Name"]),
                            Password = Convert.ToString(reader["Password"]),
                            User_Role = Convert.ToString(reader["User_Role"])
                        };
                    }

                }
            }
            catch (SqlException )
            {
                throw;
            }

            return user;
        }

        public String SendEmail()
        {
            try
            {
                //    string sql = $"SELECT TOP 1 * FROM app_user WHERE user_name = '{username}' AND password = '{password}'";

                //    using (SqlConnection conn = new SqlConnection(databaseConnectionString))
                //    {
                //        conn.Open();

                //        SqlCommand cmd = new SqlCommand(sql, conn);
                //        SqlDataReader reader = cmd.ExecuteReader();

                //        while (reader.Read())
                //        {
                //            user = new User
                //            {
                //                Username = Convert.ToString(reader["user_name"]),
                //                Password = Convert.ToString(reader["password"]),
                //                ProfileId = Convert.ToInt32(reader["avatar_id"])
                //            };
                //        }

                //    }
                //}
                //catch (SqlException ex)
                //{
                //    throw;
                //}

                //return user;

                using (MailMessage mm = new MailMessage("obiedatdeveloper@gmail.com", "alakmo8@gmail.com"))
                {
                    mm.Subject = "Birthday Greetings";
                    mm.Body = string.Format("<b>Happy Birthday </b>Ahmad<br /><br />Many happy returns of the day.");

                    mm.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    System.Net.NetworkCredential credentials = new System.Net.NetworkCredential();
                    credentials.UserName = "alakmo8@gmail.com";
                    credentials.Password = "hrana1987AL";
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = credentials;
                    smtp.Port = 587;
                    smtp.Send(mm);
                    return "Email sent successfully !";





                    //MailMessage mail = new MailMessage();
                    //mail.To.Add("obiedatdeveloper@gmail.com");
                    ////mail.CC.Add("ccid@hotmail.com");
                    //mail.From = new MailAddress("alakmo8@gmail.com");
                    //mail.Subject = "Feedback for Website";
                    //string Body = "Name: Phone Number: TextPhone.Text";
                    //mail.Body = Body;
                    //mail.IsBodyHtml = true;
                    //SmtpClient smtp = new SmtpClient();
                    //smtp.Host = "smtp.gmail.com";
                    //smtp.EnableSsl = true;
                    //smtp.Credentials = new System.Net.NetworkCredential("alakmo8@gmail.com", "hrana1987AL");
                    //smtp.Send(mail);

                    //WriteToFile("Email sent successfully to: " + name + " " + email);
                }

            }

            catch (SqlException)
            {
                return "Faild to send an Email";
            }


        }
    }
}
