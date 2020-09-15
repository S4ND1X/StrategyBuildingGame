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
    [SerializeField] private Vector3 zoomAmount;
    [SerializeField] private Vector3 zoomAmountMouse;

    [SerializeField] private Transform cameraTransform;


    [SerializeField] private  Vector3 dragStartPosition;
    [SerializeField] private Vector3 dragCurrentPositon;

    [SerializeField] private Vector3 rotateStartPosition;
    [SerializeField] private Vector3 rotateCurrentPositon;

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
        HandleMouseInput();
        HandleMovementInput();
    }

   private void HandleMouseInput()
    {
          if(Input.mouseScrollDelta.y != 0)
        {
            newZoom += Input.mouseScrollDelta.y * zoomAmountMouse;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if(plane.Raycast(ray, out entry))
            {
                dragStartPosition = ray.GetPoint(entry);
            }

        }

        else if(Input.GetMouseButton(0))
        {
                Plane plane = new Plane(Vector3.up, Vector3.zero);

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                float entry;

                if (plane.Raycast(ray, out entry))
                {
                    dragCurrentPositon = ray.GetPoint(entry);
                    newPosition = transform.position + dragStartPosition - dragCurrentPositon;
                }
        }

        if (Input.GetMouseButtonDown(1))
        {
            rotateStartPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(1))
        {
            rotateCurrentPositon = Input.mousePosition;


            Vector3 diff = rotateStartPosition - rotateCurrentPositon;

            rotateStartPosition = rotateCurrentPositon;
            newRotation *= Quaternion.Euler(Vector3.up * (-diff.x / 5f));
        }
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

        if (Input.GetKey(KeyCode.R))
        {
            newZoom += zoomAmount;
        }
        else if (Input.GetKey(KeyCode.F))
        {
            newZoom -= zoomAmount;
        }



        transform.localPosition = Vector3.Lerp(transform.localPosition, newPosition, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);

    }
}
