using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.SqlClient;
using System.Configuration;

namespace SUTwitter.Users
{
    public partial class ListOfFollowers : System.Web.UI.Page
    {
        private const string account = "sutwitter";
        private const string key = "iP66KV8AYQ69Co5XP5pCjtx7Wp9UAffYrU6UCWPyGMUl0XcMi2Phdc7xVluc2SGlshMDVDDsBoHjmiQ/8ar6Og==";
        private const string url = "https://sutwitter.blob.core.windows.net/";
        private const string containerFollowerList = "followerslist";
        private const string containerUserTimeline = "usertimeline";
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (dt == null)
            {
                dt = new DataTable();
                AddDataColumns();
            }

            if (!Page.IsPostBack)
            {
                FillDataGrid();
            }
        }

        private string[] GetFollowersList(string userID)
        {
            string users = "";
            try
            {
                Uri baseUri = new Uri(url);

                // get storage
                StorageCredentials creds = new StorageCredentials(account, key);
                CloudBlobClient blobStorage = new CloudBlobClient(baseUri, creds);

                // get blob container
                CloudBlobContainer blobContainer = blobStorage.GetContainerReference(containerFollowerList);
                blobContainer.CreateIfNotExists(null);
                CloudBlockBlob blob = blobContainer.GetBlockBlobReference(userID + ".txt");
                // blob.DeleteIfExists();

                var options = new BlobRequestOptions()
                {
                    ServerTimeout = TimeSpan.FromMinutes(10)
                };


                using (var memoryStream = new MemoryStream())
                {
                    blob.DownloadToStream(memoryStream);
                    users = System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
            catch (Exception ex)
            {

            }

            string[] userList = users.Split("\n".ToCharArray());

            for (int i = 0; i < userList.Count(); i++)
                userList[i] = userList[i].Trim();
            return userList;

        }



        private void FillDataGrid()
        { 
        
            string currentUser = User.Identity.GetUserId();
            if (string.IsNullOrEmpty(currentUser))
                return;

            string[] followers = GetFollowersList(currentUser);

            string name = "";
            string status = "";

            for (int i = 0; i < followers.Count(); i++)
            {
                if (string.IsNullOrEmpty(followers[i]))
                    continue;

                GetValuesDB(followers[i], out name, out status);
                DataRow drValue = dt.NewRow();
                drValue["Name"] = name;
                drValue["Status"] = status;
                drValue["View"] = followers[i];
               
                dt.Rows.Add(drValue);
            }
            BindGrid();
            
        }


        private void GetValuesDB(string userID, out string name, out string status)
        {
            name = "";
            status = "";
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
                                name = reader.GetString(0);
                                status = reader.GetString(1);
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


        /// <summary>
        /// adds Columns to Data Table
        /// </summary>
        private void AddDataColumns()
        {
            dt.Columns.Add("Name");
            dt.Columns.Add("Status");
            dt.Columns.Add("View");
        }

        /// <summary>
        /// Binds Data to GridView
        /// </summary>
        private void BindGrid()
        {
            GridViewFollowers.DataSource = dt;
            GridViewFollowers.DataBind();

            for (int i = 0; i < GridViewFollowers.Rows.Count; i++)
            {
                HyperLink hyperlink = new HyperLink();
                hyperlink.NavigateUrl = "~/Profile/UserProfile.aspx?ID=" + GridViewFollowers.Rows[i].Cells[2].Text; // Serial Number passed as ID
                hyperlink.Text = "View Profile";
                GridViewFollowers.Rows[i].Cells[2].Text = "";
                GridViewFollowers.Rows[i].Cells[2].Controls.Add(hyperlink);

            }

        }
    }
}