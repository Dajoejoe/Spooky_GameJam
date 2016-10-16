using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour {

    public int boardCount;
    public bool isDead;

    Board[] boards;

	// Use this for initialization
	void Start () {
        isDead = false;
        boards = transform.FindChild("Boards").GetComponentsInChildren<Board>();
		if (boardCount == 0)
        {
            boardCount = boards.Length;
        }
        else
        {
            for (int i = boards.Length - 1; i > boardCount; i--)
            {
                boards[i].gameObject.SetActive(false);
            }
        }
        foreach (var board in boards)
        {
            board.window = this;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Returns whether the whole window is broken
    public bool AttackWindow(int amt)
    {
        bool brokeBoard = false;
        for (int i=0; i < boards.Length; i++)
        {
            if (!boards[i].isActive || boards[i].isBroken)
            {
                continue;
            }

            brokeBoard = boards[i].Damage(amt);
        }

        if (brokeBoard)
        {
            for (int i = 0; i < boards.Length; i++)
            {
                if (!boards[i].isActive || boards[i].isBroken)
                {
                    continue;
                }
                isDead = false;
            }
            isDead = true;
        }
        return isDead;
    }

    public bool RepairWindow(int amt, Board board)
    {
        return board.Repair(amt);
    }
}
