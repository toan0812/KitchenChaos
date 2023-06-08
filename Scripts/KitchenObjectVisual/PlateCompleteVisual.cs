using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitChenObjectSO_GameObject
    {
        public GameObject GameObject;
        public KitchenObjectVisualSO kitchenObjectVisualSO;
    }
    [SerializeField] PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitChenObjectSO_GameObject> kitChenObjectSO_GameObject;
    private void Start()
    {
        plateKitchenObject.OnAddIngredient += PlateKitchenObject_OnAddIngredient;

        foreach (KitChenObjectSO_GameObject kitChenObjectSOGameObject in kitChenObjectSO_GameObject)
        {
            kitChenObjectSOGameObject.GameObject.SetActive(false);
        }
    }

    private void PlateKitchenObject_OnAddIngredient(object sender, PlateKitchenObject.OnAddIngredientArgs e)
    {
        foreach(KitChenObjectSO_GameObject kitChenObjectSOGameObject in kitChenObjectSO_GameObject)
        {
            if(kitChenObjectSOGameObject.kitchenObjectVisualSO == e.kitchenObjectVisualSO)
            {
                kitChenObjectSOGameObject.GameObject.SetActive(true);
            }
        }
    }
}
