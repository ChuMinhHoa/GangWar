using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSheet : SheetBase<RoomElementData>
{
    public override void ActioncallBackOnSlot(SlotBase<RoomElementData> slot)
    {
        if (slot == currentSlot) return;
        if (currentSlot != null) (currentSlot as UpgradeSlot).DeSelectedSwitchMode();
        currentSlot = slot;
        if (actionCallback != null)
            actionCallback(slot);
    }

    public void SetCurrentRoomType(RoomType rType) {
        for (int i = 0; i < listSlots.Count; i++)
        {
            (listSlots[i] as UpgradeSlot).SetCurrentRoomType(rType);
        }
    }

    public void GetNextSlot() {
        for (int i = 0; i < listSlots.Count; i++) {
            if (listSlots[i] == currentSlot) {
                if (i+1<listSlots.Count)
                    listSlots[i + 1].OnChoose();
                else listSlots[0].OnChoose();
                return;
            }
        }
    }
}
