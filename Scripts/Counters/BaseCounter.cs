using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseCounter : MonoBehaviour,IKitChenObject
{
    public static event EventHandler OnAnyObjectPlacedHere;
    new public static void ResetStaticData()
    {
        OnAnyObjectPlacedHere = null;
    }
    [SerializeField] private Transform counterPoin;
    private KitchenObject kitchenObjects;
    public virtual void Interact(Player player)
    {
        Debug.LogError("BaseCounter.Interact()");
    }   public virtual void InteractAltnate(Player player)
    {
        //Debug.LogError("BaseCounter.InteractAltnate()");
    }
    public Transform GetKitChenObjectFollowTransform()
    {
        return counterPoin;
    }

    public void SetKitChenObject(KitchenObject kitchenObject)
    {
        this.kitchenObjects = kitchenObject;
        if(kitchenObject != null)
        {
            OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObjects;
    }
    public void ClearKitChenObject()
    {
        kitchenObjects = null;
    }

    public bool HasKitChenObject()
    {
        return kitchenObjects != null;
    }

}
