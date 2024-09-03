using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using Unity.Burst;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.RuleTile.TilingRuleOutput;

// https://people.ece.cornell.edu/land/courses/ece4760/labs/s2021/Boids/Boids.html#:~:text=Boids%20is%20an%20artificial%20life,very%20simple%20set%20of%20rules.

public class Boid 
{
    #region Public Class Members
    public GameObject Boidian { get; private set; }
    public Vector3 Position 
    { 
        get => Boidian.transform.position;
        set
        {
            Boidian.transform.position = value;
        }
    }
    public float PosX { get => Boidian.transform.position.x; }
    public float PosY { get => Boidian.transform.position.y; }
    public float VelX { get; private set; }
    public float VelY { get; private set; }
    #endregion

    #region Private Class Members
    private float Duration { get; set; }
    private float ElapsedTimeX { get; set; }
    private float ElapsedTimeY { get; set; }
    private Vector2 SavedVelocity { get; set; }
    #endregion

    public Boid(GameObject boidian, float minimumSpeed)
    {
        Boidian = boidian;

        Duration = 0;
        ElapsedTimeX = 0;
        ElapsedTimeY = 0;
        SavedVelocity = Vector2.zero;

        VelX = Random.Range(-minimumSpeed, minimumSpeed);
        VelY = Random.Range(-minimumSpeed, minimumSpeed);
    }

    public void CalcMove(List<Boid> boids, BoidData data)
    {
        //Seperation
        float seperationX = 0;
        float seperationY = 0;

        //Alignment
        float xVelAvg = 0;
        float yVelAvg = 0;

        //Cohesion
        float xPosAvg = 0;
        float yPosAvg = 0;

        int neighbors = 0;

        foreach (Boid b in boids)
        {
            if (b.Boidian == Boidian) continue;

            float distance = Vector3.Distance(Position, b.Position);

            // Seperation
            if (distance <= data.ProtectedRange)
            {
                seperationX += PosX - b.PosX;
                seperationY += PosY - b.PosY;
            };

            if (distance > data.VisualRange) continue;

            //Alignment
            xVelAvg += b.VelX;
            yVelAvg += b.VelY;

            //Cohesion
            xPosAvg += b.PosX;
            yPosAvg += b.PosY;
            neighbors++;
        }

        // Seperation
        VelX += seperationX * data.SeperationFactor;
        VelY += seperationY * data.SeperationFactor;

        if (neighbors > 0)
        {
            //Alignment
            xVelAvg /= neighbors;
            yVelAvg /= neighbors;

            VelX += (xVelAvg - VelX) * data.AlignmentFactor;
            VelY += (yVelAvg - VelY) * data.AlignmentFactor;

            //Cohesion
            xPosAvg /= neighbors;
            yPosAvg /= neighbors;

            VelX += (xPosAvg - PosX) * data.CohesionFactor;
            VelY += (yPosAvg - PosY) * data.CohesionFactor;
        }
    }

    public void Move(BoidData data)
    {
        ContainSpeed(data.MinSpeed, data.MaxSpeed);
        AvoidEdges(data.MaxX, data.MaxY);
        FaceTowardHeading();

        Position += new Vector3(VelX, VelY, 0) * Time.deltaTime;
    }

    private void ContainSpeed(float minSpeed, float maxSpeed)
    {
        // Distance between (0,0) and Veloctiy(x, y) (pythagorean theorem)
        float speed = Mathf.Sqrt((VelX * VelX) + (VelY * VelY));

        if (speed > maxSpeed)
        {
            VelX = (VelX / speed) * maxSpeed;
            VelY = (VelY / speed) * maxSpeed;
        }
        if (speed < minSpeed)
        {
            VelX = (VelX / speed) * minSpeed;
            VelY = (VelY / speed) * minSpeed;
        }
    }
    private void AvoidEdges(float maxX, float maxY)
    {
        bool xExceMaxX = Position.x > maxX;
        bool xExceNegMaxX = Position.x < -maxX;
        bool yExceMaxY = Position.y > maxY;
        bool yExceNegMaxY = Position.y < -maxY;

        if (xExceMaxX || xExceNegMaxX)
        {
            ElapsedTimeX += Time.deltaTime;
            VelX = LerpVelocity(SavedVelocity.x, ElapsedTimeX);
        }

        if (yExceMaxY || yExceNegMaxY)
        {
            ElapsedTimeY += Time.deltaTime;
            VelY = LerpVelocity(SavedVelocity.y, ElapsedTimeY);
        }

        if (!xExceMaxX && !xExceNegMaxX && !yExceMaxY && !yExceNegMaxY)
        {
            Duration = Random.Range(.7f, 2f);
            SavedVelocity = new Vector2(VelX, VelY);
            ElapsedTimeX = 0f;
            ElapsedTimeY = 0f;
        }
    }

    private float LerpVelocity(float savedHeading, float elapsedTime)
    {
        float t = Mathf.Clamp01(elapsedTime / Duration);
        return Mathf.Lerp(savedHeading, -savedHeading, t);
    }

    private void FaceTowardHeading()
    {
        float angle = Mathf.Atan2(VelY, VelX) * Mathf.Rad2Deg;

        Boidian.transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }
}