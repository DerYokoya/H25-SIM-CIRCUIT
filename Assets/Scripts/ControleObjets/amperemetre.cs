using TMPro;
using UnityEngine;

public class ToggleGun : MonoBehaviour
{
    // Référence à ton objet (le cube ou le gun)
    public GameObject gunObject;
    public TextMeshPro info;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha7)) // Touche 7 du clavier principal
        {
            if (gunObject != null)
            {
                // Active ou désactive l’objet
                gunObject.SetActive(!gunObject.activeSelf);
            }
        }
    }


}