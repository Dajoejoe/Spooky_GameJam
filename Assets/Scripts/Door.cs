using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public int boardCount;
    public bool isDead;

    Board[] boards;
    Animator animator;
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        isDead = false;
        if (transform.FindChild("Boards") == null)
            return;
        boards = transform.FindChild("Boards").GetComponentsInChildren<Board>();
        if (boardCount == 0)
        {
            boardCount = boards.Length;
        }
        else
        {
            for (int i = boards.Length - 1; i > boardCount; i--)
            {
                boards[i].isActive = false;
                boards[i].gameObject.SetActive(false);
            }
        }
        foreach (var board in boards)
        {
            board.door = this;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public bool AttackDoor(int amt, Board board)
    {
        bool brokeBoard = board.Damage(amt);
        
        if (brokeBoard)
        {
            isDead = true;
            for (int i = 0; i < boards.Length; i++)
            {
                if (!boards[i].isActive || boards[i].isBroken)
                {
                    continue;
                }
                isDead = false;
                break;
            }
        }

        if (isDead)
        {
            animator.SetTrigger("OpenDoor");
        }

        return brokeBoard;
    }

    public bool RepairDoor(int amt, Board board)
    {
        return board.Repair(amt);
    }
}
