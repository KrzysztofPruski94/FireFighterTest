using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiresController : MonoBehaviour
{
    [SerializeField]
    private GameObject firesParent;

    [SerializeField]
    private List<Fire> fires = new List<Fire>();

    private int extinguishedFires = 0;

    public Action<float> OnAllFiresExtinguished;
    public Action OnFiresStarted;

    private float timeFromStart = 0;

    private void Start()
    {
        foreach (Fire fire in fires)
        {
            fire.OnFireExtinguished += CheckForAllFiresExtinguished;
        }
        StartCoroutine(StartFires());
    }

    private void Update()
    {
        timeFromStart += Time.deltaTime;
    }

    private IEnumerator StartFires()
    {
        yield return new WaitForSeconds(5);
        firesParent.SetActive(true);
        OnFiresStarted?.Invoke();
    }

    private void CheckForAllFiresExtinguished()
    {
        extinguishedFires++;

        if (extinguishedFires == fires.Count)
        {
            firesParent.SetActive(false);
            OnAllFiresExtinguished?.Invoke(timeFromStart);
        }
    }


}
