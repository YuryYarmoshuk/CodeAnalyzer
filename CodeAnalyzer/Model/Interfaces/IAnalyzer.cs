// Интерфейс анализатора, для возможности расширения анализатора

using CodeAnalyzer.Model.Entity;

namespace CodeAnalyzer.Model.Interfaces
{
    interface IAnalyzer
    {
        int SourceLineCount { get; set; }
        int PhysicalLineCount { get; set; }
        int CommentLineCount { get; set; }
        int ClassCount { get; set; }
        int MethodCount { get; set; }
        int AvgMethodCount { get; set; }
        int DuplicationCount { get; set; }
        int Cyclomate { get; set; }
        double AvgCyclomate { get; set; }
        double Documentation { get; set; }
        double Duplication { get; set; }
        double Quality { get; set; }
        Shredder Shredder { get; set; }

        void AnalyzerController();
    }
}
