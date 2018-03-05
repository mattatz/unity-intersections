using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geometry
{

    public class Ray : Shape {

        public Vector3 Point { get { return point; } }
        public Vector3 Dir { get { return dir; } }
        public Vector3 InvDir { get { return invDir; } }
        public Vector3 Sign { get { return sign; } }

        protected Vector3 point, dir, invDir;
        protected Vector3 sign;

        public Ray(Vector3 point, Vector3 dir) : base()
        {
            this.point = point;

            this.dir = dir;
            this.invDir = new Vector3(
                1f / dir.x,
                1f / dir.y,
                1f / dir.z
            );
            this.sign = new Vector3(
                invDir.x < 0f ? 1 : 0,
                invDir.y < 0f ? 1 : 0,
                invDir.z < 0f ? 1 : 0
            );
        }

        public override void DrawGizmos(bool intersected)
        {
            Gizmos.color = intersected ? Color.red : Color.white;
            Gizmos.DrawSphere(Point, 0.1f);
            Gizmos.DrawRay(Point, Dir * float.MaxValue);
        }

    }

}


