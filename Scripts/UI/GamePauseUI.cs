using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button mainmenuButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;



    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            KitChenGameManager.instance.PauseGame();
        });
        mainmenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.scene.MainMenuScene);
        });
        optionsButton.onClick.AddListener(() =>
        {
            OptionsUI.instance.Show(Show);
            Hide(); 
        });
    }
    private void Start()
    {
        KitChenGameManager.instance.OnGamePaused += Instance_OnGamePaused;
        KitChenGameManager.instance.OnGameUnPaused += Instance_OnGameUnPaused; 
        Hide();
    }

    private void Instance_OnGameUnPaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void Instance_OnGamePaused(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
        resumeButton.Select();
    } 
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
