using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
namespace SUTwitter.Profile
{
    public partial class UserProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                Response.Redirect("~/Default.aspx");
            }
            Guid userID;
            try
            {
                userID = new Guid(Request.QueryString["ID"]);

            }
            catch
            {
                Response.Redirect("~/Default.aspx");
                return;
            }

            GetValuesDB(userID.ToString());
            var userStore = new UserStore<IdentityUser>();
            var userManager = new UserManager<IdentityUser>(userStore);
            var user = userManager.FindById(userID.ToString());
            if (user != null)
            {
                lblEmail.Text = user.Email;
            }
            else
            {
                lblEmail.Text = "--";
            }

        }




        private void GetValuesDB(string userID)
        {
           
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();

                string select = "Select Name, Status FROM TwitterUsers WHERE Id=@Id";
                using (SqlCommand selectCount = new SqlCommand(select, con))
                {
                    selectCount.Parameters.AddWithValue("@Id", userID);
                    try
                    {
                        using (SqlDataReader reader = selectCount.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lblName.Text = reader.GetString(0);
                                lblStatus.Text = reader.GetString(1);
                            }
                            reader.Close();
                        }
                    }
                    catch //(Exception ex)
                    {
                        //Response.Write("<b>something really bad happened....." + ex.Message + "</b> ");
                        // return "";

                    }
                    finally
                    {
                        con.Close();
                    }

                }
                // end of sqlcommand


            }// end of using sqlconnection 


        }
    }
}