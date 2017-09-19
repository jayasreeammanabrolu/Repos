<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HotelRoom.aspx.cs" Inherits="HotelRoom" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 32%;
            height: 163px;
        }
        .auto-style2 {
            width: 109px;
        }
        .auto-style3 {
            width: 109px;
            height: 31px;
        }
        .auto-style4 {
            height: 31px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table class="auto-style1">
            <tr>
                <td class="auto-style2">Name</td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    <br />
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Room Type</td>
                <td>
                    <asp:RadioButton ID="RadioButton1" groupname ="g1" runat ="server" Text="Delux" ToolTip="rent:1000" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="RadioButton2" groupname ="g1" runat="server" Text="Ordinary" ToolTip="rent:500" />
                    <br />
                </td>
            </tr>
            <tr>
                <td class="auto-style3">Amenities</td>
                <td class="auto-style4">
                    <asp:CheckBox ID="CheckBox1" runat="server" Text="AC"  ToolTip="300"/>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="CheckBox2" runat="server" Text="Computer" ToolTip="200"/>
&nbsp;&nbsp;
                    <br />
&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Register" OnClick="Button1_Click" />
                    <br />
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label1" runat="server" Text="Total"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TotalTxt" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
