using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geometry
{

    public class Plane : Shape {

        public Vector3 Normal { get { return normal; } }
        public float Distance { get { return distance; } }

        protected Vector3 normal;
        protected float distance;

        public Plane(Vector3 normal, float distance) : base()
        {
            this.normal = normal;
            this.distance = distance;
        }

        public override void DrawGizmos(bool intersected)
        {
            Gizmos.color = intersected ? Color.red : Color.white;
        }

    }

}


