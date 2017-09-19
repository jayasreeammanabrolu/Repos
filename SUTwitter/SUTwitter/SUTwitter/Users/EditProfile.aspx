<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditProfile.aspx.cs" Inherits="SUTwitter.Users.EditProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
     <div class="panel panel-primary">
         <div class="panel-heading" id="panelHeading" runat="server">
                Edit Your Profile
         </div>
         <div class="panel-body">
             <div class="row">
                 <div class="col-md-1">Name:</div>
                 <div class="col-md-4">
                     <asp:TextBox ID="txtBoxName" CssClass="form-control" runat="server"></asp:TextBox></div>
             </div>
             <br />
             <div class="row">
                 <div class="col-md-1">Status:</div>
                 <div class="col-md-4">
                     <asp:TextBox ID="txtBoxStatus" CssClass="form-control" runat="server"></asp:TextBox></div>
             </div>
             <div class="row">
                 <div class="col-md-1">
                     <asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" Text="Save" OnClick="btnSave_Click" /></div>
                 
             </div>
             
         </div>
     </div>

</asp:Content>
