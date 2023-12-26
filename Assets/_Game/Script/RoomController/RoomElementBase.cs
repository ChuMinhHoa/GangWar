using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class RoomElementBase
{
    public RoomElementType rType;
    public List<Transform> pointSpawns = new List<Transform>();
    public RoomElementData rData;
}
