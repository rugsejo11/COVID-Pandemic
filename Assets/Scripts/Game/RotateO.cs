using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateO : MonoBehaviour
{
    bool isPressed;
    private float speed = 50;
    // Start is called before the first frame update
    void Start()
    {
    }

    public float rotationSpeed = 20;
    bool rotate = false;
    void FixedUpdate()
    {
        if (rotate == false)
            return;

        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        rotate = true;
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        rotate = false;
    }

    // Update is called once per frame
    void Update()
    {
        RotateCube();


        //FixedUpdate();
        //if (Input.GetKeyUp(KeyCode.W))
        //    Rotate();
        //else
        //    return;

    }
    public void RotateCube()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            //float rotX = Input.GetAxis("Mouse X") * speed * Mathf.Deg2Rad;
            //float rotY = Input.GetAxis("Mouse Y") * speed * Mathf.Deg2Rad;
            //transform.RotateAround(Vector3.up, -rotX);
            //transform.RotateAround(Vector3.right, rotY);

            transform.Rotate(0, Input.GetAxis("Mouse X") * speed * Time.deltaTime, 0);
            transform.Rotate(Input.GetAxis("Mouse Y") * speed * Time.deltaTime, 0, 0);
        }
    }

    public void Rotate()
    {
        //if (Input.GetKeyUp(KeyCode.W))
        //{
        transform.Rotate(Vector3.forward * speed * Time.deltaTime);
        //}
    }
}


//if (Input.GetKeyDown(KeyCode.S))
//{
//    transform.Rotate(Vector3.down * speed * Time.deltaTime);
//}

//if (Input.GetKeyDown(KeyCode.A))
//{
//    transform.Rotate(Vector3.left * speed * Time.deltaTime);
//}
//if (Input.GetKeyDown(KeyCode.D))
//{
//    transform.Rotate(Vector3.right * speed * Time.deltaTime);
//}