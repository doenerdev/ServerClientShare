using System;
using System.Collections.Generic;
using System.Text;

namespace ServerClientShare.Enums
{
    [Serializable]
    public enum NetworkMessageType
    {
        ServerGameIsInitializing,
        ServerGameInitialized,


        RequestHexMap,
        RequestDeck,
        RequestMarketplace,
        RequestNewTowerSegment,
        RequestActionLog,
        RequestInitialGameplayData,

        GameActionPerformed,
        ChangeTurnPerformed,

        ServerSentHexMap,
        ServerSentDeck,
        ServerSentMarketplace,
        ServerSentReady,
        ServerSentGameAction,
        ServerSentChangeTurn,
        ServerSentNewTowerSegment,
        ServerSentActionLog,
        ServerSentInitialGameplayData,
        ServerConfirmedClientMessage,
        ServerSentForfeitGame,

        ClientSentHexMap,
        ClientSentDeck,
        ClientSentMarketplace,
        ClientSentMatch,
        ClientSentChangeTurn,
        ClientSentActionLogIndex,
        ClientLeftGame,
        ClientSentWonGame,
        ClientSentForfeitGame,
        ClientSentEndedGame,
    }
}
