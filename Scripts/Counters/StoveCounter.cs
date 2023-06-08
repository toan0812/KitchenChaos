using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StoveCounter : BaseCounter,IHasProGress
{
    public event EventHandler<IHasProGress.OnprogressChangedArgs> OnprogressChanged;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;

    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned,
    }
    [SerializeField] FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] BurningRecipeSO[] burningRecipeSOArray;

    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;
    private float fryTimer;
    private float brunedTimer;
    private State state;

    private void Start()
    {
        state = State.Idle;
    }
    private void Update()
    {
        if(HasKitChenObject())
        {
            switch(state)
            {
                case State.Idle:

                    break;
                case State.Frying:
                    fryTimer += Time.deltaTime;
                    OnprogressChanged.Invoke(this, new IHasProGress.OnprogressChangedArgs
                    {
                        progressNomalized = fryTimer / fryingRecipeSO.FryingTimeMax


                    });
                    {
                        if(fryTimer> fryingRecipeSO.FryingTimeMax)
                        {
                            //Fried
                            GetKitchenObject().DestroySelf();
                            KitchenObject.SpawnKitchenObject(fryingRecipeSO.kitChenOnput, this);
                            state = State.Fried;
                            brunedTimer = 0f;
                            burningRecipeSO = GetBurningRecipeWithInput(GetKitchenObject().GetKitchenObjectVisualSO());

                            OnStateChanged.Invoke(this, new OnStateChangedEventArgs { state = state });

                        }
                    }
                    break;
                case State.Fried:
                    brunedTimer += Time.deltaTime;
                    OnprogressChanged.Invoke(this, new IHasProGress.OnprogressChangedArgs
                    {
                        progressNomalized = brunedTimer / burningRecipeSO.BurningTimeMax


                    });
                    {
                        if (brunedTimer > burningRecipeSO.BurningTimeMax)
                        {
                            //Burned                          
                            GetKitchenObject().DestroySelf();
                            KitchenObject.SpawnKitchenObject(burningRecipeSO.kitChenOnput, this);
                            state = State.Burned;

                            OnStateChanged.Invoke(this, new OnStateChangedEventArgs { state = state });
                            OnprogressChanged.Invoke(this, new IHasProGress.OnprogressChangedArgs
                            {
                                progressNomalized = 0f


                            }) ;     
                        }
                    }
                    break;
                case State.Burned:
                    break;
            }

        }
    }
    public override void Interact(Player player)
    {
        if (!HasKitChenObject())
        // there no has kitchen
        {
            if (player.HasKitChenObject())
            {
                //Player is carrying smt 
                if (HasRecipeInput(player.GetKitchenObject().GetKitchenObjectVisualSO()))
                {
                    //can be fry
                    player.GetKitchenObject().SetKitChenObjectParent(this);
                    fryingRecipeSO = GetFryingRecipeWithInput(GetKitchenObject().GetKitchenObjectVisualSO());
                    fryTimer = 0f;
                    state = State.Frying;
                    OnStateChanged.Invoke(this, new OnStateChangedEventArgs { state = state });
                    OnprogressChanged.Invoke(this, new IHasProGress.OnprogressChangedArgs
                    {
                        progressNomalized = fryTimer / fryingRecipeSO.FryingTimeMax


                    });

                }

            }
        }
        else
        {
            //there has smt
            if (player.HasKitChenObject())
            {
                // Player is carrying smt 
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //Player is holding plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectVisualSO()))
                    {
                        GetKitchenObject().DestroySelf();
                        state = State.Idle;
                        OnStateChanged.Invoke(this, new OnStateChangedEventArgs { state = state });
                        OnprogressChanged.Invoke(this, new IHasProGress.OnprogressChangedArgs
                        {
                            progressNomalized = 0f
                        });
                    }
                }
            }
            else
            {
                GetKitchenObject().SetKitChenObjectParent(player);
                state = State.Idle;
                OnStateChanged.Invoke(this, new OnStateChangedEventArgs { state = state });
                OnprogressChanged.Invoke(this, new IHasProGress.OnprogressChangedArgs
                {
                    progressNomalized = 0f


                });
            }
        }
    }

    private bool HasRecipeInput(KitchenObjectVisualSO inoutKitchenObjectVisualSO)
    {
        FryingRecipeSO cuttingRecipeSO = GetFryingRecipeWithInput(inoutKitchenObjectVisualSO);
        return cuttingRecipeSO != null;
    }

    private KitchenObjectVisualSO GetPutPutForInput(KitchenObjectVisualSO inoutKitchenObjectVisualSO)
    {
        FryingRecipeSO cuttingRecipeSO = GetFryingRecipeWithInput(inoutKitchenObjectVisualSO);
        if (cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.kitChenOnput;
        }
        return null;
    }

    private FryingRecipeSO GetFryingRecipeWithInput(KitchenObjectVisualSO inputKitchenObjectVisualSO)
    {
        foreach (FryingRecipeSO FryingRecipeSO in fryingRecipeSOArray)
        {
            if (FryingRecipeSO.kitChenInput == inputKitchenObjectVisualSO)
            {
                return FryingRecipeSO;
            }
        }
        return null;
    } 
    private BurningRecipeSO GetBurningRecipeWithInput(KitchenObjectVisualSO inputKitchenObjectVisualSO)
    {
        foreach (BurningRecipeSO BurningRecipeSO in burningRecipeSOArray)
        {
            if (BurningRecipeSO.kitChenInput == inputKitchenObjectVisualSO)
            {
                return BurningRecipeSO;
            }
        }
        return null;

    }

    public bool ISFried()
    {
       return state == State.Fried;
    }
}
