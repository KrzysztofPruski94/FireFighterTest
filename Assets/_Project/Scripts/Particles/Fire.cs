using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField]
    private Transform particlesTransform;

    private float scaleDownSpeed = 0.5f;

    public Action OnFireExtinguished;

    private void OnParticleCollision(GameObject other)
    {
        if (particlesTransform.localScale.y > 0.2f)
        {
            particlesTransform.localScale -= (Vector3.one * Time.deltaTime * scaleDownSpeed);
        }
        else
        {
            OnFireExtinguished?.Invoke();
            gameObject.SetActive(false);
        }
    }



}
