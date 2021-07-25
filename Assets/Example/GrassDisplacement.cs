using UnityEngine;

public class GrassDisplacement : MonoBehaviour
{
	void Update()
	{
		Shader.SetGlobalVector("_PlayerPosition", transform.position);
	}
}
