using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeliveryUI : MonoBehaviour
{
    private const string POP_UP = "PopUp";
    [SerializeField] private Image backGroundImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI messText;
    [SerializeField] private Color successColor;
    [SerializeField] private Color failColor;
    [SerializeField] private Sprite successSprite;
    [SerializeField] private Sprite failSprite;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFail += DeliveryManager_OnRecipeFail;
        gameObject.SetActive(false);
    }

    private void DeliveryManager_OnRecipeFail(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        animator.SetTrigger(POP_UP);
        backGroundImage.color = failColor;
        iconImage.sprite = failSprite;
        messText.text = "DELIVERY\nFAILED";
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        animator.SetTrigger(POP_UP);
        backGroundImage.color = successColor;
        iconImage.sprite = successSprite;
        messText.text = "DELIVERY\nSUCCESS";
    }
}
