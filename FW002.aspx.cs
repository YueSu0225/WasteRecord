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
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FWfood
{
    public partial class FW002 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            iErr00.Text = "";

            // 禁止瀏覽器緩存頁面
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Expires = -1;
            // 防止重複提交表單
            if (!IsPostBack)
            {
                string token = Guid.NewGuid().ToString();
                Session["FormToken"] = token;
                refToken.Value = token;
                //測試用
                //TextBox12.Text = Session["FormToken"].ToString();
                //TextBox13.Text = refToken.Value;
            }

            if (Request.Cookies["user_id"] != null &&
               Request.Cookies["user_na"] != null &&
               Request.Cookies["user_pw"] != null &&
               Request.Cookies["user_au"] != null &&
               Request.Cookies["user_db"] != null &&
               Request.Cookies["user_idate"] != null &&
               Request.Cookies["user_dtype"] != null
                )
            {
                string id = HttpUtility.UrlDecode(Request.Cookies["user_id"].Value, Encoding.GetEncoding("UTF-8"));
                string na = HttpUtility.UrlDecode(Request.Cookies["user_na"].Value, Encoding.GetEncoding("UTF-8"));
                string pw = HttpUtility.UrlDecode(Request.Cookies["user_pw"].Value, Encoding.GetEncoding("UTF-8"));
                string au = HttpUtility.UrlDecode(Request.Cookies["user_au"].Value, Encoding.GetEncoding("UTF-8"));
                string dbs = HttpUtility.UrlDecode(Request.Cookies["user_db"].Value, Encoding.GetEncoding("UTF-8"));
                string date = HttpUtility.UrlDecode(Request.Cookies["user_idate"].Value, Encoding.GetEncoding("UTF-8"));
                string dtype = HttpUtility.UrlDecode(Request.Cookies["user_dtype"].Value, Encoding.GetEncoding("UTF-8"));

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

                string dtype00 = "";
                switch (dtype)
                {
                    case "2":
                        dtype00 = "午餐";
                        break;
                    case "3":
                        dtype00 = "晚餐";
                        break;
                    default:
                        dtype00 = "未知";
                        break;
                }

                iUse00.Value = dbs;
                iUse01.Value = id;
                iUse02.Value = na;
                iUse03.Value = pw;
                iUse04.Value = au;
                iUse05.Value = date;
                iUse06.Value = dtype;

                // 判定權限
                if (iUse04.Value == "測試測試Test" || iUse04.Value == "User")
                {
                    // welcome.text是測試用
                    // welcome.Text = $"您好,{na}, 廠區[{dbs}],密碼{pw},權限[{au}][{iUse04.Value}],工號{id},日期:{date},餐別{dtype}";
                    Now2.Text = $"{iUse02.Value},您好! 當前日期:【{iUse05.Value}】";
                    Now.Text = $"廠區:【{dbs00}】 餐別:【{dtype00}】";
                }
                else
                {
                    // 無權限就定向登入頁
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage",
                        "alert('查無權限！'); window.location='FW000.aspx';", true);
                }
            }
            else
            {
                // 顯示 alert 提示用戶重新登入，然後導向 FW000.aspx
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage",
                    "alert('請重新登入！'); window.location='FW000.aspx';", true);
            }


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
            public string LogUser { get; set; }
            public DateTime LogDate { get; set; }         
            public string WasteType { get; set; }
        }

        // 按鈕點擊事件
        protected void Button01_Click(object sender, EventArgs e)
        {

            // 防止重複提交表單
            string formToken = Session["FormToken"] as string;
            if (formToken == null || formToken != refToken.Value)
            {
                iErr00.Text = "請勿重複提交！";
                return;
            }

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


            // 確保每次插入的 ID 是唯一的
            // string idtmp = Guid.NewGuid().ToString();

            // 設定連接字串
            string connectionString = ConfigurationManager.ConnectionStrings["SqlCon"].ConnectionString;

            if (!FileUpload1.HasFile) // 確保有檔案上傳
            {
                iErr00.Text = "請選擇圖片！";
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
                        string SelectedWasteType = string.Empty;

                        // 迴圈遍歷每個 WasteType 的 List，檢查它是否被選中
                        foreach (ListItem item in WasteType.Items)
                        {
                            if (item.Selected)
                            {
                                // 直接將選中的值串接到字串中
                                SelectedWasteType += item.Value;
                            }
                        }

                        // 初始 WasteData 物件並將表單資料進去對應的get;set
                        WasteData wasteData = new WasteData
                        {
                            UserID = iUse01.Value,
                            dbs = iUse00.Value,
                            DType = int.Parse(iUse06.Value),
                            Date = iUse05.Value,
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
                            LogUser = iUse01.Value,  
                            LogDate = DateTime.Now,
                            WasteType = SelectedWasteType
                        };

                        // 檢查資料是否已存在
                        bool dataExists = selectdata(conn, wasteData); // selectdata 返回值改為布林值:true/false;

                        // 如果資料已經存在
                        if (dataExists) 
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage",
                                $"alert('{wasteData.dbs}的{wasteData.Date} 已經有資料'); window.location='FW001.aspx';", true);
                            return;
                        }

                        // 轉換圖片為byte[]
                        byte[] imageBytes = CompressImage(FileUpload1.PostedFile);

                        if (imageBytes == null)
                        {
                            iErr00.Text = "圖片轉換發生錯誤";
                        }
                        // 調用 InsertNew 方法插入數據
                        InsertNew(conn, transaction, wasteData, imageBytes);
                        // 提交事務
                        transaction.Commit();

                        // 提示成功
                        //iMsg00.Text = "數據成功插入!";

                        // 2. 更新 Session 防止重複提交
                        Session["FormToken"] = "";
                        refToken.Value = "";

                        // 顯示 alert 提示成功，然後導向 FW001.aspx
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage",
                            "alert('新增成功'); window.location='FW001.aspx';", true);
                        //Response.Redirect("FW001.aspx");
                    }
                    catch (ThreadAbortException)
                    {
                        // 忽略 ThreadAbortException，因為這是 Response.Redirect() 的正常行為
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

        // 查詢是否有無資料
        private bool selectdata(SqlConnection conn, WasteData data)
        {
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Waste WHERE Date = @Date AND DType = @Dtype AND dbs = @Dbs"
                ,conn);
            cmd.Parameters.AddWithValue("@Date", data.Date);
            cmd.Parameters.AddWithValue("@Dtype", data.DType);
            cmd.Parameters.AddWithValue("@Dbs", data.dbs);

            try
            {
                // 執行並讀取結果
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())  // 如果有結果
                    {
                        int count = reader.GetInt32(0);  // 讀取 COUNT(*) 的結果
                        return count > 0;                
                    }
                }
            }
            catch (Exception ex)
            {
                // 錯誤處理
                Console.WriteLine("查詢錯誤: " + ex.Message);
            }
            return false;  // 如果資料不存在，返回 false
        }

        // 儲存資料
        private void InsertNew(SqlConnection conn, SqlTransaction transaction, WasteData data, byte[] imageBytes)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Waste (UserID, dbs, DType, Date, ResMeals, TtlNum, AtlNum, AlreaNum, NoEatNum, NorderNum, Plate, Box, NcookFood, WasteFood, WasteWeight, Photo, LogUser, LogDate, WasteType)" +
                 "VALUES (@Userid, @Dbs, @Dtype, @Date, @Resmeals, @Ttlnum, @Atlnum, @Alreanum, @Noeatnum, @Nordernum, @Plate, @Box, @Ncookfood, @Wastefood, @Wasteweight, @Photo, @Loguser, @Logdate, @Wastetype)", conn, transaction);
            //cmd.Parameters.AddWithValue("@Id", idtmp);
            cmd.Parameters.AddWithValue("@Userid", data.UserID);
            cmd.Parameters.AddWithValue("@Dbs", data.dbs);
            cmd.Parameters.AddWithValue("@Dtype", data.DType);
            cmd.Parameters.AddWithValue("@Date", data.Date);
            cmd.Parameters.AddWithValue("@Resmeals", data.ResMeals);
            cmd.Parameters.AddWithValue("@Ttlnum", data.TtlNum);
            cmd.Parameters.AddWithValue("@Atlnum", data.AtlNum);
            cmd.Parameters.AddWithValue("@Alreanum", data.AlreaNum);
            cmd.Parameters.AddWithValue("@Noeatnum", data.NoEatNum);
            cmd.Parameters.AddWithValue("@Nordernum", data.NorderNum);
            cmd.Parameters.AddWithValue("@Plate", data.Plate);
            cmd.Parameters.AddWithValue("@Box", data.Box);
            cmd.Parameters.AddWithValue("@Ncookfood", data.NcookFood);
            cmd.Parameters.AddWithValue("@Wastefood", data.WasteFood);
            cmd.Parameters.AddWithValue("@Wasteweight", data.WasteWeight);
            cmd.Parameters.AddWithValue("@Loguser", data.LogUser);
            cmd.Parameters.AddWithValue("@Logdate", data.LogDate);
            cmd.Parameters.AddWithValue("@Wastetype", data.WasteType);
            cmd.Parameters.Add("@Photo", SqlDbType.VarBinary).Value = imageBytes;

            cmd.ExecuteNonQuery(); // 確保這裡的連接已經打開
        }


        // 將圖片轉換成 byte[]
        //private byte[] ConvertToByteArray(HttpPostedFile file)
        //{
        //    using (var binaryReader = new BinaryReader(file.InputStream))
        //    {
        //        return binaryReader.ReadBytes(file.ContentLength);
        //    }
        //}


        // 壓縮圖片，並返回 byte[] 格式
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
    }
}