using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameOverUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI recipesText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button homeButton;


    private void Awake()
    {
        restartButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.scene.GameScene);
        });
        homeButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.scene.MainMenuScene);
        });
    }
    private void Start()
    {
        Hide();
        KitChenGameManager.instance.OnSTateChanged += Instance_OnSTateChanged;
    }

    private void Instance_OnSTateChanged(object sender, System.EventArgs e)
    {
        if (KitChenGameManager.instance.IsCountDownToGameOver())
        {
            Show();
            recipesText.text = DeliveryManager.Instance.GetSuccessfullDeliveryAmount().ToString();
        }
        else
        {
            Hide();
        }
    }
    private void Show()
    {
        gameObject.SetActive(true);
        restartButton.Select();
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }

}
