using UnityEngine;
using System.Collections.Generic;

public class CreerObjets : MonoBehaviour
{
    public Dictionary<KeyCode, GameObject> objetsACreer = new Dictionary<KeyCode, GameObject>(); //Dictionaire keycode-composante

    void Start()
    {
        // Charger les objets depuis les ressources
        GameObject fil = Resources.Load<GameObject>("Prefabs/Fil");
        GameObject pile = Resources.Load<GameObject>("Prefabs/Pile");
        GameObject resistance = Resources.Load<GameObject>("Prefabs/Resistance");
        GameObject ampoule = Resources.Load<GameObject>("Prefabs/Ampoule");
        GameObject interrupteur = Resources.Load<GameObject>("Prefabs/Interrupteur");

        // Associer les touches aux objets
        objetsACreer.Add(KeyCode.Alpha1, fil);
        objetsACreer.Add(KeyCode.Alpha2, pile);
        objetsACreer.Add(KeyCode.Alpha3, resistance);
        objetsACreer.Add(KeyCode.Alpha4, ampoule);
        objetsACreer.Add(KeyCode.Alpha5, interrupteur);
    }

    void Update()
    {
        Vector3 positionSouris = Input.mousePosition;
        positionSouris.z = 2; // Distance de la caméra (peut être ajustée)
        Vector3 positionSourisMonde = Camera.main.ScreenToWorldPoint(positionSouris);

        // Parcourir le dictionnaire pour vérifier si une touche a été pressée
        foreach (var paire in objetsACreer)
        {
            if (Input.GetKeyDown(paire.Key))
            {
                Instantiate(paire.Value, positionSourisMonde, Quaternion.identity);
            }
        }
    }
}