using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaine
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // 'admin', 'instructeur', 'étudiant'
    }
}
