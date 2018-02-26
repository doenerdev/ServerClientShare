using System;
using System.Collections.Generic;
using System.Text;
using PlayerIO.GameLibrary;
using ServerClientShare.Interfaces;

namespace ServerClientShare.DTO
{
    public abstract class DatabaseDTO<T> : DTO<T>, IDBSerializable
    {
        public abstract DatabaseObject ToDBObject();
        public static T FromDBObject(DatabaseObject dbObject)
        {
            return default(T);
        }
    }
}
