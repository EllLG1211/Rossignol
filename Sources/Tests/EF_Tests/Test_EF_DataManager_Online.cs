using EF_Local.Managers;
using Model.Business;
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
    public class Test_EF_DataManager_Online
    {
        [Fact]
        ///Here we're testing the "descriptive" approach to using the EFDataManager
        public void TestDataManagerDescriptiveMode()
        {
            IDataManager manager = new EFDataManager();
            ConnectedUser cu = new ConnectedUser("testman@dodo.com", "testpass");

            Entry e = new ProprietaryEntry(cu.Mail, "mako", "pask", "raidforums");

            cu.AddEntry(e);

            manager.Register(cu, cu.Mail);

            Entry e2 = new ProprietaryEntry(cu.Mail, "drake", "nk@aaa", "ter.gov");

            cu.AddEntry(e2);

            manager.AddEntryToUser(cu, e2);

            Assert.Equal(2,manager.GetEntries(cu).Count());

            manager.RemoveEntry(cu, e);
            Assert.Single(manager.GetEntries(cu));

            manager.RemoveEntry(cu, e2);
            Assert.Empty(manager.GetEntries(cu));

            Assert.True(manager.DeleteUser(cu));
            Assert.False(manager.checkUserExists(cu.Mail));
        }
    }
}
