using TMPro;
using UnityEngine;
using System.Globalization;
using UnityEditor;

public class ToggleGun : MonoBehaviour
{
    public GameObject gunObject;
    public TextMeshPro info;
    public Camera playerCamera;
    public float maxDetectionDistance = 5f;

    void Update()
    {
        // Toggle avec la touche 7
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            gunObject.SetActive(!gunObject.activeSelf);
        }

        // Détection du survol
        if (gunObject.activeSelf)
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxDetectionDistance))
            {
                ComposanteDuCircuit component = hit.collider.GetComponentInParent<ComposanteDuCircuit>();
                if (component != null)
                {
                    float current = GraphManager.Instance.GetCurrentForComponent(component); //global pour forcer a utiliser point au lieu de virgule 0.0001 au lieu de 0,001
                    // Utiliser CultureInfo.InvariantCulture pour forcer le point décimal
                    info.text = string.Format(CultureInfo.InvariantCulture, "{0:F3} A", current);
                    return;
                }
            }

            info.text = "0.000 A"; // Aucun composant détecté
        }
    }
}