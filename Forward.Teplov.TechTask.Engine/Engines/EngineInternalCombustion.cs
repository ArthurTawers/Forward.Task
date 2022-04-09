using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forward.Teplov.TechTask.Engines
{
    public class EngineInternalCombustion : TechEngine
    {
        /// <summary>
        /// Двигатель внутреннего сгорания
        /// </summary>
        /// <param name="inert">Момент инерции двигателя I</param>
        /// <param name="torq"> Кусочно-линейная зависимость крутящего момента M, вырабатываемого двигателем, от
        ///скорости вращения коленвала V</param>
        /// <param name="tOvergeating">Температура перегрева Tперегрева</param>
        /// <param name="heartToTorque">Коэффициент зависимости скорости нагрева от крутящего момента Hm</param>
        /// <param name="heartingToCrankshaft">Коэффициент зависимости скорости нагрева от скорости вращения коленвала Hv</param>
        /// <param name="coolingToEnviroment">Коэффициент зависимости скорости охлаждения от температуры двигателя и окружающей среды C</param>
        /// <param name="tEngine">Температура двигателя</param>
        /// <param name="type">Тип двигателя</param>
        public EngineInternalCombustion(double inert, List<(double, double)> torq, double tOvergeating,
            double heartToTorque, double heartingToCrankshaft, double coolingToEnviroment, double tEngine)
            : base(inert, torq, tOvergeating, heartToTorque, heartingToCrankshaft, coolingToEnviroment, tEngine, TypeOfEngine.ICE) { }

        override public double HeatingSpeed()
        {
            double V = CrankshaftSpeed;
            double M = TorqueGetM(V);

            return M * HeatingToTorque + Math.Pow(V, 2) * HeatingToCrankshaft;
        }

        override public double CoolingRateSpeed(double tempEnviroment)
        {
            return CoolingToEnvironment * (tempEnviroment - Tengine);
        }

        public override double TorqueGetM(double V)
        {
            int index = 0; double diff = V - Torque[0].Item2;

            for (int i = 1; i<Torque.Count; i++)
            {
                if (Torque[i].Item2 - V<diff)
                {
                    diff = V - Torque[i].Item2;
                    index = i;
                }
            }

            return Torque[index].Item1;
        }

        public override int StartEngine(double tempEnviroment)
        {
            IsWork = true;
            CrankshaftSpeed = 0;
            int count = 0;

            while(IsWork)
            {
                CrankshaftSpeed+=TorqueGetM(CrankshaftSpeed) / base.Inertia;
                Tengine+=HeatingSpeed();
                Tengine+=CoolingRateSpeed(tempEnviroment);

                WorkerChanged.Invoke();
                System.Threading.Thread.Sleep(7);

                count++;
            }

            return count;
        }       
    }
}
