using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using coreLY.Models;
using MySql.Data;
using MySql.Data.MySqlClient;
namespace coreLY.Controllers
{
    public class HomeController : Controller
    {
        string mysqlconnstr = "server=127.0.0.1;user id=root;database=szdb;password=test;sslmode=None;charset=utf8";
        public IActionResult Index()
        {
            MySqlConnection conn = new MySqlConnection(mysqlconnstr);
            conn.Open();
            string sqlstr = "select * from lytable";
            MySqlDataAdapter comm = new MySqlDataAdapter(sqlstr, conn);
            System.Data.DataSet ds = new System.Data.DataSet();
            comm.Fill(ds);
            System.Data.DataTable dt = ds.Tables[0];
            int cout = dt.Rows.Count;
            List<Models.lytable> list = new List<Models.lytable>();
            Models.lytable ly = null;
            for (int i = 0; i < cout; i++)
            {
                ly = new lytable()
                {
                    id = Convert.ToInt32(dt.Rows[i]["Id"].ToString()),
                    detail = dt.Rows[i]["detail"].ToString()
                };
                list.Add(ly);
            }
            ViewData["data"] = list;
            conn.Close();
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        /// <summary>
        /// 删除留言
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult delete(int id)
        {
            MySqlParameter parid = new MySqlParameter("@id", MySqlDbType.Int32);
            parid.Value = id;
            using (MySqlConnection conn = new MySqlConnection(mysqlconnstr))
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand();
                comm.CommandType = System.Data.CommandType.Text;
                comm.Connection = conn;
                comm.CommandText = "delete from lytable where `Id`=@id";
                comm.Parameters.Add(parid);
                comm.ExecuteNonQuery();

            }
                return View();
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="detail"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult add(string detail)
        {
            MySqlParameter pardetail = new MySqlParameter("@detail", MySqlDbType.VarChar, 255);
            pardetail.Value = detail;
            using (MySqlConnection con = new MySqlConnection(mysqlconnstr))
            {
                con.Open();
  
                MySqlCommand comm = new MySqlCommand();
                comm.CommandType = System.Data.CommandType.Text;
                comm.Connection = con;
                comm.CommandText = "insert into  lytable(`Id`,`detail`)values(null,@detail)";
                comm.Parameters.Add(pardetail);
                comm.ExecuteNonQuery();
            }
            return View();
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
