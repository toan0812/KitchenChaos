using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawner;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFail;
    public static DeliveryManager Instance { get; private set; }    
    [SerializeField] RecipeSOList recipeSO;
    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeMax = 4;

    private int successfullRecipesAmount;

    private void Awake()
    {
        waitingRecipeSOList = new List<RecipeSO>();
        Instance = this;
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if(spawnRecipeTimer<= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;
            if(KitChenGameManager.instance.IsGamePlaying() && waitingRecipeSOList.Count< waitingRecipeMax)
            {
                RecipeSO waitingRecipeSO = recipeSO.recipeSOList[UnityEngine.Random.Range(0, recipeSO.recipeSOList.Count)];
                
                waitingRecipeSOList.Add(waitingRecipeSO);

                OnRecipeSpawner?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeleveryRecipe(PlateKitchenObject plateKitchenObject)
    {
        for(int i =0; i < waitingRecipeSOList.Count; i++)
        {
           RecipeSO waitingRecipeSO = waitingRecipeSOList[i];
            if(waitingRecipeSO.kitChenObjectSOList.Count == plateKitchenObject.GetKitchenObjectVisualSOList().Count)
            {
                // has the same number of ingradient
                bool plateContentMatchesRecipe = true;
                foreach(KitchenObjectVisualSO recipeKitchenObjectVisualSO in waitingRecipeSO.kitChenObjectSOList)
                {
                    //
                    bool ingradientFounded = true;
                    foreach(KitchenObjectVisualSO plateKitChenObjectSO in plateKitchenObject.GetKitchenObjectVisualSOList())
                    {
                        //
                        if(plateKitChenObjectSO == recipeKitchenObjectVisualSO)
                        {
                            // ingradient Match
                            ingradientFounded = true;
                            break;
                        }
                    }
                    if(!ingradientFounded)
                    {
                        // this Recipe ingradient was not found on the plate
                        plateContentMatchesRecipe = false;
                    }
                   
                }
                if (plateContentMatchesRecipe) 
                {
                    // player deliveried the correct recipe!                  
                    waitingRecipeSOList.RemoveAt(i);
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    successfullRecipesAmount += 1;
                    return;


                }
            }
        }
        //Not Match
        //Player did not deliveried a correct recipe
        OnRecipeFail?.Invoke(this, EventArgs.Empty);
    }


    public List<RecipeSO> GetWaitingRecipeSOList()
        { return waitingRecipeSOList; 
    }

    public int GetSuccessfullDeliveryAmount()
    {
        return successfullRecipesAmount;
    }

}
