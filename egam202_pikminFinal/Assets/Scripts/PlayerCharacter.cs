using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerCharacter : MonoBehaviour
{

    // What happens here?
    // ==> See if the pikmin has been selected or not.
    // It will allow pikmins to move around.


    private pikmin PlayerChar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseposition = Input.mousePosition;
            Ray worldRay = Camera.main.ScreenPointToRay(mouseposition);

            if (Physics.Raycast(worldRay, out RaycastHit hitinfo))
            {
                pikmin selectedPikmin = hitinfo.transform.gameObject.GetComponent<pikmin>();
                PlayerChar = selectedPikmin;
                PlayerChar.activatedPikmin(true);
            }



            //movement made here



        }
    }
}
