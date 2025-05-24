using UnityEngine;
using UnityEngine.UI;

/**
 * Liste des methodes pour les boutons des options (Plein écran, synchronisation  verticale, volume ect.)
 */
public class Options : MonoBehaviour
{
    public Toggle pleinEcran, SyncVerticale;
    // Start is called before the first frame update

    /*
     * Récupération des options actuelles.
     */
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


    /**
     * Application des changements de l'utilisateur.
     */

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
