using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forward.Teplov.TechTask.Services
{
    /// <summary>
    /// Интерфейс службы сообщений.
    /// </summary>
    internal interface IMessageServices
    {
        /// <summary>
        /// Простое сообщение
        /// </summary>
        /// <param name="message">текст сообщения</param>
        void SendMessage(string message);

        /// <summary>
        /// Выдать ошибку
        /// </summary>
        /// <param name="message">текст ошибки</param>
        void SendError(string message);

        /// <summary>
        /// Сообщение "ОК"
        /// </summary>
        /// <param name="message">текст сообщения</param>
        void SendOK(string message);

        /// <summary>
        /// Сообщение с предупреждением
        /// </summary>
        /// <param name="message">текст сообщения</param>
        void SendWarning(string message);

        double GetDouble();
    }
}
