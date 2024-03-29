﻿using EF_Model.Entities;
using EncryptedModel.Business.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace EF_Online.Managers
{
    public class UserEntityManager
    {
        /*
        public static async Task addUser(ConnectedUserEntity lue, DbContextOptions<RossignolContextOnline> options = null)
        {
            using (var context = (options == null) ? new RossignolContextOnline() : new RossignolContextOnline(options))
            {
                context.OnlinesUsers.Add(lue);
                context.SaveChanges();
            }
        }*/

        public static async Task removeUser(ConnectedUserEntity cue, DbContextOptions<RossignolContextOnline> options = null)
        {
            using (var context = (options == null) ? new RossignolContextOnline() : new RossignolContextOnline(options))
            {
                context.OnlinesUsers.Remove(cue);
                context.SaveChanges();
            }
        }

        public static async Task updateUser(ConnectedUserEntity cue, DbContextOptions<RossignolContextOnline> options = null)
        {
            using (var context = (options == null) ? new RossignolContextOnline() : new RossignolContextOnline(options))
            {
                context.OnlinesUsers.Update(cue);
                context.SaveChanges();
            }
        }

        public static async Task removeUser(Guid uid, DbContextOptions<RossignolContextOnline> options = null)
        {
            using (var context = (options == null) ? new RossignolContextOnline() : new RossignolContextOnline(options))
            {
                context.OnlinesUsers.Remove(context.OnlinesUsers.First(s => s.Uid == uid.ToString()));
                context.SaveChanges();
            }
        }

        public static int returnUserCount(DbContextOptions<RossignolContextOnline> options = null)
        {
            using (var context = (options == null) ? new RossignolContextOnline() : new RossignolContextOnline(options))
            {
                return context.OnlinesUsers.Count();
            }
        }

        public static async Task RAZ(DbContextOptions<RossignolContextOnline> options = null)
        {
            using (var context = (options == null) ? new RossignolContextOnline() : new RossignolContextOnline(options))
            {
                context.OnlinesUsers.RemoveRange(context.OnlinesUsers);
                context.EntriesSet.RemoveRange(context.EntriesSet);
                context.ReferencedUsers.RemoveRange(context.ReferencedUsers);
                context.SaveChanges();
            }
            using (var context = (options == null) ? new RossignolContextOnline() : new RossignolContextOnline(options))
            {
                context.OnlinesUsers.RemoveRange(context.OnlinesUsers);
                context.EntriesSet.RemoveRange(context.EntriesSet);
                context.ReferencedUsers.RemoveRange(context.ReferencedUsers);
                context.SaveChanges();
            }
        }

    }
}
