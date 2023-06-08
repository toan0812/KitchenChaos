using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallBAck : MonoBehaviour
{

    private bool isFirtUpdate = true;
    private void Update()
    {
        if(isFirtUpdate)
        {
            isFirtUpdate = false;
            Loader.loaderCallBack();
        }
    }
}
