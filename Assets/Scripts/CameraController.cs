using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float normalSpeed;
    [SerializeField] private float fastSpeed;
    private float movementSpeed;
    [SerializeField] private float movementTime;
    [SerializeField] private float rotationAmount;

     Vector3 newPosition;

     Quaternion newRotation;

     Vector3 newZoom;
    [SerializeField] Vector3 zoomAmount;

    [SerializeField] Transform cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;

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
        newPosition += new Vector3(Input.GetAxis("Horizontal"), 0,Input.GetAxis("Vertical")) * movementSpeed;

        if (Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        } else if (Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            newZoom += zoomAmount;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            newZoom -= zoomAmount;
        }



        transform.localPosition = Vector3.Lerp(transform.localPosition, newPosition, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);

    }
}
