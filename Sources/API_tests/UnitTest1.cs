using API_Gateway.Controllers;
using API_Gateway.Helpers;
using API_Gateway.Services;
using EF_Local.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Model.Api.Users;
using Model.Business;
using Model.Business.Entries;
using Model.Business.Users;

namespace API_tests
{
    public class UnitTest1
    {
        class blendLogger : ILogger<JwtUtils>, IDisposable
        {
            public IDisposable? BeginScope<TState>(TState state) where TState : notnull
            {
                return this;
            }

            public void Dispose()
            {
                
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                return true;
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
            {

            }
        }
        [Fact]
        public void testGateway()
        {
            IDataManager idm = new EFDataManager(":memory:");
            ILogger<JwtUtils> il = new blendLogger();
            var appset = new AppSettings()
            {
                Secret = "Test Project"
            };
            IOptions<AppSettings> ioa = Options.Create(appset);
            IJwtUtils jwt = new JwtUtils(ioa, il);
            UserService us = new UserService(idm, jwt);
            UsersController uc = new UsersController(us);

            var users = makeTestUsers();

            AuthenticateModel am = new AuthenticateModel();
            am.Username = users.First().Mail;
            am.Password = users.First().Password;
            Assert.True(uc.Authenticate(am) is BadRequestObjectResult);
        }

        private List<ConnectedUser> makeTestUsers()
        {
            ConnectedUser cu = new ConnectedUser("testman@dodo.com", "testpass");
            ConnectedUser cu2 = new ConnectedUser("duoduo@duo.com", "pspspsps");
            Entry e = new ProprietaryEntry(cu.Mail, "mako", "pask", "raidforums");
            cu.AddEntry(e);
            var ax = new List<ConnectedUser>();
            ax.Add(cu);
            ax.Add(cu2);
            return ax;
        }
    }
}