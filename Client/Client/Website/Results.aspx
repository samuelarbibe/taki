<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Results.aspx.cs" Inherits="Results" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container bg-white p-5 rounded" style="width:500px;margin-top:200px;" Runat="Server">
        <div visible="false" ID="ErrorDiv" class="alert alert-danger" role="alert" runat="server">
            <span ID="noUserError" runat="server">No Results.</span>
        </div>
        <div visible="false" ID="SuccessDiv" class="alert alert-success" role="alert" runat="server">
            <span ID="Span1" runat="server">1 Result Found:</span>
        </div>     
        <%--<div visible="false" id="ResultDiv" class="container" runat="server">
            <h1 id="Username" class="pt-3" Runat="Server"></h1>
            <h3 id="Level" class="text-secondary pt-3" Runat="Server">
            </h3>
            <div class="progress mt-5"  Runat="Server">
                <div id="ProgressBar" class="progress-bar" role="progressbar" style="width:30%" aria-valuenow="30" aria-valuemin="0" aria-valuemax="100" Runat="Server">
                </div>
            </div> 
        </div>--%>
        <div class="card" visible="false" id="ResultDiv"  runat="server">
          <div class="card-body"  runat="server">
            <h2 class="card-title" id="Username" runat="server"></h2>
            <h5 class="card-subtitle mb-2 text-muted" id="Level" runat="server"></h5>
            <div class="progress mb-3"  Runat="Server">
                <div id="ProgressBar" class="progress-bar" role="progressbar" style="width:30%" aria-valuenow="30" aria-valuemin="0" aria-valuemax="100" Runat="Server">
                </div>
            </div> 
              <button class="btn btn-primary">Send Friend Request</button>
              <button class="btn btn-outline-primary">View Mutual Games</button>
          </div>
        </div>
    </div>
</asp:Content>

