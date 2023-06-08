using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnWaringUI : MonoBehaviour
{

    [SerializeField] StoveCounter stoveCounter;

    private void Start()
    {
        stoveCounter.OnprogressChanged += StoveCounter_OnprogressChanged;
        Hide();
    }

    private void StoveCounter_OnprogressChanged(object sender, IHasProGress.OnprogressChangedArgs e)
    {
        float burnShowProgressAmount = .5f;
        bool show = stoveCounter.ISFried() && e.progressNomalized>= burnShowProgressAmount;

        if(show)
        {
            Show();
        } 
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    } 
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
