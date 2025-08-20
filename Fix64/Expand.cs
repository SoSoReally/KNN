using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

/// 重要: FPMatrix4x4  m14,m24,m34 位 坐标position,
/// float4x4 (start 0) m31,m32,m33 是position

namespace FixMath
{
    //unity math to fix math
    public static class FPMathEx
    {
        public static FPVector2 Mul(ref FPVector2 a, ref FPMatrix2x2 fPMatrix2X2)
        {
            return new FPVector2(
                fPMatrix2X2.m11 * a.x + fPMatrix2X2.m21 * a.x,
                fPMatrix2X2.m12 * a.y + fPMatrix2X2.m22 * a.y);
        }
        public static FPVector3 GetTranslation(this ref FPMatrix4x4 fPMatrix4X4)
        {
            return new FPVector3(fPMatrix4X4.m14, fPMatrix4X4.m24, fPMatrix4X4.m34);
        }
        public static FPMatrix4x4 Translation(FPVector3 position)
        {
            var result = FPMatrix4x4.Identity;
            result.m14 = position.x;
            result.m24 = position.y;
            result.m34 = position.z;
            result.m44 = Fix64.One;
            return result;
        }
        public static FPMatrix4x4 Scale(FPVector3 scale)
        {
            var result = FPMatrix4x4.Identity;
            result.m11 = scale.x;
            result.m22 = scale.y;
            result.m33 = scale.z;
            return result;
        }

        public static FPVector4 Mul(FPVector4 fPVector4, FPMatrix4x4 matrix4X4)
        {
            return new FPVector4(
                fPVector4.x * matrix4X4.m11 + fPVector4.y * matrix4X4.m21 + fPVector4.z * matrix4X4.m31 + fPVector4.w * matrix4X4.m41,
                fPVector4.x * matrix4X4.m12 + fPVector4.y * matrix4X4.m22 + fPVector4.z * matrix4X4.m32 + fPVector4.w * matrix4X4.m42,
                fPVector4.x * matrix4X4.m13 + fPVector4.y * matrix4X4.m23 + fPVector4.z * matrix4X4.m33 + fPVector4.w * matrix4X4.m43,
                fPVector4.x * matrix4X4.m14 + fPVector4.y * matrix4X4.m24 + fPVector4.z * matrix4X4.m34 + fPVector4.w * matrix4X4.m44
            );
        }

        public static FPVector4 Mul(FPMatrix4x4 matrix4X4, FPVector4 fPVector4)
        {
            return new FPVector4(
                fPVector4.x * matrix4X4.m11 + fPVector4.y * matrix4X4.m12 + fPVector4.z * matrix4X4.m13 + fPVector4.w * matrix4X4.m14,
                fPVector4.x * matrix4X4.m21 + fPVector4.y * matrix4X4.m22 + fPVector4.z * matrix4X4.m23 + fPVector4.w * matrix4X4.m24,
                fPVector4.x * matrix4X4.m31 + fPVector4.y * matrix4X4.m32 + fPVector4.z * matrix4X4.m33 + fPVector4.w * matrix4X4.m34,
                fPVector4.x * matrix4X4.m41 + fPVector4.y * matrix4X4.m42 + fPVector4.z * matrix4X4.m43 + fPVector4.w * matrix4X4.m44
            );
        }
        public static FPVector3 Mul(FPVector3 fPVector3, FPMatrix4x4 matrix4X4)
        {
            return new FPVector3(
                fPVector3.x * matrix4X4.m11 + fPVector3.y * matrix4X4.m21 + fPVector3.z * matrix4X4.m31 + matrix4X4.m14,
                fPVector3.x * matrix4X4.m12 + fPVector3.y * matrix4X4.m22 + fPVector3.z * matrix4X4.m32 + matrix4X4.m24,
                fPVector3.x * matrix4X4.m13 + fPVector3.y * matrix4X4.m23 + fPVector3.z * matrix4X4.m33 + matrix4X4.m34
            );
        }
        public static FPVector4 C0(this ref FPMatrix4x4 fPMatrix4X4)
        {
            return new FPVector4(fPMatrix4X4.m11, fPMatrix4X4.m21, fPMatrix4X4.m31, fPMatrix4X4.m41);
        }
        public static FPVector4 C1(this ref FPMatrix4x4 fPMatrix4X4)
        {
            return new FPVector4(fPMatrix4X4.m12, fPMatrix4X4.m22, fPMatrix4X4.m32, fPMatrix4X4.m42);
        }
        public static FPVector4 C2(this ref FPMatrix4x4 fPMatrix4X4)
        {
            return new FPVector4(fPMatrix4X4.m13, fPMatrix4X4.m23, fPMatrix4X4.m33, fPMatrix4X4.m43);
        }
        public static FPVector4 C3(this ref FPMatrix4x4 fPMatrix4X4)
        {
            return new FPVector4(fPMatrix4X4.m14, fPMatrix4X4.m24, fPMatrix4X4.m34, fPMatrix4X4.m44);
        }
    }
}

public static class Fix64Extensions
{
    public static float AsFloat(this in Fix64 fix64)
    {
        return (float)fix64;
    }
    public static int AsInt(this in Fix64 fix64)
    {
        return (int)fix64;
    }

    public static FPVector2 ToFPVector2(this in float2 float2)
    {
        return new FPVector2(float2.x, float2.y);
    }

    public static FPVector3 ToFPVector3(this in float3 float3)
    {
        return new FPVector3(float3.x, float3.y, float3.z);
    }

    public static FPVector2 ToFPVector2(this in Vector2 vector2)
    {
        return new FPVector2(vector2.x, vector2.y);
    }
    public static FPVector3 ToFPVector3(this in Vector3 vector3)
    {
        return new FPVector3() { x = (Fix64)vector3.x, y = (Fix64)vector3.y, z = (Fix64)vector3.z };
    }
    public static FPVector2 ToFPVector2(this in Vector3 vector3)
    {
        return new FPVector2() { x = (Fix64)vector3.x, y = (Fix64)vector3.y };
    }
    public static Vector3 ToVector3(this in FPVector3 vector3)
    {
        return new Vector3((float)vector3.x, (float)vector3.y, (float)vector3.z);
    }
    public static FPVector3 Normalizesafe(this in FPVector3 fPVector3)
    {
        if (math.all(fPVector3.bool3()))
        {
            return FPVector3.Zero;
        }
        return FPVector3.Normalize(fPVector3);
    }
    public static bool3 bool3(this in FPVector3 fPVector3)
    {
        return new bool3(fPVector3.x == 0, fPVector3.y == 0, fPVector3.z == 0);
    }
    public static Vector2 ToVector2(this in FPVector2 vector2)
    {
        return new Vector2((float)vector2.x, (float)vector2.y);
    }

    public static FPVector2 ToFPVector2(this in FPVector3 fvector3)
    {
        return new FPVector2(fvector3.x, fvector3.y);
    }

    public static Vector4 ToVector4(this in FPVector4 fvector3)
    {
        return new Vector4((float)fvector3.x, (float)fvector3.y, (float)fvector3.z, (float)fvector3.w);
    }
    /// <summary>
    ///  重要: FPMatrix4x4  m41,m42,m43 位 坐标position,
    /// float4x4 (start 0) m13,m23,m33 是position
    /// </summary>
    /// <param name="fpMatrix4x4"></param>
    /// <param name="tranposePos"></param>
    /// <returns></returns>
    public static float4x4 Tofloat4x4(this in FPMatrix4x4 fpMatrix4x4,bool tranposePos = true)
    {
        if (tranposePos)
        {
            return new float4x4()
            {
                c0 = new float4((float)fpMatrix4x4.m11, (float)fpMatrix4x4.m21, (float)fpMatrix4x4.m31, (float)fpMatrix4x4.m14),
                c1 = new float4((float)fpMatrix4x4.m12, (float)fpMatrix4x4.m22, (float)fpMatrix4x4.m32, (float)fpMatrix4x4.m24),
                c2 = new float4((float)fpMatrix4x4.m13, (float)fpMatrix4x4.m23, (float)fpMatrix4x4.m33, (float)fpMatrix4x4.m34),
                c3 = new float4((float)fpMatrix4x4.m41, (float)fpMatrix4x4.m42, (float)fpMatrix4x4.m43, (float)fpMatrix4x4.m44)
            };
        }
        return new float4x4()
        {
            c0 = new float4((float)fpMatrix4x4.m11, (float)fpMatrix4x4.m21, (float)fpMatrix4x4.m31, (float)fpMatrix4x4.m41),
            c1 = new float4((float)fpMatrix4x4.m12, (float)fpMatrix4x4.m22, (float)fpMatrix4x4.m32, (float)fpMatrix4x4.m42),
            c2 = new float4((float)fpMatrix4x4.m13, (float)fpMatrix4x4.m23, (float)fpMatrix4x4.m33, (float)fpMatrix4x4.m43),
            c3 = new float4((float)fpMatrix4x4.m14, (float)fpMatrix4x4.m24, (float)fpMatrix4x4.m34, (float)fpMatrix4x4.m44)
        };
    }
    public static FPQuaternion ToFPQuaternion(this in Quaternion quaternion)
    {
        return new FPQuaternion(quaternion.x, quaternion.y, quaternion.z, quaternion.w);
    }
    public static Quaternion ToQuaternion(this in FPQuaternion quaternion)
    {
        return new Quaternion((float)quaternion.x, (float)quaternion.y, (float)quaternion.z, (float)quaternion.w);
    }

    public static FPMatrix4x4 ToFPMatrix4x4(this in Matrix4x4 matrix4X4)
    {
        return new FPMatrix4x4(
            (Fix64)matrix4X4.m00, (Fix64)matrix4X4.m01, (Fix64)matrix4X4.m02, (Fix64)matrix4X4.m03,
            (Fix64)matrix4X4.m10, (Fix64)matrix4X4.m11, (Fix64)matrix4X4.m12, (Fix64)matrix4X4.m13,
            (Fix64)matrix4X4.m20, (Fix64)matrix4X4.m21, (Fix64)matrix4X4.m22, (Fix64)matrix4X4.m23,
            (Fix64)matrix4X4.m30, (Fix64)matrix4X4.m31, (Fix64)matrix4X4.m32, (Fix64)matrix4X4.m33
        );
    }

    //public static quaternion ToQuaternion(this in FPQuaternion quaternion)
    //{
    //    return new quaternion((float)quaternion.x, (float)quaternion.y, (float)quaternion.z, (float)quaternion.w);
    //}
}
