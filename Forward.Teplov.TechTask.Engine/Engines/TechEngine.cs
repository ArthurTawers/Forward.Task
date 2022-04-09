using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forward.Teplov.TechTask.Engines
{
    /// <summary>
    /// Тип двигателя
    /// </summary>
    /// <remarks name="ICE">ICE - внутреннего сгорания;
    /// EM - электромотор</remarks> 
    public enum TypeOfEngine
    {
        ICE,EM
    }

    public abstract class TechEngine
    {
        public TechEngine(double inert, List<(double, double)> torq, double tOvergeating,
            double heartToTorque, double heatingToCrankshaft, double coolingToEnviroment, double tEngine, TypeOfEngine type)
        {
            Tengine = tEngine;
            Inertia = inert;
            Torque = torq;
            TOverheating = tOvergeating;
            HeatingToTorque = heartToTorque;
            HeatingToCrankshaft = heatingToCrankshaft;
            CoolingToEnvironment = coolingToEnviroment;
            Tengine = tEngine;
            TypeOfEngine = type;
            
        }

        /// <summary>
        /// Момент инерции двигателя I
        /// </summary>
        public double Inertia 
        {
            get => inertia;
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Неправильные параметры при указании момента энерции");

                inertia = value;
            }
        }

        /// <summary>
        /// Кусочно-линейная зависимость крутящего момента M, вырабатываемого двигателем, от
        ///скорости вращения коленвала V
        ///</summary>
        ///<returns>Кортеж (M;V), где M - крутящий момент, V - скорости вращения коленвала </returns>
        public List<(double, double)> Torque 
        {
            get => torque; 
            private set
            {
                if (value == null || value.Count == 0)
                    throw new ArgumentNullException("Пустое значение кусочно линейной зависимости или null");

                torque = value;
            }
        }


        /// <summary>
        /// Температура перегрева Tперегрева
        /// </summary>
        public double TOverheating
        {
            get => tOverheating;
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Неправильное значение температуры перегрева");

                tOverheating = value;
            }
        }

        /// <summary>
        /// Коэффициент зависимости скорости нагрева от крутящего момента Hm
        /// </summary>
        public double HeatingToTorque
        {
            get => heatingToTorque;
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Неправильное значение коэффициента зависимости" +
                                                            " скорости нагрева от крутящего момента");
                heatingToTorque = value;
            }
        }

        /// <summary>
        /// Коэффициент зависимости скорости нагрева от скорости вращения коленвала Hv
        /// </summary>
        public double HeatingToCrankshaft
        {
            get => heatingToCrankshaft;
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Неправильное значение коэффициента зависимости" +
                        " скорости нагрева от скорости вращения коленвала Hv ");

                heatingToCrankshaft = value;
            }
        }

        /// <summary>
        /// Коэффициент зависимости скорости охлаждения от температуры двигателя и окружающей среды C
        /// </summary>
        public double CoolingToEnvironment
        {
            get => coolingToEnvironment;
            private set
            {
                if(value < 0)
                    throw new ArgumentOutOfRangeException("Неправильное значение коэффициента зависимости" +
                        "скорости охлаждения от температуры двигателя и окружающей среды С");

                coolingToEnvironment = value;
            }
        }

        /// <summary>
        /// Температура двигателя
        /// </summary>
        public double Tengine
        {
            get => tempEngine;
            set
            {
                tempEngine = value;
            }
        }

        /// <summary>
        /// Тип двигателя
        /// </summary>
        public TypeOfEngine TypeOfEngine
        {
            get => typeOfEngine;
            private set
            {
                typeOfEngine = value;
            }
        }

        /// <summary>
        /// Скорость вращения коленвала
        /// </summary>
        public double CrankshaftSpeed
        {
            get => crankshaftSpeed;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Неправильное значение скорости вращения коленвала!");

                crankshaftSpeed = value;
            }
        }

        /// <summary>
        /// Скорость нагрева двигателя с градусах C/сек
        /// </summary>
        /// <param name="momentIndex">момент времени относительно графика зависимости крутящего момента от скорости</param>
        /// <returns></returns>
        public abstract double HeatingSpeed();

        /// <summary>
        /// Скорость охлаждения двигателя в градусах C/сек
        /// </summary>
        /// <param name="momentIndex">Момент времени относительно графика зависимости крутящего момента от скорости</param>
        /// <param name="tempEnviroment">Температура окружающей среды</param>
        /// <returns></returns>
        public abstract double CoolingRateSpeed( double tempEnviroment);

        /// <summary>
        /// Кусочно-линейная зависимость крутящего момента M, вырабатываемого двигателем, от
        ///скорости вращения коленвала V
        /// </summary>
        /// <param name="V">скорость вращения коленвала V</param>
        /// <returns>крутящий момента M</returns>
        public abstract double TorqueGetM(double V);

        /// <summary>
        /// Старт работы двигателя
        /// </summary>
        /// <param name="tempEnviroment">Температура окружающей среды</param>
        /// <returns>Время работы двигателя(секунд)</returns>
        public abstract int StartEngine(double tempEnviroment);

        /// <summary>
        /// Состояние двигателя
        /// </summary>
        public bool IsWork { get => worker;  set { worker = value; } }

        /// <summary>
        /// Останавливает работу двигателя
        /// </summary>
        public void WorkStop()
        {
            worker = false;
        }

        public delegate void WorkerChangedHandler();

        /// <summary>
        /// Изменения в процессе работы двигателя
        /// </summary>
        public WorkerChangedHandler WorkerChanged;

        #region private fields
        double tempEngine;
        double inertia;
        List<(double, double)> torque;
        double tOverheating;
        double heatingToTorque;
        double heatingToCrankshaft;
        double coolingToEnvironment;
        TypeOfEngine typeOfEngine;
        double crankshaftSpeed;
        bool worker;
        #endregion
    }
}
