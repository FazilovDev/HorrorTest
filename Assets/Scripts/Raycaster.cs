using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    public float Distance = 4f;
    public float Offset = 0.2f;

    public GameObject Target;
    public Vector3 TargetPosition;
    public bool Hited { get; private set; }
    public RaycastHit HitInfo;

    private Transform transformComp;

    private void Awake()
    {
        transformComp = transform;
    }

    private void FixedUpdate()
    {
        Hited = Physics.Raycast(transformComp.position + transformComp.forward * Offset, transformComp.forward, out HitInfo, Distance);
        if (!Hited)
        {
            Target = null;
            return;
        }

        TargetPosition = HitInfo.point;
        Target = HitInfo.transform.gameObject;
    }
}
