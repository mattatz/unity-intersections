using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geometry
{

    public class TriangleTester : IntersectionTester {

        [SerializeField] protected Vector3 a = Vector3.up, b = Vector3.left, c = Vector3.right;

        public override Shape GetShape()
        {
            var pa = transform.TransformPoint(a);
            var pb = transform.TransformPoint(b);
            var pc = transform.TransformPoint(c);
            return new Triangle(pa, pb, pc);
        }

        void OnDrawGizmos ()
        {
            var tri = GetShape() as Triangle;
            tri.DrawGizmos(intersected);
        }

    }

}


