using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        plateKitchenObject.OnAddIngredient += PlateKitchenObject_OnAddIngredient;
    }

    private void PlateKitchenObject_OnAddIngredient(object sender, PlateKitchenObject.OnAddIngredientArgs e)
    {
        UpdateVisual();

    }

    private void UpdateVisual()
    {
        foreach (Transform child in transform)
        {
            if (child != iconTemplate)
            {
                Destroy(child.gameObject);
            }
            else continue;
        }
        foreach (KitchenObjectVisualSO kitchenObjectVisualSO in plateKitchenObject.GetKitchenObjectVisualSOList())
        {
            Transform iconTransform = Instantiate(iconTemplate, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<PlateSingleUI>().SetKitChenObjectSO(kitchenObjectVisualSO);
        }
    }
}
