using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{

    private Player player;
    private float footTimer;
    private float footTimerMax = .1f;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        footTimer -= Time.deltaTime;
        if(footTimer < 0f)
        {
            footTimer = footTimerMax;
            if (player.PlayerIsWalking())
            {
                float volume = 1f;
                SoundManager.Instance.FootStepsSound(player.transform.position, volume);
            }
            
        }
    }
}
