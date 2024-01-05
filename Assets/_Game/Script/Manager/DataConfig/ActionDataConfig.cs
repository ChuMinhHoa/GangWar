using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActionDataConfig", menuName = "ScriptableObject/ActionDataConfig")]
public class ActionDataConfig : ScriptableObject
{
    public List<ActionData> actionDatas = new List<ActionData>();

    public ActionData GetActionData(ActionType actionType) {
        return actionDatas.Find(e => e.actionType == actionType);
    }
}

[System.Serializable]
public class ActionData {
    public ActionType actionType;
    public float timeDoAction;
}
