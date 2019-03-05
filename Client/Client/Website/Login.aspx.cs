using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TakiService;

public partial class Login : Page
{
    ServiceClient _service;

    protected void Page_Load(object sender, EventArgs e)
    {
        _service = new ServiceClient();
        if (_service == null)
        {
            ErrorDiv.Visible = true;
            noUserError.InnerText = "Not Connected to server.";
        }
    }

    protected void Button_Click(object sender, EventArgs e)
    {
        string usernameValue = Username.Value;
        string passwordValue = Password.Value;

        if (Username.Value.Length > 0 && Password.Value.Length > 0)
        {
            User currentUserCheck = _service.Login(usernameValue, passwordValue);

            if (currentUserCheck != null)
            {
                ErrorDiv.Visible = false;
                noUserError.InnerText = "";
                MasterPage.CurrentUser = currentUserCheck;
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

    protected void Fill_Click(object sender, EventArgs e)
    {
        Username.Value = "Samuelov1";
        Password.Value = "123";

        Button_Click(null, null);
    }
}