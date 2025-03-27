<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FW000.aspx.cs" Inherits="FWfood.FW000" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title></title>
    <style type="text/css">
        div {
            text-align: center;
            margin: 10px auto;
        }

        .table {
            border: 3px double #FF6600;
            width: 100%;
            font-family: '微軟正黑體';
            background-color: #f9f9f9;
            border-radius: 10px;
            padding: 10px;
        }

        .center {
            width: 50%;
            margin: 5px auto;
            text-align: center;
        }

        .btn {
            font-size: large;
            font-weight: bold;
            font-family: '微軟正黑體';
            border: 0px #fff none;
            color: #fff;
            background-color: #FF6600;
            Width: 130px;
            height: 30px;
            border-radius: 12px;
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
        .auto-style1 {
            height: 47px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="iUse00" runat="server" Value="" />
        <asp:HiddenField ID="iUse01" runat="server" Value="" />
        <asp:HiddenField ID="iUse02" runat="server" Value="" />
        <asp:HiddenField ID="iUse03" runat="server" Value="" />
        <asp:HiddenField ID="iUse04" runat="server" Value="" />
        <div style="width: 350px" class="center item">
            <h1>廚餘報表-登入</h1>
        </div>
        <div style="width: 350px" class="center item">
            <table class="table">
                <tr>
                    <td class="auto-style1">
                        <div>
                            <asp:Label ID="Labe03" runat="server" Font-Size="Medium" ForeColor="Black" Text="選擇廠區報表" style="margin-left: -60px;"/>
                            <asp:DropDownList runat="server" ID="Dbs" Width="110px" Height="30px" >
                            </asp:DropDownList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <div>
                            <asp:Label ID="Label01" runat="server" Font-Size="Medium" ForeColor="Black" Text="帳號" />
                            <asp:TextBox ID="TextBox01" runat="server" Width="100px" Text="" Height="30px" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <div>
                            <asp:Label ID="Labe02" runat="server" Font-Size="Medium" ForeColor="Black" Text="密碼" />
                            <asp:TextBox ID="TextBox02" runat="server" Width="100px" Text="" Type="password" Height="30px" />
                        </div>
                    </td>
                </tr>
            </table>
            <div style="width: 350px" class="center item">
                <asp:Button ID="Button00" runat="server" Text="登入" CssClass="btn" OnClick="Button00_Click" />
            </div>
            <div style="width: 350px" class="center item">
                <asp:Label ID="iErr00" runat="server" Font-Size="Large" Font-Names="Microsoft JhengHei" ForeColor="Red" Text="" />
            </div>

        </div>
    </form>
</body>
</html>

