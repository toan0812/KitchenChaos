using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CuttingRecipeSO : ScriptableObject
{
    public KitchenObjectVisualSO kitChenInput;
    public KitchenObjectVisualSO kitChenOnput;
    public int cuttingProgressMax;
}
