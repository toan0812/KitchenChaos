using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    public override void Interact(Player Player)
    {
       if(!HasKitChenObject())
            // there no has kitchen
        {
            if(Player.HasKitChenObject())
            {
                //Player is carrying smt
                Player.GetKitchenObject().SetKitChenObjectParent(this);
            }          
        }
       else
        {
            //there has smt
            if(Player.HasKitChenObject())
            {
               // Player is carrying smt 
               if(Player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
               {
                    //Player is holding plate
                    if(plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectVisualSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
               }
               else
                {
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        //Counter is holding smt
                        if (plateKitchenObject.TryAddIngredient(Player.GetKitchenObject().GetKitchenObjectVisualSO()))
                        {
                            Player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else
            {
                GetKitchenObject().SetKitChenObjectParent(Player);
            }
        }
    }

}
