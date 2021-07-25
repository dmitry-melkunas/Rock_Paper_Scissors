using System;
using System.Linq;
using System.Security.Cryptography;

namespace Rock_Paper_Scissors
{
    class Program
    {
        static int Main(string[] args)
        { 
            if (args.Length < 3){
                Console.WriteLine("Input 3 or more arguments!\nFor example: 'rock paper scissors' or '1 2 3 4 5'\n");
                return 0;
            }
        
            if (args.Length % 2 == 0) {
                Console.WriteLine("There must be odd count of arguments!");
                return 0;
            }

            if (args.Distinct().Count() != args.Length){
                Console.WriteLine("Arguments must be non-repeating!");
                return 0;
            }
            

        }

    }
}
