﻿using Microsoft.AspNetCore.Identity;
using Finance.Core.Entities;
using Finance.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(250, ErrorMessage = "Name cannot be more than 250 charachters")]
        public string?FirstName { get; set; }
        public string?LastName { get; set; }
        public string ?ImgProfilePath { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<ApplicationRole> UserRoles { get; set; }

    }
}
