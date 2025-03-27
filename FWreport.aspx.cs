using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FWfood
{
    public partial class FWreport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 使用 KeyValuePair 儲存月份text和對應的value
                List<KeyValuePair<string, string>> monthList = new List<KeyValuePair<string, string>>()
                    {
                        new KeyValuePair<string, string>("一月", "01"),
                        new KeyValuePair<string, string>("二月", "02"),
                        new KeyValuePair<string, string>("三月", "03"),
                        new KeyValuePair<string, string>("四月", "04"),
                        new KeyValuePair<string, string>("五月", "05"),
                        new KeyValuePair<string, string>("六月", "06"),
                        new KeyValuePair<string, string>("七月", "07"),
                        new KeyValuePair<string, string>("八月", "08"),
                        new KeyValuePair<string, string>("九月", "09"),
                        new KeyValuePair<string, string>("十月", "10"),
                        new KeyValuePair<string, string>("十一月", "11"),
                        new KeyValuePair<string, string>("十二月", "12")
                    };

                // 使用 foreach 循環來創建 ListItem 並添加到 DropDownList
                foreach (var ddlmon in monthList)
                {
                    ListItem dbsListItem = new ListItem();
                    dbsListItem.Text = ddlmon.Key;  // 顯示廠區名稱
                    dbsListItem.Value = ddlmon.Value; // 設置 ID 作為 Value，轉為字串

                    // 添加到 DropDownList 控制項
                    MonthList.Items.Add(dbsListItem);
                }

                // 餐別
                List<KeyValuePair<string, string>> dinnerList = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("午餐", "2"),
                    new KeyValuePair<string, string>("晚餐", "3")
                };
                foreach (var ddldin in dinnerList)
                {
                    ListItem dinnerItems = new ListItem();
                    dinnerItems.Text = ddldin.Key;
                    dinnerItems.Value = ddldin.Value;

                    DinnerType.Items.Add(dinnerItems);
                }

                // 年
                for (int year = 2000; year <= DateTime.Now.Year; year++)
                {
                    YearList.Items.Add(new ListItem(year.ToString(), year.ToString()));
                }

                YearList.SelectedValue = DateTime.Now.Year.ToString();
                MonthList.SelectedValue = DateTime.Now.Month.ToString("D2");// 選擇當天的月份，由於list的value是兩位數，所以使用D2

                // 廠區
                List<KeyValuePair<string, string>> dbsList = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("龍潭廠", "T2"),
                    new KeyValuePair<string, string>("神岡廠", "T3"),
                    new KeyValuePair<string, string>("二林廠", "T4"),
                    new KeyValuePair<string, string>("路竹廠", "T6"),
                    new KeyValuePair<string, string>("南崁廠", "T9"),
                    new KeyValuePair<string, string>("雲林廠", "T10")
                };
                foreach (var dlldbs in dbsList)
                {
                    ListItem dbsItems = new ListItem();
                    dbsItems.Text = dlldbs.Key;
                    dbsItems.Value = dlldbs.Value;

                    DbsList.Items.Add(dbsItems);
                }
            }


        }
        public DataTable GetData()
        {

            string connectionString = ConfigurationManager.ConnectionStrings["SqlCon"].ConnectionString;

            string monthchoose = MonthList.SelectedItem.Value;
            int dinnerchoose = int.Parse(DinnerType.SelectedItem.Value);
            string yearchoose = YearList.SelectedValue;

            // 建立 SQL 查詢語句
            string query = $"SELECT  Date, ResMeals, TtlNum, AtlNum, AlreaNum, NoEatNum, " +
                           $"NorderNum, Plate, Box, WasteWeight, NcookFood, WasteFood, WasteType " +
                           $"FROM Waste WHERE Date LIKE '%{yearchoose}-{monthchoose}%' AND DType = {dinnerchoose} " + 
                           $"ORDER BY Date";


            // 使用 SqlConnection 來建立資料庫連接
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // 建立 SqlDataAdapter 來執行查詢
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);

                // 使用 DataTable 來儲存查詢結果
                DataTable dt = new DataTable();

                // 執行查詢並填充 DataTable
                dataAdapter.Fill(dt);

                // 處理 Date 欄位，僅保留日期中的「日」部分
                foreach (DataRow row in dt.Rows)
                {
                    if (row["Date"] != DBNull.Value) // 確保 Date 欄位不為空
                    {
                        DateTime date = Convert.ToDateTime(row["Date"]);
                        row["Date"] = date.Day.ToString("00"); // 僅顯示日，格式為兩位數
                    }
                }

                // 回傳 DataTable，需要綁定到 GridView 的資料
                return dt;
            }
        }

        private void Select(object sender, EventArgs e)
        {
            string connection = ConfigurationManager.ConnectionStrings["SqlCon"].ConnectionString;

            string monthchoose = MonthList.SelectedItem.Value;
            int dinnerchoose = int.Parse(DinnerType.SelectedItem.Value);
            string yearchoose = YearList.SelectedValue;

            string sql = $"SELECT Date, dbs, DType FROM Waste WHERE Date LIKE '%{yearchoose}-{monthchoose}%' AND dbs = 'T4' AND DType = {dinnerchoose} ";



            using (SqlConnection conn = new SqlConnection(connection))
            {
                // 創建 SQL 命令
                SqlCommand cmd = new SqlCommand(sql, conn);

                try
                {
                    // 開啟連接
                    conn.Open();

                    // 執行查詢並使用 SqlDataReader 讀取資料
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // 迭代結果
                        while (reader.Read())
                        {
                            // 讀取每一行資料，假設 Date, dbs, DType 都是字串型態，根據實際資料型態修改
                            string date = reader["Date"].ToString();
                            string dbs = reader["dbs"].ToString();
                            int dtype = Convert.ToInt32(reader["DType"]);


                            // 使用 DateTime 解析字串為日期型別
                            DateTime dateStr = DateTime.Parse(date);
                            // 提取年份和月份
                            string year = dateStr.Year.ToString(); // 年份
                            string month = dateStr.Month.ToString("00"); // 月份，保證是兩位數

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
                            string type = "";
                            if (dtype == 2)
                            {
                                type = "午餐";
                            }
                            else
                            {
                                type = "晚餐";
                            }


                            Label1.Text = $"{dbs00} {yearchoose}年";
                            Label2.Text = $"{type}";
                            Labelmon.Text = $"{monthchoose}月";
                        }
                    }
                }
                catch (Exception ex)
                {
                    // 捕捉並顯示錯誤
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
                finally
                {
                    // 確保連接會被關閉
                    conn.Close();
                }

            }

        }

        protected void monthsel_click(object sender, EventArgs e)
        {
            DataTable dt = GetData();
            GridView1.DataSource = dt;
            GridView1.DataBind();
            Select(sender, e);
        }

        protected void Excel(object sender, EventArgs e)
        {

            // 設置表格為 Excel 格式
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", $"attachment;filename={Label1.Text}{Labelmon.Text}{Label2.Text}報表.xls");
            Response.ContentType = "application/vnd.ms-excel";

            // 禁用頁面內容的緩存
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                // 在匯出之前，先渲染 Label1 和 Label2
                // 匯出 Label1 內容
                hw.WriteLine("<b>" + Label1.Text + "</b>" + Labelmon.Text + "</b>");
                hw.WriteLine("<br/>");

                // 匯出 Label2 內容
                hw.WriteLine("<b>" + Label2.Text + "</b>");
                hw.WriteLine("<br/>");

                // 設定 GridView 的輸出樣式
                GridView1.AllowPaging = false;  // 禁用分頁
                GridView1.DataBind();  // 確保綁定資料

                // 渲染 GridView 內容
                GridView1.RenderControl(hw);

                // 輸出結果
                Response.Write(sw.ToString());
                Response.End();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            // 這裡不需要做任何事情，只是告訴 ASP.NET 這個控制項可以被渲染
        }

    }
}