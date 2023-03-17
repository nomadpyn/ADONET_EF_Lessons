using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace l8_Game_Migration
{
    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        public Game(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public override string ToString()
        {
            return this.Title +"\n" + this.Description;
        }
    }
}
