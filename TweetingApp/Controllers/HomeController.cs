using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;
using Tweetinvi.Core.Controllers;
using Tweetinvi.Core.Factories;
using Tweetinvi.Core.Helpers;
using Tweetinvi.Models.DTO;
using Tweetinvi.Parameters;
using TweetingApp.Models;




namespace TweetingApp.Controllers
{
    
    public class HomeController : Controller
    {  

        //tweet olayı icin
        internal static ITwitterCredentials _credentials;
        public HomeController()
        {
            _credentials = GenerateCredentials();
        }
        public static ITwitterCredentials GenerateCredentials()
        {
            return new TwitterCredentials(
                System.Configuration.ConfigurationManager.AppSettings["consumerKey"],
                System.Configuration.ConfigurationManager.AppSettings["consumerSecret"],
                System.Configuration.ConfigurationManager.AppSettings["accessToken"],
                System.Configuration.ConfigurationManager.AppSettings["accessTokenSecret"]);
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult tweet_gonder(string Gonderi)
        {
            Auth.ExecuteOperationWithCredentials(_credentials, () =>
            {
                var publishOptions = new PublishTweetOptionalParameters();
                return Tweetinvi.Tweet.PublishTweet(Gonderi, publishOptions);
            }
           );
            return View("tweet_yazdir");
        }

        public ActionResult tweet_yazdir()
        {


            return View();
        }

        [HttpPost]
        public ActionResult Anasayfa(string Gonderi)
        {
            Auth.ExecuteOperationWithCredentials(_credentials, () =>
            {
                var publishOptions = new PublishTweetOptionalParameters();
                return Tweetinvi.Tweet.PublishTweet(Gonderi, publishOptions);
            }
           );
            return View("tweet_yazdir");

        }
        public ActionResult tweet_gonder()
        {
            return View();
        }
        public ActionResult Anasayfa()
        {
            var result = Auth.ExecuteOperationWithCredentials(_credentials, () =>
              {
                  
                  var tweets2 = Tweetinvi.Timeline.GetHomeTimeline(50);
                  var tweets = tweets2 == null ? new List<ITweet>() : tweets2.ToList();
                  var profil = Tweetinvi.User.GetUserFromScreenName("JFernweh_");
                  //var tt = Tweetinvi.Trends.GetTrendsAt(23424969);

                  var trendler = Tweetinvi.Trends.GetTrendsAt(23424969).Trends.Take(8);
                  var trend_ler = trendler == null ? new List<ITrend>() : trendler.ToList();
                  //var suggestedCategories = Account.GetSuggestedCategories();
                  //var followingsuggestion = new List<IUser>();

                  //foreach (var userCategory in suggestedCategories.Take(15))
                  //{
                  //   var user=(Account.GetSuggestedUsers(userCategory.Slug));
                  //    var user_ = user == null ? Account.GetSuggestedUsers(userCategory.Slug) : user;
                  //    followingsuggestion.AddRange(user);
                  //}
                  
                  //var following_suggestion = followingsuggestion == null ? new List<IUser>():followingsuggestion.ToList();

                  AnasayfaModel model = new AnasayfaModel()
                  {
                      Tweets = tweets,
                      User = profil,
                      //Trend = tt,
                      Trendler = trend_ler,
                      //Following_Suggestion = following_suggestion

                  };
                  return model;

              });

            return View(result);
        }

     
        public ActionResult Profil(string id, string query)
        {
            var result = Auth.ExecuteOperationWithCredentials(_credentials, () =>
            {
                query = string.IsNullOrEmpty(query) ? "Tweetinvi" : query;
                id = string.IsNullOrEmpty(id) ? "JFernweh_" : id;
                var tweets2 = Tweetinvi.Timeline.GetHomeTimeline(50);
                var tweets = tweets2 == null ? new List<ITweet>() : tweets2.ToList();
                var profil = Tweetinvi.User.GetUserFromScreenName(id);
                //var tt = Tweetinvi.Trends.GetTrendsAt(23424969);
                var trendler = Tweetinvi.Trends.GetTrendsAt(23424969).Trends.Take(10);
                var trend_ler = trendler == null ? new List<ITrend>() : trendler.ToList();
                var ptimeline = Tweetinvi.User.GetUserFromScreenName(id).GetUserTimeline(50).ToList();
                var p_timeline = ptimeline == null ? new List<ITweet>() : ptimeline.ToList();


                var searchParameter = new SearchTweetsParameters(query)
                {
                    //GeoCode = new GeoCode(-122.398720, 37.781157, 1, DistanceMeasure.Miles),
                    //SearchType = SearchResultType.Popular,
                    //MaximumNumberOfResults = 100,
                    //Until = new DateTime(2017, 07, 02),
                    //SinceId = 399616835892781056,
                    //MaxId = 405001488843284480,
                    //Filters = TweetSearchFilters.Images,
                    SearchQuery =query
                };

                var s_tweets = Search.SearchTweets(searchParameter).ToList();
               
                //var s_tweets = Tweetinvi.Search.SearchTweets(query).ToList();
                AnasayfaModel model = new AnasayfaModel()
                {
                    Tweets = tweets,
                    User = profil,
                    //Trend = tt,
                    Trendler = trend_ler,
                    Profile_Tweets = p_timeline,
                    Gundem_Tweets = s_tweets

                };
                return model;
                     });

            return View(result);
        }



        public ActionResult Bildirimler()
        {
            var result = Auth.ExecuteOperationWithCredentials(_credentials, () =>
            {
                var tweets2 = Tweetinvi.Timeline.GetHomeTimeline(50);
                var tweets = tweets2 == null ? new List<ITweet>() : tweets2.ToList();
                var profil = Tweetinvi.User.GetUserFromScreenName("JFernweh_");
                //var tt = Tweetinvi.Trends.GetTrendsAt(23424969);
                var trendler = Tweetinvi.Trends.GetTrendsAt(23424969).Trends.Take(10);
                var trend_ler = trendler == null ? new List<ITrend>() : trendler.ToList();
                var  request= Tweetinvi.Account.GetUserIdsRequestingFriendship(10).ToList();
                var bildirimRequest = new List<IUser>();
                foreach(var item in request)
                {
                    bildirimRequest.AddRange(Account.GetUsersRequestingFriendship(10).ToList());
                }
                var retweetleyenler = Tweetinvi.Tweet.GetRetweetersIds(10);
                var likers = new List<long>();
                foreach(var item in retweetleyenler)
                {
                    likers.AddRange(retweetleyenler);
                }
                
                //var bildirimretweet = Stream.CreateUserStream();
                //var streamListe = new List<IUser>();
                //bildirimretweet.FollowedByUser += (sender, args) =>
                //{
                //    streamListe.Add(args.User);

                //};

                //bildirimretweet.StartStream();
                AnasayfaModel model = new AnasayfaModel()
                {
                    Tweets = tweets,
                    User = profil,
                    //Trend = tt,
                    Trendler = trend_ler,
                    BildirimRequest=bildirimRequest,
                    //BildirimRetweet=bildirimretweet
                    Retweetleyenler=retweetleyenler
                };
                return model;

            });
            return View(result);
        }
        //public ActionResult Retweeters()
        //{ var rliste = new List<Tweetinvi.Models.ITweet>();
        //    Auth.ExecuteOperationWithCredentials(_credentials, () =>
        //    {
        //        var r = Tweet.GetRetweets(10);
        //        rliste.AddRange(r);

        //    });
        //    return Json(true);
        //}
        public ActionResult Gundem(string query)
        {
            var result = Auth.ExecuteOperationWithCredentials(_credentials, () =>
            {
                query = string.IsNullOrEmpty(query) ? "Tweetinvi" : query;
                //id = string.IsNullOrEmpty(id) ? "JFernweh_" : id;
                var tweets2 = Tweetinvi.Timeline.GetHomeTimeline(50);
                var tweets = tweets2 == null ? new List<ITweet>() : tweets2.ToList();
                //var profil = Tweetinvi.User.GetUserFromScreenName(id);
                //var tt = Tweetinvi.Trends.GetTrendsAt(23424969);
               
                var trendler = Tweetinvi.Trends.GetTrendsAt(23424969).Trends.Take(10);
                var trend_ler = trendler == null ? new List<ITrend>() : trendler.ToList();
                //var ptimeline = Tweetinvi.User.GetUserFromScreenName(id).GetUserTimeline(50).ToList();
                //var p_timeline = ptimeline == null ? new List<ITweet>() : ptimeline.ToList();


                var searchParameter = new SearchTweetsParameters(query)
                {
                    //GeoCode = new GeoCode(-122.398720, 37.781157, 1, DistanceMeasure.Miles),
                    SearchType = SearchResultType.Mixed,
                    //MaximumNumberOfResults = 100,
                    //Until = new DateTime(2017, 07, 02),
                    //SinceId = 399616835892781056,
                    //MaxId = 405001488843284480,
                    //Filters = TweetSearchFilters.Images,
                    SearchQuery = query,
                  
                };

                var stweets = Search.SearchTweets(searchParameter).ToList();
                var s_tweets = stweets == null ? new List<ITweet>() : stweets.ToList();
                //var s_tweets = Tweetinvi.Search.SearchTweets(query).ToList();

                //var userSearchParameter = new SearchUsersParameters(query)
                //{
                //    SearchQuery=query,
                //};
                var users_search = Search.SearchUsers(query).Take(3);
                var gundem_kullanici = users_search == null ? new List<IUser>(): users_search.ToList();

                AnasayfaModel model = new AnasayfaModel()
                {
                    Tweets = tweets,
                    //User = profil,
                    //Trend = tt,
                    Trendler = trend_ler,
                    //Profile_Tweets = p_timeline,
                    Gundem_Tweets = s_tweets,
                    Gundem_Kullanici=gundem_kullanici

                };
                return model;

            });

            return View(result);
        }
        public ActionResult Mesajlar()
        {
            var result = Auth.ExecuteOperationWithCredentials(_credentials, () =>
            {
                var tweets2 = Tweetinvi.Timeline.GetHomeTimeline(50);
                var tweets = tweets2 == null ? new List<ITweet>() : tweets2.ToList();
                var profil = Tweetinvi.User.GetUserFromScreenName("JFernweh_");
                var tt = Tweetinvi.Trends.GetTrendsAt(23424969);
                var trendler = Tweetinvi.Trends.GetTrendsAt(23424969).Trends.Take(10);
                var trend_ler = trendler == null ? new List<ITrend>() : trendler.ToList();
                var mesajgelen = Message.GetLatestMessagesReceived();
                var mesaj_gelen = Tweetinvi.Message.GetLatestMessagesReceived();
                var mesajgiden = Message.GetLatestMessagesSent();
                var mesaj_giden = Tweetinvi.Message.GetLatestMessagesSent();
                AnasayfaModel model = new AnasayfaModel()
                {
                    Tweets = tweets,
                    User = profil,
                    //Trend = tt,
                    Trendler = trend_ler,
                    Mesaj_gelen=mesaj_gelen,
                    Mesaj_giden=mesaj_giden
               };
                return model;

            });
            return View(result);
        }

        [HttpPost]
        public ActionResult Mesajlar(string scname, string mesaj)
        {
            Auth.ExecuteOperationWithCredentials(_credentials, () =>
            {
                var m = new PublishMessageParameters(mesaj, scname);
                return Tweetinvi.Message.PublishMessage(mesaj, scname);

            });
            return View("tweet_yazdir");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Fav(long twitId)
        {
            Auth.ExecuteOperationWithCredentials(_credentials, () =>
            {
                var t = Tweetinvi.Tweet.GetTweet(twitId);
                if (t != null)
                {
                    if (t.Favorited)
                    {
                        t.UnFavorite();
                    }
                    else
                    {
                        t.Favorite();
                    }
                }
            });
            return Json(true);

        }
        [HttpPost]
        public ActionResult retweet(long tweetId)
        {
            Auth.ExecuteOperationWithCredentials(_credentials, () =>
            {
                var t = Tweet.GetTweet(tweetId);
                if (t != null)
                {
                    if (t.Retweeted)
                    {
                        t.UnRetweet();
                    }
                    else
                    {
                        t.PublishRetweet();
                    }
                }
            });
            return Json(true);
        }

        [HttpPost]
        public ActionResult tweet_sil(long tweetId)
        {
            Auth.ExecuteOperationWithCredentials(_credentials, () =>
            {
                var t = Tweet.GetTweet(tweetId);
                t.Destroy();
            });
            return Json(true);
        }

        public ActionResult Following(string id)
        {
            var result = Auth.ExecuteOperationWithCredentials(_credentials, () =>
            {
                id = string.IsNullOrEmpty(id) ? "JFernweh_" : id;
                var tweets2 = Tweetinvi.Timeline.GetHomeTimeline(50);
                var tweets = tweets2 == null ? new List<ITweet>() : tweets2.ToList();
                var profil = Tweetinvi.User.GetUserFromScreenName(id);
                //var tt = Tweetinvi.Trends.GetTrendsAt(23424969);
                var trendler = Tweetinvi.Trends.GetTrendsAt(23424969).Trends.Take(10);
                var trend_ler = trendler == null ? new List<ITrend>() : trendler.ToList();

                var following_ = Tweetinvi.User.GetUserFromScreenName(id).GetFriends(250).ToList();
                var following = following_ == null ? new List<IUser>() : following_.ToList();






                AnasayfaModel model = new AnasayfaModel()
                {
                    Tweets = tweets,
                    User = profil,
                    //Trend = tt,
                    Trendler = trend_ler,
                    Following = following



                };
                return model;

            });
            return View(result);
        }

        public ActionResult Followers(string id)
        {
            var result = Auth.ExecuteOperationWithCredentials(_credentials, () =>
            {
                id = string.IsNullOrEmpty(id) ? "JFernweh_" : id;
                var tweets2 = Tweetinvi.Timeline.GetHomeTimeline(50);
                var tweets = tweets2 == null ? new List<ITweet>() : tweets2.ToList();
                var profil = Tweetinvi.User.GetUserFromScreenName(id);
                //var tt = Tweetinvi.Trends.GetTrendsAt(23424969);
                var trendler = Tweetinvi.Trends.GetTrendsAt(23424969).Trends.Take(10);
                var trend_ler = trendler == null ? new List<ITrend>() : trendler.ToList();
                var followers_ = Tweetinvi.User.GetUserFromScreenName(id).GetFollowers(250).ToList();
                var followers = followers_ == null ? new List<IUser>() : followers_.ToList();



                AnasayfaModel model = new AnasayfaModel()
                {
                    Tweets = tweets,
                    User = profil,
                    //Trend = tt,
                    Trendler = trend_ler,
                    Followers = followers



                };
                return model;

            });


            return View(result);
        }

        #region takip_kontrol tekrar bak

        [HttpPost]
        public ActionResult takip_kontrol(long friendId)
        {
            Auth.ExecuteOperationWithCredentials(_credentials, () =>
            {
                var t = Tweetinvi.User.GetUserFromId(friendId);


                if (t == Tweetinvi.User.GetUserFromScreenName("JFernweh_").GetFriendIds())
                {
                    Tweetinvi.User.UnFollowUser(t);
                }
                else
                {
                    Tweetinvi.User.FollowUser(t);
                }

            });
            return Json(true);
        }
        #endregion


        [HttpPost]
        public ActionResult Tweet_Yanitla(long twitId, string text)  //duzeltilmeye ihtiyaci var
        {
            Auth.ExecuteOperationWithCredentials(_credentials, () =>
            {
                var tweetToReplyTo = Tweet.GetTweet(twitId);
                

                var tweet = Tweet.PublishTweet(text, new PublishTweetOptionalParameters
                {
                    InReplyToTweet = Tweetinvi.Tweet.PublishTweetInReplyTo(" ",tweetToReplyTo)
                    
                });

                //var t = new PublishTweetParameters(txtReplayTweet);
                //return Tweetinvi.Tweet.PublishTweetInReplyTo(txtReplayTweet, twitId);
            });


            return Json(true);

        }

        public ActionResult mesaj_sil(long mesajId)
        {
            Auth.ExecuteOperationWithCredentials(_credentials, () =>
            {
                var t = Message.GetExistingMessage(mesajId);
                t.Destroy();
               
            });
            return Json(true);
        }

       
 

    }
}