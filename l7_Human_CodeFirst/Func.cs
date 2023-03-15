using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace l7_Human_CodeFirst
{
    public class Func
    {

        public static void addHuman()
        {
            try
            {
                using (HumanContext db = new HumanContext())
                {
                    db.Humans.Add(createHuman());
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void showHumans()
        {
            try
            {
                using (HumanContext db = new HumanContext())
                {
                    var humans = db.Humans.ToList();

                    foreach (Human human in humans)
                    {
                        Console.WriteLine(human + "\n");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        private static Human createHuman()
        {
            Console.WriteLine("Введите фамилию человека");
            string? fname = Console.ReadLine();
            Console.WriteLine("Введите имя человека");
            string? name = Console.ReadLine();
            Console.WriteLine("Введите возраст человека");
            int age = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Выберите пол человека");
            Console.WriteLine("М - Мужской, Ж - Женский");
            char choise = Char.Parse(Console.ReadLine());
            string gender = "не указан";
            if (choise == 'М')
            {
                gender = "Мужской";
            }
            if (choise == 'Ж')
            {
                gender = "Женский";
            }

            return new Human(name, fname, gender, age);
        }
    }
}
