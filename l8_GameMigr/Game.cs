using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace l8_GameMigr
{
    public class Game
    {
        public int Id { get; set;}   

        public string? Title { get; set;}

        public string? Description { get; set;} 

        public Game(string title, string description)
        {
            this.Title = title;
            this.Description = description;
        }

        public override string ToString()
        {
            return this.Title + "\n" + this.Description;
        }
    }
}
