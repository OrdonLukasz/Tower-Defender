using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float panSpeed = 30f;
    [SerializeField] private float windowEdgeBorder = 50f;
    [SerializeField] private float leftLimit = 0f;
    [SerializeField] private float rightLimit = 70f;
    [SerializeField] private float upLimit = 65f; 
    [SerializeField] private float downLimit = -4f;

    void Update()
    {
        float mouseY = Input.mousePosition.y;
        float mouseX = Input.mousePosition.x;

        if (mouseX >= 0 && mouseX <= Screen.width && mouseY >= 0 && mouseY <= Screen.height)
        {
            if ((Input.GetKey("w") || mouseY >= Screen.height - windowEdgeBorder) && transform.position.z < upLimit)
            {
                transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
            }

            if ((Input.GetKey("a") || mouseX <= windowEdgeBorder) && transform.position.x > leftLimit)
            {
                transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
            }

            if ((Input.GetKey("s") || mouseY <= windowEdgeBorder) && transform.position.z > downLimit)
            {
                transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
            }

            if ((Input.GetKey("d") || mouseX >= Screen.width - windowEdgeBorder) && transform.position.x < rightLimit)
            {
                transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
            }
        }
    }
}
