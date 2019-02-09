<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AdminPage.aspx.cs" Inherits="AdminPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div class="container" style="margin-top:200px;" Runat="Server">
            <asp:GridView ID="GridView1" class="table table-striped table-bordered" runat="server">

            </asp:GridView>
        </div>
</asp:Content>
