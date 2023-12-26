using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelBase : MonoBehaviour
{
    public UIPanelType uIPanelType;
    public virtual void Awake() {
        UIManager.Instance.RegisterPanel(uIPanelType, gameObject);
    }
}
