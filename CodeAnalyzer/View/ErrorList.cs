// Класс отвечающий за вывод ошибок

using System.Windows.Forms;

namespace CodeAnalyzer.Model
{
    public class ErrorList
    {
        /// <summary>
        /// Метод генерации текста ошибки
        /// </summary>
        /// <param name="errorID"></param>
        /// <returns></returns>
        private string ErrorMesEditor(int errorID)
        {
            string errMes = "";

            switch (errorID)
            {
                case 1:
                    errMes = "Select file in the menu 'File'!!!";
                    break;
                case 2:
                    errMes = "Select and analyze file!!!";
                    break;
                default:
                    errMes = "Unknown error!!!";
                    break;
            }

            return errMes;
        }

        /// <summary>
        /// Метод выводящий сообщение об ошибке
        /// </summary>
        /// <param name="errorID">Id ошибки</param>
        public void ErrorWindow(int errorID)
        {
            MessageBox.Show(ErrorMesEditor(errorID), "Error", MessageBoxButtons.OKCancel);
        }

        /// <summary>
        /// Метод выводящий сообщение об ошибке
        /// </summary>
        /// <param name="errorMsg">Текст ошибки</param>
        public void ErrorWindow(string errorMsg)
        {
            MessageBox.Show(errorMsg, "Error", MessageBoxButtons.OKCancel);
        }
    }
}
