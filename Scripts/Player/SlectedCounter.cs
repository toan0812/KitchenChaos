using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlectedCounter : MonoBehaviour
{
    [SerializeField] private BaseCounter clearCounter;
    [SerializeField] private GameObject[] VisualCounterObject;
    private void Start()
    {
        Player.Instance.OnSelectedCounter += Player_OnSelectedCounter;
    }

    private void Player_OnSelectedCounter(object sender, Player.OnSelectedCounterChangedArgs e)
    {
        if(e.selectedCounter == clearCounter)
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
        foreach(GameObject visualGameObject in VisualCounterObject)
        {
            visualGameObject.SetActive(true);
        }
        
    } 
    private void Hide()
    {
        foreach (GameObject visualGameObject in VisualCounterObject)
        {
            visualGameObject.SetActive(false);
        }
    }
}
