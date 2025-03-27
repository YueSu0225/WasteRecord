<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FW001.aspx.cs" Inherits="FWfood.FW001" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title></title>
    <style>
        .auto-style1 {
            width: 100%;
        }

        .btn {
            font-size: large;
            font-weight: bold;
            font-family: Microsoft JhengHei;
            border: 0px #fff none;
            color: #fff;
            background-color: #f87b1e;
            Width: 130px;
            height: 30px;
            border-radius: 12px;
        }

        .auto-style2 {
            width: 100%;
            height: 59px;
        }


        .center {
            width: 50%;
            margin: 5px auto;
            text-align: center;
        }

        .table {
            border: 3px double #808080;
            width: 100%;
            font-family: '微軟正黑體';
            background-color: #f9f9f9;
            border-radius: 10px;
            padding: 10px;
        }

        .flex-container {
            display: flex;
            align-items: center;
            justify-content: space-between;
            width: 100%;
            padding: 5px 5px;
        }

        .btn:hover {
            background-color: #e06b18;
            transform: scale(1.05);
        }

        .tx1 {
            text-align: left;
            flex-grow: 2;
        }
        /* 大螢幕 */
        @media (min-width: 1024px) {
            .item {
                width: 30%;
            }
        }

        /* 中等螢幕 */
        @media (max-width: 1023px) and (min-width: 600px) {
            .item {
                width: 48%;
            }
        }

        /* 小螢幕 */
        @media (max-width: 599px) {
            .item {
                width: 100%;
            }
        }
    </style>
    <script type="text/javascript">
        // 測試用:
        <%--        window.onload = function datechange() {
            //日期選擇器:
            var dateInput = document.getElementById('dateChoose');
            var hiddenField = document.getElementById('iDate');

            // 如果 input 沒有值，則設為今天日期
            if (!dateInput.value) {
                var today = new Date().toISOString().split('T')[0]; // 格式化為 yyyy-MM-dd
                dateInput.value = today; // 設定 input 預設值
                hiddenField.value = today; // 設定 HiddenField 值
                document.getElementById('<%= TextBox12.ClientID %>').value = today; // 設定測試用TextBox12 值(正式後要刪除)
            }
        }--%>

        function updateDate() {
            var newDate = document.getElementById('dateChoose').value;
            document.getElementById('iDate').value = newDate;
            // 格式化日期為 yyyy-MM-dd 格式並更新 TextBox12 的值(正式後，以下全刪除，只要日期能帶進iDate就可以)
<%--            var formattedDate = new Date(newDate).toISOString().split('T')[0]; // 轉換為 yyyy-MM-dd 格式
            document.getElementById('<%= TextBox12.ClientID %>').value = formattedDate;--%>
        }
    </script>
</head>
<body>


    <form id="form1" runat="server">
        <asp:HiddenField ID="iDate" runat="server" Value="" />
        <asp:HiddenField ID="iUse00" runat="server" Value="" />
        <asp:HiddenField ID="iUse01" runat="server" Value="" />
        <asp:HiddenField ID="iUse02" runat="server" Value="" />
        <asp:HiddenField ID="iUse03" runat="server" Value="" />
        <%-- <div style="width: 350px" class="item center">
            <asp:Label ID="cookies" runat="server" Font-Size="Large" Font-Names="Microsoft JhengHei" ForeColor="Red" Text="" /><br />
        </div>--%>
        <asp:HiddenField ID="iUse04" runat="server" Value="" />
        <div style="width: 350px" class="center item">
            <h1>廚餘日報</h1>
            <asp:Label ID="welcome" runat="server" Font-Size="Medium" Font-Names="新細明體" ForeColor="Blue" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
        </div>
        <div style="width: 350px" class="item center">
            <asp:Label ID="iErr00" runat="server" Font-Size="Large" Font-Names="Microsoft JhengHei" ForeColor="Red" Text="" /><br />
            <asp:Label ID="iMsg00" runat="server" Font-Size="Large" Font-Names="Microsoft JhengHei" ForeColor="#006600" Text="" />
        </div>
        <div style="width: 350px" class="center item" id="divDate">
            <asp:Label runat="server" Text="查詢日期:"></asp:Label>
            <input type="date" id="dateChoose" value="預設" onchange="updateDate()" style="width: 45%; height: 20px;" />
        </div>

        <div style="width: 350px" class="center item">
            <asp:Button ID="Addbtn" runat="server" Text="新增" Style="display: none" Enabled="false" OnClick="Addbtn_Click" />

            <asp:Button ID="Updatebtn" runat="server" Text="修改" Style="display: none" Enabled="false" OnClick="Updatebtn_Click" />
            <asp:CheckBox ID="CheckBox1" runat="server" Text="修改" Style="display: none" Enabled="false" />
        </div>
        <div style="width: 350px" class="center item">
            <asp:Button ID="Searchbtn" runat="server" Text="使用查詢功能" Style="display: none" Enabled="false" OnClick="Searchbtn_Click" />
        </div>
        <div style="width: 350px" class="center item">
            <table style="border: thick double #808080; width: 100%; font-family: 微軟正黑體;" class="table">
                <tr>
                    <td class="auto-style1">
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                            <asp:ListItem Text="午餐" Value="lunch" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="晚餐" Value="dinner"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <%--                <tr>
                    <td>
                        <div class="flex-container">
                            <asp:Label ID="Label12" runat="server" Font-Size="Medium" ForeColor="Black" Text="測試日期用:" class="tx1" />
                            <asp:TextBox ID="TextBox12" runat="server" Width="100px" Height="19px" Enabled="False" />
                        </div>
                    </td>
                </tr>--%>
                <tr>
                    <td>
                        <div class="flex-container">
                            <asp:Label ID="Label13" runat="server" Font-Size="Medium" ForeColor="Black" Text="目前表單日期:" class="tx1" />
                            <asp:TextBox ID="TextBox13" runat="server" Width="100px" Height="19px" Enabled="False" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="flex-container">
                            <asp:Label ID="Label01" runat="server" Font-Size="Medium" ForeColor="Black" Text="預定餐數" class="tx1" />
                            <asp:TextBox ID="TextBox01" runat="server" Width="50px" Height="19px" Enabled="False" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="flex-container">
                            <asp:Label ID="Label02" runat="server" Font-Size="Medium" ForeColor="Black" Text="總訂餐人數" class="tx1" />
                            <asp:TextBox ID="TextBox02" runat="server" Width="50px" Height="19px" Enabled="False" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="flex-container">
                            <asp:Label ID="Label03" runat="server" Font-Size="Medium" ForeColor="Black" Text="實際用餐人數" class="tx1" />
                            <asp:TextBox ID="TextBox03" runat="server" Width="50px" Height="19px" Enabled="False" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="flex-container">
                            <asp:Label ID="Label04" runat="server" Font-Size="Medium" ForeColor="Black" Text="已用餐人數" class="tx1" />
                            <asp:TextBox ID="TextBox04" runat="server" Width="50px" Height="19px" Enabled="False" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="flex-container">
                            <asp:Label ID="Label05" runat="server" Font-Size="Medium" ForeColor="Black" Text="未用餐人數" class="tx1" />
                            <asp:TextBox ID="TextBox05" runat="server" Width="50px" Height="19px" Enabled="False" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="flex-container">
                            <asp:Label ID="Label06" runat="server" Font-Size="Medium" ForeColor="Black" Text="未訂用餐人數" class="tx1" />
                            <asp:TextBox ID="TextBox06" runat="server" Width="50px" Height="19px" Enabled="False" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="flex-container">
                            <asp:Label ID="Label07" runat="server" Font-Size="Medium" ForeColor="Black" Text="實際用餐盤數" class="tx1" />
                            <asp:TextBox ID="TextBox07" runat="server" Width="50px" Height="19px" Enabled="False" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="flex-container">
                            <asp:Label ID="Label08" runat="server" Font-Size="Medium" ForeColor="Black" Text="實際便當人數" class="tx1" />
                            <asp:TextBox ID="TextBox08" runat="server" Width="50px" Height="19px" Enabled="False" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="flex-container">
                            <asp:Label ID="Label09" runat="server" Font-Size="Medium" ForeColor="Black" Text="未煮食材(KG)" class="tx1" />
                            <asp:TextBox ID="TextBox09" runat="server" Width="50px" Height="19px" Enabled="False" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="flex-container">
                            <asp:Label ID="Label10" runat="server" Font-Size="Medium" ForeColor="Black" Text="已煮未用食材(KG)" class="tx1" />
                            <asp:TextBox ID="TextBox10" runat="server" Width="50px" Height="19px" Enabled="False" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="flex-container">
                            <asp:Label ID="Label11" runat="server" Font-Size="Medium" ForeColor="Black" Text="員工丟掉廚餘重量(KG)" class="tx1" />
                            <asp:TextBox ID="TextBox11" runat="server" Width="50px" Height="19px" Enabled="False" />
                        </div>
                    </td>
                    <tr>
                        <td>
                            <div>
                                <asp:CheckBoxList ID="WasteType" runat="server">
                                    <asp:ListItem Value="A">骨頭</asp:ListItem>
                                    <asp:ListItem Value="B">湯水</asp:ListItem>
                                    <asp:ListItem Value="C">菜渣</asp:ListItem>
                                    <asp:ListItem Value="D">飯渣</asp:ListItem>
                                    <asp:ListItem Value="E">廚餘</asp:ListItem>
                                </asp:CheckBoxList>
                            </div>
                        </td>
                    </tr>
                </tr>
                <tr>
                    <td>
                        <div class="center">
                            <asp:Label ID="NoInfo" runat="server" Font-Size="Large" Font-Names="Microsoft JhengHei" ForeColor="Red" Text="" Style="display: none;" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="Label14" runat="server" Font-Size="Medium" ForeColor="Black" Text="拍照上傳:" class="tx1" Style="border: 0 0 0" />
                        <!-- FileUpload 控制項 -->
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                    </td>
                </tr>
            </table>
            <div style="width: 350px" class="center item">
                <asp:Button ID="Button00" runat="server" Text="查詢" CssClass="btn" OnClick="Button00_Click" />
            </div>
            <div style="width: 350px" class="center item">
                <asp:Button ID="Savebtn" runat="server" Text="儲存修改" CssClass="btn" Style="display: none" Enabled="false" OnClick="Savebtn_Click" />
            </div>
        </div>
        <div style="width: 350px" class="center item">
            <!-- 用來顯示圖片的地方 -->
            <asp:Image ID="Image1" runat="server" Width="350px" Height="300px" Style="display: none;" />
            <asp:Label ID="Imagemsg" runat="server" Font-Size="Large" Font-Names="Microsoft JhengHei" ForeColor="Red" Text="" /><br />
        </div>
    </form>
    <script>
        window.onload = function () {
            var searchbtn = document.getElementById('<%= Searchbtn.ClientID %>');
            var dateChooseInput = document.getElementById('divDate');


            if (searchbtn.style.display === 'none') {

                dateChooseInput.style.display = 'block';
            } else {

                dateChooseInput.style.display = 'none';
            }


            var fileUpload = document.getElementById('<%= FileUpload1.ClientID %>');
            fileUpload.setAttribute("accept", "image/*");
            fileUpload.setAttribute("capture", "environment"); // 使用後鏡頭
        };


    </script>
</body>

</html>
