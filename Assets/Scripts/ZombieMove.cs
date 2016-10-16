using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMove : MonoBehaviour {

    public GameObject targetNodes;
    public float speed;
    public float attackSpeed = 1f;

    int nodeIndex = 0;
    Transform currentTarget;
    Transform parentTransform;
    Vector3 direction;
    ZombieState state;
    float timer;

    enum ZombieState
    {
        MovingToWindow, AttackingWindow, EnteringWindow, MovingToPlayer
    }

    // Use this for initialization
    void Start () {
        state = ZombieState.MovingToWindow;
        parentTransform = transform.parent;
        SetNextNode();
        direction = Vector3.Normalize(currentTarget.transform.position - parentTransform.position);
    }
	
	// Update is called once per frame
	void Update ()
    {
        switch (state)
        {
            case ZombieState.MovingToWindow:
                Move();

                if (ReachedTarget())
                {
                    Debug.Log("reached window");
                    state = ZombieState.EnteringWindow;
                    timer = attackSpeed;
                    SetNextNode();
                }
                break;
            case ZombieState.AttackingWindow:
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    // Attack - return bool of window broken
                    timer = attackSpeed;
                }
                break;
            case ZombieState.EnteringWindow:
                Move();

                if (ReachedTarget())
                {
                    Debug.Log(nodeIndex);
                    if (nodeIndex == 2)
                    {
                        SetNextNode();
                    }
                    else
                    {
                        currentTarget = GameObject.FindGameObjectWithTag(Tags.Player).transform;
                        state = ZombieState.MovingToPlayer;
                        Debug.Log("movign to player");
                    }
                }

                break;
            case ZombieState.MovingToPlayer:
                Move();
                break;
        }
	}

    private void SetNextNode()
    {
        currentTarget = targetNodes.transform.GetChild(nodeIndex);
        nodeIndex++;
        direction = Vector3.Normalize(currentTarget.transform.position - parentTransform.position);
        Debug.Log(currentTarget);
        Debug.Log(direction);
    }
    
    private bool ReachedTarget()
    {
        return Vector3.SqrMagnitude(currentTarget.transform.position - parentTransform.position) < speed * speed * Time.deltaTime * Time.deltaTime ;
    }

    private void Move()
    {
        direction = Vector3.Normalize(currentTarget.transform.position - parentTransform.position);
        parentTransform.position += direction * speed * Time.deltaTime;
    }
}
