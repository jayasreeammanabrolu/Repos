using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Text;

namespace SUTwitter.Users
{
    public partial class Tweet : System.Web.UI.Page
    {
        private const string account = "sutwitter";
        private const string key = "iP66KV8AYQ69Co5XP5pCjtx7Wp9UAffYrU6UCWPyGMUl0XcMi2Phdc7xVluc2SGlshMDVDDsBoHjmiQ/8ar6Og==";
        private const string url = "https://sutwitter.blob.core.windows.net/";
        private const string containerFollowerList = "followerslist";
        private const string containerUserTimeline = "usertimeline";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private int InsertTweetInDB(string userID)
        {
            int id = -1;
            // now insert values
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();

                string insert = "insert into [Tweet] (  [UserId]," + "[TweetText]," + "[TweetTime]) " +
                                "values(@UserId, @TweetText, @TweetTime) ; SELECT SCOPE_IDENTITY()";
                using (SqlCommand insertuser = new SqlCommand(insert, con))
                {
                    insertuser.Parameters.AddWithValue("@UserId", new Guid(userID));
                    insertuser.Parameters.AddWithValue("@TweetText", TxtBoxTweet.Text);
                    insertuser.Parameters.AddWithValue("@TweetTime", DateTime.Now);
                    try
                    {
                        id = System.Convert.ToInt32(insertuser.ExecuteScalar());

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
            return id;

        }


        private void AddToTweetLine(string followerID, string tweetID, DateTime dt)
        {
            try
            {
                Uri baseUri = new Uri(url);

                // get storage
                StorageCredentials creds = new StorageCredentials(account, key);
                CloudBlobClient blobStorage = new CloudBlobClient(baseUri, creds);

                // get blob container
                CloudBlobContainer blobContainer = blobStorage.GetContainerReference(containerUserTimeline);
                blobContainer.CreateIfNotExists(null);
                CloudBlockBlob blob = blobContainer.GetBlockBlobReference(followerID + ".txt");


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

                text = tweetID +"\r\n" + text;

                using (var stream = new MemoryStream(Encoding.Default.GetBytes(text), false))
                {
                    blob.UploadFromStream(stream, null, options);
                }

            }
            catch (Exception ex)
            {

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



        protected void BtnTweet_Click(object sender, EventArgs e)
        {
            // Add Tweet in Database
            string currentUser = User.Identity.GetUserId();
            if (string.IsNullOrEmpty(currentUser))
                return;
            int tweetID = -1;
            tweetID = InsertTweetInDB(currentUser);

            DateTime dt = DateTime.Now;

            // first find all my followers
            // then find their timelines
            // then add tweet ID, DateTime into their TimeLine

            string[] followers = GetFollowersList(currentUser);

            for (int i = 0; i < followers.Count(); i++)
            { 
                // Add Tweet ID and timestamp on their timeline
                AddToTweetLine(followers[i], tweetID.ToString(), dt);
            }
            
        }
    }
}