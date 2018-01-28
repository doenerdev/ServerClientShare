using System.Collections.Generic;
using System.Text;
//using Newtonsoft.Json;
using ServerClientShare.MiniJson;
using ServerClientShare.Interfaces;

namespace ServerClientShare.DTO
{
    public abstract class DTO : IJsonSerializable
    {
        public virtual string ToJson()
        {
            return Json.Serialize(this);
            //return JsonConvert.SerializeObject(this);
        }
    }
}
