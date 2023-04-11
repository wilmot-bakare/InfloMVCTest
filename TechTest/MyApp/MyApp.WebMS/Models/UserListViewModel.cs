using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyApp.WebMS.Models
{
    public class UserListViewModel
    {
        public IList<UserListItemViewModel> Items { get; set; } 
    }

    public class UserListItemViewModel
    {
        public long Id { get; set; }
        [Required]
        public string Forename { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }

        public bool IsActive { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
    }
}