using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessThatNumber
{
    public class Program
    {
        //this is the number the user needs to guess.  Set its value in your code using a RNG.
        static int NumberToGuess = 0;

        static void Main(string[] args)
        {
            Random rng = new Random();
            bool isPlaying = true;
            string numberString = null;
            int userNumber = 0;
            Console.Write("\nPlease enter an integer between 1 and 100: ");
            numberString = Console.ReadLine();
            SetNumberToGuess(rng.Next(1, 101));

            while (isPlaying)
            {
                if (ValidateInput(numberString) == false)
                {
                    Console.WriteLine("That is not a valid entry!");
                    Console.Write("\nPlease enter an integer between 1 and 100: ");
                    numberString = Console.ReadLine();
                }
                else if (ValidateInput(numberString) == true)
                {

                    userNumber = int.Parse(numberString);
                    while (userNumber != NumberToGuess)
                    {

                        if (IsGuessTooHigh(userNumber) == true)
                        {
                            Console.WriteLine("\nYour guess is too high!");
                            Console.Write("Please try again: ");
                            numberString = Console.ReadLine();
                            userNumber = int.Parse(numberString);
                        }
                        else if (IsGuessTooLow(userNumber) == true)
                        {
                            Console.WriteLine("Your guess is too low!");
                            Console.WriteLine("Please try again: ");
                            numberString = Console.ReadLine();
                            userNumber = int.Parse(numberString);
                        }

                    }
                }
                isPlaying = false;
            }

            Console.WriteLine("You guessed it! My number was {0}", NumberToGuess);
            Console.ReadKey();

        }

        public static bool ValidateInput(string userInput)
        {
            int validNumber = 0;
            int.TryParse(userInput, out validNumber);


            if (validNumber > 100 || validNumber < 0)
            {
                return false;
            }
            else if (validNumber == 0)
            {
                return false;
            }
            else if (validNumber < 100 || validNumber > 0)
            {

                return true;
            }


            //check to make sure that the users input is a valid number between 1 and 100.
            return false;
        }
        public static void SetNumberToGuess(int number)
        {

            //TODO: make this function override your global variable holding the number the user needs to guess.  This is used only for testing methods.
            NumberToGuess = number;
        }

        public static bool IsGuessTooHigh(int userGuess)
        {
            
            
            //TODO: return true if the number guessed by the user is too high
            if (userGuess > NumberToGuess)
            {
                return true;
            }

            return false;
        }

        public static bool IsGuessTooLow(int userGuess)
        {
            //TODO: return true if the number guessed by the user is too low
            if (userGuess < NumberToGuess)
            {
                return true;
            }
            return false;
        }
    }
}
