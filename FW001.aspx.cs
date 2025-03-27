using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FWfood
{
    public partial class FW001 : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            iMsg00.Text = "";
            NoInfo.Text = "";
            if (!IsPostBack)
            {
                iErr00.Text = "請選擇日期查詢";

                Imagemsg.Text = "";

                CheckBox1.Style["display"] = "none";
                CheckBox1.Enabled = false;
                Label14.Style["display"] = "none";
                FileUpload1.Style["display"] = "none";
                WasteType.Enabled = false;

                ToggleControls(false);
                // 測試
                //HttpCookie userIdCookie = Request.Cookies["user_id"];
                //HttpCookie userNameCookie = Request.Cookies["user_na"];
                //HttpCookie userPawwordCookie = Request.Cookies["user_pw"];
                //HttpCookie userAuCookie = Request.Cookies["user_au"];
                //HttpCookie userDbCookies = Request.Cookies["user_db"];
                //if (userIdCookie != null)
                //{
                //    // 使用 .Value 屬性來取得 cookie 的實際值
                //    cookies.Text = HttpUtility.UrlDecode(Request.Cookies["user_na"].Value, Encoding.GetEncoding("UTF-8"));
                //}
            }

            // 確保所有 Cookies 都不為 null
            if (Request.Cookies["user_id"] != null &&
                Request.Cookies["user_na"] != null &&
                Request.Cookies["user_pw"] != null &&
                Request.Cookies["user_au"] != null &&
                Request.Cookies["user_db"] != null)
            {
                //string id = Request.Cookies["user_id"].Value;
                //string na = Request.Cookies["user_na"].Value;
                //string pw = Request.Cookies["user_pw"].Value;
                //string au = Request.Cookies["user_au"].Value;
                //string dbs = Request.Cookies["user_db"].Value;
                string id = HttpUtility.UrlDecode(Request.Cookies["user_id"].Value, Encoding.GetEncoding("UTF-8"));
                string na = HttpUtility.UrlDecode(Request.Cookies["user_na"].Value, Encoding.GetEncoding("UTF-8"));
                string pw = HttpUtility.UrlDecode(Request.Cookies["user_pw"].Value, Encoding.GetEncoding("UTF-8"));
                string au = HttpUtility.UrlDecode(Request.Cookies["user_au"].Value, Encoding.GetEncoding("UTF-8"));
                string dbs = HttpUtility.UrlDecode(Request.Cookies["user_db"].Value, Encoding.GetEncoding("UTF-8"));
                string dbs00 = "";
                switch (dbs)
                {
                    case "T2":
                        dbs00 = "龍潭廠";
                        break;
                    case "T3":
                        dbs00 = "神岡廠";
                        break;
                    case "T4":
                        dbs00 = "二林廠";
                        break;
                    case "T6":
                        dbs00 = "路竹廠";
                        break;
                    case "T9":
                        dbs00 = "南崁廠";
                        break;
                    case "T10":
                        dbs00 = "雲林廠";
                        break;
                    default:
                        dbs00 = "未知";
                        break;
                }

                // 顯示登入資訊
                welcome.Text = $"您好,【{na}】 當前廠區:【{dbs00}】";
                iUse00.Value = dbs;
                iUse01.Value = id;
                iUse02.Value = na;
                iUse03.Value = pw;
                iUse04.Value = au;

                // cookies.text 測試用的
                // cookies.Text = $"您好,{na}, 廠區:[{dbs00}],密碼:{pw},權限:{au},工號:{id}";

                // 判定權限:顯示或隱藏按鈕
                if (iUse04.Value == "測試測試Test")
                {
                    Addbtn.Style["display"] = "inline-block";
                    Addbtn.Enabled = false;
                    Updatebtn.Style["display"] = "inline-block";

                }
                else if (iUse04.Value == "User")
                {
                    Addbtn.Style["display"] = "inline-block";
                    Addbtn.Enabled = false;
                }


            }
            else
            {
                // 顯示 alert 提示用戶重新登入，然後導向 FW000.aspx
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage",
                    "alert('請重新登入！'); window.location='FW000.aspx';", true);
            }


            // Button00_Click(sender, e);
        }

        public class WasteData
        {
            public string UserID { get; set; }
            public string dbs { get; set; }
            public int DType { get; set; }
            public string Date { get; set; }
            public string ResMeals { get; set; }
            public string TtlNum { get; set; }
            public string AtlNum { get; set; }
            public string AlreaNum { get; set; }
            public string NoEatNum { get; set; }
            public string NorderNum { get; set; }
            public string Plate { get; set; }
            public string Box { get; set; }
            public decimal NcookFood { get; set; }
            public decimal WasteFood { get; set; }
            public decimal WasteWeight { get; set; }
            public string UpdateUser { get; set; }
            public DateTime UpdateTime { get; set; }
        }


        protected void Addbtn_Click(object sender, EventArgs e)
        {
            HttpCookie idatecookie = new HttpCookie("user_idate");
            idatecookie.Value = HttpUtility.UrlEncode(TextBox13.Text, Encoding.GetEncoding("UTF-8"));
            Response.Cookies.Add(idatecookie);

            HttpCookie dbcookie = new HttpCookie("user_db");
            dbcookie.Value = HttpUtility.UrlEncode(iUse00.Value, Encoding.GetEncoding("UTF-8"));
            Response.Cookies.Add(dbcookie);

            int DType = 0;
            string selectedValue = RadioButtonList1.SelectedValue;

            // 餐別判斷
            if (selectedValue == "lunch")
            {
                DType = 2;
            }
            else if (selectedValue == "dinner")
            {
                DType = 3;
            }

            HttpCookie dtypecookie = new HttpCookie("user_dtype");
            dtypecookie.Value = HttpUtility.UrlEncode(DType.ToString(), Encoding.GetEncoding("UTF-8"));
            Response.Cookies.Add(dtypecookie);

            Response.Cookies["user_idate"].Path = "/";
            Response.Cookies["user_db"].Path = "/";
            Response.Cookies["user_dtype"].Path = "/";

            Response.Cookies["user_idate"].Expires = DateTime.Now.AddHours(1);
            Response.Cookies["user_db"].Expires = DateTime.Now.AddHours(1);
            Response.Cookies["user_dtype"].Expires = DateTime.Now.AddHours(1);

            Response.Redirect("FW002.aspx");
        }
        // 修改按鈕事件
        protected void Updatebtn_Click(object sender, EventArgs e)
        {
            iErr00.Text = "當前是'修改'功能";
            Updatebtn.Style["display"] = "none";
            // 顯現物件
            Searchbtn.Style["display"] = "inline-block";
            Searchbtn.Enabled = true;
            CheckBox1.Style["display"] = "inline-block";
            CheckBox1.Enabled = true;
            Savebtn.Style["display"] = "inline-block";
            Savebtn.Enabled = true;
            Label14.Style["display"] = "inline-block";
            FileUpload1.Style["display"] = "inline-block";

            // 隱藏/禁用 物件
            Addbtn.Enabled = false;
            Button00.Style["display"] = "none";
            Button00.Enabled = false;
            RadioButtonList1.Enabled = false;

            TextBox01.Enabled = true;
            TextBox02.Enabled = true;
            TextBox03.Enabled = true;
            TextBox04.Enabled = true;
            TextBox05.Enabled = true;
            TextBox06.Enabled = true;
            TextBox07.Enabled = true;
            TextBox08.Enabled = true;
            TextBox09.Enabled = true;
            TextBox10.Enabled = true;
            TextBox11.Enabled = true;

            if (NoInfo.Text == "查無資料!") {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage",
    "alert('查無資料，請重新輸入日期！'); window.location='FW001.aspx';", true);
            }

            // 以下測試用:

            //// 設定參數
            //int DType = 0;
            //string selectedValue = RadioButtonList1.SelectedValue;

            //// 餐別判斷
            //if (selectedValue == "lunch")
            //{
            //    DType = 2;
            //}
            //else if (selectedValue == "dinner")
            //{
            //    DType = 3;
            //}
            //string date = TextBox13.Text;
            //string dbs = iUse00.Value;

            //// 設定連接字串
            //string connectionString = ConfigurationManager.ConnectionStrings["SqlCon"].ConnectionString;

            //// 創建連接對象
            //using (SqlConnection conn = new SqlConnection(connectionString))
            //{
            //    // 開啟連接
            //    conn.Open();

            //    // 創建 SQL 交易（可選）
            //    using (SqlTransaction transaction = conn.BeginTransaction())
            //    {
            //        try
            //        {

            //            // 調用 InsertNew 方法插入數據
            //            SelectInfo(conn, transaction, date, DType, dbs);
            //            // 提交事務
            //            transaction.Commit();
            //            TextBox01.Enabled = true;
            //            TextBox02.Enabled = true;
            //            TextBox03.Enabled = true;
            //            TextBox04.Enabled = true;
            //            TextBox05.Enabled = true;
            //            TextBox06.Enabled = true;
            //            TextBox07.Enabled = true;
            //            TextBox08.Enabled = true;
            //            TextBox09.Enabled = true;
            //            TextBox10.Enabled = true;
            //            TextBox11.Enabled = true;
            //        }
            //        catch (Exception ex)
            //        {
            //            // 發生錯誤時回滾事務
            //            transaction.Rollback();

            //            // 顯示錯誤信息
            //            iErr00.Text = "查詢時發生錯誤: " + ex.Message;
            //        }

            //    }
            //}

        }

        protected void Searchbtn_Click(object sender, EventArgs e)
        {
            // 顯現物件
            Updatebtn.Style["display"] = "inline-block";
            Button00.Style["display"] = "inline-block";
            Button00.Enabled = true;
            Addbtn.Style["display"] = "inline-block";
            RadioButtonList1.Enabled = true;


            // 隱藏/禁用 物件
            Searchbtn.Style["display"] = "none";
            Searchbtn.Enabled = false;
            CheckBox1.Style["display"] = "none";
            CheckBox1.Enabled = false;
            Savebtn.Style["display"] = "none";
            Savebtn.Enabled = false;
            Label14.Style["display"] = "none";
            FileUpload1.Style["display"] = "none";
            TextBox01.Enabled = false;
            TextBox02.Enabled = false;
            TextBox03.Enabled = false;
            TextBox04.Enabled = false;
            TextBox05.Enabled = false;
            TextBox06.Enabled = false;
            TextBox07.Enabled = false;
            TextBox08.Enabled = false;
            TextBox09.Enabled = false;
            TextBox10.Enabled = false;
            TextBox11.Enabled = false;


            // 直接調用 Button00_Click 方法
            //Button00_Click(sender, e);

            iErr00.Text = "當前是'查詢'功能";
        }

        protected void Savebtn_Click(object sender, EventArgs e)
        {
            // 設定連接字串
            string connectionString = ConfigurationManager.ConnectionStrings["SqlCon"].ConnectionString;

            bool isValid = true;

            for (int i = 0; i < 11; i++)
            {

                // 使用兩位數格式來確保找到 TextBox01 到 TextBox11
                string textBoxId = string.Format("TextBox{0:D2}", i + 1); // 格式化為兩位數，如 TextBox01, TextBox02, ..., TextBox11

                TextBox textBox = (TextBox)FindControl(textBoxId);

                if (textBox == null) // 確保 TextBox 控件存在
                {
                    continue;
                }

                string value = textBox.Text.Trim(); // 取得並去掉前後空白

                // 檢查是否為空
                if (string.IsNullOrWhiteSpace(value))
                {
                    isValid = false;
                    break;
                }

                // 檢查 TextBox01 到 TextBox09 是否為整數
                if (i < 8) // 這裡的 i < 9 用於檢查 TextBox01 到 TextBox09
                {
                    int t;
                    if (!int.TryParse(value, out t))
                    {
                        isValid = false;
                        break;
                    }
                }
                // 檢查 TextBox10 和 TextBox11 是否為小數或整數
                else
                {
                    decimal d;
                    if (!decimal.TryParse(value, out d))
                    {
                        isValid = false;
                        break;
                    }
                }
            }

            if (!isValid)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage",
                    "alert('請正確填寫資料！');", true);
                return;
            }

            // 創建連接對象
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // 開啟連接
                conn.Open();

                // 創建 SQL 交易（可選）
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        int DType = 0;
                        string selectedValue = RadioButtonList1.SelectedValue;

                        // 餐別判斷
                        if (selectedValue == "lunch")
                        {
                            DType = 2;
                        }
                        else if (selectedValue == "dinner")
                        {
                            DType = 3;
                        }
                        // 創建 WasteData 物件並將表單資料賦值給其屬性
                        WasteData wasteData = new WasteData
                        {
                            Date = TextBox13.Text,
                            dbs = iUse00.Value,
                            DType = DType,
                            ResMeals = TextBox01.Text,
                            TtlNum = TextBox02.Text,
                            AtlNum = TextBox03.Text,
                            AlreaNum = TextBox04.Text,
                            NoEatNum = TextBox05.Text,
                            NorderNum = TextBox06.Text,
                            Plate = TextBox07.Text,
                            Box = TextBox08.Text,
                            NcookFood = decimal.Parse(TextBox09.Text),
                            WasteFood = decimal.Parse(TextBox10.Text),
                            WasteWeight = decimal.Parse(TextBox11.Text),
                            UpdateUser = iUse01.Value,  // 這可以是登錄用戶的名稱
                            UpdateTime = DateTime.Now
                        };

                        if (CheckBox1.Checked)
                        {

                            if (!FileUpload1.HasFile)
                            {

                                UpdateInfo(conn, transaction, wasteData);
                                // 提交事務
                                transaction.Commit();

                                CheckBox1.Style["display"] = "none";
                                CheckBox1.Enabled = false;
                                Label14.Style["display"] = "none";
                                FileUpload1.Style["display"] = "none";

                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage",
                                            "alert('修改成功！');window.location='FW001.aspx';", true);
                            }
                            else
                            {
                                // 轉換圖片為byte[]
                                byte[] imageBytes = CompressImage(FileUpload1.PostedFile);

                                if (imageBytes == null)
                                {
                                    iErr00.Text = "圖片轉換發生錯誤";
                                }

                                UpdatePhoto(conn, transaction, wasteData, imageBytes);
                                transaction.Commit();
                                CheckBox1.Style["display"] = "none";
                                CheckBox1.Enabled = false;
                                Label14.Style["display"] = "none";
                                FileUpload1.Style["display"] = "none";

                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage",
                                            "alert('修改成功！');window.location='FW001.aspx';", true);
                            }
                        }
                        else
                        {

                            iErr00.Text = "請先勾選修改確認";
                            Updatebtn.Style["display"] = "none";
                        }

                    }
                    catch (Exception ex)
                    {
                        // 發生錯誤時回滾事務
                        transaction.Rollback();

                        // 顯示錯誤信息
                        iErr00.Text = "插入數據時發生錯誤: " + ex.Message;
                    }
                }
            }


        }

        // 查詢按鈕事件
        protected void Button00_Click(object sender, EventArgs e)
        {
            int DType = 0;
            string selectedValue = RadioButtonList1.SelectedValue;

            // 餐別判斷
            if (selectedValue == "lunch")
            {
                DType = 2;
            }
            else if (selectedValue == "dinner")
            {
                DType = 3;
            }

            string date = iDate.Value;
            TextBox13.Text = date;
            string dbs = iUse00.Value;

            // 設定連接字串
            string connectionString = ConfigurationManager.ConnectionStrings["SqlCon"].ConnectionString;

            // 創建連接對象
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // 開啟連接
                conn.Open();

                    try
                    {

                        // 調用 InsertNew 方法插入數據
                        SelectInfo(conn, date, DType, dbs);

                        // 提示成功
                        iMsg00.Text = "查詢成功!";
                        iErr00.Text = "";

                    }
                    catch (Exception ex)
                    {
                        // 顯示錯誤信息
                        iErr00.Text = "查詢時發生錯誤: " + ex.Message;
                    }

                
            }
        }

        // Update資料無照片上傳
        private void UpdateInfo(SqlConnection conn, SqlTransaction transaction, WasteData wastedata)
        {
            string sqlUpdate = @"UPDATE Waste
                                 SET ResMeals = @resmeals,
                                     TtlNum = @ttlnum,
                                     AtlNum = @atlnum,
                                     AlreaNum = @alreanum,
                                     NoEatNum = @noeatnum,
                                     NorderNum = @nordernum,
                                     Plate = @plate,
                                     Box = @box,
                                     NcookFood = @ncookfood,
                                     WasteFood = @wastefood,
                                     WasteWeight = @wasteweight,
                                     UpdateUser = @updateuser,
                                     UpdateTime = @updatetime
                                 WHERE Date = @Date AND DType = @Dtype AND dbs = @Dbs";
            SqlCommand cmd = new SqlCommand(sqlUpdate, conn, transaction);

            cmd.Parameters.AddWithValue("@Date", wastedata.Date);
            cmd.Parameters.AddWithValue("@Dtype", wastedata.DType);
            cmd.Parameters.AddWithValue("@Dbs", wastedata.dbs);
            cmd.Parameters.AddWithValue("@resmeals", wastedata.ResMeals);
            cmd.Parameters.AddWithValue("@ttlnum", wastedata.TtlNum);
            cmd.Parameters.AddWithValue("@atlnum", wastedata.AtlNum);
            cmd.Parameters.AddWithValue("@alreanum", wastedata.AlreaNum);
            cmd.Parameters.AddWithValue("@noeatnum", wastedata.NoEatNum);
            cmd.Parameters.AddWithValue("@nordernum", wastedata.NorderNum);
            cmd.Parameters.AddWithValue("@plate", wastedata.Plate);
            cmd.Parameters.AddWithValue("@box", wastedata.Box);
            cmd.Parameters.AddWithValue("@ncookfood", wastedata.NcookFood);
            cmd.Parameters.AddWithValue("@wastefood", wastedata.WasteFood);
            cmd.Parameters.AddWithValue("@wasteweight", wastedata.WasteWeight);
            cmd.Parameters.AddWithValue("@updateuser", wastedata.UpdateUser);
            cmd.Parameters.AddWithValue("@updatetime", wastedata.UpdateTime);

            cmd.ExecuteNonQuery(); // 確保這裡的連接已經打開
        }

        //Update資料有照片上傳
        private void UpdatePhoto(SqlConnection conn, SqlTransaction transaction, WasteData wastedata, byte[] imageBytes)
        {
            string sqlUpdate = @"UPDATE Waste
                                 SET ResMeals = @resmeals,
                                     TtlNum = @ttlnum,
                                     AtlNum = @atlnum,
                                     AlreaNum = @alreanum,
                                     NoEatNum = @noeatnum,
                                     NorderNum = @nordernum,
                                     Plate = @plate,
                                     Box = @box,
                                     NcookFood = @ncookfood,
                                     WasteFood = @wastefood,
                                     WasteWeight = @wasteweight,
                                     Photo = @photo,
                                     UpdateUser = @updateuser,
                                     UpdateTime = @updatetime
                                 WHERE Date = @Date AND DType = @Dtype AND dbs = @Dbs";
            SqlCommand cmd = new SqlCommand(sqlUpdate, conn, transaction);

            cmd.Parameters.AddWithValue("@Date", wastedata.Date);
            cmd.Parameters.AddWithValue("@Dtype", wastedata.DType);
            cmd.Parameters.AddWithValue("@Dbs", wastedata.dbs);
            cmd.Parameters.AddWithValue("@resmeals", wastedata.ResMeals);
            cmd.Parameters.AddWithValue("@ttlnum", wastedata.TtlNum);
            cmd.Parameters.AddWithValue("@atlnum", wastedata.AtlNum);
            cmd.Parameters.AddWithValue("@alreanum", wastedata.AlreaNum);
            cmd.Parameters.AddWithValue("@noeatnum", wastedata.NoEatNum);
            cmd.Parameters.AddWithValue("@nordernum", wastedata.NorderNum);
            cmd.Parameters.AddWithValue("@plate", wastedata.Plate);
            cmd.Parameters.AddWithValue("@box", wastedata.Box);
            cmd.Parameters.AddWithValue("@ncookfood", wastedata.NcookFood);
            cmd.Parameters.AddWithValue("@wastefood", wastedata.WasteFood);
            cmd.Parameters.AddWithValue("@wasteweight", wastedata.WasteWeight);
            cmd.Parameters.AddWithValue("@updateuser", wastedata.UpdateUser);
            cmd.Parameters.AddWithValue("@updatetime", wastedata.UpdateTime);
            cmd.Parameters.AddWithValue("@photo", SqlDbType.VarBinary).Value = imageBytes;

            cmd.ExecuteNonQuery(); // 確保這裡的連接已經打開
        }

        // 查詢資料
        private void SelectInfo(SqlConnection conn, string Date, int DType, string dbs)
        {
            // SQL 查詢語句
            string sqlQuery = @"
        SELECT ResMeals, TtlNum, AtlNum, AlreaNum, NoEatNum, NorderNum, Plate, Box, NcookFood, WasteFood, WasteWeight, Photo, WasteType
        FROM Waste
        WHERE Date = @Date AND DType = @Dtype AND dbs = @Dbs";

            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            cmd.Parameters.AddWithValue("@Date", Date);
            cmd.Parameters.AddWithValue("@Dtype", DType);
            cmd.Parameters.AddWithValue("@Dbs", dbs);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read(); // 讀取第一行資料

                    string resMeals = reader["ResMeals"].ToString();
                    string ttlNum = reader["TtlNum"].ToString();
                    string atlNum = reader["AtlNum"].ToString();
                    string alreaNum = reader["AlreaNum"].ToString();
                    string noeatNum = reader["NoEatNum"].ToString();
                    string norderNum = reader["NorderNum"].ToString();
                    string plate = reader["Plate"].ToString();
                    string box = reader["Box"].ToString();
                    string ncookFood = reader["NcookFood"].ToString();
                    string wasteFood = reader["WasteFood"].ToString();
                    string wasteWeight = reader["WasteWeight"].ToString();
                    string wasteType = reader["WasteType"].ToString();

                    byte[] photo = reader["Photo"] as Byte[];


                    // 將查詢到的資料放入 TextBox 中
                    TextBox01.Text = resMeals;
                    TextBox02.Text = ttlNum;
                    TextBox03.Text = atlNum;
                    TextBox04.Text = alreaNum;
                    TextBox05.Text = noeatNum;
                    TextBox06.Text = norderNum;
                    TextBox07.Text = plate;
                    TextBox08.Text = box;
                    TextBox09.Text = ncookFood;
                    TextBox10.Text = wasteFood;
                    TextBox11.Text = wasteWeight;

                    // 迴圈遍歷每個 ListItem
                    foreach (ListItem item in WasteType.Items)
                    {
                        // 如果該項目的 Value 出現在資料庫中儲存的字串中，將其設為選中
                        if (wasteType.Contains(item.Value))
                        {
                            item.Selected = true;
                        }
                        else
                        {
                            item.Selected = false;
                        }
                    }

                    // 顯示相關控件
                    ToggleControls(true);

                    ShowImage(photo);

                    Addbtn.Enabled = false;
                    Updatebtn.Enabled = true;

                }
                else
                {
                    // 沒有資料時顯示 "查無資料" 訊息
                    ToggleControls(false);
                    NoInfo.Text = "查無資料!";

                    Addbtn.Enabled = true;
                    Updatebtn.Enabled = false;
                }

                reader.Close();
            }
        }

        // 顯示/隱藏控件的方法
        private void ToggleControls(bool showData)
        {
            // 控制 Label 和 TextBox 的顯示
            Label01.Style["display"] = showData ? "inline" : "none";
            Label02.Style["display"] = showData ? "inline" : "none";
            Label03.Style["display"] = showData ? "inline" : "none";
            Label04.Style["display"] = showData ? "inline" : "none";
            Label05.Style["display"] = showData ? "inline" : "none";
            Label06.Style["display"] = showData ? "inline" : "none";
            Label07.Style["display"] = showData ? "inline" : "none";
            Label08.Style["display"] = showData ? "inline" : "none";
            Label09.Style["display"] = showData ? "inline" : "none";
            Label10.Style["display"] = showData ? "inline" : "none";
            Label11.Style["display"] = showData ? "inline" : "none";

            TextBox01.Style["display"] = showData ? "inline" : "none";
            TextBox02.Style["display"] = showData ? "inline" : "none";
            TextBox03.Style["display"] = showData ? "inline" : "none";
            TextBox04.Style["display"] = showData ? "inline" : "none";
            TextBox05.Style["display"] = showData ? "inline" : "none";
            TextBox06.Style["display"] = showData ? "inline" : "none";
            TextBox07.Style["display"] = showData ? "inline" : "none";
            TextBox08.Style["display"] = showData ? "inline" : "none";
            TextBox09.Style["display"] = showData ? "inline" : "none";
            TextBox10.Style["display"] = showData ? "inline" : "none";
            TextBox11.Style["display"] = showData ? "inline" : "none";

            NoInfo.Style["display"] = showData ? "none" : "block";
            Image1.Style["display"] = showData ? "inline" : "none";
            Image1.Visible = showData;
        }

        // 處理圖片顯示邏輯
        private void ShowImage(byte[] photo)
        {
            if (photo != null && photo.Length > 0)
            {
                string base64String = Convert.ToBase64String(photo);
                string imageType = "image/jpeg";  // 默認為 JPEG 格式

                string extension = GetImageExtension(photo);  // 用來獲取圖片的副檔名

                switch (extension.ToLower())
                {
                    case ".jpg":
                    case ".jpeg":
                        imageType = "image/jpeg";
                        break;
                    case ".png":
                        imageType = "image/png";
                        break;
                    case ".gif":
                        imageType = "image/gif";
                        break;
                    default:
                        imageType = "image/jpeg";  // 默認為 JPEG
                        break;
                }
                Image1.ImageUrl = $"data:{imageType};base64,{base64String}";
                Image1.Style["display"] = "block";
                Image1.Visible = true;  // 顯示圖片
                Imagemsg.Style["display"] = "none";
            }
            else
            {
                Imagemsg.Style["display"] = "block";
                Imagemsg.Text = "查無圖片!";
                Image1.Style["display"] = "none";
                Image1.Visible = false;  // 如果沒有圖片，隱藏圖片
            }
        }

        // 判定圖片的格式
        private string GetImageExtension(byte[] photo)
        {
            // 根據字節數據的開頭判斷擴展名
            if (photo.Length > 4)
            {
                // JPEG 檔案的開頭是 FF D8
                if (photo[0] == 0xFF && photo[1] == 0xD8)
                {
                    return ".jpg";
                }
                // PNG 檔案的開頭是 89 50 4E 47
                if (photo[0] == 0x89 && photo[1] == 0x50 && photo[2] == 0x4E && photo[3] == 0x47)
                {
                    return ".png";
                }
                // GIF 檔案的開頭是 47 49 46
                //if (photo[0] == 0x47 && photo[1] == 0x49 && photo[2] == 0x46)
                //{
                //    return ".gif";
                //}
            }

            return ".jpg";  // 默認為 JPG
        }


        // 將圖片轉換成 byte[]
        //private byte[] ConvertToByteArray(HttpPostedFile file)
        //{
        //    using (var binaryReader = new BinaryReader(file.InputStream))
        //    {
        //        return binaryReader.ReadBytes(file.ContentLength);
        //    }
        //}

        private byte[] CompressImage(HttpPostedFile file)
        {
            using (var originalImage = System.Drawing.Image.FromStream(file.InputStream))
            {
                // 設定新的圖片大小（您可以根據需要調整）
                int maxWidth = 800;
                int maxHeight = 600;

                int width = originalImage.Width;
                int height = originalImage.Height;

                // 根據最大寬度和高度縮放圖片
                if (width > maxWidth || height > maxHeight)
                {
                    float ratio = Math.Min((float)maxWidth / width, (float)maxHeight / height);
                    width = (int)(width * ratio);
                    height = (int)(height * ratio);
                }

                // 創建一個新的圖片以便縮放
                var resizedImage = new Bitmap(originalImage, new Size(width, height));

                // 使用 MemoryStream 來將圖片壓縮為 byte[]
                using (var memoryStream = new MemoryStream())
                {
                    // 設定圖片格式為 JPEG 並壓縮圖片（80%的質量）
                    resizedImage.Save(memoryStream, ImageFormat.Jpeg);
                    return memoryStream.ToArray();  // 返回壓縮後的圖片 byte[]
                }
            }
        }


        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Button00_Click(sender, e);

        }
    }
}