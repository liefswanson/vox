using UnityEngine;
using System.Linq;

public class InvertMesh : MonoBehaviour {
    void Start () {
        var mesh = gameObject.GetComponent<MeshFilter>().mesh;
        mesh.triangles = mesh.triangles.Reverse().ToArray();
	}
}
