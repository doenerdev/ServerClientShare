using System;
using System.Collections.Generic;
using System.Text;
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL || UNITY_IOS || UNITY_IPHONE || UNITY_ANDROID || UNITY_WII || UNITY_PS4 || UNITY_SAMSUNGTV || UNITY_XBOXONE || UNITY_TIZEN || UNITY_TVOS || UNITY_WP_8_1 || UNITY_WSA || UNITY_WSA_8_1 || UNITY_WSA_10_0 || UNITY_WINRT || UNITY_WINRT_8_1 || UNITY_WINRT_10_0
using PlayerIOClient;
#else
using PlayerIO.GameLibrary;
#endif
using ServerClientShare.DTO;
using ServerClientShare.Interfaces;
using ServerClientShare.MiniJson;
using ServerClientShare.Models;



public class PlayerActionsLogDTO : DatabaseDTO<PlayerActionsLogDTO>
{
    public List<PlayerAction> PlayerActions { get; set; }

    public PlayerActionsLogDTO()
    {
        PlayerActions = new List<PlayerAction>();
    }

    public override Message ToMessage(Message message)
    {
        message.Add(PlayerActions.Count);

        foreach (var playerAction in PlayerActions)
        {
            message = playerAction.ToMessage(message);
        }

        return message;
    }

    public static PlayerActionsLogDTO FromMessageArguments(Message message, ref uint offset)
    {
        var dto = new PlayerActionsLogDTO();
        var actionsCount = message.GetInt(offset++);

        for (int i = 0; i < actionsCount; i++)
        {
            dto.PlayerActions.Add(PlayerAction.FromMessageArguments(message, ref offset));
        }
        return dto;
    }

    public override DatabaseObject ToDBObject()
    {
        DatabaseObject dbObject = new DatabaseObject();

        DatabaseArray actionsDB = new DatabaseArray();
        if (PlayerActions != null)
        {
            foreach (var action in PlayerActions)
            {
                actionsDB.Add(action.ToDBObject());
            }
        }
        dbObject.Set("PlayerActions", actionsDB);

        return dbObject;
    }

    public static PlayerActionsLogDTO FromDBObject(DatabaseObject dbObject)
    {
        if (dbObject.Count == 0) return null;

        var dto = new PlayerActionsLogDTO();
        var actionsDB = dbObject.GetArray("PlayerActions");
        for (int i = 0; i < actionsDB.Count; i++)
        {
            dto.PlayerActions.Add(PlayerAction.FromDBObject((DatabaseObject)actionsDB[i]));
        }

        return dto;
    }
}

