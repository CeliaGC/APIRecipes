﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class UserItem
    {
        public int Id { get; set; }
        public int IdRol { get; set; }
       
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public DateTime InsertDate { get; set; }
        public bool IsActive { get; set; }
        public string EncryptedPassword { get; set; }
        public string EncryptedToken { get; set; }
        public DateTime TokenExpireDate { get; set; }
    }
}
