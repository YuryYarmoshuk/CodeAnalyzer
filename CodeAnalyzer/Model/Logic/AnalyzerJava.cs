using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeAnalyzer.Model.Entity;

namespace CodeAnalyzer.Model.Logic
{
    public class AnalyzerJava : Analyzer
    {
        protected override void PhysicalRowCounter()
        {
            PhysicalLineCount = Shredder.CodeArr[0].GetCount();
        }

        protected override void SourceRowCounter()
        {
            bool multyCom = false;
            bool emptyStr = false;
            string checkLine = "";

            SourceLineCount = 0;

            foreach (string line in Shredder.CodeArr[0].GetLines)
            {
                emptyStr = false;

                if (line.IndexOf("//") != -1)
                {
                    checkLine = line.Remove(line.IndexOf("//"));

                    if (checkLine.Trim() == "")
                    {
                        emptyStr = true;
                    }
                }

                if (line.IndexOf("/*") != -1)
                {
                    multyCom = true;
                }
                
                if (!multyCom && !emptyStr)
                {
                    SourceLineCount += 1;
                }

                if (line.IndexOf("*/") != -1)
                {
                    multyCom = false;
                }
            }
        }

        protected override void CommentCounter()
        {
            Code lines = Shredder.CodeArr[0];

            CommentLineCount = 0;

            bool multyComent = false;

            foreach (string line in lines.GetLines)
            {
                if (line.IndexOf("/*") != -1)
                {
                    multyComent = true;
                }

                if (!multyComent)
                {
                    if (line.IndexOf("//") != -1)
                    {
                        CommentLineCount += 1;
                    }
                }

                if (multyComent)
                {
                    CommentLineCount += 1;
                }

                if (line.IndexOf("*/") != -1)
                {
                    multyComent = false;
                }
            }
        }

        protected override void ClassCounter()
        {
            ClassCount = Shredder.ClassArr.Count();
        }

        protected override void MethodCounter()
        {
            MethodCount = Shredder.MethArr.Count();
        }

        protected override void DocumentationCalc()
        {
            Documentation = Convert.ToDouble(CommentLineCount) / Convert.ToDouble(PhysicalLineCount) * 100;
        }

        protected override void AvgMethodLineCalc()
        {
            int summ = 0;

            foreach (Code method in Shredder.MethArr)
            {
                summ += method.GetCount();
            }

            AvgMethodCount = summ / MethodCount;
        }

        protected override void CyclomateCount()
        {
            int summCyclomatic = 0;

            foreach(Code method in Shredder.MethArr)
            {
                summCyclomatic += CyclomaticForMethod(method);
            }

            Cyclomate = summCyclomatic;
        }

        protected override void AvgCyclomateCalc()
        {
            AvgCyclomate = Math.Floor(Convert.ToDouble(Cyclomate) / Convert.ToDouble(MethodCount));
        }

        private int CyclomaticForMethod(Code method)
        {
            int cyclomatic = 1;

            foreach (string line in method.GetLines)
            {
                if (line.IndexOf("if") != -1 || line.IndexOf("else if") != -1 || line.IndexOf("case") != -1
                    || line.IndexOf("default") != -1 || line.IndexOf("while") != -1 || line.IndexOf("for") != -1
                    || line.IndexOf("&&") != -1 || line.IndexOf("||") != -1)
                {
                    cyclomatic++;
                }
            }

            return cyclomatic;
        }

        protected override void DuplicationCounter()
        {
            DuplicationCount = 0;
            string changeStr;
            List<string> set = new List<string>();

            foreach (string line in Shredder.CodeArr[0].GetLines)
            {
                if (!set.Contains(line))
                {
                    changeStr = line.Trim();

                    if (changeStr.IndexOf("{") != 0 && changeStr.IndexOf("}") != 0 && changeStr.IndexOf("*") != 0
                        && line.IndexOf("else") == -1 && line.IndexOf("return") == -1 && line.IndexOf("if (") == -1)
                    {
                        set.Add(line);
                    }
                }
                else
                {
                    DuplicationCount++;
                }
            }

            set.Clear();
        }

        protected override void DuplicationCalc()
        {
            Duplication = Convert.ToDouble(DuplicationCount) / Convert.ToDouble(PhysicalLineCount) * 100;
        }

        protected override void TotalQuality()
        {
            Quality = 0;

            if (AvgMethodCount <= 10)
            {
                Quality += 3;
            }
            else if (AvgMethodCount > 10 && AvgMethodCount <= 20)
            {
                Quality += 1;
            }

            if (Duplication <= 10)
            {
                Quality += 3;
            }
            else if (Duplication > 10 && Duplication <= 20)
            {
                Quality += 1;
            }

            if (AvgCyclomate <= 5)
            {
                Quality += 2;
            }
            else if (AvgCyclomate > 5 && AvgCyclomate <= 10)
            {
                Quality += 1;
            }
            
            if (Documentation >= 60)
            {
                Quality += 2;
            }
            else if (Documentation >= 30 && Documentation < 60)
            {
                Quality += 1;
            }
        }

        public override void AnalyzerController()
        {
            PhysicalRowCounter();
            SourceRowCounter();
            CommentCounter();
            ClassCounter();
            MethodCounter();
            DocumentationCalc();
            AvgMethodLineCalc();
            CyclomateCount();
            AvgCyclomateCalc();
            DuplicationCounter();
            DuplicationCalc();
            TotalQuality();
        }
    }
}
