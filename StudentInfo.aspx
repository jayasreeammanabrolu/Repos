<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StudentInfo.aspx.cs" Inherits="StudentInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="Name"></asp:Label>
&nbsp;&nbsp;
        <asp:TextBox ID="NameTxt" runat="server"></asp:TextBox>
        <br />
        <br />
        Sub1&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="Sub1Txt" runat="server"></asp:TextBox>
        <br />
        <br />
        Sub2&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="Sub2Txt" runat="server"></asp:TextBox>
        <br />
        <br />
        Sub3&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="Sub3Txt" runat="server"></asp:TextBox>
        <br />
        <br />
        Total&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TotalTxt" runat="server" style="margin-left: 0px" Width="116px"></asp:TextBox>
        <br />
        <br />
        Grade&nbsp;&nbsp; <asp:TextBox ID="GradeTxt" runat="server"></asp:TextBox>
        <br />
        <br />
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit" />
        <br />
    
    </div>
    </form>
</body>
</html>
