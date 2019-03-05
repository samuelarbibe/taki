using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Results : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (MasterPage.CurrentUser == null)
        {
            Response.Redirect("Login.aspx");
        }

        if (MasterPage.SearchResults != null)
        {
            ErrorDiv.Visible = false;
            ResultDiv.Visible = true;
            SuccessDiv.Visible = true;

            Username.InnerText = MasterPage.SearchResults.Username;
            Level.InnerText = "Level "+MasterPage.SearchResults.Level.ToString();
        }
        else
        {
            ErrorDiv.Visible = true;
            ResultDiv.Visible = false;
            SuccessDiv.Visible = false;
        }
    }
}