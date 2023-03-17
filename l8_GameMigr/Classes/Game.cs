using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace l8_GameMigr.Classes
{
    public class Game
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }
        public Year Year { get; set; }

        public override string ToString()
        {
            return Title + "\nГод выхода: " + Year  + "\n" + Description;
        }
    }
}
