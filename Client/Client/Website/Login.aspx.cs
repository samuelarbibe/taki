using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        if (Service == null)
        {
            ErrorDiv.Visible = true;
            noUserError.InnerText = "Not Connected to server.";
        }
    }

    protected void Button_Click(object sender, EventArgs e)
    {
        string usernameValue = Username.Value;
        string passwordValue = Password.Value;

        List<Control> textBoxes = new List<Control>();
        textBoxes.Add(Username);
        textBoxes.Add(Password);

        if (Username.Value.Length > 0 && Password.Value.Length > 0)
        {
            User currentUserCheck = Service.Login(usernameValue, passwordValue);

            if (currentUserCheck != null)
            {
                ErrorDiv.Visible = false;
                noUserError.InnerText = "";
                MasterPage.currentUser = currentUserCheck;
                Response.Redirect("Home.aspx");
            }
            else
            {
                ErrorDiv.Visible = true;      
                noUserError.InnerText = "Your Password or Username are incorrect. Try again";
            }
        }
        else
        {
            ErrorDiv.Visible = true;
            noUserError.InnerText = "Please fill up all the fields!";
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Console.WriteLine("Button Clicked");
    }
}