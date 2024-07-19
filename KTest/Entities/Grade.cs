using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Entities
{
    internal class Grade : BaseEntity
    {
        public decimal Point {  get; set; }
        public int AsingmentId { get; set; }
    }
}
