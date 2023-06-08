using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateSingleUI : MonoBehaviour
{
    [SerializeField] private Image Image;
  public void SetKitChenObjectSO(KitchenObjectVisualSO kitchenObjectVisualSO)
    {
        Image.sprite = kitchenObjectVisualSO.sprite;
    }
}
