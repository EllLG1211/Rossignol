﻿using EF_Model.Entities;
using EF_Model.Utils;
using Model.Business.Entries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Online.Managers
{
    public class EntryEntityManager
    {
        /*
        public static async Task addEntry(ProprietaryEntry e, ConnectedUserEntity lue, DbContextOptions<RossignolContextOnline> options)
        {
            using (var context = new RossignolContextOnline(options))
            {
                context.EntriesSet.Add(e.ToEntity(lue));
                context.SaveChanges();
            }
        }

        public static async Task removeEntry(ProprietaryEntry e, ConnectedUserEntity lue, DbContextOptions<RossignolContextOnline> options)
        {
            using (var context = new RossignolContextOnline(options))
            {
                context.EntriesSet.Remove(e.ToEntity(lue));
                context.SaveChanges();
            }
        }

        public static async Task updateEntry(ProprietaryEntry e, ConnectedUserEntity lue, DbContextOptions<RossignolContextOnline> options)
        {
            using (var context = new RossignolContextOnline(options))
            {
                context.EntriesSet.Update(e.ToEntity(lue));
                context.SaveChanges();
            }
        }
        */
        public static async Task updateEntry(EntryEntity e, ConnectedUserEntity lue, DbContextOptions<RossignolContextOnline> options)
        {
            using (var context = new RossignolContextOnline(options))
            {
                context.EntriesSet.Update(e);
                context.SaveChanges();
            }
        }
        /*
        public static async Task removeEntry(EntryEntity e, ConnectedUserEntity lue, DbContextOptions<RossignolContextOnline> options)
        {
            using (var context = new RossignolContextOnline(options))
            {
                if (!context.OnlinesUsers.Contains(lue)) throw new ArgumentException(nameof(lue));
                if (context.EntriesSet.Contains(e))
                {
                    context.EntriesSet.Remove(e);
                    context.OnlinesUsers.First(s => s.Uid == lue.Uid).OwnedEntries.Remove(e);
                    context.SaveChanges();
                }
                else
                {
                    throw new ArgumentException(nameof(e));
                }
            }
        }

        public static async Task clearDB(DbContextOptions<RossignolContextOnline> options)
        {
            using (var context = new RossignolContextOnline(options))
            {
                context.EntriesSet.RemoveRange(context.EntriesSet);
                context.SaveChanges();
            }
        }
        public static async Task RAZ(DbContextOptions<RossignolContextOnline> options = null)
        {
            using (var context = (options == null) ? new RossignolContextOnline() : new RossignolContextOnline(options))
            {
                context.Database.ExecuteSqlRaw("delete from EntriesSet");
                context.SaveChanges();
            }
        }
        */
    }
}
