
using System;
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL || UNITY_IOS || UNITY_IPHONE || UNITY_ANDROID || UNITY_WII || UNITY_PS4 || UNITY_SAMSUNGTV || UNITY_XBOXONE || UNITY_TIZEN || UNITY_TVOS || UNITY_WP_8_1 || UNITY_WSA || UNITY_WSA_8_1 || UNITY_WSA_10_0 || UNITY_WINRT || UNITY_WINRT_8_1 || UNITY_WINRT_10_0
using PlayerIOClient;
#else
using PlayerIO.GameLibrary;
#endif
using ServerClientShare.Interfaces;
using ServerClientShare.PeristenceMessages;

namespace ServerClientShare.Models
{
    [Serializable]
    public class PlayerAction : IMessageSerializable<PlayerAction>
    {
        private readonly string _playerName;
        private readonly string _actionName;
        private readonly string _actionJson;

        public string PlayerName => _playerName;
        public string ActionName => _actionName;
        public string ActionJson => _actionJson;

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

        public static PlayerAction FromMessageArguments(ClientSentActionMessage actionMessage)
        {
            return new PlayerAction(actionMessage.PlayerName, actionMessage.ActionName, actionMessage.ActionJson);
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
