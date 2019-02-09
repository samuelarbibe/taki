using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TakiService;

public partial class Register : System.Web.UI.Page
{
    ServiceClient Service;

    protected void Page_Load(object sender, EventArgs e)
    {

        Service = new ServiceClient();

        ErrorDiv.Visible = false;

        if (Service == null)
        {
            ErrorDiv.Visible = true;
            noUserError.InnerText = "Not Connected to server.";
        }
    }

    protected void Register_Button_Click(object sender, EventArgs e)
    {
        string firstNameValue = validationDefault01.Value;
        string lastNameValue = validationDefault02.Value;
        string usernameValue = validationDefault03.Value;
        string passwordValue = validationDefault04.Value;
        string confirmPasswordValue = validationDefault05.Value;

        if (passwordValue.Equals(confirmPasswordValue)) //checks if password and confirm password are equal
        {
            if (Service.PasswordAvailable(passwordValue) && Service.UsernameAvailable(usernameValue)) //checks if this password is used or not
            {
                ErrorDiv.Visible = false;
                noUserError.InnerText = "";

                if (Service.Register(firstNameValue, lastNameValue, usernameValue, passwordValue)) //checks if registration succeeded
                {
                    noUserError.InnerText = "";
                    ErrorDiv.Visible = false;
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    ErrorDiv.Visible = true;
                    noUserError.InnerText = "There was an error. we couldn't register you.";
                }
            }
            else
            {
                ErrorDiv.Visible = true;
                noUserError.InnerText = "password already in use.";
            }
        }
        else
        {
            ErrorDiv.Visible = true;
            noUserError.InnerText = "passwords don't match";
        }
    }
}
