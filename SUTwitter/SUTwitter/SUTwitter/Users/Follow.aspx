<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Follow.aspx.cs" Inherits="SUTwitter.Users.Follow" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <div class="row">
        <div class="col-md-2">
            User EMail:
        </div>
        <div class="col-md-3">
            <asp:TextBox ID="TxtBoxEMail" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-2">
            <asp:Button ID="BtnAddFriend" CssClass="btn btn-success" runat="server" Text="Follow" OnClick="BtnAddFriend_Click" />
        </div>
    </div>
    <div class="row">
        <div class ="col-md-5">
            <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
        </div>
    </div>
</asp:Content>
