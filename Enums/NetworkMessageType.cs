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
        ServerSentHexMap,
    }
}
