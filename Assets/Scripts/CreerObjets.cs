using UnityEngine;

public class CreerObjets : MonoBehaviour
{
    public GameObject fil; // On va rendre le code plus g�n�ral apr�s
    private Vector3 apparition;

    void Start()
    {
        fil = Resources.Load<GameObject>("Prefabs/Fil");
        if (fil == null)
        {
            Debug.LogError("Le prefab n'a pas charg�");
            return;
        }
        else
        {
            Debug.Log("Le prefab a charg�");
        }
    }

    void Update()
    {
        apparition = new Vector3(0.051f, 3.5f, -8.652f); // A changer

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Instantiate(fil, apparition, Quaternion.identity);
        }
    }
}