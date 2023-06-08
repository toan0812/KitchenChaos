using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectVisualSO kitchenObjectSO;

    public event EventHandler OnPlayerGrabbed;
    public override void Interact(Player Player)
    {
        if(!Player.HasKitChenObject())
        {
           KitchenObject.SpawnKitchenObject(kitchenObjectSO, Player);

            OnPlayerGrabbed?.Invoke(this, EventArgs.Empty);
        }
       
    }

   
}
