﻿using Microsoft.EntityFrameworkCore;
using ORD.NET.Model.Business;
using ORD.NET.Model.Tables;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORD.NET.DAL
{
    public class GroupsRepository : IGroupsRepository
    {
        private DbContext _ctx;

        public GroupsRepository(DbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<List<Group>> GetGroups(string user)
        {
            var result = from gruppi in _ctx.Set<Gruppi>()
                         where (from rel in _ctx.Set<RelGruppiUtenti>()
                                where rel.Utente == user
                                select 1).Any()
                         select new Group
                         {
                             Id = gruppi.Id,
                             Name = gruppi.Nome
                         };

            return await result.ToListAsync();
        }

        public async Task<bool> SaveGroup(Group group)
        {
            // TODO
            Gruppi entry = await _ctx.Set<Gruppi>().SingleOrDefaultAsync(x => x.Id == group.Id);

            if (entry != null)
            {
                entry.Immagine = group.GroupImage;
                entry.Nome = group.Name;
            }

            return false;
        }
    }
}
