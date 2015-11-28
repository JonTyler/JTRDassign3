using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace JonTylerRyanDarch_Assign2
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class WordScrambleGame : IWordScrambleGame
    {
        //max allowed players constant
        private const int MAX_ALLOWED_PLAYERS = 5;
        //user hosting the game. If it's null, no one is hosting.
        private static string currentUserHostingGame = null;
        //the word of the now, holding the scrambled and unscrambled words.
        private static Word gameWord;
        //list of players
        private static List<String> activePlayers = new List<string>();

        [OperationBehavior]
        private string scrambleWord(string word)
        {
            char[] chars = word.ToArray();

            Random r = new Random(2011);

            for (int i = 0; i < chars.Length; i++)

            {

                int randomIndex = r.Next(0, chars.Length);

                char temp = chars[randomIndex];

                chars[randomIndex] = chars[i];

                chars[i] = temp;

            }

            return new string(chars);
        }
        [OperationBehavior]
        public bool isGameBeingHosted()
        {
            if(currentUserHostingGame != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        [OperationBehavior]
        public string hostGame(string userName, string hostAddress, string WordToScramble)
        {
            if(currentUserHostingGame != null)
            {
                GameBeingHostedFault alreadyThere = new GameBeingHostedFault();
                alreadyThere.userName = userName;
                alreadyThere.problem = "You cannot join as host to a game already being hosted.";
                throw new FaultException<GameBeingHostedFault>(alreadyThere);
            }
            //add the host's userName as host
            currentUserHostingGame = userName;
            //scramble the host's word
            gameWord.unscrambledWord = WordToScramble;
            gameWord.scrambledWord = scrambleWord(WordToScramble);
            // return the scrambled word to the host.
            return gameWord.scrambledWord;
        }
        [OperationBehavior]
        public Word join(string playerName)
        {
            //if nobody is hosting the game, they can't join as a player.
            if (currentUserHostingGame == null)
            {
                NoGameHostedFault nobodyHere = new NoGameHostedFault();
                nobodyHere.problem = "No one is hosting the game. It is sad day. :c";
                throw new FaultException<NoGameHostedFault>(nobodyHere);
            }
            //if activeplayers count is five, throw an exception.
            if (activePlayers.Count > MAX_ALLOWED_PLAYERS)
            {
                MaximumPlayersReachedFault maximumPlayersFault = new MaximumPlayersReachedFault();
                maximumPlayersFault.playerName = playerName;
                maximumPlayersFault.problem = "Maximum players reached: " + MAX_ALLOWED_PLAYERS.ToString();
                throw new FaultException<MaximumPlayersReachedFault>(maximumPlayersFault);
            }
            //if the host's name is taken, they can't join as a player
            if(playerName == currentUserHostingGame)
            {
                HostCannotJoinGameFault hostJoinFault = new HostCannotJoinGameFault();
                hostJoinFault.userName = playerName;
                hostJoinFault.problem = "You are already hosting, you cannot join as a player.";
                throw new FaultException<HostCannotJoinGameFault>(hostJoinFault);
            }
            else
            {
                activePlayers.Add(playerName);
                return gameWord;
            }
        }
        [OperationBehavior]
        public bool guessWord(string playerName, string guessedWord, string unscrambledWord)
        {
           //find the player name in the active player list, if not found, throw exception.
            if(!activePlayers.Contains(playerName))
            {
                PlayerNotPlayingGameFault noGame4u = new PlayerNotPlayingGameFault();
                noGame4u.problem = "You're not playing this game.";
                throw new FaultException<PlayerNotPlayingGameFault>(noGame4u);
            }

            //compare (must EXACTLY MATCH)
            if(guessedWord.Trim() == unscrambledWord.Trim())
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        
    }
}
