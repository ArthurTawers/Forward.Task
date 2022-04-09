using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forward.Teplov.TechTask.Services;
using Forward.Teplov.TechTask.Engines;
using Forward.Teplov.TechTask.Tests;


namespace Forward.Teplov.TechTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                List<(double, double)> functionVtoM = new List<(double, double)>() { (20, 0), (75, 75), (100, 150), (105, 200), (75, 250), (0, 300) };
                EngineInternalCombustion engine = new EngineInternalCombustion(10, functionVtoM, 110, 0.01, 0.0001, 0.1, 30);

                ConsoleServices cs = new ConsoleServices();
                Overheating overheating = new Overheating(engine, cs);
                overheating.StartTest();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine();
        }
    }
}
