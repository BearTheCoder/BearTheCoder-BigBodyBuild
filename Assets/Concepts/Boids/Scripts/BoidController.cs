using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidController : MonoBehaviour
{
    #region Serialized Variables
    [SerializeField] GameObject BoidObject;

    [SerializeField] int BoidCount;

    [SerializeField] float VisualRange;
    [SerializeField] float ProtectedRange;
    [SerializeField] float SeperationFactor;
    [SerializeField] float AlignmentFactor;
    [SerializeField] float CohesionFactor;

    [SerializeField] float MinSpeed;
    [SerializeField] float MaxSpeed;
    [SerializeField] float MaxX;
    [SerializeField] float MaxY;
    #endregion

    #region Private Variables
    private List<Boid> Boids;
    #endregion

    private void Start()
    {
        Boids = new List<Boid>();
        SpawnBoids();
        StartCoroutine(CalcMove());
    }

    private void SpawnBoids()
    {
        for (int i = 0; i < BoidCount; i++)
        {
            float randX = Random.Range(-MaxX, MaxX);
            float randY = Random.Range(-MaxY, MaxY);
            GameObject boid = Instantiate(BoidObject, new Vector3(randX, randY, 0), Quaternion.identity);
            Boids.Add(new Boid(boid, MinSpeed));
        }
    }

    private void Update()
    {
        BoidData data = new BoidData()
        {
            VisualRange = VisualRange,
            ProtectedRange = ProtectedRange,
            SeperationFactor = SeperationFactor,
            AlignmentFactor = AlignmentFactor,
            CohesionFactor = CohesionFactor,
            MinSpeed = MinSpeed,
            MaxSpeed = MaxSpeed,
            MaxX = MaxX,
            MaxY = MaxY,
        };

        foreach (Boid b in Boids)
        {
            b.Move(data);
        }
    }

    private IEnumerator CalcMove()
    {
        while (true)
        {
            yield return new WaitForSeconds(.1f);

            BoidData data = new BoidData()
            {
                VisualRange = VisualRange,
                ProtectedRange = ProtectedRange,
                SeperationFactor = SeperationFactor,
                AlignmentFactor = AlignmentFactor,
                CohesionFactor = CohesionFactor,
                MinSpeed = MinSpeed,
                MaxSpeed = MaxSpeed,
                MaxX = MaxX,
                MaxY = MaxY,
            };

            foreach (Boid b in Boids)
            {
                b.CalcMove(Boids, data);
            }
        }
    }
}
