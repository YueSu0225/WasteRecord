<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FWreport.aspx.cs" Inherits="FWfood.FWreport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        body {
            width: 1000px;
            height: 800px;
        }

        table {
            width: 100%;
            border-collapse: collapse;
        }

        th, td {
            border: 1px solid black;
            padding: 8px;
            text-align: center;
        }

        .label {
            font-family: Arial;
        }

        .label-container {
            text-align: center;
            margin-bottom: 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:DropDownList ID="DbsList" runat="server">
            </asp:DropDownList>
            <asp:DropDownList ID="YearList" runat="server">
            </asp:DropDownList>
            <asp:DropDownList ID="MonthList" runat="server">
            </asp:DropDownList>
            <asp:DropDownList ID="DinnerType" runat="server">
            </asp:DropDownList>
            <asp:Button ID="Monthbtn" runat="server" Text="查詢" OnClick="monthsel_click" />
        </div>
        <div class="label-container">
            <asp:Label ID="Label1" runat="server" Text="廠 年" Font-Size="XX-Large" class="label"></asp:Label>
            <asp:Label ID="Labelmon" runat="server" Text="月" Font-Size="XX-Large" class="label"></asp:Label>
        </div>
        <div class="label-container">
            <asp:Label ID="Label2" runat="server" Text="餐別" Font-Size="X-Large" class="label"></asp:Label>
        </div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table"
            CellPadding="4" ForeColor="#333333" GridLines="None">
            <Columns>
                <asp:BoundField DataField="Date" HeaderText="日期" SortExpression="Date" />
                <asp:BoundField DataField="ResMeals" HeaderText="預定人數" SortExpression="ResMeals" />
                <asp:BoundField DataField="TtlNum" HeaderText="總訂人數" SortExpression="TtlNum" />
                <asp:BoundField DataField="AtlNum" HeaderText="實際用餐人數" SortExpression="AtlNum" />
                <asp:BoundField DataField="AlreaNum" HeaderText="已用餐人數" SortExpression="AlreaNum" />
                <asp:BoundField DataField="NoEatNum" HeaderText="未用餐人數" SortExpression="NoEatNum" />
                <asp:BoundField DataField="NorderNum" HeaderText="未訂用餐人數" SortExpression="NorderNum" />
                <asp:BoundField DataField="Plate" HeaderText="實際用餐盤數" SortExpression="Plate" />
                <asp:BoundField DataField="Box" HeaderText="實際便當人數" SortExpression="Box" />
                <asp:BoundField DataField="WasteWeight" HeaderText="員工丟掉廚餘重量" SortExpression="WasteWeight" />
                <asp:BoundField DataField="NcookFood" HeaderText="未煮食材重量" SortExpression="NcookFood" />
                <asp:BoundField DataField="WasteFood" HeaderText="已煮未用食材重量" SortExpression="WasteFood" />
                <asp:BoundField DataField="WasteType" HeaderText="廚餘種類" SortExpression="WasteType" />
            </Columns>
        </asp:GridView>
        <br />
        <div>
            <asp:Button ID="Excelbtn" runat="server" Text="匯出為 Excel" OnClick="Excel" />
        </div>
    </form>


</body>
</html>
