using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSight : MonoBehaviour
{
    private void Update()
    {
        CannonRotationToSight();
    }

    private void CannonRotationToSight()
    {
        transform.Rotate(0, 0, 40 * Time.deltaTime);
    }
}
