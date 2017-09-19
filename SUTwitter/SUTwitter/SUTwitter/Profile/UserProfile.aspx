<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="SUTwitter.Profile.UserProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="well"  style="margin-top: 20px">
        <div class="panel panel-primary">
             <div class="panel-heading" id="panelHeading" runat="server">
               Profile 
             </div>
             <div class="panel-body">
                 <div class ="row">
                      <div class="col-md-2">
                        <strong>Name:</strong>
                      </div>
                     <div class="col-md-2">
                         <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                      </div>
                 </div>
                 <br />
                 <div class="row">
                     <div class="col-md-2">
                        <strong>EMail:</strong>
                      </div>
                     <div class="col-md-2">
                         <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
                      </div>
                 </div>

                  <div class="row">
                     <div class="col-md-2">
                        <strong>Status:</strong>
                      </div>
                     <div class="col-md-2">
                         <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                      </div>
                 </div>
             </div>

        </div>
    </div>
</asp:Content>
