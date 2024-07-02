using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera mainCam;
    
    public CinemachineVirtualCamera GetMainCam(){
        return mainCam;
    }

}
