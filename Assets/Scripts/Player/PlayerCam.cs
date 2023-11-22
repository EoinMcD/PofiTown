using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [SerializeField] GameObject playerCamPrefab;
    CameraScript cams;

    CinemachineVirtualCamera mainCam;

    private void Start() {
        cams=playerCamPrefab.GetComponent<CameraScript>();
        mainCam=cams.GetMainCam();
        Instantiate(playerCamPrefab);
        mainCam.Priority=100;
        mainCam.m_Follow=this.gameObject.transform;
    }



    
}
