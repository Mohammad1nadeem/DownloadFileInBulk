<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CS.aspx.cs" Inherits="CS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        table
        {
            border: 1px solid #ccc;
            border-collapse: collapse;
        }
        table th
        {
            background-color: #F7F7F7;
            color: #333;
            font-weight: bold;
        }
        table th, table td
        {
            padding: 5px;
            border: 1px solid #ccc;
        }
        table img
        {
            height: 150px;
            width: 150px;
            cursor: pointer;
        }
        #dialog img
        {
            height: 550px;
            width: 575px;
            cursor: pointer;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" EmptyDataText="No files available">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="chkSelect" runat="server" />
                    <asp:Label ID="lblFilePath" runat="server" Text='<%# Eval("Value") %>' Visible="false"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Text" HeaderText="File Name" />
        </Columns>
    </asp:GridView>
    <br />
    <asp:Button ID="btnDownload" runat="server" Text="Download" OnClick="DownloadFiles" />
        <br />
        <div>
    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" EmptyDataText="No files available">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Image Id" />                       
            <asp:BoundField DataField="Name" HeaderText="Name" />
             <asp:TemplateField HeaderText="Image">
                <ItemTemplate>
                    <asp:Image ImageUrl='<%# ConvertImage(Eval("Data"))%>' ID="ImageData" Width="50Px"
                        Height="50px" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Select">
                <ItemTemplate>
                    <asp:CheckBox ID="chkSelect" runat="server" />
                  <%--  <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name")%>' Visible="true"></asp:Label>--%>
                   <asp:HiddenField ID="hdnData" runat="server" Value='<%# Eval("Data")%>' />
                    <asp:Label ID="lblFilePath" runat="server" Text='<%# FilePath(Eval("Data"))%>' Visible="false" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <br />
    <asp:Button ID="btnOnDownload" runat="server" Text="Download" OnClick="OnDownload" />
</div>
    </form>
</body>
</html>
