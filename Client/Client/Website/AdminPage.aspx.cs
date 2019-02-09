using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TakiService;

public partial class AdminPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ServiceClient Service = new ServiceClient();
        UserList ul = Service.GetAllUsers();

        if (MasterPage.currentUser != null && MasterPage.currentUser.Admin == true)
        {
            this.GridView1.DataSource = ul;
            this.GridView1.DataBind();
        }
        else
        {
            Response.Redirect("Home.aspx");
        }

        this.GridView1.DataSource = Service.GetAllUsers();
        this.GridView1.DataBind();
    }
}