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
        public Year Released { get; set; }

        public Game(string title, string description)
        {
            Title = title;
            Description = description;
        }
        public Game(string title, string description, Year released)
        {
            Title = title;
            Description = description;
            Released = released;
        }

        public override string ToString()
        {
            return Title + "\nГод выхода: " + Released  + "\n" + Description;
        }
    }
}
