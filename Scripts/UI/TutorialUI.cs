using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI KeyMoveUpText;
    [SerializeField] private TextMeshProUGUI KeyMoveDownText;
    [SerializeField] private TextMeshProUGUI KeyMoveRightText;
    [SerializeField] private TextMeshProUGUI KeyMoveLeftText;
    [SerializeField] private TextMeshProUGUI KeyInteractText;
    [SerializeField] private TextMeshProUGUI KeyInteractaltText;
    [SerializeField] private TextMeshProUGUI KeyPauseText;

    private void Start()
    {
        UpdateVisual();
        KitChenGameManager.instance.OnSTateChanged += KitChenGameManager_OnSTateChanged;

        Show();
    }

    private void KitChenGameManager_OnSTateChanged(object sender, System.EventArgs e)
    {
        if(KitChenGameManager.instance.IsCountDownToStartActive())
        {
            Hide();
        }
    }

    private void UpdateVisual()
    {

        KeyMoveUpText.text = GameInput.instance.GetBindingText(GameInput.Binding.Move_up);
        KeyMoveDownText.text = GameInput.instance.GetBindingText(GameInput.Binding.Move_down);
        KeyMoveRightText.text = GameInput.instance.GetBindingText(GameInput.Binding.Move_Left);
        KeyMoveLeftText.text = GameInput.instance.GetBindingText(GameInput.Binding.Move_Right);
        KeyInteractText.text = GameInput.instance.GetBindingText(GameInput.Binding.Interact);
        KeyInteractaltText.text = GameInput.instance.GetBindingText(GameInput.Binding.Interact_alt);
        KeyPauseText.text = GameInput.instance.GetBindingText(GameInput.Binding.Pause);
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
