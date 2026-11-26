using UnityEngine;

public class MyVector3
{
    public MyVector3(float _x, float _y, float _z) { x = _x; y = _y; z = _z; }

    public float x; 
    public float y; 
    public float z;

    // Automatically converts MyVector3 -> UnityEngine.Vector3
    public static implicit operator Vector3(MyVector3 v)
    {
        return new Vector3(v.x, v.y, v.z);
    }

    // Automatically converts UnityEngine.Vector3 -> MyVector3
    public static implicit operator MyVector3(Vector3 v)
    {
        return new MyVector3(v.x, v.y, v.z);
    }

    public float Magnitude()
    {
        return  MyMath.Sqrt(x*x + y*y + z*z);
    }

    public MyVector3 Normalize()
    {
        float mag = this.Magnitude();
        return new MyVector3(x / mag, y / mag, z / mag);
    }

    public static MyVector3 operator *(MyVector3 v, float s)
    {
        return new MyVector3(v.x * s, v.y * s, v.z * s);
    }

    public static MyVector3 operator *(float s, MyVector3 v)
    {
        return v * s;
    }


    public static MyVector3 operator +(MyVector3 v, MyVector3 s)
    {
        return new MyVector3(v.x + s.x, v.y + s.y, v.z + s.z);
    }

    

}
