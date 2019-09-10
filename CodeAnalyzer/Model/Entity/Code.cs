// Класс-сущность, который содержит строковое представление кода

using System;
using System.Collections.Generic;
using System.Text;

namespace CodeAnalyzer.Model.Entity
{
    public class Code
    {
        public List<string> GetLines { get; }

        public Code()
        {
            GetLines = new List<string>();  // получение листа строк
        }

        /// <summary>
        /// Добавление строки в массив
        /// </summary>
        /// <param name="line">Строка кода</param>
        public void Add(string line)
        {
            GetLines.Add(line); // заносит строку в лист
        }

        /// <summary>
        /// Возвращает количество строк в массиве
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            return GetLines.Count;  // получение количества элементов листа
        }

        /// <summary>
        /// Возвращает весь массив в строковом виде
        /// </summary>
        /// <returns></returns>
        public override string ToString()   //вывод листа
        {
            StringBuilder sb = new StringBuilder();
            
            foreach (string line in GetLines)
            {
                sb.AppendLine(line);
            }
            
            sb.AppendLine("\n");

            return sb.ToString();
        }
    }
}
