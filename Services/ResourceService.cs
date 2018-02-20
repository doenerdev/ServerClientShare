using System;
using System.Collections.Generic;
using System.Text;
using ServerClientShare.DTO;
using ServerClientShare.Enums;
using ServerClientShare.Helper;


public class ResourceService
{
    private RandomGenerator _rndGenerator;
    private Die _die;

    public ResourceService(ServerClientShare.Helper.Die die, ServerClientShare.Helper.RandomGenerator rndGenerator)
    {
        _die = die;
        _rndGenerator = rndGenerator;
    }

    public TowerSegmentDTO GenerateNewTowerSegment()
    {
        int qtyRessources = DieThrowToRessourceCount(_die.RollW12());
        List<TowerResourceDTO> requiredRessources = new List<TowerResourceDTO>();
        for (int i = 0; i < qtyRessources; i++)
        {
            requiredRessources.Add(DieThrowToRessource(_die.RollW6()));
        }

        return new TowerSegmentDTO(requiredRessources);
    }

    private TowerResourceDTO DieThrowToRessource(int die)
    {
        return die >= 6
            ? new TowerResourceDTO(ResourceType.Glass)
            : die >= 4
                ? new TowerResourceDTO(ResourceType.Glass)
                : new TowerResourceDTO(ResourceType.Wood);

    }

    private int DieThrowToRessourceCount(int die)
    {
        die = die >= 15 ? die + 3 : die >= 10 ? die + 2 : die >= 5 ? die + 1 : die;
        return die >= 12 ? 5 : die >= 10 ? 4 : die >= 7 ? 3 : 2;
    }
}

