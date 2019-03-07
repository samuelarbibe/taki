using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TakiService;

public partial class MasterPage : System.Web.UI.MasterPage
{
    public static User CurrentUser;
    public static User SearchResults;

    protected void Page_Load(object sender, EventArgs e)
    {
        if(CurrentUser == null)
        {
            loggedTools.Visible = false;
        }
        else
        {
            loggedTools.Visible = true;
            if (CurrentUser.Admin)
            {
                admin.Visible = true;
            }
            else
            {
                admin.Visible = false;
            }
        }
    }

    protected void Logout_Clicked(object sender, EventArgs e)
    {
        ServiceClient service = new ServiceClient();
        service.Logout(CurrentUser.Id);
        CurrentUser = null;
        Response.Redirect("Login.aspx");
    }

    protected void Admin_Clicked(object sender, EventArgs e)
    {
        Response.Redirect("AdminPage.aspx");
    }

    protected void Search_Clicked(object sender, EventArgs e)
    {
        if(searchBar.Value != null)
        {
            ServiceClient service = new ServiceClient();
            SearchResults = service.GetUserByUsername(searchBar.Value.ToString());
            Response.Redirect("Results.aspx");
        }
    }
}
