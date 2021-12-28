using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class MouseCameraRotate : MonoBehaviour
{
    public Cinemachine.CinemachineFreeLook mainCamera;
    [DllImport("user32.dll")]

    static extern bool SetCursorPos(int X, int Y);

// Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(1))
        {
            float xValue = Input.GetAxisRaw("Mouse X");
            mainCamera.m_XAxis.Value += xValue;
            Cursor.visible = false;
        }
        else
        {
            if(Cursor.visible == false)
            {
                SetCursorPos(Screen.width / 2, Screen.height / 2);
            }
            Cursor.visible = true;
        }
    }
}
