using KependudukanAPI.Libs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Npgsql;
using System.Data;
using System;

namespace KependudukanAPI.Controllers
{
    public class BasetrxController : Controller
    {
        /*public IActionResult Index()
        {
            return View();
        }*/
        private lDbConn dbconn = new lDbConn();

        public string InsertDataWarga(JObject json)
        {
            string strout = "";
            JObject jo = new JObject();
            var conn = dbconn.conString();
            NpgsqlTransaction trans;
            NpgsqlConnection connection = new NpgsqlConnection(conn);
            connection.Open();
            trans = connection.BeginTransaction();
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand("public.insert_data_warga", connection, trans);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("p_dw_id", Convert.ToInt32(json.GetValue("dw_id").ToString()));
                cmd.Parameters.AddWithValue("p_dw_no_kk", json.GetValue("dw_no_kk").ToString());
                cmd.Parameters.AddWithValue("p_dw_nik", json.GetValue("dw_nik").ToString());
                cmd.Parameters.AddWithValue("p_dw_nama_lengkap", json.GetValue("dw_nama_lengkap").ToString());
                cmd.Parameters.AddWithValue("p_dw_jenis_kelamin", json.GetValue("dw_jenis_kelamin").ToString());
                cmd.Parameters.AddWithValue("p_dw_alamat", json.GetValue("dw_alamat").ToString());
                cmd.Parameters.AddWithValue("p_dw_no_hp", json.GetValue("dw_no_hp").ToString());
                cmd.Parameters.AddWithValue("p_dw_ktp", json.GetValue("dw_ktp").ToString());
                cmd.Parameters.AddWithValue("p_dw_user_submit", json.GetValue("dw_user_submit").ToString());
                cmd.ExecuteNonQuery();

                trans.Commit();
                strout = "success";
            }
            catch (Exception ex)
            {
                trans.Rollback();
                strout = ex.Message;
            }
            connection.Close();
            NpgsqlConnection.ClearPool(connection);
            return strout;
        }

        public string UpdateDataWarga (JObject json)
        {
            string strout = "";
            JObject jo = new JObject();
            var conn = dbconn.conString();
            NpgsqlTransaction trans;
            NpgsqlConnection connection = new NpgsqlConnection(conn);
            connection.Open();
            trans = connection.BeginTransaction();
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand("public.update_data_warga", connection, trans);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_dw_id", Convert.ToInt32(json.GetValue("dw_id").ToString())); // update berdasarkan id / id auto increment

                cmd.Parameters.AddWithValue("p_dw_no_kk", json.GetValue("dw_no_kk").ToString());
                cmd.Parameters.AddWithValue("p_dw_nik", json.GetValue("dw_nik").ToString());
                cmd.Parameters.AddWithValue("p_dw_nama_lengkap", json.GetValue("dw_nama_lengkap").ToString());
                cmd.Parameters.AddWithValue("p_dw_jenis_kelamin", json.GetValue("dw_jenis_kelamin").ToString());
                cmd.Parameters.AddWithValue("p_dw_alamat", json.GetValue("dw_alamat").ToString());
                cmd.Parameters.AddWithValue("p_dw_no_hp", json.GetValue("dw_no_hp").ToString());
                cmd.Parameters.AddWithValue("p_dw_ktp", json.GetValue("dw_ktp").ToString());
                cmd.Parameters.AddWithValue("p_dw_user_submit", json.GetValue("dw_user_submit").ToString());
                cmd.ExecuteNonQuery();

                trans.Commit();
                strout = "success";
            }
            catch (Exception ex)
            {
                trans.Rollback();
                strout = ex.Message;
            }
            connection.Close();
            NpgsqlConnection.ClearPool(connection);
            return strout;
        }

        public string DeleteDataWarga(JObject json)
        {
            string strout = "";
            JObject jo = new JObject();
            var conn = dbconn.conString();
            NpgsqlTransaction trans;
            NpgsqlConnection connection = new NpgsqlConnection(conn);
            connection.Open();
            trans = connection.BeginTransaction();
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand("public.delete_data_warga", connection, trans);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_dw_id", Convert.ToInt32(json.GetValue("dw_id").ToString()));
                cmd.ExecuteNonQuery();

                trans.Commit();
                strout = "success";
            }
            catch (Exception ex)
            {
                trans.Rollback();
                strout = ex.Message;
            }
            connection.Close();
            NpgsqlConnection.ClearPool(connection);
            return strout;
        }
    }
}
