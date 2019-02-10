<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="container bg-white p-5 rounded" style="width:700px;margin-top:200px;" runat="server">
            <div class="form-group pb-4">
                <h2>Create a new Account</h2>
            </div>
          <div class="form-row" runat="server">
            <div class="col-md-6 mb-3" runat="server">
              <label for="validationDefault01">First name</label>
              <input type="text" class="form-control" id="validationDefault01" placeholder="First name" required runat="server">
            </div>
            <div class="col-md-6 mb-3" runat="server">
              <label for="validationDefault02">Last name</label>
              <input type="text" class="form-control" id="validationDefault02" placeholder="Last name" required runat="server">
            </div>
          </div>
          <div class="form-row" runat="server">
            <div class="col-md-6 mb-3" runat="server">
              <label for="validationDefault03">Username</label>
              <input type="text" class="form-control" id="validationDefault03" placeholder="Username" required runat="server">
            </div>
            <div class="col-md-6 mb-3" runat="server">
              <label for="validationDefault04">Password</label>
              <input type="Password" class="form-control" id="validationDefault04" placeholder="Password" required runat="server">
            </div>
          </div>
          <div class="form-row" runat="server">
            <div class="col-md-6 mb-3" runat="server">
              <label for="validationDefault05">Confirm Password</label>
              <input type="Password" class="form-control" id="validationDefault05" placeholder="Confirm Password" required runat="server">
            </div>
            <div class="col-md-6 mb-3" runat="server">
              <label for="submit"> </label>
              <asp:button ID="submit" onclick="Register_Button_Click" class="form-control btn btn-primary mt-2" runat="server" Text="Register" />
            </div>       
          </div>
            <div class="form-group" runat="server">
                <div visible="false" ID="ErrorDiv" class="alert alert-danger" role="alert" runat="server">
                    <span ID="noUserError" runat="server"></span>
                </div>
            </div>
          <button type="reset" class="btn btn-outline-secondary">Reset</button>
          <a href="Login.aspx" class="text-muted float-sm-right pt-2" ID="Text" runat="server">Back to Login</a>
        </div>
</asp:Content>

