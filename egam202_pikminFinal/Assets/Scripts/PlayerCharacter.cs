using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Timeline;
using static pikmin;


public class PlayerCharacter : MonoBehaviour
{
    // What happens here?
    // ==> See if the pikmin has been selected or not.
    // It will allow pikmins to move around.

  
    public Camera myCamera;
    private pikmin PlayerChar; //Individual Pikmins

    private Treasure Treasure; //treasure





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
            Vector2 mouseposition = Input.mousePosition;
            Ray worldRay = Camera.main.ScreenPointToRay(mouseposition);
            //Determine where the mouse is clicked


            if (Physics.Raycast(worldRay, out RaycastHit hitinfo)) 
            {
                
                //Treasure selectedTreasure = hitinfo.transform.GetComponent<Treasure>();
                //once hit the treasure, it will call the position of the treasure that has been selected.

                if (PlayerChar == null) //if the Player character is not selected,
                {
                    pikmin selectedPikmin = hitinfo.transform.gameObject.GetComponent<pikmin>();
                    //If pikmin is hitted, then it will call that pikmin and put it in here.


                    PlayerChar = selectedPikmin; //it will select the pikmin
                    PlayerChar.currentState = PikminStates.Active;
                }

                else if(PlayerChar.currentState == PikminStates.Active)
                {
                    PlayerChar.activatedMovement(hitinfo.point);
                    //Treasure = selectedTreasure;

                    //Treasure.activatedTreasure(true);
                }
                
                //else if(PlayerChar != null) //if the Player character is already selected, but has been clicked
                //{
                //    PlayerChar.activatedMovement(hitinfo.point);

                //}

                
            }

           

        }


        if (Input.GetMouseButtonDown(1)) //When right-clicked, it will deactivate the Pikmin.
        {


           // Treasure.activatedTreasure(false);
            PlayerChar.currentState = PikminStates.Idle;
            PlayerChar = null; //selection is emptied
            //Treasure = null;


        }
    }

}
