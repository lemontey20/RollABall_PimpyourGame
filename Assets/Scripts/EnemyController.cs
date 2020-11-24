using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject m_playerObject = null; //reference to our player object
    [SerializeField] private float      m_detectionRadius       = 4f; //observed radius of the enemy
    [SerializeField] private string      m_state ; 
    [SerializeField] private Material m_idleMaterial    = null; //material which is attached while in 'idle mode'
    [SerializeField] private Material m_chasingMaterial = null; //material which is attached while in 'chasing mode'
    [SerializeField] private Material m_chasedMaterial = null; //material which is attached while in 'chased mode'
    private NavMeshAgent m_agent = null; //the reference to the NavMeshAgent component
    private Vector3      m_initialPosition = Vector3.zero; //member to store the initial position of this enemy

    protected virtual Vector3 GetNextDestination()
    {
        return m_initialPosition; //standard enemy should just return to its initial position when in 'idle mode'
    }
    
    private void Start()
    {
        m_agent           = gameObject.GetComponent<NavMeshAgent>(); //get the NavMeshAgent component
        m_initialPosition = gameObject.transform.position; //get the initial position
        ChangeState("idle");
    }
    public void ChangeState(string state)
    {
        m_state = state; //precising the variable m state
        if (m_state == "idle") //if the mstate is equal to idle
        {
            m_agent.GetComponent<Renderer>().material = m_idleMaterial; //then affect the m_idle material (orange)
        }
        else if (m_state == "chasing") //if m state equals to chasing
        {
            m_agent.GetComponent<Renderer>().material = m_chasingMaterial; //then affect the m_chasing material (red)
        }
        else if (m_state == "chased")//if m state equals to chased
        {
            m_agent.GetComponent<Renderer>().material = m_chasedMaterial; //then affect the m_chasedMaterial (same color as collectables)
        }
    }

    public string GetState()
    {
        return(m_state);
    }

    private void Update()
    {
        if(m_state == "idle" || m_state == "chasing") //if the state is idle or chasing
        {
            if (Vector3.Distance(m_playerObject.transform.position, gameObject.transform.position) < m_detectionRadius) //if the player position if under the detection radius of the ennemy (wether in idle or chasing state)
            {
                ChangeState("chasing");//then change the state to chasing
                m_agent.SetDestination(m_playerObject.transform.position); //set the position of the player
                return;//then go out of this
            }
            //enemy is in 'idle mode'
            
            if(m_agent.remainingDistance < 0.5f) 
                m_agent.SetDestination(GetNextDestination()); 
        }
        else {
            m_agent.SetDestination(GetNextDestination());
        }
    }
}
