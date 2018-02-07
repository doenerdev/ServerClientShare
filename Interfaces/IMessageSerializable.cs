using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayerIO.GameLibrary;
using ServerClientShare.Enums;

namespace ServerClientShare.Interfaces
{
    public interface IMessageSerializable<T>
    {
        Message ToMessage(Message message);
    }
}
