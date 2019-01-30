<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div class="grid-container">
          <div>
              <P class="head">Login to The Game</P>
          </div>
          <div>
              <input ID="Username" placeholder="Username" runat="server" />
          </div>  
          <div>
              <input ID="Password" placeholder="Password" runat="server" TextMode="Password" >
          </div>
          <div>
              <Button ID="Button1" runat="server" OnClick="Button1_Click" > Login </Button>
              <span ID="noUserError" runat="server" Text="Label" />
          </div>
        </div>
</asp:Content>

