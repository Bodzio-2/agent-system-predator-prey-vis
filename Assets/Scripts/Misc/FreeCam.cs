using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FreeCam : MonoBehaviour
{
    [SerializeField]
    float speed = 100;

    [SerializeField]
    TextMeshProUGUI camLockText;

    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;

    public float minimumX = -360F;
    public float maximumX = 360F;

    public float minimumY = -60F;
    public float maximumY = 60F;

    float rotationY = 0F;

    bool canControl = true;

    private void FixedUpdate()
    {
        if (canControl)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            float y = 0f;
            if (Input.GetKey(KeyCode.Space))
                y = 1;
            else if (Input.GetKey(KeyCode.LeftShift))
                y = -1;
            Vector3 movement = new Vector3(x, y, z);
            transform.Translate(movement * speed * Time.fixedDeltaTime);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("f"))
            canControl = !canControl;

        if (camLockText != null)
            camLockText.text = canControl ? "ACTIVE" : "LOCKED";

        if (canControl)
        {
            if (axes == RotationAxes.MouseXAndY)
            {
                float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
            }
            else if (axes == RotationAxes.MouseX)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
            }
            else
            {
                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
            }
        }
    }
}
