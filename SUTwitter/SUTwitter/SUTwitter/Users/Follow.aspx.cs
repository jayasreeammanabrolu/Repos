using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SUTwitter.Users
{
    public partial class Follow : System.Web.UI.Page
    {
        private const string account = "sutwitter";
        private const string key = "iP66KV8AYQ69Co5XP5pCjtx7Wp9UAffYrU6UCWPyGMUl0XcMi2Phdc7xVluc2SGlshMDVDDsBoHjmiQ/8ar6Og==";
        private const string url = "https://sutwitter.blob.core.windows.net/";
        private const string containerNameFollowerList = "followerslist";
        private const string containerNameUserTimeline = "usertimeline";
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        private void AddToFollowerList(string szFollower, string leader)
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
                CloudBlockBlob blob = blobContainer.GetBlockBlobReference(leader + ".txt");
                // blob.DeleteIfExists();

                var options = new BlobRequestOptions()
                {
                    ServerTimeout = TimeSpan.FromMinutes(10)
                };

                string text;
                using (var memoryStream = new MemoryStream())
                {
                    blob.DownloadToStream(memoryStream);
                    text = System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
                }

                text = szFollower + "\r\n" + text;

                using (var stream = new MemoryStream(Encoding.Default.GetBytes(text), false))
                {
                    blob.UploadFromStream(stream, null, options);
                }

            }
            catch (Exception ex)
            {

            }

        }



        protected void BtnAddFriend_Click(object sender, EventArgs e)
        {
            // first find the user
            var userStore = new UserStore<IdentityUser>();
            var userManager = new UserManager<IdentityUser>(userStore);
            var leader = userManager.FindByEmail(TxtBoxEMail.Text.Trim());
            if (leader == null)
            {
                lblStatus.Text = "Please check the EMail address";
                return;
            }

            // get ID of current user 
            string currentUser = User.Identity.GetUserId();
            if (string.IsNullOrEmpty(currentUser))
            {
                lblStatus.Text = "Something weird happened, seems like you are not logged in.";
                return;
            }

            // then find the container for the person to be followed
            // then add this user in that person's followed list
            AddToFollowerList(currentUser, leader.Id);

            lblStatus.Text = " You are now following " + leader.Email;
            TxtBoxEMail.Text = "";

        }
    }
}