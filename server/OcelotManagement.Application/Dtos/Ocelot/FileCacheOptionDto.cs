﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Application.Dtos.Ocelot
{
    public class FileCacheOptionDto
    {
        public int? TtlSeconds { get; set; }

        public string Region { get; set; }
    }
}
