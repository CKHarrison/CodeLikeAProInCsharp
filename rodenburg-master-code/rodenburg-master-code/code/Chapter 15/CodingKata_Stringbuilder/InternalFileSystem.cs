using CodingKata_Stringbuilder.Interfaces;
using Microsoft.VisualBasic.FileIO;
using System.Text;

namespace CodingKata_Stringbuilder
{
    public sealed class InternalFileSystem : IDataStorage
    {
        private const string FILE_PATH = @"C:\names.csv"; // Change this to reflect the file location on your system
        
        public string Read()
        {
            StringBuilder stringBuilder = new StringBuilder();
            using (TextFieldParser parser = new TextFieldParser(FILE_PATH))
            {
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    stringBuilder.Append(parser.ReadLine());
                    stringBuilder.Append(" | ");
                }
            }

            return stringBuilder.ToString();
        }
    }
}
