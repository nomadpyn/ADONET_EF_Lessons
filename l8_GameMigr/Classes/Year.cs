using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace l8_GameMigr.Classes
{
    public class Year
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public ICollection<Game> Games { get; set; }

        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}
