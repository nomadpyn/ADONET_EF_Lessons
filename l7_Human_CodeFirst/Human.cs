using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace l7_Human_CodeFirst
{
    public class Human
    {
        public int Id { get; set; }
        public string Name { get; set; }   
        public string Fname { get; set; }
        public string Gender { get; set; }  
        public int Age { get; set; }

        public Human( string name, string fname, string gender, int age)
        {
            Name = name;
            Fname = fname;
            Gender = gender;
            Age = age;
        }

        public override string ToString()
        {
            return this.Fname + " " + this.Name + "\nПол: " + this.Gender
                + " Возраст " + this.Age;
        }
    }
}
