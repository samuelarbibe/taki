using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TakiService;

public partial class AdminPage : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ServiceClient service = new ServiceClient();
        UserList ul = service.GetAllUsers();

        if (MasterPage.CurrentUser != null && MasterPage.CurrentUser.Admin == true)
        {
            GridView1.DataSource = ul;
            GridView1.DataBind();
        }
        else
        {
            Response.Redirect("Home.aspx");
        }

        GridView1.DataSource = service.GetAllUsers();
        GridView1.DataBind();
    }
}