using UnityEngine;
using System.Collections.Generic;

public class CreerObjets : MonoBehaviour
{
    public Camera camera;
    public Dictionary<KeyCode, GameObject> objetsACreer = new();
    public AudioClip creationAudio;
    private AudioSource sourceAudio;

    void Start()
    {
        sourceAudio = gameObject.AddComponent<AudioSource>();
        sourceAudio.clip = creationAudio;

        // Charger les objets depuis les ressources
        objetsACreer.Add(KeyCode.Alpha1, Resources.Load<GameObject>("Prefabs/Fil"));
        objetsACreer.Add(KeyCode.Alpha2, Resources.Load<GameObject>("Prefabs/Pile"));
        objetsACreer.Add(KeyCode.Alpha3, Resources.Load<GameObject>("Prefabs/Ampoule"));
        objetsACreer.Add(KeyCode.Alpha4, Resources.Load<GameObject>("Prefabs/Resistance"));
        objetsACreer.Add(KeyCode.Alpha5, Resources.Load<GameObject>("Prefabs/Interrupteur"));
        objetsACreer.Add(KeyCode.Alpha6, Resources.Load<GameObject>("Prefabs/Fusible"));
    }

    void Update()
    {
        camera = GameObject.Find("Camera").GetComponent<Camera>();

        Vector3 positionSouris = Input.mousePosition;
        positionSouris.z = 3;
        Vector3 positionSourisMonde = camera.ScreenToWorldPoint(positionSouris);

        foreach (var paire in objetsACreer)
        {
            if (Input.GetKeyDown(paire.Key))
            {
                GameObject sol = GameObject.FindGameObjectWithTag("Ground");
                BoxCollider solCollider = sol.GetComponent<BoxCollider>();
                Bounds limites = solCollider.bounds;

                if (positionSourisMonde.z < limites.max.z && positionSourisMonde.z > limites.min.z &&
                    positionSourisMonde.x > limites.min.x && positionSourisMonde.x < limites.max.x)
                {
                    GameObject nouvelObjet = Instantiate(
                        paire.Value,
                        new Vector3(positionSourisMonde.x, 23.491f, positionSourisMonde.z),
                        Quaternion.identity
                    );

                    // Donne un nom unique
                    string nomBase = paire.Value.name.Replace("(Clone)", "").Trim();
                    nouvelObjet.name = TrouverNomDisponible(nomBase);

                    sourceAudio.Play();
                }
            }
        }
    }

    private string TrouverNomDisponible(string nomBase)
    {
        int index = 1;
        string nomTest = nomBase + index;

        while (GameObject.Find(nomTest) != null)
        {
            index++;
            nomTest = nomBase + index;
        }

        return nomTest;
    }
}