using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private RaycastController raycastController;

    [SerializeField]
    private MyThirdPersonController myThirdPersonController;

    void Start()
    {
        raycastController.OnFloorClick += myThirdPersonController.StartMovingTowards;
    }
}
