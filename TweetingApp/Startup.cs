using Microsoft.Owin;
using Owin;
using System.Diagnostics;
using Tweetinvi;

[assembly: OwinStartupAttribute(typeof(TweetingApp.Startup))]
namespace TweetingApp
{
    public partial class Startup
    {
      
        public void Configuration(IAppBuilder app)
        {
            Auth.SetUserCredentials("parameter1", "parameter2", "parameter3", "parameter4");
            var user = User.GetAuthenticatedUser();

                              

       
            
            //Debug.WriteLine(user.ScreenName);

            //var tweet = Tweet.PublishTweet("Bu tweet tweetinv ile atıldı!");

            ConfigureAuth(app);
        }
    }
}
