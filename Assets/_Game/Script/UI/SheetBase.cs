using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SheetBase <Data> : MonoBehaviour
{
    public List<SlotBase<Data>> listSlots;
    public SlotBase<Data> slotPref;
    public SlotBase<Data> currentSlot;
    public Transform trsParents;
    SlotBase<Data> slotTemp;
    public UnityAction<SlotBase<Data>> actionCallback;
    public void LoadData(List<Data> datas) {
        for (int i = 0; i < listSlots.Count; i++)
            listSlots[i].gameObject.SetActive(false);
        for (int i = 0; i < datas.Count; i++)
        {
            slotTemp = GetSlotFree();
            slotTemp.gameObject.SetActive(true);
            slotTemp.InitData(datas[i]);
            slotTemp.SetActionCallback(ActioncallBackOnSlot);
        }
    }
    public void SetActionCallBack(UnityAction<SlotBase<Data>> actionCallback) { this.actionCallback = actionCallback; }
    public virtual void ActioncallBackOnSlot(SlotBase<Data> slot) {
        if (slot == currentSlot) return;
        currentSlot = slot;
        if (actionCallback != null)
            actionCallback(slot);
    }

    public SlotBase<Data> GetSlotFree()
    {
        for (int i = 0; i < listSlots.Count; i++)
        {
            if (!listSlots[i].gameObject.activeSelf)
                return listSlots[i];
        }
        SlotBase<Data> newSlot = Instantiate(slotPref, trsParents);
        listSlots.Add(newSlot);
        return newSlot;
    }

    public virtual void ReloadDataOnSlot() {
        currentSlot.ReloadData();
    }
}
