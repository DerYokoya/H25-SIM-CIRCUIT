using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComposantSnapping : MonoBehaviour
{
    public bool estDansLazone = false;

    public bool estColler = false;

    public bool estEnMouvement = false;

    public GameObject autre; 



    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            estEnMouvement = true;
        }
        else
        {
            estEnMouvement = false;
        }
        if (estDansLazone)
        {
            collerLesObjet(autre.transform);
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        estDansLazone = true;
    }



    public void OnTriggerExit(Collider other)
    {
        estDansLazone = false;
    }

    void collerLesObjet(Transform other)
    {
        if (estDansLazone && !estEnMouvement)
        {
            other.position = this.transform.position;
            estColler = true;
        }
        else
        {
            estColler = false;
        }

    }

}
