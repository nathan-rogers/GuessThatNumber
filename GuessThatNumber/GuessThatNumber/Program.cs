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
        //string to store user input publicly
        static string UserNumberString = null;
        //stores the current user input as int
        static int publicUserNumber = 0;
        //stores the most recent difference between selected random number and user input
        //compares hot and cold
        static int newestDifference = 0;
        //this is the previously entered number, error prevention
        static int lastEnteredNumber = 0;
        //how many times does the user guess?
        static int guesses = 0;
        static int invalidAttempts = 0;

        static void Main(string[] args)
        {
            //instantiates randomizer
            Random rand = new Random();
            //sets random value publicly
            SetNumberToGuess(rand.Next(1, 101));
            //if user wants to keep playing, which is set to true to start
            bool looping = true;
            //checks to see if the user wants to keep playing
            string keepPlaying = null;
            //checks the old difference for comparing
            //hot and cold, only want to modify locally
            int oldDifference = 0;

            //while I want to play
            while (looping == true)
            {
                //validate input is set to false by default
                //because value is set to 0 in function
                if (ValidateInput(UserNumberString) == false)
                {
                    //get user input
                    UserInput();
                }

                //if the user input is valid
                if (ValidateInput(UserNumberString) == true)
                {
                    //and while the input guess is wrong
                    while (publicUserNumber != NumberToGuess)
                    {
                        //check to see if the value entered is the same as the previous value entered
                        while (publicUserNumber == lastEnteredNumber)
                        {
                            //prompts user for a different
                            Console.WriteLine("Please enter a DIFFERENT number: ");
                            UserInput();
                        }

                        //if the user number is higher than the random number
                        if (IsGuessTooHigh(publicUserNumber))
                        {
                            //set the last entered number so the program doesn't freeze
                            lastEnteredNumber = publicUserNumber;
                            //if the user is close to the number
                            if (publicUserNumber - NumberToGuess < 10)
                            {
                                Console.WriteLine("\nYou're so close!");
                            }
                            //while there is no previous entry
                            while (oldDifference == 0)
                            {
                                //let the user know to guess down next time
                                //and get more input
                                Console.WriteLine("\nYour guess is too high. Guess again: ");
                                oldDifference = publicUserNumber - NumberToGuess;
                                UserInput();
                            }

                            //prints if the user gets hot or cold
                            HotCold(oldDifference);
                            //set the new difference to the old difference
                            //compares the most recent difference during the next loop
                            oldDifference = newestDifference;
                        }

                        //if the numbers is too low
                        else if (IsGuessTooLow(publicUserNumber))
                        {
                            //given more time, this section belongs in HotCold()
                            //set last entered number so the program doesn't crash
                            lastEnteredNumber = publicUserNumber;
                            //if the user is close to the number
                            if (NumberToGuess - publicUserNumber < 10)
                            {
                                Console.WriteLine("\nYou're so close!");
                            }

                            //while there is no previous entry
                            while (oldDifference == 0)
                            {
                                //let the user know to guess up next time
                                //get more input
                                Console.WriteLine("\nYour guess is too low. Guess again: ");
                                oldDifference = NumberToGuess - publicUserNumber;
                                UserInput();
                            }
                            //prints if user is hot or cold
                            HotCold(oldDifference);
                            //compares most recent diffference during next loop
                            oldDifference = newestDifference;

                        }

                    }
                }
                //printed when correct number is guessed
                Console.WriteLine("\nYou guessed it! My number is {0}!", NumberToGuess);

                //asks user to play again
                Console.WriteLine("\nIt only took you {0} attempts! ", guesses);
                if (invalidAttempts >= 5)
                {
                    Console.WriteLine("You might do better next time if you'd stop messing around!");
                }
                Console.WriteLine("\nWould you like to play again? Y/N");
                //converts user entered name to upper for case comparison
                keepPlaying = Console.ReadLine().ToUpper();
                switch (keepPlaying)
                {
                    //yes, keep playing and generate new random number
                    case "Y":
                        SetNumberToGuess(rand.Next(1, 101));
                        //reset old difference so the user initially knows whether they are high or low
                        oldDifference = 0;
                        //resets last entered number in case user wishes to guess previous number on 
                        //new game
                        lastEnteredNumber = 0;
                        //resets attempts counter
                        guesses = 0;
                        UserInput();
                        break;
                    //no don't keep playing
                    case "N":
                        looping = false;
                        break;
                    //in case user enters wrong input
                    default:
                        Console.WriteLine("Please select a valid option: \nPlay again? Y/N");
                        keepPlaying = Console.ReadLine().ToUpper();
                        break;
                }
            }
            //end
            Console.WriteLine("Thanks for playing!");
            Console.ReadKey();
        }


        /// <summary>
        /// Checks to see if user input is a valid number or not
        /// </summary>
        /// <param name="userInput"></param>
        /// <returns></returns>
        public static bool ValidateInput(string userInput)
        {
            //sets user number int to 0
            int userNumber = 0;
            //try to parse user input to int
            int.TryParse(userInput, out userNumber);


            //if number is still 0, user entered invalid input
            //if it is NOT 0
            if (userNumber > 0 && userNumber < 101)
            {
                //set publicly viewable int
                publicUserNumber = userNumber;

                //boolean value is true, it is a valid input
                return true;
            }
            invalidAttempts++;
            //user did not make a valid selection
            return false;
        }
        /// <summary>
        /// Method for getting user input without typing this all out in main method
        /// </summary>
        public static void UserInput()
        {
            //prompts user
            Console.WriteLine("Please enter a number between 1 and 100: ");
            //get input
            UserNumberString = Console.ReadLine();

            guesses++;
            //if the user enters invalid input
            while (ValidateInput(UserNumberString) == false)
            {
                //prompt the user for input
                Console.WriteLine("\nPlease enter a valid input.");
                Console.WriteLine("Please enter a number between 1 and 100: ");
                UserNumberString = Console.ReadLine();

                guesses++;
            }

        }
        /// <summary>
        /// 
        /// Set the random number publicly so that it can be viewed by tests and by main method
        /// </summary>
        /// <param name="number"></param>
        public static void SetNumberToGuess(int number)
        {
            //sets number to guess publicly
            NumberToGuess = number;
        }

        /// <summary>
        /// Checks to see if the previous guess got farther away or closer to the randomly
        /// generated number
        /// </summary>
        /// <param name="difference"></param>
        /// <returns></returns>
        public static bool GettingColder(int difference)
        {
            //if the random number is bigger than the user number
            if (NumberToGuess > publicUserNumber)
            {
                //random - user = difference
                newestDifference = NumberToGuess - publicUserNumber;
            }
            //otherwise
            else
            {
                //user - random = difference
                newestDifference = publicUserNumber - NumberToGuess;
            }
            //if the previous distance between the numbers is larger
            //i.e. if the previous guess was 5 away, and now I'm 6 away
            if (difference < newestDifference)
            {
                //the user got colder
                return true;
            }
            //the user did NOT get colder
            return false;
        }

        /// <summary>
        /// Checks to see if the user got closer are farther from 
        /// random number
        /// </summary>
        /// <param name="difference"></param>
        /// <returns></returns>
        public static bool GettingWarmer(int difference)
        {
            //if the random number is larger than user number
            if (NumberToGuess > publicUserNumber)
            {
                //random number - user number = distance between the 2 numbers
                newestDifference = NumberToGuess - publicUserNumber;
            }
            //otherwise
            else
            {
                //user number - random number = distance between the 2 numbers
                newestDifference = publicUserNumber - NumberToGuess;
            }
            //is the previous distance greater than the newest distance between the 2 numbers
            if (difference > newestDifference)
            {
                //the new distance is closer to the goal
                return true;
            }
            //the user did NOT get closer to the random number
            return false;

        }
        public static bool IsGuessTooHigh(int userGuess)
        {
            //is the user input larger than the random number?
            if (userGuess > NumberToGuess)
            {
                return true;
            }

            return false;
        }

        public static bool IsGuessTooLow(int userGuess)
        {
            //is the user input smaller than the random number?
            if (userGuess < NumberToGuess)
            {
                return true;
            }

            return false;
        }
        public static void HotCold(int oldDifference)
        {

            //If the difference is greater than before
            if (GettingColder(oldDifference))
            {
                //get more input
                Console.WriteLine("You're getting colder! Try again: ");
                UserInput();
            }
            //if the difference is less than before
            else if (GettingWarmer(oldDifference))
            {
                //get more input
                Console.WriteLine("\nYou're getting warmer! Try again: ");
                UserInput();
            }
        }
    }
}
