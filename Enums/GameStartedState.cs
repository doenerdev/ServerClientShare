using System;
using System.Collections.Generic;
using System.Text;

namespace ServerClientShare.Enums
{
    [Serializable]
    public enum GameStartedState
    {
        Pending,
        Started,
        Ended
    }
}
