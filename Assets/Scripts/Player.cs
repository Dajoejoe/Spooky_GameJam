using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public int attack;
    public int repair;
    public float delay;

    bool gotKey;
    float timer;
    GameObject highlightedObject;
    GameObject healthBarGameObject;
    GameObject healthBar;
	// Use this for initialization
	void Start () {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        healthBarGameObject = GameObject.FindGameObjectWithTag(Tags.HealthBar);
        healthBar = healthBarGameObject.transform.FindChild("BG").FindChild("HealthBar").gameObject;
        healthBarGameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Time.timeScale == 0)
        {
            return;
        }

        GameObject board = CheckForHighlight();
        if (board)
        {
            if (timer <= 0)
            {
                CheckForInput();
            }
            if (board.GetComponent<Board>())
            {
                if (board.GetComponent<Board>().HealthPercentage() == 0)
                {
                    healthBarGameObject.SetActive(false);
                }
                else
                {
                    healthBarGameObject.SetActive(true);
                    healthBar.transform.localScale = new Vector3(board.GetComponent<Board>().HealthPercentage(), 1, 1);
                }
            }
        }
        else if (highlightedObject != null)
        {
            HighlightObject(false);
            highlightedObject = null;
            healthBarGameObject.SetActive(false);
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

	}

    private GameObject CheckForHighlight()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hits = Physics.RaycastAll(ray);
        if (hits.Length > 0)
        {
            var hitList = new List<RaycastHit>();
            var hit = hits[0];
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].distance < hit.distance)
                {
                    hit = hits[i];
                }
            }

            if (hit.distance > 3)
                return null;
            var gameObjectHit = hit.collider.gameObject;
            if (gameObjectHit.GetComponent<Board>() || gameObjectHit.GetComponent<Key>() || gameObjectHit.GetComponent<Journal>())
            { 
                if (highlightedObject == null && !(gameObjectHit.GetComponent<Board>() && gameObjectHit.GetComponent<Board>().isBroken && gameObjectHit.GetComponent<Board>().door))
                {
                    highlightedObject = gameObjectHit;
                    HighlightObject(true);
                }
            }
            return gameObjectHit;
        }
        return null;
    }

    private void CheckForInput()
    {
        if (Input.GetMouseButton(0) && highlightedObject)
        {
            var board = highlightedObject.GetComponent<Board>();
            if (board != null)
            {
                var full = board.Repair(repair);
                if (full)
                {
                    HighlightObject(false);
                }
            }
            else if (highlightedObject.GetComponent<Key>())
            {
                highlightedObject.GetComponent<Key>().CollectKey();
            }
            else if (highlightedObject.GetComponent<Journal>())
            {
                highlightedObject.GetComponent<Journal>().ReadJournal();
                Time.timeScale = 0;
            }
            timer = delay;
            return;
        }
        if (Input.GetMouseButton(1) && highlightedObject)
        {
            var board = highlightedObject.GetComponent<Board>();
            if (board != null && !board.isBroken)
            {
                if (board.door != null)
                {
                    var brokeBoard = board.door.AttackDoor(attack, board);
                    if (brokeBoard)
                    {
                        HighlightObject(false);
                        highlightedObject = null;
                    }
                }
                else
                {
                    var destroyed = board.Damage(attack);
                    if (destroyed && board.window != null && board.window.isDead)
                    {
                        HighlightObject(false);
                    }
                }
            }
            timer = delay;
        }
    }

    private void HighlightObject(bool highlight)
    {
        if (highlightedObject == null)
        {
            return;
        }
        if (highlightedObject.GetComponent<Board>())
        {
            highlightedObject.GetComponent<Board>().Highlight(highlight);
        }
        else if (highlightedObject.GetComponent<Journal>())
        {
            highlightedObject.GetComponent<Journal>().Highlight(highlight);
        }
        else if (highlightedObject.GetComponent<Key>())
        {
            highlightedObject.GetComponent<Key>().Highlight(highlight);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == Tags.Zombie)
        {
            SceneManager.LoadScene(1);
        }
    }
}
