-/* Курсовой проект
 * По дисциплине: Операционные системы
 * Тема: Производительность кода
 * Разработал: Ярмошук Юрий Михайлович
 * 
 * Цель: анализировать программный код по различным метрикам
 *       "Плохой код, анализирующий плохой код"
 * Для анализа был выбран язык программирования Java */

using System;
using System.Windows.Forms;
using CodeAnalyzer.Model;
using CodeAnalyzer.View;
using CodeAnalyzer.Model.Logic;

namespace CodeAnalyzer
{
    public partial class CodeAnalyzer : Form
    {
        string _pathLoad = "";  // путь к файлу для открытия
        string _pathSave = "";  // путь к файлу для сохранения
        ErrorList _errorList = null;    // объект отвечающий за вывод ошибок
        ViewInformation _viewInfo = null;   // объект отвечающий за вывод контенеров с кодом
        ShredderJava shredder = null;   // объект отвечающий за сепорацию кода на участки
        AnalyzerJava analyzer = null;   // объект отвечающий за 
        AnalyzeResult analyzeResult = null; // объект отвечающий за вывод полученных анализатором результатов

        public CodeAnalyzer()
        {
            InitializeComponent();

            _errorList = new ErrorList();   
            _viewInfo = new ViewInformation();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();    //закрытие приложения
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_pathLoad))   // если файл был выбран
            {
                shredder = new ShredderJava();  
                shredder.Path = _pathLoad;  // передаем объекту путь к файлу который нужно "нарезать"
                shredder.ShredderControll(); // "нарезание" кода на части

                if (!shredder.ErrorFinde)
                {
                    analyzer = new AnalyzerJava();
                    analyzer.Shredder = shredder;   // передаем в анализатор контенер кода
                    analyzer.AnalyzerController();  // анализируем весь контенер

                    analyzeResult = new AnalyzeResult(analyzer);

                    ViewShredder(); // выводим контенер

                    ViewLog();  // выводим результат
                    StampLoad();    // ставим "штамп" качества

                    analyzeResult.DiagramDraw(chart1, 1);   //строим диаграму
                    analyzeResult.DiagramDraw(chart2, 2);   //строим диаграму
                }
                else
                {
                    _errorList.ErrorWindow(-1); // отображение ошибки
                }
            }
            else
            {
                _errorList.ErrorWindow(1);  //отображение ошибки
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFile.FileName = "";
            if (openFile.ShowDialog() == DialogResult.OK)   //открывает диалоговое окно для выбора файла
            {
                _pathLoad = openFile.FileName;
                MessageBox.Show("Success", "File load success", MessageBoxButtons.OK);
            }
        }

        private void metricsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Показывает форму с таблицей, в которой приведены все метрики
            Metrics _metricsImage = new Metrics();
            _metricsImage.Show();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Функция для вывода контенеров из класса коллектора
        /// </summary>
        private void ViewShredder()
        {
            if (shredder != null)
            {
                if (radioButton1.Checked)
                {
                    _viewInfo.View(codeTextBox, shredder.CodeArr);  //вывод контерена всего кода
                }
                else if (radioButton2.Checked)
                {
                    _viewInfo.View(codeTextBox, shredder.ClassArr); //вывод контенера классов
                }
                else if (radioButton3.Checked)
                {
                    _viewInfo.View(codeTextBox, shredder.MethArr);  //вывод контенера методов
                }
            }
        }

        /// <summary>
        /// Функция для вывода результатов полученных анализатором
        /// </summary>
        private void ViewLog()
        {
            if (analyzeResult != null)
            {
                logTextBox.Text = "";
                logTextBox.Text = analyzeResult.ViewResult(MetricId());
            }
        }

        /// <summary>
        /// Фунция для получения активного переключателя. Индекс активного по порядку сверху-вниз (1, 2, ...) All - (-1) 
        /// </summary>
        /// <returns></returns>
        public int MetricId()
        {
            if (metric1.Checked)
            {
                return 1;
            }
            if (metric2.Checked)
            {
                return 2;
            }
            if (metric3.Checked)
            {
                return 3;
            }
            if (metric4.Checked)
            {
                return 4;
            }
            if (metric5.Checked)
            {
                return 5;
            }
            if (metric6.Checked)
            {
                return 6;
            }
            return -1;  //Если активирован All
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            ViewShredder();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            ViewShredder();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            ViewShredder();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            ViewShredder();
        }

        private void all_CheckedChanged(object sender, EventArgs e)
        {
            ViewLog();
        }

        private void metric6_CheckedChanged(object sender, EventArgs e)
        {
            ViewLog();
        }

        private void saveLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (analyzeResult != null)  //проверяем создан ли анализатор результата
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)    //по нажатию принять сохраняем
                {
                    _pathSave = saveFileDialog1.FileName;   //получаем из диалогого окна путь
                    analyzeResult.Save(_pathSave);  //сохраняем в файл
                }
            }
            else
            {
                _errorList.ErrorWindow(2);  //вывод ошибки
            }
        }

        /* Полностью копирует saveLogToolStripMenuItem_Click
         * Сохраняет по такому же порядку
         */
        private void button3_Click(object sender, EventArgs e)
        {
            if (analyzeResult != null)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    _pathSave = saveFileDialog1.FileName;
                    analyzeResult.Save(_pathSave);
                }
            }
            else
            {
                _errorList.ErrorWindow(2);
            }
        }

        private void all_CheckedChanged_1(object sender, EventArgs e)
        {
            ViewLog();
        }

        private void metric1_CheckedChanged_1(object sender, EventArgs e)
        {
            ViewLog();
        }

        private void metric2_CheckedChanged_1(object sender, EventArgs e)
        {
            ViewLog();
        }

        private void metric3_CheckedChanged_1(object sender, EventArgs e)
        {
            ViewLog();
        }

        private void metric4_CheckedChanged_1(object sender, EventArgs e)
        {
            ViewLog();
        }

        private void metric5_CheckedChanged_1(object sender, EventArgs e)
        {
            ViewLog();
        }

        /// <summary>
        /// Функция, которая устанавливает избражение со штампом в зависимости от полученной анализатором оценки
        /// </summary>
        private void StampLoad()
        {
            if (analyzer != null)
            {
                if (analyzer.Quality == 10)
                {
                    pictureBox1.Image = Properties.Resources.bestQualityRe; // наивысшая оценка
                }
                else if (analyzer.Quality >= 7 && analyzer.Quality < 10)    
                {
                    pictureBox1.Image = Properties.Resources.highQualityRe; //высокая оценка
                }
                else if (analyzer.Quality >= 4 && analyzer.Quality < 7)
                {
                    pictureBox1.Image = Properties.Resources.veryGoodRe;    //средняя оценка
                }
                else if (analyzer.Quality < 4)
                {
                    pictureBox1.Image = Properties.Resources.badQualityRe;  //низкая оценка
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Show();
        }
    }
}
