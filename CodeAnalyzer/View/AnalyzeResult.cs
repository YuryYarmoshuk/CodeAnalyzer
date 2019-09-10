// класс выводящий полученные данный из анализатора

using System;
using System.Text;
using CodeAnalyzer.Model.Entity;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CodeAnalyzer.Model.Logic
{
    class AnalyzeResult
    {
        private readonly StringBuilder sb;  // строковое представление результата
        public Analyzer Analyzer { get; set; }

        /// <summary>
        /// Конструктор с параметром
        /// </summary>
        /// <param name="analyzer"></param>
        public AnalyzeResult(Analyzer analyzer)
        {
            sb = new StringBuilder();
            Analyzer = analyzer;
        }

        /// <summary>
        /// Метод сохранение в файл
        /// </summary>
        /// <param name="path">Принимает путь к файлу</param>
        public void Save(string path)
        {
            if (sb.Length != 0) // если анализатор проанализировал код,
                                // т.е. если методы, которые берут данные из анализаторы,
                                // записали в строковое представление полученные данные
            {
                try
                {
                    File.WriteAllText(path, sb.ToString()); // запись результата в файл
                    MessageBox.Show("Save access", "Access", MessageBoxButtons.OK); // сообщение об успешном сохранении
                }
                catch (Exception)
                {
                    MessageBox.Show("Save faild", "Fail", MessageBoxButtons.OK); // сообщение об проваленом сохранении
                }
            }
        }
        
        /// <summary>
        /// Метод выводящий преобразующий диаграмму в круговую или гистограмму
        /// </summary>
        /// <param name="chart">Диаграмма</param>
        /// <param name="id">Вид диаграммы (1 - круговая, любой другой - гистограмма)</param>
        public void DiagramDraw(Chart chart, int id)
        {
            switch (id)
            {
                case 1: // круговая диаграмма
                    {
                        chart.Series.Clear(); // удаляем существующие диаграммы
                        
                        chart.Visible = true;   // делаем видимой, если вдруг она скрыта
                       
                        chart.Series.Add(new Series("Common metrics")   // создание диаграммы
                        {
                            ChartType = SeriesChartType.Pie // указываем тип - круговая
                        });

                        int[] yValues = { Analyzer.SourceLineCount, Analyzer.CommentLineCount };    // значения для Х
                        string[] xValues = { String.Format("Source line - {0}", Analyzer.SourceLineCount),
                                            String.Format("Comment line  - {0}", Analyzer.CommentLineCount) };  // значения для У
                        chart.Series["Common metrics"].Points.DataBindXY(xValues, yValues); // отрисовка

                        chart.ChartAreas[0].Area3DStyle.Enable3D = true; // переключаем вид в 3D
                        break;
                    }
                default: // гистограмма
                    {
                        chart.Series.Clear();   // удаляем существующие диаграммы

                        chart.Visible = true;   // делаем видимой, если вдруг она скрыта

                        chart.Series.Add("Custom metrics"); // создаем колонки метрик
                        chart.Series[0].ChartType = SeriesChartType.Column;

                        chart.Series.Add("Physical line");  // создаем колонки физических строк
                        chart.Series[1].ChartType = SeriesChartType.Column;

                        // указываем значения У для колонки соответствующей метрики ввиде (подпись колонки, значение)
                        chart.Series["Custom metrics"].Points.AddXY(String.Format("AvgMethod count - {0}", Analyzer.AvgMethodCount), Analyzer.AvgMethodCount);
                        chart.Series["Custom metrics"].Points.AddXY(String.Format("Duplication count - {0}", Analyzer.DuplicationCount), Analyzer.DuplicationCount);
                        chart.Series["Custom metrics"].Points.AddXY(String.Format("Cyclomatic - {0}", Analyzer.Cyclomate), Analyzer.Cyclomate);
                        chart.Series["Custom metrics"].Points.AddXY(String.Format("AvgCyclomatic count - {0}", Analyzer.AvgCyclomate), Analyzer.AvgCyclomate);
                        chart.Series["Custom metrics"].Points.AddXY(String.Format("Documentation - {0:0.##}", 
                                                    Analyzer.Documentation), Analyzer.Documentation * Analyzer.PhysicalLineCount / 100);
                        chart.Series["Custom metrics"].Points.AddXY(String.Format("Duplication - {0:0.##}", Analyzer.Duplication), 
                                                    Analyzer.Duplication * Analyzer.PhysicalLineCount / 100);

                        // указываем значения У для колонки физических линий ввиде (подпись колонки, значение)
                        chart.Series["Physical line"].Points.AddXY("", Analyzer.PhysicalLineCount);
                        chart.Series["Physical line"].Points.AddXY("", Analyzer.PhysicalLineCount);
                        chart.Series["Physical line"].Points.AddXY("", Analyzer.PhysicalLineCount);
                        chart.Series["Physical line"].Points.AddXY("", Analyzer.PhysicalLineCount);
                        chart.Series["Physical line"].Points.AddXY("", Analyzer.PhysicalLineCount);
                        chart.Series["Physical line"].Points.AddXY("", Analyzer.PhysicalLineCount);

                        break;
                    }
            }

        }

        /// <summary>
        /// Метод для записи результата базовых метрик
        /// </summary>
        public void CommonMetrics()
        {
            sb.AppendLine(PhisicalLineView());
            sb.AppendLine(SourceLineView());
            sb.AppendLine(CommentLineView());
            sb.AppendLine(ClassCountView());
            sb.AppendLine(MethodCountView());
        }

        /// <summary>
        /// Запись результата в текстовом варианте
        /// </summary>
        /// <param name="metricName">Название метрики</param>
        /// <param name="count">Цифровое значение метрики</param>
        /// <returns></returns>
        private string TextMetric(string metricName, string count)
        {
            return String.Format("Metric : {0} - {1}", metricName, count);
        }

        /// <summary>
        /// Вывод физических линий кода
        /// </summary>
        /// <returns></returns>
        private string PhisicalLineView()
        {
            return TextMetric("Phisical line (includ source and comment line)", Analyzer.PhysicalLineCount.ToString());
        }

        /// <summary>
        /// Вывод программных строк кода
        /// </summary>
        /// <returns></returns>
        private string SourceLineView()
        {
            return TextMetric("Source line (includ only source line)", Analyzer.SourceLineCount.ToString());
        }

        /// <summary>
        /// Вывод линий коментариев
        /// </summary>
        /// <returns></returns>
        private string CommentLineView()
        {
            return TextMetric("Comment line (includ only comment line)", Analyzer.CommentLineCount.ToString());
        }

        /// <summary>
        /// Вывод количества классов
        /// </summary>
        /// <returns></returns>
        private string ClassCountView()
        {
            return TextMetric("Class count (number of classes in the modul)", Analyzer.ClassCount.ToString());
        }

        /// <summary>
        /// Вывод количества методов
        /// </summary>
        /// <returns></returns>
        private string MethodCountView()
        {
            return TextMetric("Method count (number of methods in the modul)", Analyzer.MethodCount.ToString());
        }

        /// <summary>
        /// Вывод всех кастомных метрик
        /// </summary>
        public void CustomMetrics()
        {
            sb.AppendLine(DocumentationView());
            sb.AppendLine(AvgMethodLineView());
            sb.AppendLine(CyclomaticView());
            sb.AppendLine(AvgCyclomaticView());
            sb.AppendLine(DuplicationCountView());
            sb.AppendLine(DuplicationView());
        }

        /// <summary>
        /// Вывод процента покрытия кода коментариями
        /// </summary>
        /// <returns></returns>
        private string DocumentationView()
        {
            return TextMetric("Documentation (relation comment to physical lines, higher are better)", 
                String.Format("{0:0.##}%",Analyzer.Documentation));
        }

        /// <summary>
        /// Вывод среднего количества линий на метод
        /// </summary>
        /// <returns></returns>
        private string AvgMethodLineView()
        {
            return TextMetric("AvgMethodLine (average number of method lines, lower are better)", 
                String.Format("{0} lines", Analyzer.AvgMethodCount));
        }

        /// <summary>
        /// Вывод цикломатического числа для модуля
        /// </summary>
        /// <returns></returns>
        private string CyclomaticView()
        {
            return TextMetric("Cyclomatic (total complexity of modul, lower are better)", 
                String.Format("{0}", Analyzer.Cyclomate));
        }

        /// <summary>
        /// Вывод среднего цикломатического числа для методов
        /// </summary>
        /// <returns></returns>
        private string AvgCyclomaticView()
        {
            return TextMetric("AvgCyclomatic (average complexity of all method, lower are better)",
                String.Format("{0}", Math.Ceiling(Analyzer.AvgCyclomate)));
        }

        /// <summary>
        /// Вывод количества повторений
        /// </summary>
        /// <returns></returns>
        private string DuplicationCountView()
        {
            return TextMetric("DuplicationCount (repetition lines, lower are better)", 
                String.Format("{0}", Analyzer.DuplicationCount));
        }

        /// <summary>
        /// Вывод процента повторений в коде
        /// </summary>
        /// <returns></returns>
        private string DuplicationView()
        {
            return TextMetric("Duplication (relation repetition lines to physical lines, lower are better)",
                String.Format("{0:0.##}%", Analyzer.Duplication));
        }

        /// <summary>
        /// Вывод финальной оценки модуля
        /// </summary>
        /// <returns></returns>
        private string TotalQuality()
        {
            return String.Format("Assessment of quality is {0} this is {1}", Analyzer.Quality, Assessment());
        }

        /// <summary>
        /// Строковое представление оценки
        /// </summary>
        /// <returns></returns>
        private string Assessment()
        {
            if (Analyzer.Quality == 10) // наивысшая оценка
            {
                return "very high quality";
            }
            else if (Analyzer.Quality >= 7 && Analyzer.Quality < 10)    // высокая оценка
            {
                return "high quality";
            }
            else if (Analyzer.Quality >= 4 && Analyzer.Quality < 7) //средняя оценка
            {
                return "medium quality";
            }

            return "low quality";   // низкая оценка
        }

        /// <summary>
        /// Вывод базовых и кастомных метрик, ли конкретной метрки
        /// </summary>
        /// <param name="metricId">Ид метрики 1..6 или любое другое для вывода всех</param>
        /// <returns></returns>
        public string ViewResult(int metricId)

        {
            sb.Clear();

            sb.AppendLine("File name: " + Analyzer.Shredder.FileName);
            sb.AppendLine("");

            sb.AppendLine("\t---===Common metrics===---");
            CommonMetrics();
            sb.AppendLine("");

            switch (metricId)
            {
                case 1:
                    {
                        sb.AppendLine("\t---===Custom metrics===---");
                        sb.AppendLine(DocumentationView());
                        break;
                    }
                case 2:
                    {
                        sb.AppendLine("\t---===Custom metrics===---");
                        sb.AppendLine(AvgMethodLineView());
                        break;
                    }
                case 3:
                    {
                        sb.AppendLine("\t---===Custom metrics===---");
                        sb.AppendLine(CyclomaticView());
                        break;
                    }
                case 4:
                    {
                        sb.AppendLine("\t---===Custom metrics===---");
                        sb.AppendLine(AvgCyclomaticView());
                        break;
                    }
                case 5:
                    {
                        sb.AppendLine("\t---===Custom metrics===---");
                        sb.AppendLine(DuplicationCountView());
                        break;
                    }
                case 6:
                    {
                        sb.AppendLine("\t---===Custom metrics===---");
                        sb.AppendLine(DuplicationView());
                        break;
                    }
                default:
                    {
                        sb.AppendLine("\t---===Custom metrics===---");
                        CustomMetrics();
                        break;
                    }
            }

            sb.AppendLine("");
            sb.AppendLine("\t---===Total quality===---");
            sb.AppendLine(TotalQuality());

            return sb.ToString();
        }
    }
}
