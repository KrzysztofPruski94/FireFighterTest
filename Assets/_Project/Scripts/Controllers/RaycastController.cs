using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastController : MonoBehaviour
{
    [SerializeField]
    private float raycastDistance = 100;

    private const string equipmentLayerMask = "Equipment";
    private const string floorLayerMask = "Ground";

    public Action<RaycastHit> OnEquipmentClick;
    public Action<RaycastHit> OnFloorClick;

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit equipmentHitInfo, raycastDistance, LayerMask.GetMask(equipmentLayerMask)))
            {
                OnEquipmentClick?.Invoke(equipmentHitInfo);
            }

            if (Physics.Raycast(ray, out RaycastHit floorHitInfo, raycastDistance, LayerMask.GetMask(floorLayerMask)))
            {
                OnFloorClick?.Invoke(floorHitInfo);
            }
        }
    }

}
