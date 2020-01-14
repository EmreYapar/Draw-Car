using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DrawLine : MonoBehaviour
{
    public GameObject linePrefab;
    public GameObject currentLine;
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    public List<Vector2> fingerPositions;

    public List<Vector3> positions;

    [SerializeField]
    GameObject obj;

    [SerializeField]
    CarCreator deneme;

    [SerializeField]
    CarMover carMover;
    // Use this for initialization
    void Start()
    {
    }
    // Update is called once per frame


    public void OnFingerDown(Vector3 v3)
    {
        carMover.Levitate();
        positions.Clear();
        fingerPositions.Clear();
        CreateLine(v3);
    }

    public void Draw(Vector3 v3)
    {
       // if (positions.Count >= 2 && Vector2.Distance(positions[positions.Count - 1], positions[positions.Count - 2]) <= .01f)
         //   return;
        positions.Add(v3);
        UpdateLine(v3);
    }

   public void OnFingerUp()
    {
        Create();
        lineRenderer.positionCount = 0;
        carMover.StopLevitate();
    }

    void Create()
    {
       // foreach (var b in positions)
       //     Instantiate(obj, b,obj.transform.rotation);
        //for (int i = 0; i < lineRenderer.positionCount; i++)
        //Instantiate(obj, lineRenderer.GetPosition(i), obj.transform.rotation);
        deneme.CreateCar(positions);
    }

    void CreateLine(Vector3 v3)
    {
        currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        edgeCollider = currentLine.GetComponent<EdgeCollider2D>();
        fingerPositions.Clear();
        fingerPositions.Add(v3);
        fingerPositions.Add(v3);
        lineRenderer.SetPosition(0, fingerPositions[0]);
        lineRenderer.SetPosition(1, fingerPositions[1]);
        edgeCollider.points = fingerPositions.ToArray();
    }
    void UpdateLine(Vector2 newFingerPos)
    {
        fingerPositions.Add(newFingerPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPos);
        edgeCollider.points = fingerPositions.ToArray();
    }
}