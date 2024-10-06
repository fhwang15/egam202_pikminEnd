using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;

public class PlayerCharacter : MonoBehaviour
{

    // What happens here?
    // ==> See if the pikmin has been selected or not.
    // It will allow pikmins to move around.

    public Camera myCamera;

    private pikmin PlayerChar; //Individual Pikmins
    private NavMeshAgent PlayerMove; //Individual Pikmin movement

    public distanceMakrer marker;

    // Start is called before the first frame update
    void Start()
    {
        PlayerChar = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Determine where the mouse is clicked
            Vector2 mouseposition = Input.mousePosition;
            Ray worldRay = Camera.main.ScreenPointToRay(mouseposition);

            if (Physics.Raycast(worldRay, out RaycastHit hitinfo)) 
            {
                pikmin selectedPikmin = hitinfo.transform.gameObject.GetComponent<pikmin>();
                //If pikmin is hitted, then it will call that pikmin and call it in here.

                if (PlayerChar == null) //if the Player character is not selected,
                {
                    PlayerChar = selectedPikmin; //it will select the pikmin
                    PlayerMove = selectedPikmin.playerCharacter;

                    PlayerChar.activatedPikmin(true);
                } 
                
                else if(PlayerChar != null) //if the Player character is already selected, but has been clicked
                {
                    PlayerMove.SetDestination(hitinfo.point);
                    //It will make the selected pikmin to it move
                }
                

                

            }
            //movement made here
        }

        if (Input.GetMouseButtonDown(1)) //When right-clicked, it will deactivate the Pikmin.
        {
            PlayerChar.activatedPikmin(false);
            PlayerChar = null;

        }


    }
}
