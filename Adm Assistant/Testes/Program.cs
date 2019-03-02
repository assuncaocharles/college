using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram;

namespace Telegram.Teste
{
    class Program
    {
        static void Main(string[] args)
        {
            testApi();
            testApiAsync();
            
        }

        static async void testApiAsync()
        {
            var Bot = new Bot.Api("151294581:AAF7rEWnjYv4VLfc_pLUhUm27D4m-lIZdfI");
            var me = await Bot.GetMe();
            Console.WriteLine("Hello my name is " + me.FirstName);
            Console.ReadKey();
        }

        static void testApi()
        {
            var Bot = new Bot.Api("151294581:AAF7rEWnjYv4VLfc_pLUhUm27D4m-lIZdfI");
            var me = Bot.GetMe().Result;
            Console.WriteLine("Hello my name is " + me.FirstName);
        }
    }
}
