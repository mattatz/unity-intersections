using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Geometry
{

    public class Intersection {

        // based on https://www.scratchapixel.com/lessons/3d-basic-rendering/minimal-ray-tracer-rendering-simple-shapes/ray-box-intersection
        public static bool Intersects(Ray r, AABB aabb)
        {
            float tmin, tmax, tymin, tymax, tzmin, tzmax;

            tmin =  ((r.Sign.x <= float.Epsilon ? aabb.Min.x : aabb.Max.x) - r.Point.x) * r.InvDir.x;
            tmax =  ((r.Sign.x <= float.Epsilon ? aabb.Max.x : aabb.Min.x) - r.Point.x) * r.InvDir.x;
            tymin = ((r.Sign.y <= float.Epsilon ? aabb.Min.y : aabb.Max.y) - r.Point.y) * r.InvDir.y;
            tymax = ((r.Sign.y <= float.Epsilon ? aabb.Max.y : aabb.Min.y) - r.Point.y) * r.InvDir.y;

            if ((tmin > tymax) || (tymin > tmax)) {
                return false;
            }

            if (tymin > tmin)
            {
                tmin = tymin;
            }

            if (tymax < tmax)
            {
                tmax = tymax;
            }

            tzmin = ((r.Sign.z <= float.Epsilon ? aabb.Min.z : aabb.Max.z) - r.Point.z) * r.InvDir.z;
            tzmax = ((r.Sign.z <= float.Epsilon ? aabb.Max.z : aabb.Min.z) - r.Point.z) * r.InvDir.z;

            if ((tmin > tzmax) || (tzmin > tmax))
            {
                return false;
            }

            if (tzmin > tmin)
            {
                tmin = tzmin;
            }

            if (tzmax < tmax)
            {
                tmax = tzmax;
            }

            return true;
        }

        public static bool Intersects(AABB aabb, Ray r)
        {
            return Intersects(r, aabb);
        }

        // based on https://en.wikipedia.org/wiki/M%C3%B6ller%E2%80%93Trumbore_intersection_algorithm
        public static bool Intersects(Ray r, Triangle tri)
        {
            var e1 = tri.B - tri.A;
            var e2 = tri.C - tri.A;
            var P = Vector3.Cross(r.Dir, e2);
            var det = Vector3.Dot(e1, P);

            if (det > -float.Epsilon && det < float.Epsilon) return false;

            float invDet = 1f / det;

            var T = r.Point - tri.A;
            var u = Vector3.Dot(T, P) * invDet;
            if (u < 0f || u > 1f) return false;

            var Q = Vector3.Cross(T, e1);
            var v = Vector3.Dot(r.Dir, Q * invDet);
            if (v < 0f || u + v > 1f) return false;

            var t = Vector3.Dot(e2, Q) * invDet;
            if (t > float.Epsilon)
            {
                return true;
            }

            return false;
        }

        public static bool Intersects(Triangle tri, Ray r)
        {
            return Intersects(r, tri);
        }

        // based on https://gist.github.com/yomotsu/d845f21e2e1eb49f647f
        public static bool Intersects(Triangle tri, AABB aabb)
        {
            float p0, p1, p2, r;

            Vector3 center = aabb.Center, extents = aabb.Max - center;

            Vector3 v0 = tri.A - center,
                v1 = tri.B - center,
                v2 = tri.C - center;

            Vector3 f0 = v1 - v0,
                f1 = v2 - v1,
                f2 = v0 - v2;

            Vector3 a00 = new Vector3(0, -f0.z, f0.y),
                a01 = new Vector3(0, -f1.z, f1.y),
                a02 = new Vector3(0, -f2.z, f2.y),
                a10 = new Vector3(f0.z, 0, -f0.x),
                a11 = new Vector3(f1.z, 0, -f1.x),
                a12 = new Vector3(f2.z, 0, -f2.x),
                a20 = new Vector3(-f0.y, f0.x, 0),
                a21 = new Vector3(-f1.y, f1.x, 0),
                a22 = new Vector3(-f2.y, f2.x, 0);

            // Test axis a00
            p0 = Vector3.Dot(v0, a00);
            p1 = Vector3.Dot(v1, a00);
            p2 = Vector3.Dot(v2, a00);
            r = extents.y * Mathf.Abs(f0.z) + extents.z * Mathf.Abs(f0.y);

            if (Mathf.Max(-Mathf.Max(p0, p1, p2), Mathf.Min(p0, p1, p2)) > r)
            {
                return false;
            }

            // Test axis a01
            p0 = Vector3.Dot(v0, a01);
            p1 = Vector3.Dot(v1, a01);
            p2 = Vector3.Dot(v2, a01);
            r = extents.y * Mathf.Abs(f1.z) + extents.z * Mathf.Abs(f1.y);

            if (Mathf.Max(-Mathf.Max(p0, p1, p2), Mathf.Min(p0, p1, p2)) > r)
            {
                return false;
            }

            // Test axis a02
            p0 = Vector3.Dot(v0, a02);
            p1 = Vector3.Dot(v1, a02);
            p2 = Vector3.Dot(v2, a02);
            r = extents.y * Mathf.Abs(f2.z) + extents.z * Mathf.Abs(f2.y);

            if (Mathf.Max(-Mathf.Max(p0, p1, p2), Mathf.Min(p0, p1, p2)) > r)
            {
                return false;
            }

            // Test axis a10
            p0 = Vector3.Dot(v0, a10);
            p1 = Vector3.Dot(v1, a10);
            p2 = Vector3.Dot(v2, a10);
            r = extents.x * Mathf.Abs(f0.z) + extents.z * Mathf.Abs(f0.x);
            if (Mathf.Max(-Mathf.Max(p0, p1, p2), Mathf.Min(p0, p1, p2)) > r)
            {
                return false;
            }

            // Test axis a11
            p0 = Vector3.Dot(v0, a11);
            p1 = Vector3.Dot(v1, a11);
            p2 = Vector3.Dot(v2, a11);
            r = extents.x * Mathf.Abs(f1.z) + extents.z * Mathf.Abs(f1.x);

            if (Mathf.Max(-Mathf.Max(p0, p1, p2), Mathf.Min(p0, p1, p2)) > r)
            {
                return false;
            }

            // Test axis a12
            p0 = Vector3.Dot(v0, a12);
            p1 = Vector3.Dot(v1, a12);
            p2 = Vector3.Dot(v2, a12);
            r = extents.x * Mathf.Abs(f2.z) + extents.z * Mathf.Abs(f2.x);

            if (Mathf.Max(-Mathf.Max(p0, p1, p2), Mathf.Min(p0, p1, p2)) > r)
            {
                return false;
            }

            // Test axis a20
            p0 = Vector3.Dot(v0, a20);
            p1 = Vector3.Dot(v1, a20);
            p2 = Vector3.Dot(v2, a20);
            r = extents.x * Mathf.Abs(f0.y) + extents.y * Mathf.Abs(f0.x);

            if (Mathf.Max(-Mathf.Max(p0, p1, p2), Mathf.Min(p0, p1, p2)) > r)
            {
                return false;
            }

            // Test axis a21
            p0 = Vector3.Dot(v0, a21);
            p1 = Vector3.Dot(v1, a21);
            p2 = Vector3.Dot(v2, a21);
            r = extents.x * Mathf.Abs(f1.y) + extents.y * Mathf.Abs(f1.x);

            if (Mathf.Max(-Mathf.Max(p0, p1, p2), Mathf.Min(p0, p1, p2)) > r)
            {
                return false;
            }

            // Test axis a22
            p0 = Vector3.Dot(v0, a22);
            p1 = Vector3.Dot(v1, a22);
            p2 = Vector3.Dot(v2, a22);
            r = extents.x * Mathf.Abs(f2.y) + extents.y * Mathf.Abs(f2.x);

            if (Mathf.Max(-Mathf.Max(p0, p1, p2), Mathf.Min(p0, p1, p2)) > r)
            {
                return false;
            }

            if (Mathf.Max(v0.x, v1.x, v2.x) < -extents.x || Mathf.Min(v0.x, v1.x, v2.x) > extents.x)
            {
                return false;
            }

            if (Mathf.Max(v0.y, v1.y, v2.y) < -extents.y || Mathf.Min(v0.y, v1.y, v2.y) > extents.y)
            {
                return false;
            }

            if (Mathf.Max(v0.z, v1.z, v2.z) < -extents.z || Mathf.Min(v0.z, v1.z, v2.z) > extents.z)
            {
                return false;
            }


            var normal = Vector3.Cross(f1, f0).normalized;
            var pl = new Plane(normal, Vector3.Dot(normal, tri.A));
            return Intersects(pl, aabb);
        }

        public static bool Intersects(AABB aabb, Triangle tri)
        {
            return Intersects(tri, aabb);
        }

        public static bool Intersects(Plane pl, AABB aabb)
        {
            Vector3 center = aabb.Center,
                extents = aabb.Max - center;

            var r = extents.x * Mathf.Abs(pl.Normal.x) + extents.y * Mathf.Abs(pl.Normal.y) + extents.z * Mathf.Abs(pl.Normal.z);
            var s = Vector3.Dot(pl.Normal, center) - pl.Distance;

            return Mathf.Abs(s) <= r;
        }

        public static bool Intersects(AABB aabb, Plane pl)
        {
            return Intersects(aabb, pl);
        }

    }

}


