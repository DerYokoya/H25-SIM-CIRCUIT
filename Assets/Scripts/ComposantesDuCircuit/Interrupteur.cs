using UnityEngine;

public class Interrupteur : Fil
{
    public bool EstOuvert { get; private set; } = false;
    

    private Renderer interrupteurRenderer;

    void Start()
    {
        // Obtenir le composant Renderer de l'objet
        interrupteurRenderer = GetComponent<Renderer>();

    }
    /**
    private void Update()
      {
          base.Update();

          if (SourirEstDessu()) { 
              if (Input.GetMouseButtonDown(1))
              { 
                  OuvrirOuFermer();
                  tournerInterrupteur();
              }
          }
          
      }
    */

    public void OuvrirOuFermer()
    {
        EstOuvert = !EstOuvert;
        fonctionne = !EstOuvert;
    }

    private void tournerInterrupteur()
    { 

            if (EstOuvert)
        {
            interrupteurRenderer.material.color = Color.white;
        }
        else
        {
            interrupteurRenderer.material.color = Color.black;
        }
    }
}