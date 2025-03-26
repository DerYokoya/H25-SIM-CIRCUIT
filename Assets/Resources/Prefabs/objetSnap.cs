using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objetSnap : MonoBehaviour
{

    public GameObject objetElectric;

    public GameObject snapLocation; 

    public bool estColler = false;

    public bool objetColler = false;

    public bool estEnMouvement = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            estEnMouvement = true;
        }
        else
        {
            estEnMouvement = false;
        }

        objetColler = snapLocation.GetComponent<ComposantSnapping>().estColler;

        if (objetColler)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.SetParent(objetElectric.transform);
            estColler = true;
        }
        if (!objetColler && !estEnMouvement)
        {
            GetComponent<Rigidbody>().isKinematic = false;
        }

    }
}
