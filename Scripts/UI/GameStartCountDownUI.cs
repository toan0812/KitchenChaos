using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStartCountDownUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI countText;
    private Animator animator;
    private const string POP_UP_NUMBER = "NumberPopUp";
    private int PreviousCountDownNumber;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        KitChenGameManager.instance.OnSTateChanged += Instance_OnSTateChanged;
    }
    private void Instance_OnSTateChanged(object sender, System.EventArgs e)
    {
        if (KitChenGameManager.instance.IsCountDownToStartActive())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Update()
    {
        int CountDownNumber = Mathf.CeilToInt(KitChenGameManager.instance.GetCountDownToStart());
        countText.text = CountDownNumber.ToString();

        if(PreviousCountDownNumber!= CountDownNumber)
        {
            PreviousCountDownNumber = CountDownNumber;
            animator.SetTrigger(POP_UP_NUMBER);
            SoundManager.Instance.CountDownSound();
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
