using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FWfood
{
    public partial class FW000 : System.Web.UI.Page
    {

                    protected void Page_Init(object sender, EventArgs e)
        {
            HttpCookie db = Request.Cookies["user_db"];
            HttpCookie id = Request.Cookies["user_id"];
          //  HttpCookie pw = Request.Cookies["user_pw"];
            HttpCookie na = Request.Cookies["user_na"];
            HttpCookie au = Request.Cookies["user_au"];


            if (db == null)
                Response.Cookies["user_db"].Value = "T4";
            if (db != null)
                iUse00.Value = db.Value;
            if (id != null)
                iUse01.Value = id.Value;
            if (na != null)
                iUse03.Value = na.Value;
            if (au != null)
                iUse04.Value = au.Value;
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 使用 KeyValuePair 儲存廠區text和對應的value
                List<KeyValuePair<string, string>> dbsList = new List<KeyValuePair<string, string>>()
                    {
                        new KeyValuePair<string, string>("龍潭廠", "T2"),
                        new KeyValuePair<string, string>("神岡廠", "T3"),
                        new KeyValuePair<string, string>("二林廠", "T4"),
                        new KeyValuePair<string, string>("路竹廠", "T6"),
                        new KeyValuePair<string, string>("南崁廠", "T9"),
                        new KeyValuePair<string, string>("雲林廠", "T10")
                    };

                // 使用 foreach 循環來創建 ListItem 並添加到 DropDownList
                foreach (var dbs in dbsList)
                {
                    ListItem dbsListItem = new ListItem();
                    dbsListItem.Text = dbs.Key;  // 顯示廠區名稱
                    dbsListItem.Value = dbs.Value; // 設置 ID 作為 Value，轉為字串

                    // 添加到 DropDownList 控制項
                    Dbs.Items.Add(dbsListItem);
                }

                Dbs.Text = iUse00.Value;
                TextBox01.Text = iUse01.Value;
                TextBox02.Text = iUse02.Value;
            }


        }

        public class UserInfo
        {
            public string UserID { get; set; }
            public string Name { get; set; }
            public string Password { get; set; }
            public string Authority { get; set; }
        }

        protected void Button00_Click(object sender, EventArgs e)
        {

            string account = TextBox01.Text;
            string pw = TextBox02.Text;

            // 設定連接字串
            string connectionString = ConfigurationManager.ConnectionStrings["SqlCon"].ConnectionString;

            // 創建連接對象
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // 開啟連接
                conn.Open();
                    try
                    {
                        // 調用 Login 方法查詢用戶資料，並返回用戶資訊
                        var userInfo = Login(conn, account, pw);

                        if (userInfo != null)
                        {
                            //iErr00.Text = "登入成功！";


                            HttpCookie nacookie = new HttpCookie("user_na");
                            nacookie.Value = HttpUtility.UrlEncode(userInfo.Name, Encoding.GetEncoding("UTF-8"));
                            Response.Cookies.Add(nacookie);
                            HttpCookie idcookie = new HttpCookie("user_id");
                            idcookie.Value = HttpUtility.UrlEncode(userInfo.UserID, Encoding.GetEncoding("UTF-8"));
                            Response.Cookies.Add(idcookie);
                            HttpCookie pwcookie = new HttpCookie("user_pw");
                            pwcookie.Value = HttpUtility.UrlEncode(userInfo.Password, Encoding.GetEncoding("UTF-8"));
                            Response.Cookies.Add(pwcookie);
                            HttpCookie aucookie = new HttpCookie("user_au");
                            aucookie.Value = HttpUtility.UrlEncode(userInfo.Authority, Encoding.GetEncoding("UTF-8"));
                            Response.Cookies.Add(aucookie);
                            HttpCookie dbcookie = new HttpCookie("user_db");
                            dbcookie.Value = HttpUtility.UrlEncode(Dbs.Text, Encoding.GetEncoding("UTF-8"));
                            Response.Cookies.Add(dbcookie);


                            // 將使用者資料存入 Cookies
                            //Response.Cookies["user_id"].Value = userInfo.UserID;
                            //Response.Cookies["user_na"].Value = userInfo.Name;
                            //Response.Cookies["user_pw"].Value = userInfo.Password;
                            //Response.Cookies["user_au"].Value = userInfo.Authority;
                            //Response.Cookies["user_db"].Value = Dbs.Text;

                            Response.Cookies["user_id"].Path = "/";
                            Response.Cookies["user_na"].Path = "/";
                            Response.Cookies["user_pw"].Path = "/";
                            Response.Cookies["user_au"].Path = "/";
                            Response.Cookies["user_db"].Path = "/";

                            // 設定 Cookies 保存時間（ 1 小時）
                            Response.Cookies["user_id"].Expires = DateTime.Now.AddHours(1);
                            Response.Cookies["user_na"].Expires = DateTime.Now.AddHours(1);
                            Response.Cookies["user_pw"].Expires = DateTime.Now.AddHours(1);
                            Response.Cookies["user_au"].Expires = DateTime.Now.AddHours(1);
                            Response.Cookies["user_db"].Expires = DateTime.Now.AddHours(1);
                                             
                            // 進行頁面重定向
                            Response.Redirect("FW001.aspx");
                        }
                        else
                        {
                            // 登入失敗，顯示錯誤消息
                            iErr00.Text = "帳號或密碼錯誤！";
                        }
                    }
                    //catch (ThreadAbortException)
                    //{
                    //    // 忽略 ThreadAbortException，因為這是 Response.Redirect() 的正常行為
                    //}
                    catch (Exception ex)
                    {
                        // 顯示錯誤信息
                        iErr00.Text = "登入時發生錯誤: " + ex.Message;
                    }
                
            }
        }

        // 查詢資料
        // 查詢資料並返回使用者資訊
        private UserInfo Login(SqlConnection conn, string account, string pw)
        {
            // SQL 查詢語句
            string sqlQuery = @"
        SELECT UserID, Name, Password, Authority
        FROM Employee
        WHERE UserID = @account AND Password = @pw";

            using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
            {
                cmd.Parameters.AddWithValue("@account", account);
                cmd.Parameters.AddWithValue("@pw", pw);

                // 執行 SQL 查詢
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) // 檢查是否有符合的資料行
                    {
                        // 如果有符合的資料行，將資料存入 UserInfo 物件並返回
                        return new UserInfo
                        {
                            UserID = reader["UserID"].ToString(),
                            Name = reader["Name"].ToString(),
                            Password = reader["Password"].ToString(),
                            Authority = reader["Authority"].ToString()
                        };
                    }
                    else
                    {
                        // 沒有找到符合的資料
                        return null;
                    }
                }
            }
        }
    }
    
}