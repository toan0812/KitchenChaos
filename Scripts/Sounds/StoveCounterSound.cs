using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{

    private AudioSource audioSource;
    [SerializeField] private StoveCounter stoveCounter;
    private float warningSoundTimer;
    private bool playWarningSound;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        stoveCounter.OnprogressChanged += StoveCounter_OnprogressChanged;
    }

    private void StoveCounter_OnprogressChanged(object sender, IHasProGress.OnprogressChangedArgs e)
    {
        float burnShowProgressAmount = .5f;
        bool playwarningSound = stoveCounter.ISFried() && e.progressNomalized >= burnShowProgressAmount;

    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool PlaySound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        if(PlaySound)
        {
            audioSource.Play();
        }
        else audioSource.Pause();
    }

    private void Update()
    {
        if(playWarningSound)
        {
            warningSoundTimer -= Time.deltaTime;
            if (warningSoundTimer <= 0f)
            {
                float warningSoundTimerMax = .2f;
                warningSoundTimer = warningSoundTimerMax;

                SoundManager.Instance.PlayWarningSound(stoveCounter.transform.position);
            }
        }
       
    }
}
