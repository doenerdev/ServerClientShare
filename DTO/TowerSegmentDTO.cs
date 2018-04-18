using System;
using System.Collections.Generic;
using System.Text;
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL || UNITY_IOS || UNITY_IPHONE || UNITY_ANDROID || UNITY_WII || UNITY_PS4 || UNITY_SAMSUNGTV || UNITY_XBOXONE || UNITY_TIZEN || UNITY_TVOS || UNITY_WP_8_1 || UNITY_WSA || UNITY_WSA_8_1 || UNITY_WSA_10_0 || UNITY_WINRT || UNITY_WINRT_8_1 || UNITY_WINRT_10_0
using PlayerIOClient;
#else
using PlayerIO.GameLibrary;
#endif
using ServerClientShare.DTO;

[Serializable]
public class TowerSegmentDTO : DatabaseDTO<TowerSegmentDTO>
{
    public List<TowerResourceDTO> RequiredResources { get; set; }
    public bool IsFinished { get; set; }
    public TowerSegmentVisualStyle VisualStyle { get; set; }

    public TowerSegmentDTO()
    {
        RequiredResources = new List<TowerResourceDTO>();
        IsFinished = false;
    }

    public TowerSegmentDTO(List<TowerResourceDTO> resources)
    {
        RequiredResources = resources;
    }

    public override Message ToMessage(Message message)
    {
        message.Add(RequiredResources.Count);

        foreach (var resource in RequiredResources)
        {
            message = resource.ToMessage(message);
        }
        message.Add(IsFinished);
        message.Add((int) VisualStyle);

        return message;
    }

    public new static TowerSegmentDTO FromMessageArguments(Message message, ref uint offset)
    {
        TowerSegmentDTO dto = new TowerSegmentDTO();
        var qtyRequiredResources = message.GetInt(offset++);

        for(int i = 0; i < qtyRequiredResources; i++)
        {
            dto.RequiredResources.Add(TowerResourceDTO.FromMessageArguments(message, ref offset));
        }
        dto.IsFinished = message.GetBoolean(offset++);
        dto.VisualStyle = (TowerSegmentVisualStyle) message.GetInt(offset++);
        return dto;
    }

    public override DatabaseObject ToDBObject()
    {
        DatabaseObject dbObject = new DatabaseObject();
        DatabaseArray resourcesDB = new DatabaseArray();
        if (RequiredResources != null)
        {
            foreach (var resource in RequiredResources)
            {
                resourcesDB.Add(resource.ToDBObject());
            }
        }
        dbObject.Set("RequiredResources", resourcesDB);
        dbObject.Set("IsFinished", IsFinished);
        dbObject.Set("VisualStyle", (int) VisualStyle);
        return dbObject;
    }

    public new static TowerSegmentDTO FromDBObject(DatabaseObject dbObject)
    {
        if (dbObject.Count == 0) return null;

        TowerSegmentDTO dto = new TowerSegmentDTO();
        var resourcesDB = dbObject.GetArray("RequiredResources");
        for (int i = 0; i < resourcesDB.Count; i++)
        {
            dto.RequiredResources.Add(TowerResourceDTO.FromDBObject((DatabaseObject)resourcesDB.GetObject(i)));
        }
        dto.IsFinished = dbObject.GetBool("IsFinished");
        dto.VisualStyle = (TowerSegmentVisualStyle) dbObject.GetInt("VisualStyle");
        return dto;
    }
}

