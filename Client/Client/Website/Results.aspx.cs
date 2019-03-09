using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TakiService;

public partial class Results : Page
{
    private ServiceClient service;
    User u1;
    User u2;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (MasterPage.CurrentUser == null)
        {
            Response.Redirect("Login.aspx");
        }
        else
        {
            if (MasterPage.SearchResults == null)
            {
                ErrorDiv.Visible = true;
                ResultDiv.Visible = false;
                SuccessDiv.Visible = false;
            }
            else
            {
                ErrorDiv.Visible = false;
                ResultDiv.Visible = true;
                SuccessDiv.Visible = true;

                Username.InnerText = MasterPage.SearchResults.Username;
                Level.InnerText = "Level " + MasterPage.SearchResults.Level.ToString();


                service = new ServiceClient();

                u1 = MasterPage.SearchResults;
                u2 = MasterPage.CurrentUser;

                // check if are friends
                if (service.AreFriends(u1.Id, u2.Id))
                {
                    AddFriendButton.Visible = false;
                    IsFriend.Visible = true;
                }
            }
        }
    }

    protected void SendFriendRequestButton_Clicked(Object sender, EventArgs e)
    {
        service.MakeFriends(u1, u2);
        AddFriendButton.Visible = false;
    }

    protected void ViewMutualGamesButton_Clicked(Object sender, EventArgs e)
    {
        GameList gl = service.GetMutualGames(u1.Id, u2.Id);

        if (gl.Count > 0)
        {
            GridView1.DataSource = gl;
            GridView1.DataBind();

            MutualGamesDiv.Visible = true;
        }
        else
        {
            NoGamesDiv.Visible = true;
        }
    }
}