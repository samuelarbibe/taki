using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Results : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (MasterPage.currentUser == null)
        {
            Response.Redirect("Login.aspx");
        }

        if (MasterPage.searchResults != null)
        {
            ErrorDiv.Visible = false;
            ResultDiv.Visible = true;
            SuccessDiv.Visible = true;

            Username.InnerText = MasterPage.searchResults.Username;
            Level.InnerText = "Level "+MasterPage.searchResults.Level.ToString();
        }
        else
        {
            ErrorDiv.Visible = true;
            ResultDiv.Visible = false;
            SuccessDiv.Visible = false;
        }
    }
}