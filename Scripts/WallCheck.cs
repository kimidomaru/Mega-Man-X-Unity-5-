using UnityEngine;
using System.Collections;

public class WallCheck : MonoBehaviour {

	public static int layerMask = (1 << 8);

	public static bool leftWall(Vector3 position)
	{
		RaycastHit2D hit = Physics2D.Raycast (new Vector2(position.x,position.y), Vector2.left, Mathf.Infinity,layerMask);

		if (hit.collider != null) {
			//Debug.Log (hit.distance);
			if (hit.distance < 0.75f) {
				return true;
			} else {
				return false;
			}
		} else {
			return false;
		}
	}

	public static bool rightWall(Vector3 position)
	{
		RaycastHit2D hit = Physics2D.Raycast (new Vector2(position.x,position.y), Vector2.right, Mathf.Infinity,layerMask);

		if (hit.collider != null) {
			//Debug.Log (hit.distance);
			if (hit.distance < 0.75f) {
				return true;
			} else {
				return false;
			}
		} else {
			return false;
		}
	}
}
