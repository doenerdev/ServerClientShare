using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerClientShare.Interfaces;
using ServerClientShare.Models;
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL || UNITY_IOS || UNITY_IPHONE || UNITY_ANDROID || UNITY_WII || UNITY_PS4 || UNITY_SAMSUNGTV || UNITY_XBOXONE || UNITY_TIZEN || UNITY_TVOS || UNITY_WP_8_1 || UNITY_WSA || UNITY_WSA_8_1 || UNITY_WSA_10_0 || UNITY_WINRT || UNITY_WINRT_8_1 || UNITY_WINRT_10_0
using PlayerIOClient;
#else
using PlayerIO.GameLibrary;
using ServerGameCode;
#endif

namespace ServerClientShare.Models
{
    public class PlayerActionsLog
    {
        private readonly List<PlayerAction> _playerActions;

        public PlayerActionsLogDTO DTO
        {
            get
            {
                var dto = new PlayerActionsLogDTO();
                foreach (var action in PlayerActions)
                {
                    dto.PlayerActions.Add(action);
                }
                return dto;
            }
        }

#if !(UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL || UNITY_IOS || UNITY_IPHONE || UNITY_ANDROID || UNITY_WII || UNITY_PS4 || UNITY_SAMSUNGTV || UNITY_XBOXONE || UNITY_TIZEN || UNITY_TVOS || UNITY_WP_8_1 || UNITY_WSA || UNITY_WSA_8_1 || UNITY_WSA_10_0 || UNITY_WINRT || UNITY_WINRT_8_1 || UNITY_WINRT_10_0)
        private ServerCode _server;
#endif
        public const string MessageType = "PlayerActionsLog";

        public IReadOnlyList<PlayerAction> PlayerActions
        {
            get { return _playerActions.AsReadOnly(); }
        }

#if !(UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL || UNITY_IOS || UNITY_IPHONE || UNITY_ANDROID || UNITY_WII || UNITY_PS4 || UNITY_SAMSUNGTV || UNITY_XBOXONE || UNITY_TIZEN || UNITY_TVOS || UNITY_WP_8_1 || UNITY_WSA || UNITY_WSA_8_1 || UNITY_WSA_10_0 || UNITY_WINRT || UNITY_WINRT_8_1 || UNITY_WINRT_10_0)
        public PlayerActionsLog(ServerCode server)
        {
            _playerActions = new List<PlayerAction>();
            _server = server;
        }

        public PlayerActionsLog(ServerCode server, PlayerActionsLogDTO dto)
        {
            _playerActions = dto.PlayerActions;
            _server = server;
        }
#else
        public PlayerActionsLog()
        {
            _playerActions = new List<PlayerAction>();
        }

        public PlayerActionsLog(PlayerActionsLogDTO dto)
        {
            _playerActions = dto.PlayerActions;
        }
#endif



        public void AddPlayerAction(PlayerAction action)
        {
            _playerActions.Add(action);

#if !(UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL || UNITY_IOS || UNITY_IPHONE || UNITY_ANDROID || UNITY_WII || UNITY_PS4 || UNITY_SAMSUNGTV || UNITY_XBOXONE || UNITY_TIZEN || UNITY_TVOS || UNITY_WP_8_1 || UNITY_WSA || UNITY_WSA_8_1 || UNITY_WSA_10_0 || UNITY_WINRT || UNITY_WINRT_8_1 || UNITY_WINRT_10_0)
        _server.ServiceContainer.DatabaseService.WriteActionLogToDb(this);
#endif

        }

        public void AddPlayerAction(Message message)
        {
            uint offset = 0;
            _playerActions.Add(PlayerAction.FromMessageArguments(message, ref offset));


#if !(UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL || UNITY_IOS || UNITY_IPHONE || UNITY_ANDROID || UNITY_WII || UNITY_PS4 || UNITY_SAMSUNGTV || UNITY_XBOXONE || UNITY_TIZEN || UNITY_TVOS || UNITY_WP_8_1 || UNITY_WSA || UNITY_WSA_8_1 || UNITY_WSA_10_0 || UNITY_WINRT || UNITY_WINRT_8_1 || UNITY_WINRT_10_0)
            _server.ServiceContainer.DatabaseService.WriteActionLogToDb(this);
#endif
        }

        public DatabaseObject ToDBObject()
        {
            DatabaseObject dbObject = new DatabaseObject();

            DatabaseArray actionsDB = new DatabaseArray();
            if (_playerActions != null)
            {
                foreach (var action in _playerActions)
                {
                    actionsDB.Add(action.ToDBObject());
                }
            }
            dbObject.Set("PlayerActions", actionsDB);

            return dbObject;
        }

#if !(UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL || UNITY_IOS || UNITY_IPHONE || UNITY_ANDROID || UNITY_WII || UNITY_PS4 || UNITY_SAMSUNGTV || UNITY_XBOXONE || UNITY_TIZEN || UNITY_TVOS || UNITY_WP_8_1 || UNITY_WSA || UNITY_WSA_8_1 || UNITY_WSA_10_0 || UNITY_WINRT || UNITY_WINRT_8_1 || UNITY_WINRT_10_0)
        public static PlayerActionsLog FromDBObject(DatabaseObject dbObject, ServerCode server)
        {
            if (dbObject.Count == 0) return null;

            PlayerActionsLog log = new PlayerActionsLog(server);
     
            var actionsDB = dbObject.GetArray("PlayerActions");
            for (int i = 0; i < actionsDB.Count; i++)
            {
                log._playerActions.Add(PlayerAction.FromDBObject((DatabaseObject)actionsDB[i]));
            }

            return log;
        }
#else
            public static PlayerActionsLog FromDBObject(DatabaseObject dbObject)
        {
            if (dbObject.Count == 0) return null;

            PlayerActionsLog log = new PlayerActionsLog();
     
            var actionsDB = dbObject.GetArray("Cards");
            for (int i = 0; i < actionsDB.Count; i++)
            {
                log._playerActions.Add(PlayerAction.FromDBObject((DatabaseObject)actionsDB[i]));
            }

            return log;
        }
#endif
    }
}
