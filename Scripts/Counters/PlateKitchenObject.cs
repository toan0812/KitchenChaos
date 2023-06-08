using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnAddIngredientArgs> OnAddIngredient;
    public class OnAddIngredientArgs : EventArgs
    {
        public KitchenObjectVisualSO kitchenObjectVisualSO;
    }
    private List<KitchenObjectVisualSO> kitchenObjectVisualSOs;
    [SerializeField] private List<KitchenObjectVisualSO> validKitchenObjectVisualSOList;
    private void Awake()
    {
        kitchenObjectVisualSOs = new List<KitchenObjectVisualSO>();
    }
    public bool TryAddIngredient(KitchenObjectVisualSO kitchenObjectVisualSO)
    {
        if(!validKitchenObjectVisualSOList.Contains(kitchenObjectVisualSO))
        {
            return false;
        }
        if(kitchenObjectVisualSOs.Contains(kitchenObjectVisualSO))
        {
            // alredy has this type 
            return false;
        }
        else
        {
            kitchenObjectVisualSOs.Add(kitchenObjectVisualSO);
            OnAddIngredient?.Invoke(this, new OnAddIngredientArgs
            {
                kitchenObjectVisualSO = kitchenObjectVisualSO
            });
            return true;
        }

    }

    public List<KitchenObjectVisualSO> GetKitchenObjectVisualSOList()
    {
        return kitchenObjectVisualSOs;
    }
}
