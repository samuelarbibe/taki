using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TakiService;

public partial class Home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(MasterPage.currentUser == null)
        {
            Response.Redirect("Login.aspx");
        }

        ServiceClient Service = new ServiceClient();
        User ul = Service.GetUserByUsername(MasterPage.currentUser.Username);

        Username.InnerText = ul.Username;
        Level.InnerText = ul.Level.ToString();
        //ProgressBar.Style.Clear();
        //ProgressBar.Style.Add("Width", ((ul.Score % 1000)).ToString());

        GameList gl = Service.GetAllUserGames(ul.Id);

        this.GridView1.DataSource = gl;
        this.GridView1.DataBind();
    }


}