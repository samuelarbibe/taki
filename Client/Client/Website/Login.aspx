<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container bg-white p-5 rounded" style="width:500px;margin-top:200px;" Runat="Server">
                <div class="form-group pb-4">
                    <h2>Login to The Game</h2>
                </div>
                <div class="form-group">
                <label for="Username">Username</label>
                    <input class="form-control" id="Username" placeholder="Username" runat="server"/>
                </div>
                <div class="form-group">
                <label for="Password">Password</label>
                    <input type="password" class="form-control" id="Password" placeholder="Password" runat="server"/>
                </div>
                <div class="form-group">
                    <div visible="false" ID="ErrorDiv" class="alert alert-danger" role="alert" runat="server">
                        <span ID="noUserError" runat="server"></span>
                    </div>
                </div>
        <div class="container p-0">            
            <asp:button Text="Sumbit" OnClick="Button_Click" class="btn btn-primary mt-3" runat="server"></asp:button>
            <asp:button Text="Fill" OnClick="Fill_Click" class="btn btn-outline-secondary mt-3" runat="server"></asp:button>
            <a href="Register.aspx" class="text-muted float-sm-right pt-4" ID="Text" runat="server">Register</a>
        </div>           
    </div>
           
</asp:Content>

