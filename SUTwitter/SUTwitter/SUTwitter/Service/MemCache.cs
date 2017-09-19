using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SUTwitter.Service
{
    public static class MemCache
    {
        private const string account = "sutwitter";
        private const string key = "iP66KV8AYQ69Co5XP5pCjtx7Wp9UAffYrU6UCWPyGMUl0XcMi2Phdc7xVluc2SGlshMDVDDsBoHjmiQ/8ar6Og==";
        private const string url = "https://sutwitter.blob.core.windows.net/";
        private const string containerFollowerList = "followerslist";
        private const string containerUserTimeline = "usertimeline";
        public struct TweetStruct
        {
            public int guidTweet { get; set; }
            public Guid guidUser { get; set; }
            public string tweetText { get; set; }
            public DateTime DtTweetTime { get; set; }
        }


        private static uint MAX_MEMCACHE_USERS = 10;
        private static uint MAX_MEMCACHE_TWEETS = 100;

        private static Dictionary<Guid, List<Guid>> MemCacheUserTimeline ;
        private static LinkedList<Guid> listTimeLine;

        public static Dictionary<int, TweetStruct> MemCacheTweet ;
        private static LinkedList<int> listTweets;


        #region MemcacheTimeline

        public static List<Guid> GetTimeline(Guid guidUser)
        {
            if (MemCacheUserTimeline.ContainsKey(guidUser))
            {
                return MemCacheUserTimeline[guidUser];
            }
            return null;
        }


        public static void AddTimeline(Guid guidUser)
        {
            if (MemCacheUserTimeline == null)
             {
                MemCacheUserTimeline = new Dictionary<Guid, List<Guid>>();
                listTimeLine = new LinkedList<Guid>();
             }

            if (MemCacheUserTimeline.ContainsKey(guidUser))
            {
                listTimeLine.Remove(guidUser);
                listTimeLine.AddFirst(guidUser);
            }
            else
            {
                MemCacheUserTimeline.Add(guidUser, GetListOfTweets(guidUser));
                if (listTimeLine.Count >= MAX_MEMCACHE_USERS)
                {
                    RemoveTimelineFromCache();
                }
                // add timeline to first if the memcache is full then remove the last one
            }
        }

        private static List<Guid> GetListOfTweets(Guid guidUser)
        {
            string tweets = "";
            try
            {
                Uri baseUri = new Uri(url);

                // get storage
                StorageCredentials creds = new StorageCredentials(account, key);
                CloudBlobClient blobStorage = new CloudBlobClient(baseUri, creds);

                // get blob container
                CloudBlobContainer blobContainer = blobStorage.GetContainerReference(containerUserTimeline);
                blobContainer.CreateIfNotExists(null);
                CloudBlockBlob blob = blobContainer.GetBlockBlobReference(guidUser.ToString() + ".txt");
                // blob.DeleteIfExists();

                var options = new BlobRequestOptions()
                {
                    ServerTimeout = TimeSpan.FromMinutes(10)
                };


                using (var memoryStream = new MemoryStream())
                {
                    blob.DownloadToStream(memoryStream);
                    tweets = System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
            catch (Exception ex)
            {

            }

            string[] tweetList = tweets.Split("\n".ToCharArray());
            List<Guid> tweetGuidList = new List<Guid>();
            for (int i = 0; i < tweetList.Count(); i++)
            {
                if (string.IsNullOrEmpty(tweetList[i].Trim()))
                    continue;
                tweetGuidList.Add(new Guid(tweetList[i].Trim()));
            }
            return tweetGuidList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guidUser"></param>
        public static void RemoveTimelineFromCache()
        {
            Guid remove = listTimeLine.Last.Value;
            listTimeLine.RemoveLast();
            MemCacheUserTimeline.Remove(remove);
        }

        #endregion

        #region Tweets

        public static TweetStruct GetTweet(int iTweetId)
        {
            if (MemCacheTweet.ContainsKey(iTweetId))
            {
                return MemCacheTweet[iTweetId];
            }
            TweetStruct nullTweet = new TweetStruct();
            nullTweet.guidTweet = -1;
            return nullTweet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iTweetId"></param>
        public static void AddTweetToMemCache(int iTweetId, Guid guidUser, string tweetText, DateTime dtTweetTime)
        {
            if (MemCacheTweet == null)
            {
                MemCacheTweet = new Dictionary<int, TweetStruct>();
                listTweets = new LinkedList<int>();
            }

            TweetStruct tweet = new TweetStruct();
            tweet.guidTweet = iTweetId;
            tweet.guidUser = guidUser;
            tweet.tweetText = tweetText;
            tweet.DtTweetTime = dtTweetTime;

            if (listTweets.Count >= MAX_MEMCACHE_TWEETS)
            {
                int remove = listTweets.Last.Value;
                listTweets.RemoveLast();
                MemCacheTweet.Remove(remove);

            }
            else
            {
                MemCacheTweet.Add(iTweetId, tweet);
                listTweets.AddFirst(iTweetId);
                if (listTweets.Count >= MAX_MEMCACHE_TWEETS)
                {
                    int remove = listTweets.Last.Value;
                    listTweets.RemoveLast();
                    MemCacheTweet.Remove(remove);
                }

            }


        }

        #endregion



    }
}