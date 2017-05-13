using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Physics2DExtension {

	// Use this for initialization

    public static void AddForce(this Rigidbody2D rigidbody2D, Vector2 force, ForceMode mode = ForceMode.Force)
    {
        switch (mode)
        {
            case ForceMode.Force:
                rigidbody2D.AddForce(force);
                break;
            case ForceMode.Impulse:
                rigidbody2D.AddForce(force / Time.fixedDeltaTime);
                break;
            case ForceMode.Acceleration:
                rigidbody2D.AddForce(force * rigidbody2D.mass);
                break;
            case ForceMode.VelocityChange:
                rigidbody2D.AddRelativeForce(force * rigidbody2D.mass / Time.fixedDeltaTime);
                break;
        }
    }

    public static void AddForce(this Rigidbody2D rigidbody2D, float x, float y, ForceMode mode = ForceMode.Force)
    {
       AddForce(rigidbody2D, new Vector2(x, y), mode);
    }

}