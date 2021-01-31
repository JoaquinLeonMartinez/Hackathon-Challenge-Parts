using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform Target;
    float TargetDistance = 1f;
    Camera cam;
    bool activateShake = false;
    public float shakeAmount = .1f;
    Vector3 camPosOrigin;
    public float shakeDuration = .3f;
    float shakeTimer = 0f;
    Vector3 camPos;
    public int shakeFrecuency = 2;

    private void Start()
    {
        cam = GetComponent<Camera>();
        Debug.LogError("comentar input de shake!");
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.T))
        {
            shake(shakeAmount, shakeDuration, shakeFrecuency);
        }
        


        if (activateShake)
        {
            if (Time.frameCount % shakeFrecuency == 0)
            {
                camPos = new Vector3(0, 0, -TargetDistance);
                float offsetX = Random.Range(-shakeAmount, shakeAmount);
                float offsetY = Random.Range(-shakeAmount, shakeAmount);
                camPos.x += offsetX;
                camPos.y += offsetY;
                cam.transform.localPosition = camPos;
            }

            //TIMER
            if (shakeTimer >= shakeDuration)
            {
                shakeTimer = 0.0f;
                activateShake = false;
                cam.transform.localPosition = new Vector3(0, 0, -TargetDistance);
            }
            else
            {
                shakeTimer += Time.deltaTime;
            }

        }
        else
        {
            transform.localPosition = new Vector3(0, 0, -TargetDistance);
        }

    }

    void shake(float _shakeAmount, float _shakeDuration, int _shakeFrecuency)
    {
        activateShake = true;
        shakeFrecuency = _shakeFrecuency;
        camPosOrigin = cam.transform.position;
        shakeAmount = _shakeAmount;
        shakeDuration = _shakeDuration;
    }

}
