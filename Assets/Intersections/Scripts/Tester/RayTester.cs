using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geometry
{

    public class RayTester : IntersectionTester {

        [SerializeField] protected Vector3 point, dir = Vector3.forward;

        public override Shape GetShape()
        {
            return new Ray(transform.TransformPoint(point), transform.TransformDirection(dir));
        }

        void OnDrawGizmos ()
        {
            var r = GetShape() as Ray;
            r.DrawGizmos(intersected);
        }

    }

}


