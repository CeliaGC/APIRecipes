﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Relations
{
    public class Rol_Authorization
    {
        public int Id { get; set; }
        public int IdRol { get; set; }
        public int IdAuthorization { get; set; }
        public bool IsActive { get; set; }
    }
}
