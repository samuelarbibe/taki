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



    protected void Page_Load(object sender, EventArgs e)
    {
        if(currentUser == null)
        {
            logout.Visible = false;
        }
        else
        {
            logout.Visible = true;
        }
    }

    protected void Logout_Clicked(object sender, EventArgs e)
    {
        ServiceClient Service = new ServiceClient();
        Service.Logout(currentUser.Id);
        currentUser = null;
        Response.Redirect("Login.aspx");
    }
}
