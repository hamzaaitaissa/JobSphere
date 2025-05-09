﻿using JobSphere.ENUMS;
using System.ComponentModel.DataAnnotations;

namespace JobSphere.DTOs.Users
{
    public class UserDto
    {
        
        [Required]
        [StringLength(50)]
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Role { get; set; }
        //[Required]
        //[MinLength(8)]
        //public string HashedPassword { get; set; }
        //public DateTime CreationTime { get; set; }
    }
}
