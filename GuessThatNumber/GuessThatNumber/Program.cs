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
        static string UserNumberString = null;
        static int publicUserNumber = 0;
        static int newestDifference = 0;
        static void Main(string[] args)
        {

            Random rand = new Random();
            SetNumberToGuess(rand.Next(1, 101));
            bool looping = true;
            string keepPlaying = null;
            int oldDifference = 0;

            while (looping == true)
            {
                while (ValidateInput(UserNumberString) == false)
                {
                    UserInput();
                }

                if (ValidateInput(UserNumberString) == true)
                {
                    while (publicUserNumber != NumberToGuess)
                    {
                        if (IsGuessTooHigh(publicUserNumber))
                        {

                            while (oldDifference == 0)
                            {
                                Console.WriteLine("\nYour guess is too high. Guess again: ");
                                oldDifference = publicUserNumber - NumberToGuess;
                                UserInput();
                            }
                            if (GettingWarmer(oldDifference))
                            {
                                Console.WriteLine("\nGetting warmer! Guess again: ");
                                UserInput();
                            }
                            else if (GettingColder(oldDifference))
                            {
                                Console.WriteLine("\nGetting colder! Guess again: ");
                                UserInput();
                            }
                            oldDifference = newestDifference;


                        }
                        else if (IsGuessTooLow(publicUserNumber))
                        {
                            while (oldDifference == 0)
                            {
                                Console.WriteLine("\nYour guess is too low. Guess again: ");
                                oldDifference = NumberToGuess - publicUserNumber;
                                UserInput();
                            }
                            if (GettingColder(oldDifference))
                            {
                                Console.WriteLine("You're getting colder! Try again: ");
                                UserInput();
                            }
                            else if (GettingWarmer(oldDifference))
                            {
                                Console.WriteLine("You're getting warmer! Try again: ");
                                UserInput();
                            }
                            oldDifference = newestDifference;
                        }
                    }
                }
                Console.WriteLine("You guessed it! My number is {0}!", NumberToGuess);
                Console.WriteLine("Would you like to play again? Y/N");
                keepPlaying = Console.ReadLine().ToUpper();
                switch (keepPlaying)
                {
                    case "Y":
                        SetNumberToGuess(rand.Next(1, 101));
                        UserInput();
                        break;
                    case "N":
                        looping = false;
                        break;
                    default:
                        Console.WriteLine("Please select a valid option: \nPlay again? Y/N");
                        keepPlaying = Console.ReadLine().ToUpper();
                        break;
                }
            }
            Console.WriteLine("Thanks for playing!");
            Console.ReadKey();
        }



        public static bool ValidateInput(string userInput)
        {
            int userNumber = 0;
            int.TryParse(userInput, out userNumber);

            if (userNumber > 0 && userNumber < 100)
            {
                publicUserNumber = userNumber;
                return true;

            }

            //check to make sure that the users input is a valid number between 1 and 100.
            return false;
        }
        public static void UserInput()
        {
            Console.WriteLine("Please enter a number between 1 and 100: ");
            UserNumberString = Console.ReadLine();
            while (ValidateInput(UserNumberString) == false)
            {
                Console.WriteLine("\nPlease enter a valid input.");
                Console.WriteLine("Please enter a number between 1 and 100: ");
                UserNumberString = Console.ReadLine();
            }

        }
        public static void SetNumberToGuess(int number)
        {
            NumberToGuess = number;

            //TODO: make this function override your global variable holding the number the user needs to guess.  This is used only for testing methods.
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

        public static bool GettingColder(int oldDifference)
        {
            if (NumberToGuess > publicUserNumber)
            {
                newestDifference = NumberToGuess - publicUserNumber;
            }
            else
            {
                newestDifference = publicUserNumber - NumberToGuess;
            }

            if (oldDifference < newestDifference)
            {
                return true;
            }
            return false;
        }

        public static bool GettingWarmer(int oldDifference)
        {

            if (NumberToGuess > publicUserNumber)
            {
                newestDifference = NumberToGuess - publicUserNumber;
            }
            else
            {
                newestDifference = publicUserNumber - NumberToGuess;
            }

            if (oldDifference > newestDifference)
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
