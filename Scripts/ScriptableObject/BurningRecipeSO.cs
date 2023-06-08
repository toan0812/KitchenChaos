using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class BurningRecipeSO : ScriptableObject
{
    public KitchenObjectVisualSO kitChenInput;
    public KitchenObjectVisualSO kitChenOnput;
    public float BurningTimeMax;
}
