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
            Auth.SetUserCredentials("CEu1Zg6jJuw7SLxoiF2wXKrQZ", "K572O5WNZywIYtOrWbivf21v1q4M5o8Zzvl2FqveuWuSLsFGlB", "884304411257864192-XcZRVNr3RwwswTNDj7eXCqG1CeL8TqF", "NTGVpMpMJLqPwcppndAIHIBLPtkNGb0mLL35UABZ5VAxo");
            var user = User.GetAuthenticatedUser();

                              

       
            
            //Debug.WriteLine(user.ScreenName);

            //var tweet = Tweet.PublishTweet("Bu tweet tweetinv ile atıldı!");

            ConfigureAuth(app);
        }
    }
}
