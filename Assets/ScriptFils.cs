using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.SceneTemplate;
using UnityEngine;

public class ScriptFiles : MonoBehaviour
{
    List<Vector3> laser;
    float temps;
    public float delai;

    GameObject nouveauFil;
    LineRenderer etendreFil;

    public float largeur;

    // Start is called before the first frame update
    void Start()
    {
        laser = new List<Vector3>();
        temps = delai;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition), GetPositionSouris(), Color.magenta);
            temps -= Time.deltaTime;
            if (temps <= 0) { 
                laser.Add(GetPositionSouris());
                etendreFil.positionCount = laser.Count;
                etendreFil.SetPositions(laser.ToArray());
                temps = delai;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            laser.Clear();
        }
    }

    Vector3 GetPositionSouris()
    {
        Ray rayon = Camera.main.ScreenPointToRay(Input.mousePosition);
        return rayon.origin + rayon.direction * 10;
    }
}
