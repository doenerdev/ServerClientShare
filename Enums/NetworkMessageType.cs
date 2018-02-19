using System;
using System.Collections.Generic;
using System.Text;

namespace ServerClientShare.Enums
{
    public enum NetworkMessageType
    {
        ServerGameIsInitializing,
        ServerGameInitialized,


        RequestHexMap,
        RequestDeck,
        RequestMarketplace,

        GameActionPerformed,
        ChangeTurnPerformed,

        ServerSentHexMap,
        ServerSentDeck,
        ServerSentMarketplace,
        ServerSentReady,
        ServerSentGameAction,
        ServerSentChangeTurn,
    }
}
