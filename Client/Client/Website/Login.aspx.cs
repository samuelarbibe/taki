using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TakiService;

public partial class Login : System.Web.UI.Page
{
    ServiceClient Service;

    protected void Page_Load(object sender, EventArgs e)
    {
        Service = new ServiceClient();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string usernameValue = Username.Text;
        string passwordValue = Password.Text;

        List<Control> textBoxes = new List<Control>();
        textBoxes.Add(Username);
        textBoxes.Add(Password);

        if (Username.Text.Length > 0 && Password.Text.Length > 0)
        {
            User currentUserCheck = Service.Login(usernameValue, passwordValue);

            if (currentUserCheck != null)
            {
                noUserError.Text = "";
                Response.Redirect("Home.aspx");
            }
            else
            {
                noUserError.Text = "Your Password or Username are incorrect. Try again";
            }
        }
        else
        {
            noUserError.Text = "Please fill up all the fields!";
        }
    }
}