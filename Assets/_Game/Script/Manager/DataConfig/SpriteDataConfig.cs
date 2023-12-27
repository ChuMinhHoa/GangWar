using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpriteDataConfig", menuName = "ScriptableObject/SpriteDataConfig")]
public class SpriteDataConfig : ScriptableObject
{
    public List<Sprite> spriteIconRoomElement = new List<Sprite>();
    public Sprite GetSpriteRoomElement(RoomElementType rType) {
        for (int i = 0; i < spriteIconRoomElement.Count; i++)
        {
            if (spriteIconRoomElement[i].name == rType.ToString())
                return spriteIconRoomElement[i];
        }
        return null;
    }
}
