using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestEnemyMove : MonoBehaviour
{
    enum ENEMY_STATE { S_GUARD, S_CHASE, S_PATROL }; //enemy states
    ENEMY_STATE state;

    public float lookRadius = 10f;

    private Transform target;
    public Transform home;
    public Vector3 previousTarget;
    NavMeshAgent agent;

    public int health = 3;
    public AudioManager audioManager;
    RaycastHit hit;
    RaycastHit hit2;

    Vector3 rayDirection;
    Vector3 rayDirection2;

    public GameObject deathBubbles;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        agent = GetComponent<NavMeshAgent>();
        state = ENEMY_STATE.S_GUARD;
    }


    void Update()
    {
        if(health <= 0)
        {
            Debug.Log("I died");
            audioManager.updateAudio("boom");
            GameObject bubbles = Instantiate(deathBubbles, transform.position, Quaternion.identity);
            Destroy(bubbles, 2);
            gameObject.SetActive(false);
        }

        float distance = Vector3.Distance(target.position, transform.position);
        rayDirection = (target.transform.position + new Vector3(0, 1, 0)) - (transform.position + new Vector3(0, 1, 0));
        rayDirection2 = (transform.position + new Vector3(0, 1, 0) - target.transform.position + new Vector3(0, 1, 0));
        bool raycastdown = Physics.Raycast((transform.position + new Vector3(0, 1, 0)), rayDirection, out hit);
        bool raycastdown2 = Physics.Raycast((target.transform.position + new Vector3(0, 1, 0)), rayDirection2, out hit2);

        if (distance <= lookRadius) //if the player is close enough and gets spotted, chase them
        {
            if (raycastdown && hit.transform.name.Equals("OctopusPlayer Variant"))
            {
                state = ENEMY_STATE.S_CHASE;
            }
        }

        switch (state)
        {
            case ENEMY_STATE.S_CHASE: //chase the player; record where they are (for patroling)
                agent.SetDestination(target.position);
                FaceTarget(target.position);
                previousTarget = target.position;
                audioManager.updateAudio("attack");

                if (raycastdown && !hit.transform.name.Equals("OctopusPlayer Variant")) //if you don't see them anymore, go to patrol
                {
                    state = ENEMY_STATE.S_PATROL;
                }

                break;
            case ENEMY_STATE.S_GUARD: //guard the spawn lantern
                agent.SetDestination(home.position);
                FaceTarget(home.position);
                break;
            case ENEMY_STATE.S_PATROL: //go towards where the player used to be
                agent.SetDestination(previousTarget);
                FaceTarget(previousTarget);

                if (Vector3.Distance(previousTarget, transform.position) < 2f) //if you get to that point, go back to guarding the spawn lantern
                {
                    state = ENEMY_STATE.S_GUARD;
                }
                break;
        }
    }

    void FaceTarget(Vector3 lookAtPoint)
    {
        Vector3 direction = (lookAtPoint - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

}
