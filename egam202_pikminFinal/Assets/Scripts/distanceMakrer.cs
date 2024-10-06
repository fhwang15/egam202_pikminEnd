using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class distanceMakrer : MonoBehaviour
{

    public Camera myCamera;

    Transform myPosition;

    public bool move;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().enabled = false;
        myPosition = gameObject.transform;
        move = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseposition = Input.mousePosition;
        Ray worldRay = myCamera.ScreenPointToRay(mouseposition);

        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<Renderer>().enabled = true;

            if (Physics.Raycast(worldRay, out RaycastHit hitinfo))
            {
                myPosition.position = hitinfo.point;
            }
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "pikmin")
        {
            GetComponent<Renderer>().enabled = false;
        }
    }


}
