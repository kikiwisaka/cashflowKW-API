﻿using KW.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Infrastructure
{
    public class EntityFrameworkConfiguration : IDatabaseConfiguration
    {
        public void Initialise()
        {
            Database.SetInitializer<DatabaseContext>(new DatabaseInitializer());
            var ctx = new DatabaseContext();
            ctx.Database.Initialize(false);
        }
    }
}
