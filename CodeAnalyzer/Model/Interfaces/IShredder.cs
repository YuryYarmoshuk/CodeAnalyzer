// Интерфейс класса делящего код на строки, для возможности расширения данного класса

using System.Collections.Generic;
using CodeAnalyzer.Model.Entity;
using System.IO;

namespace CodeAnalyzer.Model.Interfaces
{
    public interface IShredder
    {
        List<Code> CodeArr { get; }
        List<Code> ClassArr { get; }
        List<Code> MethArr { get; }
        string FileName { get; set; }
        string Path { get; set; }

        StreamReader GetReader();
        
        void ShredderControll();

        void ClearStore();
    }
}
