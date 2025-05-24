using UnityEngine;



/**
 * Petite classe qui gêre le model 3D du fil.
 * 
 */

public class RenduFil : MonoBehaviour
{
    public Transform attacheA;
    public Transform attacheB;

    public float rayon;
    public Material cylinderMaterial;

    private GameObject cylinder;

    private BoxCollider regionDeplacementComplet;

    //Cération du cylindre dès qu'on crée un fil dans le simulateur.
    void Start()
    {
        regionDeplacementComplet = this.GetComponent<BoxCollider>();
        creerCylindre3D();
    }

    void Update()
    {
        mettreAJourCylindre();
    }


    /*
     * Création d'un cylindre 3D qui s'asllongie de l'extrmité <<attacheA>> jusqu'à  <<attacheB>>.
     * 
     */
    void creerCylindre3D()
    {
        cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        cylinder.transform.SetParent(this.transform);
        cylinder.GetComponent<Renderer>().material = cylinderMaterial;
        cylinder.AddComponent<Outline>();
        mettreAJourCylindre();
    }

    /*
    * Mise à Jour du cylindre 3D selon la position des deux attaches, extrémités car on les deplace a travers la classe deplacerObjet / mouvementSerpent.
    * 
    */
    void mettreAJourCylindre()
    {
        if (attacheA == null || attacheB == null) return;

        Vector3 middlePosition = (attacheA.position + attacheB.position) / 2f;
        cylinder.transform.position = middlePosition;

        //distance entre les deux extrémités.
        Vector3 direction = attacheB.position - attacheA.position;
        float distance = direction.magnitude;

        //ajustement de la taille du cylindre
        cylinder.transform.localScale = new Vector3(rayon, distance / 2, rayon);

        // Rotation du point A au point B
        cylinder.transform.rotation = Quaternion.LookRotation(direction);
        cylinder.transform.Rotate(Vector3.right, 90f);
    }
}