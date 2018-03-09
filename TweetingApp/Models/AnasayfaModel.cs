using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TweetingApp.Models
{
    public class AnasayfaModel
    {
        public List<Tweetinvi.Models.ITweet> Tweets { get; set; }
        public List<Tweetinvi.Models.ITweet> Profile_Tweets { get; set; }
        public Tweetinvi.Models.IUser User { get; set; }
        //public Tweetinvi.Models.IPlaceTrends Trend { get; set; }
        public List <Tweetinvi.Models.ITrend> Trendler { get;  set;}
        public List<Tweetinvi.Models.ITweet> Media { get; set; }
        public List<Tweetinvi.Models.IUser> Following_Suggestion { get; set; }
        public List<Tweetinvi.Models.IUser> Following { get; set; }
        public List<Tweetinvi.Models.IUser> Followers { get; set; }
        public List<Tweetinvi.Models.ITweet> Gundem_Tweets { get; set; }
        public List<Tweetinvi.Models.IUser> Gundem_Kullanici { get; set; }
        public List<Tweetinvi.Models.IUser> BildirimRequest { get; set; }
        //public Tweetinvi.Streaming.IUserStream BildirimRetweet { get; set; }
        public IEnumerable<Tweetinvi.Models.IMessage> Mesaj_giden { get; set; }
        public IEnumerable<Tweetinvi.Models.IMessage> Mesaj_gelen { get; set; }
        
        public IEnumerable<long> Retweetleyenler { get; set; }
        

    }

}