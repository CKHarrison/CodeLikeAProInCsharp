using CodingKata_Stringbuilder.Interfaces;
using System;
using System.Threading;

namespace CodingKata_Stringbuilder
{
    public sealed class WelcomeAboardATron3000 : IDisplay
    {
        private bool _firstMessage = true;
        public void Print(string text)
        {
            if (_firstMessage)
            {
                PrintDelay("Welcome aboard! ");
                _firstMessage = false;
            }

            new Thread(() => PrintDelay(text)).Start();
        }

        private void PrintDelay(string text)
        {
            foreach (char character in text)
            {
                Console.Write(character);
                Thread.Sleep(90);
            }
        }
    }
}
