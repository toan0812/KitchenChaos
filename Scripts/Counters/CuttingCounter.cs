using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CuttingCounter : BaseCounter,IHasProGress
{


    public static event EventHandler  OnAnyCut;
    new public static void ResetStaticData()
    {
        OnAnyCut = null;
    }
    [SerializeField] private CuttingRecipeSO[] cuttingKitchenObjectSOArray;
    private int cuttingProgress;

    public event EventHandler<IHasProGress.OnprogressChangedArgs> OnprogressChanged;
    public event EventHandler OnCut;
   

    public override void Interact(Player Player)
    {
        if (!HasKitChenObject())
        // there no has kitchen
        {
            if (Player.HasKitChenObject())
            {
                //Player is carrying smt 
                if(HasRecipeInput(Player.GetKitchenObject().GetKitchenObjectVisualSO()))
                {
                    //can be cut
                    Player.GetKitchenObject().SetKitChenObjectParent(this);
                    cuttingProgress = 0;
                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeWithInput(GetKitchenObject().GetKitchenObjectVisualSO());
                    OnprogressChanged?.Invoke(this, new IHasProGress.OnprogressChangedArgs
                    {
                        progressNomalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                    }); 
                }
                
            }
        }
        else
        {
            //there has smt
            if (Player.HasKitChenObject())
            {
                // Player is carrying smt 
                if (Player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //Player is holding plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectVisualSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
            }
            else
            {
                GetKitchenObject().SetKitChenObjectParent(Player);
            }
        }
       
    }
    public override void InteractAltnate(Player player)
    {
        if (HasKitChenObject() && HasRecipeInput(GetKitchenObject().GetKitchenObjectVisualSO()))
        {
            //has smt on the cutting counter and can be cut
            cuttingProgress++;
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeWithInput(GetKitchenObject().GetKitchenObjectVisualSO());
            OnCut?.Invoke(this, EventArgs.Empty);

            OnAnyCut?.Invoke(this, EventArgs.Empty);

            OnprogressChanged?.Invoke(this, new IHasProGress.OnprogressChangedArgs
            {
                progressNomalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
            });
            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
            {
                KitchenObjectVisualSO cuttingKitchenObjectSO = GetPutPutForInput(GetKitchenObject().GetKitchenObjectVisualSO());
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(cuttingKitchenObjectSO, this);
            }
            
        }
    }
    private bool HasRecipeInput(KitchenObjectVisualSO inoutKitchenObjectVisualSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeWithInput(inoutKitchenObjectVisualSO);
        return cuttingRecipeSO != null;
    }

    private KitchenObjectVisualSO GetPutPutForInput(KitchenObjectVisualSO inoutKitchenObjectVisualSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeWithInput(inoutKitchenObjectVisualSO);
        if(cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.kitChenOnput;
        }      
        return null;
    }

    private CuttingRecipeSO GetCuttingRecipeWithInput(KitchenObjectVisualSO inoutKitchenObjectVisualSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingKitchenObjectSOArray)
        {
            if (cuttingRecipeSO.kitChenInput == inoutKitchenObjectVisualSO)
            {
                return cuttingRecipeSO;
            }
        }
        return null;
    }

}
