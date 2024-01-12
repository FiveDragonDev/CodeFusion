using Tuple = CodeFusion.Tuple;

namespace Black_Jack
{
    public class Program
    {
        private static void Main()
        {
            Console.WriteLine("Welcome! Goal: Collect 21 scores.");
            while (true)
            {
                Tuple cards = new(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11);
                cards *= 4;
                int score = 0;
                Console.WriteLine("Press Enter to collect card. Press any key to restart.");
                while (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    int amount = (int)cards.Pop();
                    score += amount;
                    Console.WriteLine($"+{amount}. {score}/21");
                    if (score == 21)
                    {
                        Console.WriteLine("Congratulations!\n\n");
                        break;
                    }
                    else if (score > 21)
                    {
                        Console.WriteLine("Fail.\n\n");
                        break;
                    }
                }
            }
        }
    }
}