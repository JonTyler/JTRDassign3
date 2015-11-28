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
        [FaultContract(typeof(GameBeingHostedFault))]
        [OperationContract]
        //TODO: figure out what operation the connection is
        [OperationContract]
        Word getScrambledWord();

        [OperationContract]
        bool guessWord(string guessedWord, string unscrambledWord);
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
        [DataMember]
        public string connectionId;
        [DataMember]
        public string problem;
    }
    public class MaximumPlayersReachedFault
    {
        [DataMember]
        public string problem;
        [DataMember]
        public string id;
    }
    public class HostCannotJoinGameFault
    {
        [DataMember]
        public string problem;
    }
    public class PlayerNotPlayingGameFault
    {
        [DataMember]
        public string problem;
    }
}
