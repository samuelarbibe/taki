using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TakiService;

public partial class MasterPage : System.Web.UI.MasterPage
{
    public static User currentUser;
    public static User searchResults;

    protected void Page_Load(object sender, EventArgs e)
    {
        if(currentUser == null)
        {
            loggedTools.Visible = false;
        }
        else
        {
            loggedTools.Visible = true;
            if (currentUser.Admin)
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
        ServiceClient Service = new ServiceClient();
        Service.Logout(currentUser.Id);
        currentUser = null;
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
            ServiceClient Service = new ServiceClient();
            searchResults = Service.GetUserByUsername(searchBar.Value);
            Response.Redirect("Results.aspx");
        }
    }
}
