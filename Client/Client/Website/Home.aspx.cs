using System;
using System.Collections;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using TakiService;

public partial class Home : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        User _u = null;

        if (MasterPage.CurrentUser == null)
        {
            Response.Redirect("Login.aspx");
        }
        else
        {
            _u = MasterPage.CurrentUser;
        }

        ServiceClient service = new ServiceClient();

        Username.InnerText = _u.Username;
        Level.InnerText = _u.Level.ToString();
        //ProgressBar.Style.Clear();
        //ProgressBar.Style.Add("Width", ((ul.Score % 1000)).ToString());

        GameList gl = service.GetAllUserGames(_u.Id);

        GridView1.DataSource = gl;
        GridView1.DataBind();
    }
}
