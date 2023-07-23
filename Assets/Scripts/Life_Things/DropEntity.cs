using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropEntity : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _drops;
    [SerializeField, Range(0, 100)]
    private int _dropItemProbability;
    private void DropItem()
    {
        if (_drops != null)
        {
            int random = UnityEngine.Random.Range(0, 101);
            if (random < _dropItemProbability)
            {
                Instantiate(_drops[UnityEngine.Random.Range(0, _drops.Length)], transform.position, Quaternion.identity);
            }
        }
    }
}
