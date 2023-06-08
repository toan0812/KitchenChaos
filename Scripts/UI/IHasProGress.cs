using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IHasProGress 
{
    public event EventHandler<OnprogressChangedArgs> OnprogressChanged;

    public class OnprogressChangedArgs : EventArgs
    {
        public float progressNomalized;
    }
}
