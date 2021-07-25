using UnityEngine;

public class GrassCutter : MonoBehaviour
{
	void Update()
	{
		// send ray at a downwards angle infront of the player
		Vector3 origin = transform.position;
		Vector3 dir = (transform.forward * 2 + transform.up * -1) * 5;

		//Debug.DrawRay(origin, dir, Color.red);

		if (Input.GetKeyDown("space"))
		{
			// fire raycast
			RaycastHit hit;
			if (!Physics.Raycast(origin, dir, out hit))
				return;

			// only continue if it hits our grass plane
			// you could also use a tag for this instead
			if (hit.collider.gameObject.name != "GrassPlane")
				return;

			// bail out if the collider doesn't have a valid mesh
			MeshCollider meshCollider = hit.collider as MeshCollider;
			if (meshCollider == null || meshCollider.sharedMesh == null)
				return;

			// get mesh data
			Mesh mesh = meshCollider.sharedMesh;
			Vector3[] vertices = mesh.vertices;
			int[] triangles = mesh.triangles;

			// save current colours
			Color[] colours = mesh.colors;

			// change vertex colours
			Color newCol = new Color(
				0.3f, // R = scale
				1.0f, // G = tint (inverted)
				0,1); // B and A are unused

			colours[triangles[hit.triangleIndex * 3 + 0]] = newCol;
			colours[triangles[hit.triangleIndex * 3 + 1]] = newCol;
			colours[triangles[hit.triangleIndex * 3 + 2]] = newCol;

			// set new colours
			mesh.SetColors(colours);

			// you could also loop through mesh.vertices to find verts within a certain
			// radius to hit.point instead of colouring all 3 verts on the hit triangle,
			// or just do one vert at a time. SetColors must be used, changing
			// mesh.color[i] manually doesn't seem to work for some reason.
			// colours set during runtime are reset when the game ends.
		}
	}
}
