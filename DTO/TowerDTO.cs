using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerClientShare.DTO;
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL || UNITY_IOS || UNITY_IPHONE || UNITY_ANDROID || UNITY_WII || UNITY_PS4 || UNITY_SAMSUNGTV || UNITY_XBOXONE || UNITY_TIZEN || UNITY_TVOS || UNITY_WP_8_1 || UNITY_WSA || UNITY_WSA_8_1 || UNITY_WSA_10_0 || UNITY_WINRT || UNITY_WINRT_8_1 || UNITY_WINRT_10_0
using PlayerIOClient;
#else
using PlayerIO.GameLibrary;
#endif

[Serializable]
public class TowerDTO : DatabaseDTO<TowerDTO>
{
    public List<TowerSegmentDTO> TowerSegments { get; set; }
    public int CurrentTowerSegmentIndex { get; set; }

    public TowerDTO()
    {
        TowerSegments = new List<TowerSegmentDTO>();
    }

    public TowerDTO(List<TowerSegmentDTO> towerSegments)
    {
        TowerSegments = towerSegments;
    }

    public override Message ToMessage(Message message)
    {
        message.Add(TowerSegments.Count);

        foreach (var towerSegment in TowerSegments)
        {
            message = towerSegment.ToMessage(message);
        }

        message.Add(CurrentTowerSegmentIndex);

        return message;
    }

    public new static TowerDTO FromMessageArguments(Message message, ref uint offset)
    {
        TowerDTO dto = new TowerDTO();
        var qtySegments = message.GetInt(offset++);

        for (int i = 0; i < qtySegments; i++)
        {
            dto.TowerSegments.Add(TowerSegmentDTO.FromMessageArguments(message, ref offset));
        }

        dto.CurrentTowerSegmentIndex = message.GetInt(offset++);

        return dto;
    }

    public override DatabaseObject ToDBObject()
    {
        DatabaseObject dbObject = new DatabaseObject();
        DatabaseArray segmentsDB = new DatabaseArray();
        if (TowerSegments != null)
        {
            foreach (var segment in TowerSegments)
            {
                segmentsDB.Add(segment.ToDBObject());
            }
        }
        dbObject.Set("Segments", segmentsDB);
        dbObject.Set("CurrentSegmentIndex", CurrentTowerSegmentIndex);

        return dbObject;
    }

    public new static TowerDTO FromDBObject(DatabaseObject dbObject)
    {
        if (dbObject.Count == 0) return null;

        TowerDTO dto = new TowerDTO();
        var segmentsDB = dbObject.GetArray("Segments");
        for (int i = 0; i < segmentsDB.Count; i++)
        {
            dto.TowerSegments.Add(TowerSegmentDTO.FromDBObject((DatabaseObject)segmentsDB.GetObject(i)));
        }
        dto.CurrentTowerSegmentIndex = dbObject.GetInt("CurrentSegmentIndex");

        return dto;
    }
}

