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
        <div class="card" visible="false" id="ResultDiv"  runat="server">
          <div class="card-body"  runat="server">
            <h2 class="card-title" id="Username" runat="server"></h2>
            <h5 class="card-subtitle mb-2 text-muted" id="Level" runat="server"></h5>
            <div class="progress mb-3"  Runat="Server">
                <div id="ProgressBar" class="progress-bar" role="progressbar" style="width:30%" aria-valuenow="30" aria-valuemin="0" aria-valuemax="100" Runat="Server">
                </div>
            </div>
              <div ID="IsFriend" Visible="false" runat="server">
                <span>Friend</span>
                  <img src="open-iconic-master/svg/check.svg" style="fill: #46A346; Height: 13px; Width: 13px; padding-left: 5px"/>
              </div>
              <asp:button ID="AddFriendButton" class="btn btn-primary" OnClick="SendFriendRequestButton_Clicked" runat="server" Text="Send Friend Request" />
              <asp:button ID="ViewMutualGames" OnClick="ViewMutualGamesButton_Clicked" class="btn btn-outline-primary" runat="server" Text ="View Mutual Games"/>
          </div>
        </div>
        <div visible="false" ID="NoGamesDiv" class="alert alert-warning mt-3" role="alert" runat="server">
            <span ID="Span2" runat="server">You Have No Mutual Games.</span>
        </div> 
        <div class="card mt-5" visible="false" id="MutualGamesDiv"  runat="server">
            <div class="card-body"  runat="server">
                <h2 class="card-title" id="H1" runat="server">Mutual Games</h2>
                <asp:GridView ID="GridView1" class="table table-striped table-bordered mt-5" runat="server">

                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>

