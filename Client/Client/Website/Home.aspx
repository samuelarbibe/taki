<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div class="container bg-white p-5 rounded" style="width:500px;margin-top:200px;" Runat="Server">
            <h1 id="Username" class="pt-3" Runat="Server"></h1>
            <h3 id="Level" class="text-secondary pt-3" Runat="Server">
            </h3>
            <div class="progress mt-5"  Runat="Server">
                <div id="ProgressBar" class="progress-bar" role="progressbar" style="width:30%" aria-valuenow="30" aria-valuemin="0" aria-valuemax="100" Runat="Server">
                </div>
            </div>
            <div class="container">
                <asp:GridView ID="GridView1" class="table table-striped table-bordered mt-5" runat="server">

                </asp:GridView>
            </div>                
        </div>
</asp:Content>

