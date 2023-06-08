using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class FryingRecipeSO : ScriptableObject
{
    public KitchenObjectVisualSO kitChenInput;
    public KitchenObjectVisualSO kitChenOnput;
    public float FryingTimeMax;
}
