﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Domain.Entities.MongoDbEntities
{
    public class UserMongoDb
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
