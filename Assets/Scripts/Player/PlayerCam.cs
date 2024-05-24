using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [SerializeField] GameObject playerCamPrefab;
    CameraScript cams;

    CinemachineVirtualCamera mainCam;
    GameObject playerCam;

    private void Start() {
        playerCam = Instantiate(playerCamPrefab);
        cams = playerCam.GetComponent<CameraScript>();
        mainCam=cams.GetMainCam();
        mainCam.Priority=100;
        mainCam.m_Follow=this.gameObject.transform;
    }



    
}
