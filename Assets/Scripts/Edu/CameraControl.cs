using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
        RotateCamera();
        ZoomCamera();
    }

    void MoveCamera()
    {
        if(Input.GetMouseButton(0))
        {
            transform.Translate(Input.GetAxis("Mouse X") / 10f,
                Input.GetAxis("Mouse Y") / 10f, 0f);
        }
    }

    void RotateCamera()
    {
        if(Input.GetMouseButton(1))
        {
            transform.Rotate(Input.GetAxis("Mouse Y") * -10f,
                Input.GetAxis("Mouse X") * 10f,
                0.0f);
        }
    }



    //���� �����Ҷ� ī�޶� ��ġ�� �ű�°� �ƴ϶� Field of View �� �̿��ؾ� �Ѵ�.
    void ZoomCamera()
    {
        mainCamera.fieldOfView += (-20 * Input.GetAxis("Mouse ScrollWheel"));
        if (mainCamera.fieldOfView < 10f)
            mainCamera.fieldOfView = 10f;
        if (mainCamera.fieldOfView > 100f)
            mainCamera.fieldOfView = 100f;
    }

}
