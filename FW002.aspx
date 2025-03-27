<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FW002.aspx.cs" Inherits="FWfood.FW002" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title></title>
    <style type="text/css">
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
            padding: 5px 0px;
        }


        .tx1 {
            text-align: left;
            flex-grow: 2;
        }

        .btn:hover {
            background-color: #e06b18;
            transform: scale(1.05);
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
        window.onload = function () {
            var fileUpload = document.getElementById('<%= FileUpload1.ClientID %>');
            fileUpload.setAttribute("accept", "image/*");
            fileUpload.setAttribute("capture", "environment"); // 使用後鏡頭
        };

        function previewImage() {
            var file = document.getElementById('<%= FileUpload1.ClientID %>').files[0];
            var reader = new FileReader();

            reader.onloadend = function () {
                var img = document.getElementById("Image1");
                if (img) {
                    img.src = reader.result;
                    img.style.display = "block"; // 確保圖片顯示
                }
            };

            if (file) {
                reader.readAsDataURL(file);
            }
        }


    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="iUse00" runat="server" Value="" />
        <asp:HiddenField ID="iUse01" runat="server" Value="" />
        <asp:HiddenField ID="iUse02" runat="server" Value="" />
        <asp:HiddenField ID="iUse03" runat="server" Value="" />
        <asp:HiddenField ID="iUse04" runat="server" Value="" />
        <asp:HiddenField ID="iUse05" runat="server" Value="" />
        <asp:HiddenField ID="iUse06" runat="server" Value="" />
        <asp:HiddenField ID="refToken" runat="server" Value="" />

        <div style="width: 350px" class="center item">
            <h1>廚餘日報-新增</h1>
        </div>
        <div style="width: 350px" class="item center">
            <asp:Label ID="iErr00" runat="server" Font-Size="Large" Font-Names="Microsoft JhengHei" ForeColor="Red" Text="" /><br />
            <asp:Label ID="iMsg00" runat="server" Font-Size="Large" Font-Names="Microsoft JhengHei" ForeColor="#006600" Text="" />
            <asp:Label ID="Now2" runat="server" Text="Now" Font-Names="新細明體" Font-Size="Smaller" Font-Bold="True" Font-Italic="False" ForeColor="Blue" />
        </div>
        <div style="width: 350px" class="item center">
            <asp:Label ID="Now" runat="server" Text="Now" Font-Names="新細明體" Font-Size="Smaller" Font-Bold="True" Font-Italic="False" ForeColor="Blue" />
        </div>
        <%--        <div style="width: 350px" class="item center">
            <asp:Label ID="welcome" runat="server" Font-Size="Large" Font-Names="Microsoft JhengHei" ForeColor="#006600" Text="" />
        </div>--%>
        <div style="width: 350px" class="center item">
            <table style="border: thick double #808080; width: 100%; font-family: 微軟正黑體;" class="table">


                <tr>
                    <td class="auto-style1">
                        <div class="flex-container">

                            <asp:Label ID="Label01" runat="server" Font-Size="Medium" ForeColor="Black" Text="預定餐數" class="tx1" />
                            <asp:TextBox ID="TextBox01" runat="server" Width="50px" Height="19px" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <div class="flex-container">
                            <asp:Label ID="Label02" runat="server" Font-Size="Medium" ForeColor="Black" Text="總訂餐人數" class="tx1" Style="border: 0 0 0" />
                            <asp:TextBox ID="TextBox02" runat="server" Width="50px" Text="" Height="19px" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <div class="flex-container">
                            <asp:Label ID="Label03" runat="server" Font-Size="Medium" ForeColor="Black" Text="實際用餐人數" class="tx1" Style="border: 0 0 0" />
                            <asp:TextBox ID="TextBox03" runat="server" Width="50px" Text="" Height="19px" />
                        </div>
                    </td>

                </tr>
                <tr>
                    <td class="auto-style1">
                        <div class="flex-container">
                            <asp:Label ID="Label04" runat="server" Font-Size="Medium" ForeColor="Black" Text="已用餐人數" class="tx1" Style="border: 0 0 0" />
                            <asp:TextBox ID="TextBox04" runat="server" Width="50px" Text="" Height="19px" />
                        </div>
                    </td>

                </tr>
                <tr>
                    <td class="auto-style1">
                        <div class="flex-container">
                            <asp:Label ID="Label05" runat="server" Font-Size="Medium" ForeColor="Black" Text="未用餐人數" class="tx1" Style="border: 0 0 0" />
                            <asp:TextBox ID="TextBox05" runat="server" Width="50px" Text="" Height="19px" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <div class="flex-container">
                            <asp:Label ID="Label06" runat="server" Font-Size="Medium" ForeColor="Black" Text="未訂用餐人數" class="tx1" Style="border: 0 0 0" />
                            <asp:TextBox ID="TextBox06" runat="server" Width="50px" Text="" Height="19px" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <div class="flex-container">
                            <asp:Label ID="Label07" runat="server" Font-Size="Medium" ForeColor="Black" Text="實際用餐盤數" class="tx1" Style="border: 0 0 0" />
                            <asp:TextBox ID="TextBox07" runat="server" Width="50px" Text="" Height="19px" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <div class="flex-container">
                            <asp:Label ID="Label08" runat="server" Font-Size="Medium" ForeColor="Black" Text="實際便當人數" class="tx1" Style="border: 0 0 0" />
                            <asp:TextBox ID="TextBox08" runat="server" Width="50px" Text="" Height="19px" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <div class="flex-container">
                            <asp:Label ID="Label09" runat="server" Font-Size="Medium" ForeColor="Black" Text="未煮食材(KG)" class="tx1" Style="border: 0 0 0" />
                            <asp:TextBox ID="TextBox09" runat="server" Width="50px" Text="" Height="19px" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <div class="flex-container">
                            <asp:Label ID="Label10" runat="server" Font-Size="Medium" ForeColor="Black" Text="已煮未用食材(KG)" class="tx1" Style="border: 0 0 0" />
                            <asp:TextBox ID="TextBox10" runat="server" Width="50px" Text="" Height="19px" />
                        </div>

                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <div class="flex-container">
                            <asp:Label ID="Label11" runat="server" Font-Size="Medium" ForeColor="Black" Text="員工丟掉廚餘重量(KG)" class="tx1" Style="border: 0 0 0" />
                            <asp:TextBox ID="TextBox11" runat="server" Width="50px" Text="" Height="19px" />
                        </div>
                    </td>
                </tr>
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
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="Label12" runat="server" Font-Size="Medium" ForeColor="Black" Text="拍照上傳:" class="tx1" Style="border: 0 0 0" />
                        <!-- FileUpload 控制項 -->
                        <asp:FileUpload ID="FileUpload1" runat="server" onchange="previewImage()" />
                    </td>
                </tr>
                <!-- 測試用 -->
                <!-- <tr>
                    <td>
                        <div class="flex-container">
                            <asp:Label ID="Label13" runat="server" Font-Size="Medium" ForeColor="Black" Text="sessionToken" class="tx1" />
                            <asp:TextBox ID="TextBox12" runat="server" Width="50px" Height="19px" Enabled="False" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="flex-container">
                            <asp:Label ID="Label14" runat="server" Font-Size="Medium" ForeColor="Black" Text="refHidden" class="tx1" />
                            <asp:TextBox ID="TextBox13" runat="server" Width="50px" Height="19px" Enabled="False" />
                        </div>
                    </td>
                </tr> -->
            </table>
        </div>
        <div style="width: 350px" class="center item">
            <asp:Button ID="Button01" runat="server" Text="儲存"
                UseSubmitBehavior="false" CssClass="btn" OnClick="Button01_Click" />
        </div>
        <div style="width: 350px" class="center item">
            <!-- 用來顯示圖片的地方 -->
            <asp:Image ID="Image1" runat="server" src="" Width="350px" Height="300px" Style="display: none;" />
        </div>

    </form>
</body>
</html>

