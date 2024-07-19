using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Entities
{
    internal class Group : BaseEntity
    {
        public string Name { get; set; }
        public int Limit { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public ICollection<Student> Students { get; set; }
        public ICollection<Asignment> Asigments { get; set; }
    }
}
