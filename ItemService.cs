using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using WebApplication8.Models;

namespace WebApplication8.Services
{
    public class ItemService
    {
        private readonly static string cnstr = ConfigurationManager.ConnectionStrings["Guestbooks"].ConnectionString;
        private readonly SqlConnection conn = new SqlConnection(cnstr);
        public Item GetDataById(int Id)
        {
            Item Data = new Item();
            string sql = $@"select * from Pet where Id = {Id}";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Data.Id = Convert.ToInt32(dr["Id"]);
                Data.Image = dr["Image"].ToString();
                Data.Account = dr["Account"].ToString();
                Data.date = Convert.ToDateTime(dr["date"]);
                Data.animal = dr["animal"].ToString();
                Data.Variety = dr["Variety"].ToString();
                Data.Color = dr["Color"].ToString();
                Data.Place = dr["Place"].ToString();
                Data.Sex = dr["Sex"].ToString();
                Data.Phone = dr["Phone"].ToString();
                Data.Remark = dr["Remark"].ToString();
            }
            catch (Exception e)
            {
                Data = null;
            }finally
            {
                conn.Close();
            }return Data;
        }
        public List<int> GetIdList(ForPaging Paging)
        {
            SetMaxPaging(Paging);
            List<int> IdList = new List<int>();
            string sql = $@"select Id from Pet";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    IdList.Add(Convert.ToInt32(dr["Id"]));
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }return IdList;
        }
        public void SetMaxPaging(ForPaging Paging)
        {
            int Row = 0;
            string sql = $@"select * from Pet";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Row++;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
            Paging.MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Row) / Paging.ItemNum));
            Paging.SetRightPage();
        }
        
        public void Insert(Item newData)
        {
            string sql = $@"insert into Pet(Account,date,animal,Variety,Color,Place,Sex,Phone,Remark,Image)
            values('{newData.Account}','{newData.date.ToString("yyyy-MM-dd")}','{newData.animal}','{newData.Variety}','{newData.Color}','{newData.Place}','{newData.Sex}','{newData.Phone}','{newData.Remark}','{newData.Image}')";
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
        public bool CheckUpdate(int A_Id)
        {
            Item Data = GetDataById(A_Id);
            int MessageCount = 0;
            string sql = $@" SELECT * FROM Message WHERE P_Id = {A_Id}; ";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    MessageCount++;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
            return (Data != null && MessageCount == 0);
        }
        public void UpdateArticle(Item UpdateData)
        {
            string sql = $@" UPDATE Pet SET date = '{UpdateData.date.ToString("yyyy-MM-dd")}', animal = '{UpdateData.animal}', Variety = '{UpdateData.Variety}', Color = '{UpdateData.Color}', Place = '{UpdateData.Place}',
            Sex = '{UpdateData.Sex}', Phone = '{UpdateData.Phone}', Remark = '{UpdateData.Remark}' WHERE Id = {UpdateData.Id}; ";
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
        public void Delete(int Id)
        {
            string sql = $@"Delete From Pet Where Id = {Id}";
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
        public Item GetAllDataList(int Id)
        {
            Item Data = new Item();
            string sql = $@" select * from Pet Where Id Like '%{Id}%' ";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                {
                    Data.Id = Convert.ToInt32(dr["Id"]);
                    Data.Image = dr["Image"].ToString();
                    Data.Account = dr["Account"].ToString();
                    Data.date = Convert.ToDateTime(dr["date"]);
                    Data.animal = dr["animal"].ToString();
                    Data.Variety = dr["Variety"].ToString();
                    Data.Color = dr["Color"].ToString();
                    Data.Place = dr["Place"].ToString();
                    Data.Sex = dr["Sex"].ToString();
                    Data.Phone = dr["Phone"].ToString();
                    Data.Remark = dr["Remark"].ToString();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
            return Data;
        }
        public List<int> GetIdLists(Search Search)
        {
            
            List<int> IdList = new List<int>();
            string sql = $@"select Id from Pet where (Place like '%{Search.Splace}%' and Sex Like '%{Search.Ssex}%' and animal like '%{Search.Sanimal}%') ";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    IdList.Add(Convert.ToInt32(dr["Id"]));
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
            return IdList;
        }
        public List<User> GetUser()
        {
            List<User> DataList = new List<User>();
            string sql = $@"select * from Member ";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while(dr.Read())
                {
                    User Data = new User();
                    Data.Account = dr["Account"].ToString();
                    Data.Name = dr["Account"].ToString();
                    Data.Email = dr["Account"].ToString();
                    Data.IsAdmin = Convert.ToBoolean(dr["IsAdmin"]);
                    DataList.Add(Data);
                }


            }
            catch (Exception e)
            {
                DataList = null;
            }
            finally
            {
                conn.Close();
            }
            return DataList;
        }
        public void DeleteUser(int Account)
        {
            string sql = $@"Delete From Member Where Account = {Account}";
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
        public Item GetPet()
        {
            Item Data = new Item();
            string sql = $@"select * from Pet";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Data.Id = Convert.ToInt32(dr["Id"]);
                Data.Image = dr["Image"].ToString();
                Data.Account = dr["Account"].ToString();
                Data.date = Convert.ToDateTime(dr["date"]);
                Data.animal = dr["animal"].ToString();
                Data.Variety = dr["Variety"].ToString();
                Data.Color = dr["Color"].ToString();
                Data.Place = dr["Place"].ToString();
                Data.Sex = dr["Sex"].ToString();
                Data.Phone = dr["Phone"].ToString();
                Data.Remark = dr["Remark"].ToString();
            }
            catch (Exception e)
            {
                Data = null;
            }
            finally
            {
                conn.Close();
            }
            return Data;
        }
        public void DeletePet(int Id)
        {
            string sql = $@"Delete From Pet Where Id = {Id}";
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
    }
}