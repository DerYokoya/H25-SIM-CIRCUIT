using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class MouvementPersonnage : MonoBehaviour
{
    public Camera cameraJoueur;
    public float vitesseMarche;
    public float vitesseCourse;
    public float puissanceSaut;
    public float vitesseRegard;
    public float limiteRegardX;
    private Vector3 directionMouvement = Vector3.zero;
    private float rotationX = 0;
    private CharacterController controleurPersonnage;
    private bool peutBouger = true;
    private float vitesseMarche_initiale;
    private float vitesseCourse_initiale;

    void Start()
    {
        controleurPersonnage = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; // Rendre le curseur de la souris invisible
        vitesseMarche_initiale = vitesseMarche;
        vitesseCourse_initiale = vitesseCourse;
    }

    void Update()
    {
        Vector3 avant = transform.TransformDirection(Vector3.forward);
        Vector3 droite = transform.TransformDirection(Vector3.right);
        bool estEnCourse = Input.GetKey(KeyCode.LeftShift);

        /* Calcul de la vitesse de déplacement avant/arrière :
          - Si le joueur ne peut pas bouger : vitesse = 0
          - Sinon : utilise vitesseCourse si «shift» est enfoncé ou vitesseMarche si «shift» n'est pas enfoncé
          - Multiplie par l'«input» vertical ou horizontal (W/S) qui varie de -1 à 1 */
        float vitesseActuelleX = peutBouger ? (estEnCourse ? vitesseCourse : vitesseMarche) * Input.GetAxis("Vertical") : 0;
        float vitesseActuelleY = peutBouger ? (estEnCourse ? vitesseCourse : vitesseMarche) * Input.GetAxis("Horizontal") : 0;

        float directionMouvementY = directionMouvement.y;
        directionMouvement = (avant * vitesseActuelleX) + (droite * vitesseActuelleY);

        // Saut du personnage
        if (Input.GetButton("Jump") && peutBouger && controleurPersonnage.isGrounded)
        {
            directionMouvement.y = puissanceSaut;
        }
        else
        {
            directionMouvement.y = directionMouvementY;
        }

        // Application de la gravité
        if (!controleurPersonnage.isGrounded)
        {
            directionMouvement.y += Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            vitesseMarche = vitesseMarche_initiale;
            vitesseCourse = vitesseCourse_initiale;
        }

        // Déplacement du personnage
        controleurPersonnage.Move(directionMouvement * Time.deltaTime);

        // Gestion de la caméra et rotation
        if (peutBouger)
        {
            rotationX += -Input.GetAxis("Mouse Y") * vitesseRegard;
            rotationX = Mathf.Clamp(rotationX, -limiteRegardX, limiteRegardX);
            cameraJoueur.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * vitesseRegard, 0);
        }
    }
}