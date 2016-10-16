using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour {

    public int boardCount;

    Board[] boards;
    bool isDead;

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
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool AttackWindow(int amt)
    {
        for (int i=0; i < boards.Length; i++)
        {
            if (!boards[i].isActive || boards[i].isBroken)
            {
                continue;
            }


        }

        return true;
    }
}
