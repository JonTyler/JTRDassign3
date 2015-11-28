using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JTRdarchOutofSolutionConsole.WordScrambleGame;

namespace JTRdarchOutofSolutionConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            IWordScrambleGame game = new WordScrambleGame.WordScrambleGameClient();

            Word scrambledWord = game.getScrambledWord();

            Console.WriteLine("Can you unscramble this word?");

            Console.WriteLine("=> " + scrambledWord.scrambledWord);

            string guessedWord = Console.ReadLine();

            if (game.guessWord(guessedWord, scrambledWord.unscrambledWord))
            {

                Console.WriteLine("Wow, You Won! ;-)");

            }

            else
            {

                Console.WriteLine("Sorry, You Lose :-( You get NOTHING. Good DAY sir!");

            }

            Console.ReadKey();

        }
    }
}
