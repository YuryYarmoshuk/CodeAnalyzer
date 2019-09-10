// Абстрактный класс разделяющий исходный код на листы сущностей Code 

using System;
using System.Collections.Generic;
using System.IO;

namespace CodeAnalyzer.Model.Entity
{
    public abstract class Shredder : Interfaces.IShredder
    {
        public bool ErrorFinde { get; set; }
        public List<Code> CodeArr { get; }  // лист содержащий весь код целиком
        public List<Code> ClassArr { get; } // лист содержащий классы
        public List<Code> MethArr { get; }  // лист содержащий методы
        public string FileName { get; set; }
        public string Path { get; set; }

        /// <summary>
        /// Конструктор без параметром
        /// </summary>
        /// <param name="path"></param>
        public Shredder()
        {
            ErrorFinde = false;
            CodeArr = new List<Code>();
            ClassArr = new List<Code>();
            MethArr = new List<Code>();
        }

        /// <summary>
        /// Конструктор с параметром
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        public Shredder(string path)
        {
            ErrorFinde = false;
            CodeArr = new List<Code>();
            ClassArr = new List<Code>();
            MethArr = new List<Code>();
            Path = path;
        }

        /// <summary>
        /// Функция создания потока для чтения
        /// </summary>
        /// <returns></returns>
        public StreamReader GetReader()
        {
            try
            {
                StreamReader reader = new StreamReader(Path, System.Text.Encoding.Default);
                return reader;
            }
            catch (Exception)
            {
                ErrorFinde = true;
            }
            return null;
        }

        /// <summary>
        /// Метод, "нарезающий" весь код по линиям
        /// </summary>
        /// <param name="streamReader"></param>
        protected abstract void ShredCode(StreamReader streamReader);

        /// <summary>
        /// Метод, "нарезающий" весь код на классы
        /// </summary>
        /// <param name="streamReader"></param>
        protected abstract void ShredClass(StreamReader streamReader);

        /// <summary>
        /// Метод, "нарезающий" весь код на методы
        /// </summary>
        /// <param name="streamReader"></param>
        protected abstract void ShredMethod(StreamReader streamReader);

        /// <summary>
        /// Контроллер класса
        /// </summary>
        public abstract void ShredderControll();

        /// <summary>
        /// Функция получения имени файла
        /// </summary>
        public void GetFileName()
        {
            if (Path != null)
            {
                string fileName = Path;

                while (fileName.IndexOf("\\") != -1)    
                {
                    fileName = fileName.Remove(0, fileName.IndexOf("\\") + 1);  //отрезает часть пути, пока не останется имя файла
                }

                FileName = fileName;
            }
        }

        /// <summary>
        /// Метод отчитки всех контенеров
        /// </summary>
        public void ClearStore()
        {
            CodeArr.Clear();
            ClassArr.Clear();
            MethArr.Clear();
        }
    }
}
