using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float vitesse = 5f; // Vitesse de déplacement
    public float forceSaut = 5f; // Force de saut

    private Rigidbody rb;
    private bool auSol;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal") * vitesse;
        float moveZ = Input.GetAxis("Vertical") * vitesse;

        Vector3 movement = new Vector3(moveX, 0f, moveZ);
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

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