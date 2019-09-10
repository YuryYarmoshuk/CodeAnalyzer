using System;
using System.IO;
using CodeAnalyzer.Model.Entity;

namespace CodeAnalyzer.Model
{
    public class ShredderJava : Entity.Shredder
    {
        protected override void ShredCode(StreamReader streamReader)
        {
            string line;
            Code code = new Code();
            CodeArr.Add(code);

            while((line = streamReader.ReadLine()) != null)
            {
                if (line != "")
                {
                    CodeArr[0].Add(line);
                }
            }
        }

        protected override void ShredClass(StreamReader streamReader)
        {
            string line;
            bool classStart = false;
            bool classEnd = false;
            bool multyCom = false;
            int braceCount = 0;
            int count = 0;

            while ((line = streamReader.ReadLine()) != null)
            {
                if (line.IndexOf("class") != -1 && line.IndexOf("*") == -1 && line.IndexOf("//") == -1 && line.IndexOf("static") == -1)
                {
                    if (classEnd)
                    {
                        classEnd = false;
                        count++;
                    }

                    Code code = new Code();
                    ClassArr.Add(code);
                }
                
                if (line.IndexOf("{") != -1 && !classStart && line.IndexOf("*") == -1 && line.IndexOf("//") == -1)
                {
                    classStart = true;
                }

                if (line != "" && classStart)
                {
                    if (line.IndexOf("{") != -1)
                    {
                        braceCount++;
                    }

                    if (line.IndexOf("}") != -1)
                    {
                        braceCount--;
                    }

                    if (line.IndexOf("//") != -1)
                    {
                        line = line.Remove(line.IndexOf("//"));
                    }

                    if (line.IndexOf("/*") != -1)
                    {
                        multyCom = true;
                    }

                    if (line.IndexOf("class") == -1 && classStart && !multyCom && line.Trim() != "")
                    {
                        try
                        {
                            ClassArr[count].Add(line);
                        }
                        catch(Exception)
                        {
                            ErrorFinde = true;
                            break;
                        }
                    }

                    if (braceCount == 0)
                    {
                        classEnd = true;
                        classStart = false;
                    }

                    if (line.IndexOf("*/") != -1)
                    {
                        multyCom = false;
                    }
                }
            }
        }

        protected override void ShredMethod(StreamReader streamReader)
        {
            string line;
            string[] words;
            bool methStart = false;
            bool methEnd = false;
            bool multyCom = false;
            int braceCount = 0;
            int count = 0;

            while ((line = streamReader.ReadLine()) != null)
            {
                if (line.IndexOf("(") != -1 && line.IndexOf("class") == -1 && line.IndexOf(".") == -1 &&
                    line.IndexOf("if") == -1 && line.IndexOf("=") == -1 && line.IndexOf("implements") == -1 && 
                    line.IndexOf("interface") == -1 && line.IndexOf("*") == -1 && line.IndexOf("//") == -1)
                {
                    words = line.Split(' ');
                    if (words.Length > 2)
                    {
                        if (methEnd)
                        {
                            methEnd = false;
                            count++;
                        }

                        Code code = new Code();
                        MethArr.Add(code);
                    }
                }

                if (line.IndexOf("{") != -1 && line.IndexOf("interface") == -1 &&
                    line.IndexOf("class") == -1 && !methStart && line.IndexOf("*") == -1 &&
                    line.IndexOf("implements") == -1 && line.IndexOf("//") == -1)
                {
                    methStart = true;
                }

                if (methStart && line != "")
                {
                    
                    if (line.IndexOf("{") != -1)
                    {
                        braceCount++;
                    }

                    if (line.IndexOf("}") != -1)
                    {
                        braceCount--;
                    }
                    
                    if (line.IndexOf("//") != -1)
                    {
                        line = line.Remove(line.IndexOf("//"));
                    }

                    if (line.IndexOf("/*") != -1)
                    {
                        multyCom = true;
                    }

                    if (!multyCom && line.Trim() != "")
                    {
                        try
                        {
                            MethArr[count].Add(line);
                        }
                        catch (Exception)
                        {
                            ErrorFinde = true;
                            break;
                        }
                    }

                    if (braceCount == 0)
                    {
                        methEnd = true;
                        methStart = false;
                    }

                    if (line.IndexOf("*/") != -1)
                    {
                        multyCom = false;
                    }
                }
            }
        }

        public override void ShredderControll()
        {
            if (Path != null)
            {
                GetFileName();
                ClearStore();

                StreamReader streamReaderCode = GetReader();
                ShredCode(streamReaderCode);    // "нарезаем" весь код
                streamReaderCode.Close();

                StreamReader streamReaderClass = GetReader();
                ShredClass(streamReaderClass);  // "нарезаем" на классы
                streamReaderClass.Close();

                StreamReader streamReaderMethod = GetReader();
                ShredMethod(streamReaderMethod);    // "нарезаем" на методы
                streamReaderMethod.Close();

                // если в ходе считывания файла была получена ошибка
                // отчищается хранилище строк
                if (ErrorFinde)
                {
                    ClearStore();
                }
            }
        }
    }         
}
