using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Entities
{
    internal class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public bool IsDeleted { get; set; }
        public ICollection<Group> Groups { get; set; }
        public Teacher()
        {
            
            IsDeleted = false;
        }
    }
}
