using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace l7_Human_CodeFirst
{
    public class Menu
    {
        public void Start()
        {
            bool cont = true;
            do {
                Console.WriteLine("Что вы хотите сделать?");
                Console.WriteLine("1- Показать список людей\n2 - Добавить человека из списка");
                Console.WriteLine("3 - Редактировать человека в списке\n4 - Удалить человека из списка\n0 - Выход");
                if(Int32.TryParse(Console.ReadLine(), out int choise))
                {

                    Console.Clear();
                    switch (choise)
                    {
                        case 1:
                            {
                                Func.showHumans();
                                break;
                            }
                        case 2:
                            {
                                Func.addHuman();
                                break;
                            }
                        case 3:
                            {
                                this.updateHuman();
                                break;
                            }
                        case 4:
                            {
                                this.deleteHuman();
                                break;
                            }
                        case 0:
                            {
                                Console.WriteLine("До свидания!");
                                cont = false;
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Вы не выбрали");
                                break;
                            }

                    }
                }
            }
            while (cont == true);    
        }
        
        private void updateHuman()
        {
            Func.showHumans();
            Console.WriteLine("Выберите кого ходите изменить по id");
            if(Int32.TryParse(Console.ReadLine(), out int choise_id))
            {
                Func.updateHuman(choise_id);
            }
            else
            {
                Console.WriteLine("Вы неправильно ввели Id");
            }
        }

        private void deleteHuman()
        {
            Func.showHumans();
            Console.WriteLine("Выберите кого ходите удалить по id");
            if (Int32.TryParse(Console.ReadLine(), out int choise_id))
            {
                Func.deleteHuman(choise_id);
            }
            else
            {
                Console.WriteLine("Вы неправильно ввели Id");
            }
        }
    }
}
