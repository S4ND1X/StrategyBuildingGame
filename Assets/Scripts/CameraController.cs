using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float normalSpeed;
    [SerializeField] private float fastSpeed;
    private float movementSpeed;
    [SerializeField] private float movementTime;

    [SerializeField] Vector3 newPosition;

    // Start is called before the first frame update
    void Start()
    {
        newPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovementInput();
    }

    private void HandleMovementInput()
    {
        //Detect if is pressing shift to move faster or not
        movementSpeed = Input.GetKey(KeyCode.LeftShift) ? fastSpeed : normalSpeed;
        newPosition += new Vector3( Input.GetAxis("Vertical"),0,Input.GetAxis("Horizontal")*-1) * movementSpeed;
        transform.localPosition = Vector3.Lerp(transform.localPosition, newPosition, Time.deltaTime * movementTime);
    }
}
