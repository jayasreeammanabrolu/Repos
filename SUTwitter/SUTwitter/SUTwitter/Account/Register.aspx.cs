using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using SUTwitter.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

namespace SUTwitter.Account
{
    public partial class Register : Page
    {
        private const string account = "sutwitter";
        private const string key = "iP66KV8AYQ69Co5XP5pCjtx7Wp9UAffYrU6UCWPyGMUl0XcMi2Phdc7xVluc2SGlshMDVDDsBoHjmiQ/8ar6Og==";
        private const string url = "https://sutwitter.blob.core.windows.net/";
        private const string containerNameFollowerList = "followerslist";
        private const string containerNameUserTimeline = "usertimeline";

        private void AddFollowersList(string userID)
        {
            try
            {
                Uri baseUri = new Uri(url);

                // get storage
                StorageCredentials creds = new StorageCredentials(account, key);
                CloudBlobClient blobStorage = new CloudBlobClient(baseUri, creds);

                // get blob container
                CloudBlobContainer blobContainer = blobStorage.GetContainerReference(containerNameFollowerList);
                blobContainer.CreateIfNotExists(null);
                CloudBlockBlob blob = blobContainer.GetBlockBlobReference(userID + ".txt");
                blob.DeleteIfExists();

                var options = new BlobRequestOptions()
                {
                    ServerTimeout = TimeSpan.FromMinutes(10)
                };

                using (var stream = new MemoryStream(Encoding.Default.GetBytes(userID +"\r\n"), false))
                {
                    blob.UploadFromStream(stream, null, options);
                }


            }
            catch (Exception)
            {
                return;
            }
        }

        private void AddUserTimeline(string userID)
        {
            try
            {
                Uri baseUri = new Uri(url);

                // get storage
                StorageCredentials creds = new StorageCredentials(account, key);
                CloudBlobClient blobStorage = new CloudBlobClient(baseUri, creds);

                // get blob container
                CloudBlobContainer blobContainer = blobStorage.GetContainerReference(containerNameUserTimeline);
                blobContainer.CreateIfNotExists(null);
                CloudBlockBlob blob = blobContainer.GetBlockBlobReference(userID + ".txt");
                blob.DeleteIfExists();

                using (var stream = new MemoryStream(Encoding.Default.GetBytes(""), false))
                {
                    blob.UploadFromStream(stream, null, null);
                }

            }
            catch (Exception)
            {
                return;
            }
        }


        private void InsertUser(string userID, string EMail)
        {
            // now insert values
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();

                string insert = "insert into [TwitterUsers] ([Id],  [Name]," +
                                                        "[Status]," +
                                                        "[FollowersListURL]," +
                                                    "[UserTimelineURL]) " +
                                "values(@ID, @Name, @Status, @FollowersListURL, @UserTimelineURL)";
                using (SqlCommand insertuser = new SqlCommand(insert, con))
                {
                    insertuser.Parameters.AddWithValue("@ID", new Guid(userID));
                    insertuser.Parameters.AddWithValue("@Name", EMail);
                    insertuser.Parameters.AddWithValue("@Status", "New to SU Twitter");
                    insertuser.Parameters.AddWithValue("@FollowersListURL", url + "followerslist/" + userID + ".txt");
                    insertuser.Parameters.AddWithValue("@UserTimelineURL", url + "usertimeline/" + userID + ".txt");
                    try
                    {
                        insertuser.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // delete package from the cloud storage

                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }

        }


        protected void CreateUser_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            var user = new ApplicationUser() { UserName = Email.Text, Email = Email.Text };
            IdentityResult result = manager.Create(user, Password.Text);
            if (result.Succeeded)
            {
                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                //string code = manager.GenerateEmailConfirmationToken(user.Id);
                //string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
                //manager.SendEmail(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>.");

                var userStore = new UserStore<IdentityUser>();
                var userManager = new UserManager<IdentityUser>(userStore);
                var newUser = userManager.FindByEmail(Email.Text);
                if (newUser != null)
                {
                    userManager.AddToRole(newUser.Id, "user");
                    AddFollowersList(newUser.Id);
                    AddUserTimeline(newUser.Id);
                    InsertUser(newUser.Id, Email.Text);
                }

               


                signInManager.SignIn( user, isPersistent: false, rememberBrowser: false);
                IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
            }
            else 
            {
                ErrorMessage.Text = result.Errors.FirstOrDefault();
            }
        }
    }
}