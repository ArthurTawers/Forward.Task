using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forward.Teplov.TechTask.Engines;
using Forward.Teplov.TechTask.Services;

namespace Forward.Teplov.TechTask.Tests
{
    /// <summary>
    /// Тестирование на перегрев двигателя
    /// </summary>
    internal class Overheating
    {
        TechEngine engine { get; set; }

        IMessageServices messageServices { get; set; }

        public Overheating(TechEngine eng,IMessageServices msg)
        {
            engine = eng;
            messageServices = msg;
        }

        public void StartTest()
        {
            messageServices.SendMessage("Начало теста для " + engine.TypeOfEngine.ToString());
            messageServices.SendMessage("Введите температуру окружающей среды: ");
            double tempEnviroment = messageServices.GetDouble();

            engine.Tengine = tempEnviroment;
            int seconds = 0;

            var engineWork = Task.Factory.StartNew(() => 
            {
                seconds = engine.StartEngine(tempEnviroment);
            });

            int cZero = 0;
            double tempEngine = 0;

            engine.WorkerChanged += () =>
            {
                if (engine.Tengine!=tempEngine)
                {
                    tempEngine = engine.Tengine;
                    messageServices.SendMessage("Температура двигателя: " + tempEngine);
                }
                else
                    cZero++;

                if(cZero==5)
                {
                    engine.IsWork = false;
                }
                    
                if (engine.Tengine>=110)
                    engine.IsWork = false;
                            
            };
            engineWork.Wait();

            if (tempEngine>=110)
                messageServices.SendWarning("Двигатель перегрелся на " + seconds + " секунде");
            else
                messageServices.SendMessage("Двигательне не перегрелся и уровнял температуру, проработав " + seconds + " секунд");

            messageServices.SendOK("Тест завершен!");
        }

    }
}
