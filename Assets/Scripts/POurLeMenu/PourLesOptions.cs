using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{

    public Toggle pleinEcran, SyncVerticale;
    // Start is called before the first frame update
    void Start()
    {
        pleinEcran.isOn = Screen.fullScreen;

        if (QualitySettings.vSyncCount == 0)
        {
            SyncVerticale.isOn = false;
        } else
        {
            SyncVerticale.isOn = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AppliquerGraphisme()
    {
        Screen.fullScreen = pleinEcran.isOn;

        if (SyncVerticale.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
    }

}
