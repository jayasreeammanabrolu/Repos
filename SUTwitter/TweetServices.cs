using SUTwitter.Users;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SUTwitter.Service
{
    public class TweetServices
    {
        public static int InsertUserTweetInDB(Guid UserID, string tweetText) // returns tweet id of inserted tweet
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
                    insertuser.Parameters.AddWithValue("@UserId", UserID);
                    insertuser.Parameters.AddWithValue("@TweetText", tweetText);
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


        // update time line for all the users who are in memcache.
        //Get the list of all the user- call get followers list function.
        //write a forloop and add the tweet all the list of users in memcache.

        public static void UpdateMemcacheUsers(Guid userID, string tweetText)
        {
            string str = userID.ToString();
            int tweetId = InsertUserTweetInDB(userID,tweetText); // Get the tweet ID
            List<Guid> memcacheusers =  MemCache.GetTimeline(userID); // Returns all the users in memcache

            if (memcacheusers != null)
            {
                for (int i = 0; i < memcacheusers.Count; i++)
                {
                    MemCache.AddTweetToMemCache(tweetId, memcacheusers[i], tweetText, DateTime.Now);// Adds the tweet to all the users in memcache.
                }
            }

        }

        //update timeline for users who are in database but not in memcache.
        public static void UpdateOtherUsers(Guid userId, string tweetText) // Updates users not in memcache
        {
            string str = userId.ToString();            
            List<string> usersList = Tweet.GetFollowersList(str).ToList(); // Get all the users in database
            usersList.RemoveAll(x => string.IsNullOrEmpty(x));
            List<Guid> userguids = new List<Guid>();
            List<Guid> memcacheusers = MemCache.GetTimeline(userId); // Returns all the users in memcache
            List<Guid> Otherusers = new List<Guid>();
            foreach(string user in usersList)
            {
                userguids.Add(new Guid(user)); // convert user lists to guids
            }
                for (int i = 0; i < userguids.Count; i++)
                {
                    if (memcacheusers == null || !memcacheusers.Contains(userguids[i]))
                    {
                        Otherusers.Add(userguids[i]); // Adds the users not in memcache
                    }
                }
                for (int i = 0; i < Otherusers.Count; i++)
                {
                    InsertUserTweetInDB(Otherusers[i], tweetText); // updates users not in memcache with tweet.
                }
             
        }


    }
}