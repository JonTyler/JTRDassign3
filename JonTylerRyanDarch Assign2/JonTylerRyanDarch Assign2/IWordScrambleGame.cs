using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace JonTylerRyanDarch_Assign2
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IWordScrambleGame
    {
        // TODO: Add your service operations here
        //is the bloody thing beign hosted?
        [OperationContract]
        bool isGameBeingHosted();

        [FaultContract(typeof(GameBeingHostedFault))]
        [OperationContract]
        //player userName tries to host game with the string Word to be scrambled
        string hostGame(string userNamne, string hostAddress, string WordToScramble);

        [FaultContract(typeof(MaximumPlayersReachedFault))]
        [FaultContract(typeof(HostCannotJoinGameFault))]
        [FaultContract(typeof(NoGameHostedFault))]
        [OperationContract]
        //player playerName tries to join game and gets the word to be unscrambled
        //
        Word join(string playerName);
       
        [FaultContract(typeof(PlayerNotPlayingGameFault))]
        [OperationContract]
        bool guessWord(string playerName, string guessedWord, string unscrambledWord);
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "JonTylerRyanDarch_Assign2.ContractType".
    [DataContract]
    public class Word
    {
        [DataMember]
        public string unscrambledWord;
        [DataMember]
        public string scrambledWord;
    }
    public class GameBeingHostedFault
    {
        //is the game already hosted by someone else?
        [DataMember]
        public string userName;
        [DataMember]
        public string problem;
    }
    public class MaximumPlayersReachedFault
    {
        // is a 6th person trying to join?
        [DataMember]
        public string problem;
        [DataMember]
        public string playerName;
    }
    public class HostCannotJoinGameFault
    {
        //is a host trying to join as a user?
        [DataMember]
        public string problem;
        [DataMember]
        public string userName;
    }
    public class PlayerNotPlayingGameFault
    {
        //is a user trying to play the game when they're not?
        [DataMember]
        public string problem;
    }
    public class NoGameHostedFault
    {
        //is no one hosting a game?
        [DataMember]
        public string problem;
    }
}
