using System;
using System.Linq;
using DidactischeLeermiddelen.Models.DAL;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DidactischeLeermiddelen.Startup))]
namespace DidactischeLeermiddelen
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
