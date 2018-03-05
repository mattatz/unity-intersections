using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Geometry
{

    public abstract class Shape {

        Type type;

        public Shape()
        {
            type = GetType();
        }

        public bool Intersects(Shape shape)
        {
            MethodInfo method = typeof(Intersection).GetMethod("Intersects", new Type[2] { type, shape.GetType() });
            if(method != null)
            {
                var ret = method.Invoke(typeof(Intersection), new object[] { this, shape });
                return Convert.ToBoolean(ret);
            }

            return false;
        }

        public abstract void DrawGizmos(bool intersected);

    }

}

