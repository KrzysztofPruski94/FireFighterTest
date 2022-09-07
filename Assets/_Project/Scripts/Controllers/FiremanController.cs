using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiremanController : MonoBehaviour
{
    public Action<string> OnWrongEquipment;

    [SerializeField]
    private GameObject fireExtinguisher, uniform, helmet;

    [SerializeField]
    private Outline fireExtinguisherOutline, uniformOutline, helmetOutline;

    [SerializeField]
    private Transform handPivot, headPivot;

    [SerializeField]
    private SkinnedMeshRenderer firemanBodyRenderer;

    [SerializeField]
    private GameObject foam;

    [SerializeField]
    private float minimumDistanceToActivateEq = 1f;

    private bool isUniformWorn = false, isHelmetWorn = false, isExtinguisherWorn = false;

    private void Update()
    {
        SetAllEquipmentOutlines();
    }

    private void SetAllEquipmentOutlines()
    {
        if (!isUniformWorn)
        {
            SetEquipmentOutline(uniform.transform, uniformOutline);
        }
        else if (!isHelmetWorn)
        {
            SetEquipmentOutline(helmet.transform, helmetOutline);
            return;
        }
        else if (!isExtinguisherWorn)
        {
            SetEquipmentOutline(fireExtinguisher.transform, fireExtinguisherOutline);
        }
    }


    public void WearEquipment(RaycastHit hitInfo)
    {
        if (IsEquipmentInInteractionRange(firemanBodyRenderer.transform.position, hitInfo.point))
        {
            if (hitInfo.collider.gameObject == uniform)
            {
                WearUniform();
                isUniformWorn = true;

            }
            else if (hitInfo.collider.gameObject == helmet)
            {
                if (isUniformWorn)
                {
                    WearHelmet(hitInfo);
                    isHelmetWorn = true;
                }
                else
                {
                    OnWrongEquipment?.Invoke("Wear uniform first");
                }
            }
            else if (hitInfo.collider.gameObject == fireExtinguisher)
            {
                if (isHelmetWorn)
                {
                    WearExtinguisher(hitInfo);
                    isExtinguisherWorn = true;
                }
                else
                {
                    OnWrongEquipment?.Invoke("Wear helmet first");
                }
            }
        }
    }

    public void ToggleExtinguisherFoam(bool toggle)
    {
        if (isExtinguisherWorn)
        {
            foam.SetActive(toggle);
        }
    }

    private void SetEquipmentOutline(Transform equipment, Outline outline)
    {
        if (IsEquipmentInInteractionRange(firemanBodyRenderer.transform.position, equipment.position))
        {
            outline.OutlineColor = Color.green;
        }
        else
        {
            outline.OutlineColor = Color.yellow;
        }
    }

    private bool IsEquipmentInInteractionRange(Vector3 firemanPos, Vector3 eqPos)
    {
        float distance = (firemanPos - eqPos).magnitude;
        return distance < minimumDistanceToActivateEq;
    }

    private void WearUniform()
    {
        foreach (Material material in firemanBodyRenderer.materials)
        {
            material.color = Color.red;
        }
        uniformOutline.enabled = false;
    }

    private void WearHelmet(RaycastHit hitInfo)
    {
        hitInfo.collider.enabled = false;
        helmet.transform.position = headPivot.position;
        helmet.transform.SetParent(headPivot);
        helmetOutline.enabled = false;
    }

    private void WearExtinguisher(RaycastHit hitInfo)
    {
        hitInfo.collider.enabled = false;
        fireExtinguisher.transform.position = handPivot.position;
        fireExtinguisher.transform.right = firemanBodyRenderer.transform.forward;
        fireExtinguisher.transform.SetParent(firemanBodyRenderer.transform, true);
        fireExtinguisherOutline.enabled = false;
    }
}
