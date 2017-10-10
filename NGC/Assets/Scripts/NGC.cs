using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace NGC {
	public static class Active {
		public static void SetActive(this GameObject go, bool active) {
			go.transform.localScale = active ? Vector3.one : Vector3.zero;
		}

		public static void SetActive(this Graphic g, bool active) {
			g.transform.localScale = active ? Vector3.one : Vector3.zero;
		}
	}
}
