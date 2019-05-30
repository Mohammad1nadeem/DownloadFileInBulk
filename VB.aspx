<%@ Page Language="VB" AutoEventWireup="false" CodeFile="VB.aspx.vb" Inherits="VB" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
    </form>
</body>
</html>
