using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI instance;
    [Header("Button")]
    [SerializeField] private Button soundEffectBt;
    [SerializeField] private Button musicBt;
    [SerializeField] private Button close;
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactaltButton;
    [SerializeField] private Button pauseButton;

    [Header("Text MeshPro")]
    [SerializeField] private TextMeshProUGUI soundEffectText;
    [SerializeField] private TextMeshProUGUI musicEffectText;
    [SerializeField] private TextMeshProUGUI moveUp;
    [SerializeField] private TextMeshProUGUI moveDown;
    [SerializeField] private TextMeshProUGUI moveRight;
    [SerializeField] private TextMeshProUGUI moveLeft;
    [SerializeField] private TextMeshProUGUI interact;
    [SerializeField] private TextMeshProUGUI interactalt;
    [SerializeField] private TextMeshProUGUI pause;
    private Action onclickButtonAction;

    private void Awake()
    {
        instance = this;
        soundEffectBt.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        musicBt.onClick.AddListener(() =>
        {
            MusicManager.instance.ChangeVolume();
            UpdateVisual();
        });
        close.onClick.AddListener(() =>
        {
            onclickButtonAction();
            Hide();
        });

    }

    private void Start()
    {
        KitChenGameManager.instance.OnGameUnPaused += Instance_OnGameUnPaused;
        UpdateVisual();
        Hide();
    }

    private void Instance_OnGameUnPaused(object sender, System.EventArgs e)
    {
       Hide();
    }

    private void UpdateVisual()
    {
        soundEffectText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() *10f);
        musicEffectText.text = "Music: " + Mathf.Round(MusicManager.instance.GetVolume() *10f);


        moveUp.text = GameInput.instance.GetBindingText(GameInput.Binding.Move_up);
        moveDown.text = GameInput.instance.GetBindingText(GameInput.Binding.Move_down);
        moveLeft.text = GameInput.instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRight.text = GameInput.instance.GetBindingText(GameInput.Binding.Move_Right);
        interact.text = GameInput.instance.GetBindingText(GameInput.Binding.Interact);
        interactalt.text = GameInput.instance.GetBindingText(GameInput.Binding.Interact_alt);
        pause.text = GameInput.instance.GetBindingText(GameInput.Binding.Pause);
    }

    public void Show(Action onclickButtonAction)
    {
        this.onclickButtonAction = onclickButtonAction;
        gameObject.SetActive(true);
        soundEffectBt.Select();
    } 
    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
