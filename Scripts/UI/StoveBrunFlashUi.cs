using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBrunFlashUi : MonoBehaviour
{
    [SerializeField] StoveCounter stoveCounter;
    private const string FLASHING = "IsFlashing";
    private Animator Animator;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        stoveCounter.OnprogressChanged += StoveCounter_OnprogressChanged;
        Animator.SetBool(FLASHING, false);
    }

    private void StoveCounter_OnprogressChanged(object sender, IHasProGress.OnprogressChangedArgs e)
    {
        float burnShowProgressAmount = .5f;
        bool show = stoveCounter.ISFried() && e.progressNomalized >= burnShowProgressAmount;

        Animator.SetBool(FLASHING, show);
    }
}
