using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{

    [SerializeField] private KitchenObjectVisualSO kitchenObjectVisualSO;

    private IKitChenObject IKitChenObjectParent;
    public KitchenObjectVisualSO GetKitchenObjectVisualSO()
    {
        return kitchenObjectVisualSO;
    }

    public void SetKitChenObjectParent(IKitChenObject IKitChenObjectParent)
    {
        if(this.IKitChenObjectParent != null)
        {
            this.IKitChenObjectParent.ClearKitChenObject();
        }
        if(IKitChenObjectParent.HasKitChenObject())
        {
            //Debug.LogError("Counter has object");
        }
        this.IKitChenObjectParent = IKitChenObjectParent;
        IKitChenObjectParent.SetKitChenObject(this);
        transform.parent = IKitChenObjectParent.GetKitChenObjectFollowTransform();
        transform.localPosition = Vector3.zero;

    }
    public IKitChenObject GetKitChenObjectParent()
    {
        return IKitChenObjectParent;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectVisualSO kitchenObjectVisualSO, IKitChenObject kitChenObject)
    {
        Transform kitChanObjectTransform = Instantiate(kitchenObjectVisualSO.preFab);
        KitchenObject kitChenObject1 = kitChanObjectTransform.GetComponent<KitchenObject>();
        kitChenObject1.SetKitChenObjectParent(kitChenObject);

        return kitChenObject1;
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if (this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else plateKitchenObject = null; 
        return false;
    }

}
