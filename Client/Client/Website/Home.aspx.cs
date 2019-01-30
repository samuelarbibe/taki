using System;
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
        ServiceClient Service = new ServiceClient();

        this.GridView1.DataSource = Service.GetAllUsers();
        this.GridView1.DataBind();
    }


}