using System;
using System.Linq;
using System.Security.Cryptography;

namespace Rock_Paper_Scissors
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 3){
                Console.WriteLine("Input 3 or more arguments!\nFor example: 'rock paper scissors' or '1 2 3 4 5'\n");
                return;
            }
            if (args.Length % 2 == 0) {
                Console.WriteLine("There must be odd count of arguments!");
                return;
            }
            if (args.Distinct().Count() != args.Length){
                Console.WriteLine("Arguments must be non-repeating!");
                return;
            }
            
            int playerMove;
            bool playerLose = false;
            int index = args.GetUpperBound(0);
            var secretKey = new byte[16];
            RandomNumberGenerator.Fill(secretKey);
            HMACSHA256 hmac = new HMACSHA256(secretKey);
            int computerMove = RandomNumberGenerator.GetInt32(0, index + 1);
            byte[] byteString = System.Text.Encoding.Default.GetBytes(args[computerMove]);
            var byteHash = hmac.ComputeHash(byteString);
            
            while (true)
            {
                Console.WriteLine($"HMAC: {BitConverter.ToString(byteHash).Replace("-", string.Empty)}");
                Console.WriteLine("Available moves:");
                for (int i = 0; i < index + 1; i++)
                    Console.WriteLine($"{i + 1} - {args[i]}");
                Console.WriteLine("0 - exit");
                Console.Write("Enter your move: ");
                playerMove = Convert.ToInt32(Console.ReadLine()) - 1;
                if (playerMove == -1) return;
                if (playerMove < args.GetLowerBound(0) || playerMove > args.GetUpperBound(0))
                {
                    Console.WriteLine($"Enter a number from 0 to {index + 1}!\nPress any key to re-enter and continue...");
                    Console.ReadKey();
                    continue;
                }
                Console.WriteLine($"Your move: {args[playerMove]}");
                Console.WriteLine($"Computer move: {args[computerMove]}");
                break;
            }
            if (computerMove == playerMove)
            {
                Console.WriteLine("Draw!");
                Console.WriteLine($"HMAC key: {BitConverter.ToString(hmac.Key).Replace("-", string.Empty)}");
                return;
            }
            var halfLine = decimal.Floor(Convert.ToInt32(index / 2));
            for (int i = 1; i < halfLine + 1; i++)
            {
                int tmp = playerMove + i;
                if (tmp > index)
                    tmp = tmp % index - 1;
                if (args[tmp] == args[computerMove])
                {
                    playerLose = true;
                    break;
                }
            }
            Console.WriteLine(playerLose ? "You Lose!" : "You Win!");
            Console.WriteLine($"HMAC key: {BitConverter.ToString(hmac.Key).Replace("-", string.Empty)}");
        }
    }
}
