// Абстрактный класс, который анализирует код

namespace CodeAnalyzer.Model.Entity
{
    public abstract class Analyzer : Interfaces.IAnalyzer
    {
        /* Метрики:
         * PhysicalLineCount - количество физических строк
         * SourceLineCount - количество строк кода
         * CommentLineCount - количество коментариев
         * ClassCount - количество классов
         * MethodCount - количество методов
         * AvgMethodCount - среднее количество строк на метод
         * DuplicationCount - количество повторений
         * Cyclomate - сложность всего модуля
         * AvgCyclomate - средняя сложность методов
         * Documentation - процент покрытие кода коментариями
         * Duplication - процент повторений в коде
         * Quality - итоговая оценка результатов */
        

        public int PhysicalLineCount { get; set; }
        public int SourceLineCount { get; set; }
        public int CommentLineCount { get; set; }
        public int ClassCount { get; set; }
        public int MethodCount { get; set; }
        public int AvgMethodCount { get; set; }
        public int DuplicationCount { get; set; }
        public int Cyclomate { get; set; }
        public double AvgCyclomate { get; set; }
        public double Documentation { get; set; }
        public double Duplication { get; set; }
        public double Quality { get; set; }

        public Shredder Shredder { get; set; }

        /// <summary>
        /// Метод подсчета физических строк
        /// </summary>
        protected abstract void PhysicalRowCounter();

        /// <summary>
        /// Метод подсчета строчек кода
        /// </summary>
        protected abstract void SourceRowCounter();

        /// <summary>
        /// Метод подсчета количества классов
        /// </summary>
        protected abstract void ClassCounter();

        /// <summary>
        /// Метод подсчета количества методов
        /// </summary>
        protected abstract void MethodCounter();

        /// <summary>
        /// Метод подсчета строк коментариев
        /// </summary>
        protected abstract void CommentCounter();

        /// <summary>
        /// Метод подсчета покрытия кода коментариями
        /// </summary>
        protected abstract void DocumentationCalc();

        /// <summary>
        /// Метод подсчета среднего количества строк на метод
        /// </summary>
        protected abstract void AvgMethodLineCalc();

        /// <summary>
        /// Метод подсчета сложности модуля
        /// </summary>
        protected abstract void CyclomateCount();

        /// <summary>
        /// Метод подсчета средней сложности методов
        /// </summary>
        protected abstract void AvgCyclomateCalc();

        /// <summary>
        /// Метод подсчета количества повторений строк в коде
        /// </summary>
        protected abstract void DuplicationCounter();

        /// <summary>
        /// Метод подсчета процентного соотношения дублирования ко всему коду
        /// </summary>
        protected abstract void DuplicationCalc();

        /// <summary>
        /// Метод подсчета общей оценки качества модуля
        /// </summary>
        protected abstract void TotalQuality();

        /// <summary>
        /// Контроллер анализатора
        /// </summary>
        public abstract void AnalyzerController();
    }
}
