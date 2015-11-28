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
            throw new NotImplementedException();
        }
        [OperationBehavior]
        public string hostGame(string userNamne, string WordToScramble)
        {
            throw new NotImplementedException();
        }
        [OperationBehavior]
        public Word join(string playerName)
        {
            throw new NotImplementedException();
        }
        [OperationBehavior]
        public bool guessWord(string playerName, string guessedWord, string unscrambledWord)
        {
            throw new NotImplementedException();
        }


        
    }
}
