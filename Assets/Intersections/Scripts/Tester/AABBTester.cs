using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geometry
{

    public class AABBTester : IntersectionTester {

        [SerializeField] protected Vector3 min = Vector3.zero, max = Vector3.one;

        public override Shape GetShape()
        {
            var pmin = transform.TransformPoint(min);
            var pmax = transform.TransformPoint(max);
            return new AABB(pmin, pmax);
        }

        void OnDrawGizmos ()
        {
            var aabb = GetShape() as AABB;
            aabb.DrawGizmos(intersected);
        }

    }

}


