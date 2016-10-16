using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMove : MonoBehaviour {

    public Window window;
    public float speed;
    public float attackSpeed = 1f;
    public int attack = 2;

    int nodeIndex = 0;
    GameObject targetNodes;
    Transform currentTarget;
    Transform parentTransform;
    Vector3 direction;
    ZombieState state;
    float timer;
    Animator animator;

    enum ZombieState
    {
        MovingToWindow, AttackingWindow, EnteringWindow, MovingToPlayer
    }

    // Use this for initialization
    void Start () {
        state = ZombieState.MovingToWindow;
        parentTransform = transform.parent;
        targetNodes = window.transform.FindChild("Nodes").gameObject;
        SetNextNode();
        SetVerticalPosition();
        direction = Vector3.Normalize(currentTarget.transform.position - parentTransform.position);
        animator = GetComponent<Animator>();
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
                    state = ZombieState.AttackingWindow;
                    timer = attackSpeed;
                }
                break;
            case ZombieState.AttackingWindow:
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    // Attack - return bool of window broken
                    if (window.AttackWindow(attack) || window.isDead)
                    {
                        state = ZombieState.EnteringWindow;
                        SetNextNode();
                        animator.SetTrigger("EnterWindow");
                    }
                    timer = attackSpeed;
                }
                break;
            case ZombieState.EnteringWindow:
                Move();

                if (ReachedTarget())
                {
                    currentTarget = GameObject.FindGameObjectWithTag(Tags.Player).transform;
                    state = ZombieState.MovingToPlayer;
                    animator.SetTrigger("ExitWindow");
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

    private void SetVerticalPosition()
    {
        Vector3 pos = parentTransform.position;
        pos.y = currentTarget.position.y;
        parentTransform.position = pos;
    }
}
