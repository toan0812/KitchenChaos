using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKitChenObject
{
    public Transform GetKitChenObjectFollowTransform();
    public void SetKitChenObject(KitchenObject kitchenObject);
    public KitchenObject GetKitchenObject();
    public void ClearKitChenObject();
    public bool HasKitChenObject();
}
