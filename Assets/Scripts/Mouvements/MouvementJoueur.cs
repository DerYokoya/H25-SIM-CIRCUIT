using Unity.Mathematics;
using UnityEngine;

public class MouvementJoueur : MonoBehaviour
{
    public float vitesse; // Vitesse de déplacement
    public float forceSaut = 5f; // Force de saut
    private Vector2 previousMousePosition;
    private float sencibiliter = 10f;
  

    private Rigidbody rb;
    private bool auSol;

    Camera cam1;
    Camera cam2;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        previousMousePosition = Input.mousePosition;
        rb.transform.Rotate(new Vector3(0 , 0 
, 0.0f));


        

    }

    void Update()
    {

        vitesse = 0.05f;


        float moveX = Input.GetAxis("Horizontal") * vitesse;
        float moveZ = Input.GetAxis("Vertical") * vitesse;
        Vector3 movement = new Vector3(moveX, 0f, moveZ);
        rb.transform.Translate(movement);


        float rotationY = Input.GetAxis("Mouse X");
        float rotationX = Input.GetAxis("Mouse Y");// a ajouter apres

        rb.transform.Rotate(new Vector3(0,rotationY* sencibiliter ,0.0f));
     
        


        
   

        if (Input.GetKeyDown(KeyCode.Space) && auSol)
        {
            rb.AddForce(Vector3.up * forceSaut, ForceMode.Impulse);
            auSol = false;
        }

       


    }

    void OnCollisionEnter(Collision collision)
    {
        // Vérifie si le joueur est au sol avec le tag Ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            auSol = true;
        }
    }

 
}