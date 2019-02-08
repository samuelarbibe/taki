<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div class="container bg-white p-5 rounded" style="width:700px;margin-top:200px;" Runat="Server">
            <div class="form-group pb-4">
                    <h2>Create a new Account</h2>
                </div>
          <div class="form-row">
            <div class="col-md-6 mb-3">
              <label for="validationDefault01">First name</label>
              <input type="text" class="form-control" id="validationDefault01" placeholder="First name" required>
            </div>
            <div class="col-md-6 mb-3">
              <label for="validationDefault02">Last name</label>
              <input type="text" class="form-control" id="validationDefault02" placeholder="Last name" required>
            </div>
          </div>
          <div class="form-row">
            <div class="col-md-6 mb-3">
              <label for="validationDefault03">Username</label>
              <input type="text" class="form-control" id="validationDefault03" placeholder="Username" required>
            </div>
            <div class="col-md-6 mb-3">
              <label for="validationDefault04">Password</label>
              <input type="Password" class="form-control" id="validationDefault04" placeholder="Password" required>
            </div>
          </div>
          <div class="form-row">
            <div class="col-md-6 mb-3">
              <label for="validationDefault05">Confirm Password</label>
              <input type="Password" class="form-control" id="validationDefault05" placeholder="Confirm Password" required>
            </div>
            <div class="col-md-6 mb-3">
              <label for="submit"> </label>
              <button ID="submit" class="form-control btn btn-primary mt-2" type="submit">Submit form</button>
            </div>
              
          </div>
          <button type="reset" class="btn btn-outline-secondary">Reset</button>
          <a href="Login.aspx" class="text-muted float-sm-right pt-2" ID="Text" runat="server">Back to Login</a>
        </div>
</asp:Content>

