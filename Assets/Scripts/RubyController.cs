using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    void Update()
    {
        var position = transform.position;
        position.x = position.x + 0.1f;
        transform.position = position;
    }
}
