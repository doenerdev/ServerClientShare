using PlayerIO.GameLibrary;
using ServerClientShare.Interfaces;

namespace ServerGameCode
{
    public class PlayerAction : IMessageSerializable<PlayerAction>
    {
        private readonly string _playerName;
        private readonly string _actionName;
        private readonly string _actionJson;

        public PlayerAction(string playerName, string actionName, string actionJson)
        {
            _actionName = actionName;
            _playerName = playerName;
            _actionJson = actionJson;
        }

        public Message ToMessage(Message message)
        {
            message.Add(_actionName);
            message.Add(_playerName);
            message.Add(_actionJson);
            return message;
        }

        public static PlayerAction FromMessageArguments(Message message, ref uint offset)
        {
            var actionName = message.GetString(offset++);
            var playerName = message.GetString(offset++);
            var actionJson = message.GetString(offset++);

            return new PlayerAction(playerName, actionName, actionJson);
        }

        public DatabaseObject ToDBObject()
        {
            DatabaseObject dbObject = new DatabaseObject();
            dbObject.Set("ActionName", _actionName);
            dbObject.Set("PlayerName", _playerName);
            dbObject.Set("ActionJson", _actionJson);
            return dbObject;
        }

        public static PlayerAction FromDBObject(DatabaseObject dbObject)
        {
            if (dbObject.Count == 0) return null;

            var actionName = dbObject.GetString("ActionName");
            var playerName = dbObject.GetString("PlayerName");
            var actionJson = dbObject.GetString("ActionJson");
            return new PlayerAction(playerName, actionName, actionJson);
        }
    }
}

public enum CommandId
{
    EndTurn,
    WeaponSelection,
    Default,
}
