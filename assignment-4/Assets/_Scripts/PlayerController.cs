using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private enum PLAYER_STATE { S_WALK, S_IDLE, S_JUMP} //add or remove states here
    PLAYER_STATE state;
    Animator anim;
    Rigidbody rb;

    void Start()
    {
        state = PLAYER_STATE.S_IDLE; //set initial state to idle
        anim = gameObject.GetComponent<Animator>(); //get animator and rigidbody
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) //testing animation state switching
        {
            Debug.Log("Now in idle");
            state = PLAYER_STATE.S_IDLE;
        }

        if (Input.GetKeyDown(KeyCode.S)) //testing animation state switching
        {
            Debug.Log("Now in jump");
            state = PLAYER_STATE.S_JUMP;
        }

        if (Input.GetKeyDown(KeyCode.D)) //testing animation state switching
        {
            Debug.Log("Now in walk");
            state = PLAYER_STATE.S_WALK;
        }

        switch (state)
        {
            case PLAYER_STATE.S_IDLE: //idle state
                anim.SetTrigger("IDLE");
                break;
            case PLAYER_STATE.S_JUMP: //jump state
                anim.SetTrigger("JUMP");
                break;
            case PLAYER_STATE.S_WALK: //walk state
                anim.SetTrigger("WALK");
                break;
        }
    }
}
