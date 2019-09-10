// Класс выводящий информацию из контенеров

using System.Collections.Generic;
using System.Windows.Forms;
using CodeAnalyzer.Model.Entity;

namespace CodeAnalyzer.View
{
    public class ViewInformation
    { 
        /// <summary>
        /// Метод выводящий информацию из контенера
        /// </summary>
        /// <param name="textBox">Поле вывода</param>
        /// <param name="array">Контенер</param>
        public void View(TextBox textBox, List<Code> array)
        {
            textBox.Clear();
            string str = "";

            foreach (Code element in array)
            {
                str += element.ToString();
            }

            textBox.Text = str;
        }
    }
}
