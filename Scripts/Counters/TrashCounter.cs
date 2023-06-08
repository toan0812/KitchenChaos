using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnAnyTrashed;
    new public static void ResetStaticData()
    {
        OnAnyTrashed = null;
    }
    public override void Interact(Player player)
    {
        if(player.HasKitChenObject())
        {
            player.GetKitchenObject().DestroySelf();
            OnAnyTrashed?.Invoke(this, EventArgs.Empty);
        }
    }
}
