using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordScrambleClient.WordScrambleGameService;
using System.ServiceModel;

namespace WordScrambleClient
{
    class JTRDarchClient
    {
        static void Main(string[] args)
        {
            IWordScrambleGame game = new WordScrambleClient.WordScrambleGameService.WordScrambleGameClient();
            WordScrambleGameClient proxy = new WordScrambleGameClient();
            bool canPlayGame = true;
            Console.WriteLine("Player's name?");
            String playerName = Console.ReadLine();

            if (!proxy.isGameBeingHosted())
            {
                Console.WriteLine("Welcome " + playerName +
                "! Do you want to host the game?");
                if (Console.ReadLine().CompareTo("yes") == 0)
                {
                    Console.WriteLine("Type the word to scramble.");
                    string inputWord = Console.ReadLine();
                    inputWord = inputWord.Trim();
                    string scrambledWord = "";
                    try
                    {
                        scrambledWord = proxy.hostGame(playerName, "", inputWord);
                    }
                    catch (FaultException<GameBeingHostedFault> e)
                    {
                        Console.WriteLine("WARNING: {0}", e.Detail.problem);
                    }
                    canPlayGame = false;
                    Console.WriteLine("You're hosting the game with word '" + inputWord + "' scrambled as '" + scrambledWord + "'");
                    Console.ReadKey();
                }
            }

            if (canPlayGame)
            {
                Console.WriteLine("Do you want to play the game?");
                if (Console.ReadLine().CompareTo("yes") == 0) {
                    //catch the host cannot join game exception
                    try
                    {
                        //catch the nogame hosted exception
                        try
                        {
                            //catch the maxium players reached exception
                            try
                            {
                                Word gameWords = proxy.join(playerName);
                                Console.WriteLine("Can you unscramble this word? => " + gameWords.scrambledWord);
                                String guessedWord;

                                bool gameOver = false;
                                while (!gameOver)
                                {
                                    guessedWord = Console.ReadLine();
                                    //catch the 'not playing game' fault
                                    try
                                    {
                                        gameOver = proxy.guessWord(playerName, guessedWord, gameWords.unscrambledWord);
                                    }
                                    catch(FaultException<PlayerNotPlayingGameFault> e)
                                    {
                                        Console.WriteLine("Warning: {0}", e.Detail.problem);
                                    }
                                    if (!gameOver)
                                    {
                                        Console.WriteLine("Nope, try again...");
                                    }
                                }
                                Console.WriteLine("You WON!!!");
                            }
                            catch (FaultException<MaximumPlayersReachedFault> e)
                            {

                                Console.WriteLine("WARNING: you, {1} cannot join due to {0}", e.Detail.problem, e.Detail.playerName);
                            }
                        }
                        catch (FaultException<NoGameHostedFault> e)
                        {
                            Console.WriteLine("WARNING: {0}", e.Detail.problem);
                        }
                       
                    }
                    catch (FaultException<HostCannotJoinGameFault> e)
                    {
                        Console.WriteLine("WARNING: {0} due to your name {1} hosting.", e.Detail.problem, e.Detail.userName);
                    }
                }
            }
        }
    }
}