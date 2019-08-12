using UnityEngine;
using System.Collections;

public class VerificaChao : MonoBehaviour {

	public static int layerMask = (1 << 8);


	public static bool checkGround(Vector3 position)
	{
		RaycastHit2D hit = Physics2D.Raycast (new Vector2(position.x,position.y), Vector2.down, Mathf.Infinity,layerMask);

		if (hit.collider != null) {
			if (hit.distance < 0.8f) {
				return true;
			} else {
				return false;
			}
		} else {
			return false;
		}
	}
}
