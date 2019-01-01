using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScr : MonoBehaviour {

    //MainObjects
    public MapManger mapManger;
    private Camera camera;  //Camera must be tagged as Main Camera

    //Data
    RaycastHit hit = new RaycastHit();

    //Keyboard in
    public Vector2 movement;    //Standart auf WASD: Vector2(vorwärts-bewegung, seitwärts-bewegung)

    //Mouse in
    public bool select;

    //Keybindings
    public bool keybindingfileExists;
    private KeyCode forward;
    private KeyCode backwards;
    private KeyCode sidewaysL;
    private KeyCode sidewaysR;

    //Debug and Testing
    NavAgentScr navAgent; //Durfe für n paar sachen nicht lokal sein


    // Use this for initialization
    void Start () {
        camera = Camera.main;

        BindKeys();

        mapManger = gameObject.AddComponent<MapManger>() as MapManger;
        mapManger.LoadMapFromData("Map3");
        //mapManger.CreateMap2();

        ///TESTING
        navAgent = new NavAgentScr(this);
        //navAgent.SetStartAndGoal(new Vector2(3,27),new Vector2(27,3));
        //navAgent.SetStartAndGoal(new Vector2(3, 27), new Vector2(3, 15));
        //navAgent.SetStartAndGoal(new Vector2(3, 15), new Vector2(27, 3));
        //navAgent.SetStartAndGoal(new Vector2(13, 13), new Vector2(13, 1));
        //navAgent.SetStartAndGoal(new Vector2(1, 1), new Vector2(8, 8));
        navAgent.SetStartAndGoal(new Vector2(10, 190), new Vector2(150, 100));
        navAgent.FindPath();
        ///END


    }
	
	// Update is called once per frame
	void Update () {
        CheckMouse();
        CheckKeyboard();
        InputLogic();
	}

    private void InputLogic()
    {
        if (select)
        {
            var ray = camera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit))
            {
                GameObject _foundGameObject = hit.collider.gameObject;
                if (_foundGameObject.tag.Equals("Tile"))
                {
                    navAgent.TestReveal(new Vector2(_foundGameObject.transform.position.x, _foundGameObject.transform.position.z));
                }
            }


        }
    }

    private void CheckMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            select = true;
        }
        else
        {
            select = false;
        }
    }

    private void CheckKeyboard()
    {
        movement = Vector2.zero;
        if (Input.GetKey(forward))
            movement.x++;
        if (Input.GetKey(backwards))
            movement.x--;
        if (Input.GetKey(sidewaysL))
            movement.y--;
        if (Input.GetKey(sidewaysR))
            movement.y++;
    }

    private void BindKeys()
    {
        if (keybindingfileExists)
        {
            Debug.Log("Schreibe Keybindingladeautomatismus");
        } else
        {
            forward = KeyCode.W;
            backwards = KeyCode.S;
            sidewaysL = KeyCode.A;
            sidewaysR = KeyCode.D;
        }
    }
}
