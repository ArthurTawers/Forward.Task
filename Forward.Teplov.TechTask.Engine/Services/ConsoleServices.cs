using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forward.Teplov.TechTask.Services
{
    internal class ConsoleServices : IMessageServices
    {
        public double GetDouble()
        {
            double ret;
            while (!double.TryParse(Console.ReadLine(), out ret)) { SendError("Некорректное значение!"); }
            return ret;
        }

        public void SendError(string message)
        {
            Console.WriteLine($"Ошибка! {message}");
        }

        public void SendMessage(string message)
        {
            Console.WriteLine($"{message}");
        }

        public void SendOK(string message)
        {
            Console.WriteLine($"Успешно! {message}");
        }

        public void SendWarning(string message)
        {
            Console.WriteLine($"Предупреждение! {message}");
        }
    }
}
