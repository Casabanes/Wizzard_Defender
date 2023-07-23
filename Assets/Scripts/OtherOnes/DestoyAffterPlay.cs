using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoyAffterPlay : MonoBehaviour
{
    [SerializeField] private float _effectDuration;
    void Start()
    {
        StartCoroutine(DestroyThis());
    }
    IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(_effectDuration);
        Destroy(gameObject);
    }
}
