using API_REST.DTOs;
using AutoMapper;
using Model.Business.Entries;
using Model.Business.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiREST_Tests
{
    [TestClass]
    public class MapperTests
    {
        [TestMethod]
        public void AccountToDtoConfiguration()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<AccountDTO, ConnectedUser>());
            config.AssertConfigurationIsValid();
        }

        [TestMethod]
        public void AccountToModelConfiguration()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ConnectedUser, AccountDTO>());
            config.AssertConfigurationIsValid();
        }

        [TestMethod]
        public void MailedUserToDtoConfiguration()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<MailedUser, MailedUserDTO>());
            config.AssertConfigurationIsValid();
        }

        [TestMethod]
        public void MailedUserToModelConfiguration()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<MailedUserDTO, MailedUser>());
            config.AssertConfigurationIsValid();
        }

        [TestMethod]
        public void EntryToDtoConfiguration()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Entry, EntryDTO>());
            config.AssertConfigurationIsValid();
        }

        [TestMethod]
        public void EntryToModelConfiguration()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<EntryDTO, Entry>());
            config.AssertConfigurationIsValid();
        }

        [TestMethod]
        public void SharedEntryToDtoConfiguration()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<SharedEntry, SharedEntryDTO>());
            config.AssertConfigurationIsValid();
        }

        [TestMethod]
        public void SharedEntryToModelConfiguration()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<SharedEntryDTO, SharedEntry>());
            config.AssertConfigurationIsValid();
        }
    }
}
