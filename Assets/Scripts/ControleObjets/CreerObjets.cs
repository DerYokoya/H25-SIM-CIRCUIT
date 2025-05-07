using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class CreerObjets : MonoBehaviour
{
    public Camera camera;
    public Dictionary<KeyCode, GameObject> objetsACreer = new Dictionary<KeyCode, GameObject>(); //Dictionaire keycode-composante

    public AudioClip creationAudio; // À définir dans l'inspecteur directement
    private AudioSource sourceAudio;

    void Start()
    {

        sourceAudio = gameObject.AddComponent<AudioSource>();
        sourceAudio.clip = creationAudio;
        
        
        // Charger les objets depuis les ressources
        GameObject fil = Resources.Load<GameObject>("Prefabs/Fil");
        GameObject pile = Resources.Load<GameObject>("Prefabs/Pile");
        GameObject resistance = Resources.Load<GameObject>("Prefabs/Resistance");
        GameObject ampoule = Resources.Load<GameObject>("Prefabs/Ampoule");
        GameObject interrupteur = Resources.Load<GameObject>("Prefabs/Interrupteur");
        GameObject fusible = Resources.Load<GameObject>("Prefabs/Fusible");

        // Associer les touches aux objets
        objetsACreer.Add(KeyCode.Alpha1, fil);
        objetsACreer.Add(KeyCode.Alpha2, pile);
        objetsACreer.Add(KeyCode.Alpha3, ampoule);
        objetsACreer.Add(KeyCode.Alpha4, resistance);
        objetsACreer.Add(KeyCode.Alpha5, interrupteur);
        objetsACreer.Add(KeyCode.Alpha6, fusible);
    }

    void Update()
    {
        camera = GameObject.Find("Camera").GetComponent<Camera>();

        Vector3 positionSouris = Input.mousePosition;
        positionSouris.z = 3; // Distance de la caméra (peut être ajustée)
        Vector3 positionSourisMonde = camera.ScreenToWorldPoint(positionSouris);

        // Parcourir le dictionnaire pour vérifier si une touche a été pressée
        foreach (var paire in objetsACreer)
        {
            
            if (Input.GetKeyDown(paire.Key))
            {
                GameObject sol = GameObject.FindGameObjectWithTag("Ground");
                BoxCollider solCollider = sol.GetComponent<BoxCollider>();
                Bounds limites = solCollider.bounds;
                if (positionSourisMonde.z < limites.max.z && positionSourisMonde.z > limites.min.z && positionSourisMonde.x > limites.min.x && positionSourisMonde.x < limites.max.x)
                Instantiate(paire.Value, new Vector3(positionSourisMonde.x, 23.491f, positionSourisMonde.z), Quaternion.identity);
                sourceAudio.Play(); // Jouer le son à la création de l'objet
            }
        }
    }
}