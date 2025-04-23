using UnityEngine;

public class DeplacerObjet : MonoBehaviour
{
    public Camera camera;
    Vector3 positionSouris;
    public float yConstant; // Positon de y fixe est initialisee, mais pas encore declaree

    private void Start()
    {
        yConstant = transform.position.y; // Position de y sera fixe
        camera = GameObject.Find("Camera").GetComponent<Camera>();
    }

    public Vector3 GetPositionSouris()
    {
        return camera.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown()
    {
        positionSouris = Input.mousePosition - GetPositionSouris();

    }

    public void OnMouseDrag()
    {
        Vector3 posSouris = Input.mousePosition;

        float profondeur = GetPositionSouris().z;

        Vector3 positionMonde = camera.ScreenToWorldPoint(new Vector3(posSouris.x, posSouris.y, profondeur));

        Vector3 nouvellePosition = new Vector3(positionMonde.x, yConstant, positionMonde.z);

        transform.position = nouvellePosition;
        Rotation();
    }
    public void Rotation()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y + 90f, 0f);
        }
    }
}