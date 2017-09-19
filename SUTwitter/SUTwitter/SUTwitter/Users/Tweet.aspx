<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tweet.aspx.cs" Inherits="SUTwitter.Users.Tweet" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <div class="row">
        <div class="col-md-10">
            <asp:TextBox ID="TxtBoxTweet" CssClass="form-control"  runat="server" MaxLength="140" TextMode="MultiLine"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <asp:Button ID="BtnTweet" CssClass="btn btn-success" runat="server" Text="Tweet" OnClick="BtnTweet_Click" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-5">
            <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
        </div>
    </div>
</asp:Content>
