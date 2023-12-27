using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoomElementData
{
    public RoomElementType rType;
    public int rElementID;
    public int levelDefault;
    public List<BigNumber> prices = new List<BigNumber>();
    public BigNumber GetPrice(int level) {
        return prices[level] * (1f + rElementID / 10f);
    }
}

[System.Serializable]
public class RoomElementDescription
{
    public RoomElementType rElementType;
    public string description;
}
