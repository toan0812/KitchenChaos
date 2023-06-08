using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image barImage;
    [SerializeField] private GameObject IHasProgressGameObject;
    private IHasProGress iHasProGress;

    private void Start()
    {
        iHasProGress = IHasProgressGameObject.GetComponent<IHasProGress>();
        iHasProGress.OnprogressChanged += IHasProGress_OnprogressChanged;
        barImage.fillAmount = 0f;
        Hide();
    }

    private void IHasProGress_OnprogressChanged(object sender, IHasProGress.OnprogressChangedArgs e)
    {
        barImage.fillAmount = e.progressNomalized;
        if(e.progressNomalized == 0f || e.progressNomalized ==1f)
        {
            Hide();
        }
        else
        {
            Show();
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
