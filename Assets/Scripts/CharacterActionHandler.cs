using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterActionHandler : MonoBehaviour
{
    
    public Vector3 movementV3;
    //Rigidbody rb;
    GameObject go;

    private void Awake()
    {

        //var gamplayActionMap = playerControls.FindActionMap("Player");
        //movement = gamplayActionMap.FindAction("Move");  

        //movement.performed += OnMovementChanged;
        //movement.canceled += OnMovementChanged;
    }

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        go = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        var tempPos = go.transform.position;
        go.transform.position = tempPos + (3 * movementV3 * Time.deltaTime);
    }

    public void OnMovementChanged(InputAction.CallbackContext context)
    {

            Vector2 movement = context.ReadValue<Vector2>();

        if (context.performed)
        {
            movementV3 = new Vector3(movement.x, movement.y, 0);          
        }
        else if (context.canceled)
        {
            movementV3 = new Vector3(0, 0, 0);
        }
        

        //transform.position = new Vector3(1.2f * transform.position.x + movementV3.x * Time.deltaTime, (1.2f * transform.position.y + movementV3.y * Time.deltaTime, (1.2f * (transform.position.z + movementV3.z)) * Time.deltaTime);
        
        
    }

}

