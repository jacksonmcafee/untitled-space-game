using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPhysics : MonoBehaviour
{
    //allow gravity to be activated/deactivated
    //bool gravity_active;

    //gravitational constant, set in editor
    [SerializeField] float g = 1f;
    static float G;

    //we always want our bodies to be affected by gravity
    public static List<Rigidbody2D> bodies = new List<Rigidbody2D>();

    void FixedUpdate()
    {
        //realtime updates to g
        G = g;
        //if(gravity_active)
        //{
            applyGravity();
        //}
    }

    //iterate through the list of all existing bodies and apply gravity to them
    public static void applyGravity()
    {
        for (int i = 0; i < bodies.Count; i++)
        {
            for (int j = i+1; j < bodies.Count; j++)
            {
                gravAttract(bodies[i],bodies[j]);
            }
        }
    }

    //apply a gravitational force between two bodies, accelerating both
    public static void gravAttract(Rigidbody2D M, Rigidbody2D m)
    {
        //position difference as a 2d vector
        Vector3 position_difference = M.position - m.position;
        //Debug.Log("pos_diff: " + position_difference);
        
        //magnitude of that 2d vector is a distance, which we consider our radius
        float distance = position_difference.magnitude;
        //Debug.Log("dist: " + distance);

        //force magnitude defined by Newton; Keep it simple, stupid! 
        //F = G*M*m/r^2
        float topterm = G * M.mass * m.mass;
        //Debug.Log("top: " + G + " " + M.mass + " " + m.mass);
        float bottomterm = distance * distance;
        //Debug.Log("bot: " + bottomterm);
        float magnitude = topterm/bottomterm;
        //Debug.Log("mag: " + magnitude);

        //force direction is 
        Vector3 direction = position_difference.normalized;
        //Debug.Log("dir: " + direction);

        //force vector is simply magnitude * direction
        m.AddForce(magnitude * direction);
        M.AddForce(magnitude * (-direction));   
    }

}
