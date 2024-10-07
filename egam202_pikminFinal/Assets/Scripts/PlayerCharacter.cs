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

    bool pselected;



    // Start is called before the first frame update
    void Start()
    {
        PlayerChar = null;
        pselected = false;
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
                pikmin selectedPikmin = hitinfo.transform.gameObject.GetComponent<pikmin>();
                //If pikmin is hitted, then it will call that pikmin and put it in here.

                Treasure selectedTreasure = hitinfo.transform.GetComponent<Treasure>();
                //once hit the treasure, it will call the position of the treasure that has been selected.

                if (selectedPikmin != null && PlayerChar == null)
                {
                    PlayerChar = selectedPikmin; //it will select the pikmin

                    if (PlayerChar.currentState == PikminStates.Idle)
                    {
                        PlayerChar.currentState = PikminStates.Active;
                    }

                    pselected = true;
                }

                else if (pselected && selectedTreasure != null)
                {
                    Treasure = selectedTreasure;

                    
                    Treasure.activatedTreasure(true);
                    PlayerChar.activatedMovement(Treasure.transform.position, true);
                }

                else if(pselected) //if its not a treasure+pikmin yet you have to move around :)
                {
                    if (PlayerChar.currentState == PikminStates.Active)
                    {

                        PlayerChar.activatedMovement(hitinfo.point, false);
                    }
                }
            }

        }


        if (Input.GetMouseButtonDown(1)) //When right-clicked, it will deactivate the Pikmin.
        {
            Treasure.activatedTreasure(false);
            PlayerChar.currentState = PikminStates.Idle;
            PlayerChar = null; //selection is emptied
            pselected = false;
            Treasure = null;


        }
    }
           

}


        
  
