using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Message = WebApplication8.Models.Message;

namespace WebApplication8.Services
{
    public class MessageDBService
    {
        private readonly static string cnstr = ConfigurationManager.ConnectionStrings["Guestbooks"].ConnectionString;
        private readonly SqlConnection conn = new SqlConnection(cnstr);
        public List<Message> GetDataList(ForPaging Paging, int P_Id)
        {
            List<Message> DataList = new List<Message>();
            SetMaxPaging(Paging, P_Id);
            DataList = GetAllDataList(Paging, P_Id);
            return DataList;
        }

        public void SetMaxPaging(ForPaging Paging, int P_Id)
        {
            int Row = 0;
            string sql = $@" SELECT * FROM Message WHERE P_Id = {P_Id}; ";
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
        public List<Message> GetAllDataList(ForPaging paging, int P_Id)
        {
            List<Message> DataList = new List<Message>();
            string sql = $@" SELECT m.*, d.Name FROM (SELECT row_number() 
            OVER(ORDER BY M_Id) AS sort,* FROM Message WHERE P_Id = {P_Id}) m INNER
            JOIN Member d ON m.Account = d.Account WHERE m.sort BETWEEN {(paging.
            NowPage - 1) * paging.ItemNum + 1} AND {paging.NowPage * paging.ItemNum}; ";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                // 取得 Sql 資料
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Message Data = new Message();
                    Data.M_Id = Convert.ToInt32(dr["M_Id"]);
                    Data.P_Id = Convert.ToInt32(dr["P_Id"]);
                    Data.Account = dr["Account"].ToString();
                    Data.Content = dr["Content"].ToString();
                    Data.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
                    Data.User.Name = dr["Name"].ToString();
                    DataList.Add(Data);
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
            return DataList;
        }
        public void InsertMessage(Message newData)
        {
            newData.M_Id = LastMessageFinder(newData.P_Id);
            string sql = $@" INSERT INTO Message (P_Id,M_Id,Account,Content,CreateTime) VALUES ( '{newData.P_Id}','{newData.M_Id}','{newData.Account}','{newData.Content}','{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}' ); ";
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
        public int LastMessageFinder(int P_Id)
        {
            int Id;
            string sql = $@" SELECT TOP 1 * FROM Message WHERE P_Id = {P_Id} ORDER BY M_Id DESC;";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Id = Convert.ToInt32(dr["M_Id"]);
            }
            catch (Exception e)
            {
                Id = 0;
            }
            finally
            {
                conn.Close();
            }
            return Id + 1;
        }
        public void UpdateMessage(Message UpdateData)
        {
            string sql = $@" UPDATE Message SET Content = '{UpdateData.Content}' 
            WHERE P_Id = {UpdateData.P_Id} AND M_Id = {UpdateData.M_Id}; ";
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
        public void DeleteMessage(int P_Id, int M_Id)
        {
            string DeleteMessage = $@" DELETE FROM Message WHERE P_Id = {P_Id} AND M_Id = {M_Id}; ";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(DeleteMessage, conn);
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
        
        public Message ContentMessage(int P_Id, int M_Id)
        {
            Message Data = new Message();
            string sql = $@" Select * FROM Message WHERE P_Id = {P_Id} AND M_Id = {M_Id}; ";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                // 取得 Sql 資料
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                {
                    Data.M_Id = Convert.ToInt32(dr["M_Id"]);
                    Data.P_Id = Convert.ToInt32(dr["P_Id"]);
                    Data.Account = dr["Account"].ToString();
                    Data.Content = dr["Content"].ToString();
                    Data.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
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
        
    }
}