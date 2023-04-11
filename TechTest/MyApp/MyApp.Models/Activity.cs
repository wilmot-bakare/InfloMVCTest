using MyApp.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Models
{
    public class Activity : ModelBase
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}
