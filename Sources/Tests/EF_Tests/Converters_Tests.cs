using EF_Model.Entities;
using EF_Model.Utils;
using EncryptedModel.Business.Managers;
using EncryptedModel.Business.Users;
using Model.Business.Entries;
using Model.Business.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EF_Tests
{
    
    public class Converters_Tests
    {
        [Fact]
        public void test()
        {
            try
            {
                string password = "pass";
                LocalUser ls = new LocalUser(password, new List<Entry>());
                EncryptedLocalUser usr = UserEncryptionManager.LocalToEncryptedUser(ls, password);
                List<EncryptedLocalUser> users = new List<EncryptedLocalUser>() { usr };

                //List<LocalUserEntity> lue = LocalUserConverter.LToEntities(users).ToList();
                //List<EncryptedLocalUser> users2 = LocalUserConverter.LToModels(lue).ToList();
                //Assert.Equal(users.Count, users2.Count);
            }
            catch(Exception ex)
            {
                Assert.True(false);
            }
            
        }

        [Fact]
        public void testConv()
        {
            try
            {
                string password = "pass";
                ConnectedUser ls = new ConnectedUser(password, "pass", new List<Entry>());
                EncryptedConnectedUser usr = UserEncryptionManager.ConnectedToEncryptedUser(ls, password);
                //List<EncryptedConnectedUser> users = new List<EncryptedConnectedUser>() { usr };

                //List<ConnectedUserEntity> lue = ConnectedUserConverter.ToEntities(users).ToList();
                //List<EncryptedConnectedUser> users2 = ConnectedUserConverter.ToModels(lue).ToList();
                //Assert.Equal(lue.Count, lue.Count);
            }
            catch
            {
                Assert.True(false);
            }
        }
    }
}
