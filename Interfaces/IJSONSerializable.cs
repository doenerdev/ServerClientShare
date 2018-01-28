using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerClientShare.Interfaces
{
    public interface IJsonSerializable
    {
        string ToJson();
    }
}
