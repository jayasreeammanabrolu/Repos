using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

namespace SUTwitter.Users
{
    public partial class EditProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FillValues();
            }
        }

        private void FillValues()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();

                string select = "Select Name, Status FROM TwitterUsers WHERE ID=@ID";
                using (SqlCommand selectNameStatus = new SqlCommand(select, con))
                {
                    selectNameStatus.Parameters.AddWithValue("@ID", User.Identity.GetUserId());
                    try
                    {
                        using (SqlDataReader reader = selectNameStatus.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                txtBoxName.Text = reader.GetString(0);
                                txtBoxStatus.Text = reader.GetString(1);
                            }
                            reader.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<b>something really bad happened....." + ex.Message + "</b> ");


                    }
                    finally
                    {
                        con.Close();
                    }

                }
                // end of sqlcommand
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();
                string update = "UPDATE [TwitterUsers] SET [Name]=@Name, [Status]=@Status " +
                                        " WHERE Id=@ID";
                using (SqlCommand updateTable = new SqlCommand(update, con))
                {
                    updateTable.Parameters.AddWithValue("@ID", User.Identity.GetUserId());
                    updateTable.Parameters.AddWithValue("@Name", txtBoxName.Text);
                    updateTable.Parameters.AddWithValue("@Status", txtBoxStatus.Text);

                    try
                    {
                        updateTable.ExecuteNonQuery();
                        // Show a messageBox here and Disable the checkbox clear the Text Field
                    }
                    catch //(Exception ex)
                    {
                        //Response.Write("<b>something really bad happened....." + ex.Message + "</b> ");
                        return;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }// end of using sqlconnection

            Response.Redirect("~/Default.aspx");
        }
    }
}