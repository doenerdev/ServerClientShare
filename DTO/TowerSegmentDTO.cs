using System;
using System.Collections.Generic;
using System.Text;
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL || UNITY_IOS || UNITY_IPHONE || UNITY_ANDROID || UNITY_WII || UNITY_PS4 || UNITY_SAMSUNGTV || UNITY_XBOXONE || UNITY_TIZEN || UNITY_TVOS || UNITY_WP_8_1 || UNITY_WSA || UNITY_WSA_8_1 || UNITY_WSA_10_0 || UNITY_WINRT || UNITY_WINRT_8_1 || UNITY_WINRT_10_0
using PlayerIOClient;
#else
using PlayerIO.GameLibrary;
#endif
using ServerClientShare.DTO;


public class TowerSegmentDTO : DTO<TowerSegmentDTO>
{
    public List<TowerResourceDTO> RequiredResources { get; set; }

    public TowerSegmentDTO() { }

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

        return message;
    }

    public new static TowerSegmentDTO FromMessageArguments(Message message, ref uint offset)
    {
        TowerSegmentDTO dto = new TowerSegmentDTO();
        var qtyRequiredResources = message.GetInt(offset++);

        while (offset < qtyRequiredResources)
        {
            dto.RequiredResources.Add(TowerResourceDTO.FromMessageArguments(message, ref offset));
        }
        return dto;
    }
}

