<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListOfFollowers.aspx.cs" Inherits="SUTwitter.Users.ListOfFollowers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="well" style="margin-top: 20px">
        <div class="panel panel-primary">
            <div class="panel-heading" id="panelHeading" runat="server">
                List of people Following you
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-2">
                        <strong>List of people</strong>
                    </div>
                </div>
                <br />
                <div class="row" id="divGridFindFollowers">
                    <div class="col-md-10">
                        <asp:GridView ID="GridViewFollowers" runat="server" CssClass="table table-bordered">
                        </asp:GridView>
                    </div>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
