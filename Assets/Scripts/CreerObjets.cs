using UnityEngine;
using static Cinemachine.CinemachineTargetGroup;

public class CreerObjets : MonoBehaviour
{
    public GameObject fil;
    public GameObject pile;

    void Start()
    {
        fil = Resources.Load<GameObject>("Prefabs/Fil");
        pile = Resources.Load<GameObject>("Prefabs/Pile");

    }

    void Update()
    {
        Vector3 positionSouris = Input.mousePosition;
        positionSouris.z = 2; // Distance de la caméra (à ajuster)
        Vector3 positionSourisMonde = Camera.main.ScreenToWorldPoint(positionSouris);

        if (Input.GetKeyDown(KeyCode.Alpha1)) // S'il appuie sur la touche 1
        {
            Instantiate(fil, positionSourisMonde, Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Instantiate(pile, positionSourisMonde, Quaternion.identity);
        }

    }
}