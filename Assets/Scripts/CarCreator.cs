using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof (MeshFilter))]
[RequireComponent(typeof (MeshRenderer))]
public class CarCreator : MonoBehaviour {

    [SerializeField]
    //List<Vector3> v3s;
    List<Vector3> vertices = new List<Vector3>();
    Dictionary<int, List<int>> verticeNumbers = new Dictionary<int, List<int>>();


	public void CreateCar (List<Vector3> v3s) {
        this.GetComponent<Rigidbody>().isKinematic = true;
        verticeNumbers.Clear();
        vertices.Clear();

        Vector3 firstVal = v3s[0];

        int number = 0;
        foreach(var v in v3s)
        {
            vertices.Add(v - firstVal + new Vector3(0, 1, 0));
            vertices.Add(v - firstVal + new Vector3(0, 1, 1));
            vertices.Add(v - firstVal + new Vector3(0, 0, 1));
            vertices.Add(v - firstVal);

            int verticeNumber = 4* number;
            List<int> list = new List<int>() { verticeNumber, verticeNumber + 1, verticeNumber + 2, verticeNumber + 3 };
            verticeNumbers.Add(number, list);
            number++;
        }

       int faceCount =  CalculateNumberOfFaces(v3s);

        List<int> Triangles = new List<int>();

        int pointCount = 0;
        for(int i = 0; i < faceCount; i++)
        {
            if(i == 0)
            {
                Triangles.Add(verticeNumbers[pointCount][0]);
                Triangles.Add(verticeNumbers[pointCount][2]);
                Triangles.Add(verticeNumbers[pointCount][1]);
                Triangles.Add(verticeNumbers[pointCount][0]);
                Triangles.Add(verticeNumbers[pointCount][3]);
                Triangles.Add(verticeNumbers[pointCount][2]);
            }
            else if(i != faceCount-1)
            {
                //calculate top 

                Triangles.Add(verticeNumbers[pointCount - 1][3]);
                Triangles.Add(verticeNumbers[pointCount][2]);
                Triangles.Add(verticeNumbers[pointCount][3]);

                Triangles.Add(verticeNumbers[pointCount - 1][3]);
                Triangles.Add(verticeNumbers[pointCount-1][2]);
                Triangles.Add(verticeNumbers[pointCount][2]);


                //calculate bottom

                Triangles.Add(verticeNumbers[pointCount - 1][0]);
                Triangles.Add(verticeNumbers[pointCount][1]);
                Triangles.Add(verticeNumbers[pointCount - 1][1]);

                Triangles.Add(verticeNumbers[pointCount - 1][0]);
                Triangles.Add(verticeNumbers[pointCount][0]);
                Triangles.Add(verticeNumbers[pointCount][1]);

                //calculate right side

                Triangles.Add(verticeNumbers[pointCount - 1][1]);
                Triangles.Add(verticeNumbers[pointCount][2]);
                Triangles.Add(verticeNumbers[pointCount][1]);

                Triangles.Add(verticeNumbers[pointCount - 1][1]);
                Triangles.Add(verticeNumbers[pointCount - 1][2]);
                Triangles.Add(verticeNumbers[pointCount][2]);

                //calculate left side
                Triangles.Add(verticeNumbers[pointCount][0]);
                Triangles.Add(verticeNumbers[pointCount - 1][3]);
                Triangles.Add(verticeNumbers[pointCount - 1][0]);

                Triangles.Add(verticeNumbers[pointCount][0]);
                Triangles.Add(verticeNumbers[pointCount][3]);
                Triangles.Add(verticeNumbers[pointCount - 1][3]);

                i += 3;
            }
            else
            {
                Triangles.Add(verticeNumbers[pointCount - 1][0]);
                Triangles.Add(verticeNumbers[pointCount - 1][2]);
                Triangles.Add(verticeNumbers[pointCount - 1][1]);

                Triangles.Add(verticeNumbers[pointCount - 1][0]);
                Triangles.Add(verticeNumbers[pointCount - 1][3]);
                Triangles.Add(verticeNumbers[pointCount - 1][2]);
            }
            pointCount++;
        }
			
		Mesh mesh = GetComponent<MeshFilter> ().mesh;
		mesh.Clear ();
		mesh.vertices = vertices.ToArray();
		mesh.triangles = Triangles.ToArray();
		mesh.Optimize ();
		mesh.RecalculateNormals ();
        GetComponent<MeshCollider>().sharedMesh = mesh;

        transform.eulerAngles = new Vector3(this.transform.eulerAngles.x,0,0);

        EventManager.PlaceWheels(vertices[0], vertices[vertices.Count-1],this.gameObject);

        this.GetComponent<Rigidbody>().isKinematic = false;

        GetComponent<Rigidbody>().ResetCenterOfMass();
    }


    int CalculateNumberOfFaces(List<Vector3> v3s)
    {
        int number =  v3s.Count;
        int count = ((number - 1) * 4) + 2;
        return count;
    }

}