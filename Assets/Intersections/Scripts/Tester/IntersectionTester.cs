using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geometry
{

    public abstract class IntersectionTester : MonoBehaviour {

        [SerializeField] protected IntersectionTester other;

        protected bool intersected;

        protected virtual void Update ()
        {
            if(other != null) {
                intersected = GetShape().Intersects(other.GetShape());
            } else {
                intersected = false;
            }
        }

        public abstract Shape GetShape();

    }

}


