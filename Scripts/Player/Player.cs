using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour, IKitChenObject
{
    public static Player Instance { get; private set; }
    public event EventHandler OnPickupSomeThing;
    public class OnSelectedCounterChangedArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    public event EventHandler<OnSelectedCounterChangedArgs> OnSelectedCounter;
    [SerializeField] private float MoveSpeed = 7f;
    private bool PlayerWalking;
    [SerializeField] private GameInput gameInput;
    private Vector3 lastInteractDir;
    [SerializeField]private LayerMask clearCounterLayerMask;
    [SerializeField] private Transform kitChenHoldPoint;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObjects;
    private void Awake()
    {
        if(Instance!= null)
        {
            Debug.LogError("error instance");  
        }
        Instance = this;
    }
    private void Start()
    {
        gameInput.OnInteractActions += GameInput_OnInteractActions;
        gameInput.OnInteractAltnateActions += GameInput_OnInteractAltnateActions;
        
    }

    private void GameInput_OnInteractAltnateActions(object sender, EventArgs e)
    {
        if (!KitChenGameManager.instance.IsGamePlaying()) return;
        if (selectedCounter != null)
        {
            selectedCounter.InteractAltnate(this);
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();

    }
    private void GameInput_OnInteractActions(object sender, System.EventArgs e)
    {
        if (!KitChenGameManager.instance.IsGamePlaying()) return;
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }

    }

    public bool PlayerIsWalking()
    {
        return PlayerWalking;
    } 
    private void HandleInteractions()
    {
        Vector2 InputVector = gameInput.GetInputNomalized();
        Vector3 MoveDir = new Vector3(InputVector.x, 0f, InputVector.y);
        RaycastHit raycastHit;
        float interactDistance = 2f;
        if(MoveDir != Vector3.zero)
        {
            lastInteractDir = MoveDir;
        }
        if (Physics.Raycast(transform.position, lastInteractDir, out raycastHit, interactDistance, clearCounterLayerMask))
        {
            
            if (raycastHit.transform.TryGetComponent(out BaseCounter clearCounter))
            {
                if(clearCounter != selectedCounter)
                {
                    SetSelectedCounter(clearCounter);
                }
            }
            else {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null); 
        }
    }

    private void HandleMovement()
    {
        Vector2 InputVector = gameInput.GetInputNomalized();
        Vector3 MoveDir = new Vector3(InputVector.x, 0f, InputVector.y);

        float moveDistance = MoveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeigh = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeigh, playerRadius, MoveDir, moveDistance);
        if (!canMove)
        {
            //Cannot move towards moveDir
            //Attempt move on the X
            Vector3 moveDirX = new Vector3(MoveDir.x, 0, 0).normalized;
            canMove = MoveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeigh, playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                //Can move on the X
                MoveDir = moveDirX;
            }
            else
            {
                //Cannot move towards moveDir
                //Attempt move on the Z
                Vector3 moveDirZ = new Vector3(0, 0, MoveDir.z).normalized;
                canMove = MoveDir.z != 0! && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeigh, playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    //Can move on the Z
                    MoveDir = moveDirZ;
                }
            }

        }
        if (canMove)
        {
            transform.position += MoveDir * moveDistance;
        }

        float RotateSpeed = 10f;
        PlayerWalking = MoveDir != Vector3.zero;
        transform.forward = Vector3.Slerp(transform.forward, MoveDir, Time.deltaTime * RotateSpeed);
    }

    private void SetSelectedCounter(BaseCounter counter)
    {
        this.selectedCounter = counter;
        OnSelectedCounter?.Invoke(this, new OnSelectedCounterChangedArgs { selectedCounter = counter });
    }

    public Transform GetKitChenObjectFollowTransform()
    {
        return kitChenHoldPoint;
    }
    public void SetKitChenObject(KitchenObject kitchenObject)
    {
        this.kitchenObjects = kitchenObject;

        if(kitchenObject != null)
        {
            OnPickupSomeThing?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObjects;
    }
    public void ClearKitChenObject()
    {
        kitchenObjects = null;
    }

    public bool HasKitChenObject()
    {
        return kitchenObjects != null;
    }
}
