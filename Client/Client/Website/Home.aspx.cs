using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TakiService;

public partial class Home : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(MasterPage.CurrentUser == null)
        {
            Response.Redirect("Login.aspx");
        }

        ServiceClient service = new ServiceClient();
        User ul = service.GetUserByUsername(MasterPage.CurrentUser.Username);

        Username.InnerText = ul.Username;
        Level.InnerText = ul.Level.ToString();
        //ProgressBar.Style.Clear();
        //ProgressBar.Style.Add("Width", ((ul.Score % 1000)).ToString());

        GameList gl = service.GetAllUserGames(ul.Id);

        GridView1.DataSource = gl;
        GridView1.DataBind();
    }


}