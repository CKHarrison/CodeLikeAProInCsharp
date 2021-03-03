using CodingKata_Stringbuilder.Interfaces;

namespace CodingKata_Stringbuilder
{
    class Program
    {
        public static void Main()
        {
            IDataStorage storage = new InternalFileSystem();
            IDisplay display = new WelcomeAboardATron3000();

            string text = storage.Read();
            display.Print(text);
        }
    }
}
