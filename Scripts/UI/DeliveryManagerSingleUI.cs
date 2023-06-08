using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeName;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }
    public void SetRecipeSO(RecipeSO recipeSO)
    {
        recipeName.text = recipeSO.recipeName;

        foreach(Transform Child in iconContainer)
        {
            if (Child == iconTemplate) continue;
            Destroy(Child.gameObject);
        }

        foreach (KitchenObjectVisualSO KitchenObjectVisualSO in recipeSO.kitChenObjectSOList)
        { 
            Transform IconTransform = Instantiate(iconTemplate, iconContainer);
            IconTransform.gameObject.SetActive(true);
            IconTransform.GetComponent<Image>().sprite = KitchenObjectVisualSO.sprite;
        }

    }
}
