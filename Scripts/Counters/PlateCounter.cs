using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlateCounter : BaseCounter
{
    public event EventHandler OnSpawnPlate;
    public event EventHandler OnRemoved;
    [SerializeField] KitchenObjectVisualSO kitchenObjectSO;
    private float spawnTimer;
    private float spawnTimerMax = 4f;
    private int amountPlate;
    private int amountPlateMax = 5;


    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnTimerMax)
        {
            spawnTimer = 0f;
            if (KitChenGameManager.instance.IsGamePlaying() && amountPlate < amountPlateMax)
            {
                amountPlate++;
                OnSpawnPlate?.Invoke(this, EventArgs.Empty);
            }
                
        }
  
    }

    public override void Interact(Player player)
    {
        if(!player.HasKitChenObject())
        {
            //player is empty handed
            if(amountPlate > 0 )
            {
                amountPlate--;

                KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
                OnRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }

}
