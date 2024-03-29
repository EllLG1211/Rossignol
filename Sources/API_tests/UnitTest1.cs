using API_Gateway.Controllers;
using API_Gateway.Helpers;
using API_Gateway.Services;
using API_REST.Controllers.V1;
using DTOs;
using EF_Local.Managers;
using EF_Model.Utils;
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
        class blendLogger<T> : ILogger<T>, IDisposable
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
            ILogger<JwtUtils> il = new blendLogger<JwtUtils>();
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

        [Fact]
        public void testREST()
        {
            List<ConnectedUser> data = makeTestUsers();

            IDataManager idm = new EFDataManager(":memory:");
            ILogger<EntriesController> il = new blendLogger<EntriesController>();
            ILogger<AccountsController> ila = new blendLogger<AccountsController>();

            ILogger<JwtUtils> ilj = new blendLogger<JwtUtils>();
            var appset = new AppSettings()
            {
                Secret = "Test Project"
            };
            IOptions<AppSettings> ioa = Options.Create(appset);
            IJwtUtils jwt = new JwtUtils(ioa, ilj);
            AutoMapper.IMapper ima = null;
            EntriesController ec = new EntriesController(il,ima, idm, jwt);
            AccountsController ac = new AccountsController(ila, ima, idm);

            AccountDTO adt = new AccountDTO();
            adt.Mail = data.First().Mail;
            adt.Password = data.First().Password;
            //ac.AddUser(adt);
            
            //ec.List(jwt.GenerateJwtToken(new Model.Api.Entities.User(data.First().Uid, data.First().Mail)));
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