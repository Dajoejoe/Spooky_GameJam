using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public int attack;
    public int repair;
    public float delay;

    float timer;
    Board highlightedObject;
	// Use this for initialization
	void Start () {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
	}
	
	// Update is called once per frame
	void Update () {
        Board board = CheckForHighlight();
        if (board)
        {
            if (timer <= 0)
            {
                CheckForInput();
            }
        }
        else if (highlightedObject != null)
        {
            highlightedObject.Highlight(false);
            highlightedObject = null;
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
	}

    private Board CheckForHighlight()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hits = Physics.RaycastAll(ray);
        if (hits.Length > 0)
        {
            var hitList = new List<RaycastHit>();
            var hit = hits[0];
            for (int i=0; i < hits.Length; i++)
            {
                if (hits[i].distance < hit.distance)
                {
                    hit = hits[i];
                }
            }

            if (hit.distance > 3)
                return null;
            var gameObjectHit = hit.collider.gameObject;
            var highlight = gameObjectHit.GetComponent<Board>();
            if (highlight != null) {
                highlight.Highlight(true);
                highlightedObject = highlight;
                return highlight;
            }
            return null;
        }
        return null;
    }

    private void CheckForInput()
    {
        if (Input.GetMouseButton(0))
        {
            var board = highlightedObject.GetComponent<Board>();
            if (board != null)
            {
                var full = board.Repair(repair);
                if (full)
                {
                    highlightedObject.Highlight(false);
                    highlightedObject = null;
                }
            }
            timer = delay;
            return;
        }
        if (Input.GetMouseButton(1))
        {
            var board = highlightedObject.GetComponent<Board>();
            if (board != null)
            {
                if (board.door != null)
                {
                    var brokeBoard = board.door.AttackDoor(attack, board);
                    if (brokeBoard)
                    {
                        highlightedObject.Highlight(false);
                    }
                }
                else
                {
                    var destroyed = board.Damage(attack);
                    if (destroyed && board.window != null && board.window.isDead)
                    {
                        highlightedObject.Highlight(false);
                    }
                }
            }
            timer = delay;
        }
    }

    void OnColliionEnter(Collider colider)
    {
        if (colider.tag == Tags.Zombie)
        {
            SceneManager.LoadScene(0);
        }
    }
}
