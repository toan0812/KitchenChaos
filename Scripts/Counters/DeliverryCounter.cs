using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverryCounter : BaseCounter
{
    public static DeliverryCounter Instance { get; private set; }
    public override void Interact(Player player)
    {
        if(player.HasKitChenObject())
        {
            if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                //only access Plate
                DeliveryManager.Instance.DeleveryRecipe(plateKitchenObject);
                player.GetKitchenObject().DestroySelf();
            }
        }
    }
}
