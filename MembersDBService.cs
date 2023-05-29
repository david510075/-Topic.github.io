using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using WebApplication8.Models;
using System.Security.Cryptography;
using System.Text;

namespace WebApplication8.Services
{
    public class MembersDBService
    {
        private readonly static string cnstr = ConfigurationManager.ConnectionStrings["Guestbooks"].ConnectionString;
        private readonly SqlConnection conn = new SqlConnection(cnstr);
        public void Register(User newMember)
        {
            newMember.Password = HashPassword(newMember.Password);
            string sql = $@"insert into Member(Account,Password,Name,Email,IsAdmin) values
            ('{newMember.Account}', '{newMember.Password}','{newMember.Name}','{newMember.Email}', '0')";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        public string HashPassword(string Password)
        {
            string saltkey = $@"sdf45s6f4s5d4f4";
            string saltAndPassword = string.Concat(Password, saltkey);
            SHA256CryptoServiceProvider sha256Hasher = new SHA256CryptoServiceProvider();
            byte[] PasswordData = Encoding.Default.GetBytes(saltAndPassword);
            byte[] HashDate = sha256Hasher.ComputeHash(PasswordData);
            string Hashresult = Convert.ToBase64String(HashDate);
            return Hashresult;
        }
        private User GetDataByAccount(string Account)
        {
            User Data = new User();
            string sql = $@"select * from Member where Account = '{Account}'";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Data.Account = dr["Account"].ToString();
                Data.Password = dr["Password"].ToString();
                Data.IsAdmin = Convert.ToBoolean(dr["IsAdmin"]);
            }
            catch (Exception e)
            {
                Data = null;
            }
            finally
            {
                conn.Close();
            }return Data;
        }
        public bool AccountCheck(string Account)
        {
            User Data = GetDataByAccount(Account);
            bool result = (Data == null);
            return result;
        }

        public string LoginCheck(string Account, string Password)
        {
            User LoginMember = GetDataByAccount(Account);
            if(LoginMember != null)
            {
                    if(PasswordCheck(LoginMember, Password))
                    {
                        return "";
                    }
                    else
                    {
                        return "密碼輸入錯誤";
                    }
            }
            else
            {
                return "無此會員帳號，請去註冊";
            }
        }
        public bool PasswordCheck(User CheckMember, string Password)
        {
            bool result = CheckMember.Password.Equals(HashPassword(Password));
            return result;        
        }
        public string GetRole(string Account)
        {
            string Role = "User";
            User LoginMember = GetDataByAccount(Account);
            if (LoginMember.IsAdmin)
            {
                Role += ",Admin";
            }
            return Role;
        }
        public string ChangePassword(string Account, string Password, string newPassword)
        {
            User LoginMember = GetDataByAccount(Account);
            if(PasswordCheck(LoginMember, Password))
            {
                LoginMember.Password = newPassword;
                string sql = $@"update User set Password = '{LoginMember.Password}' where Account = '{Account}'";
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();

                }
                catch (Exception e)
                {
                    throw new Exception(e.Message.ToString());
                }
                finally
                {
                    conn.Close();
                }
                return "密碼修改成功";
            }
            else
            {
                return "密碼修改失敗";
            }
        }

    }
}