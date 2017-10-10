using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnimationManager {
	public static AnimationManager Instance = new AnimationManager();

	public void Clear(object trans) {
		DispatchManager.Instance.Clear (trans);
	}

	public void DoFadeTo(Graphic graphic, float from, float to, float duration, float delay = 0f, EaseType easyType = EaseType.None, object key = null) {
		graphic.CrossFadeAlpha (from, 0, false);
		DispatchManager.DispatchItem result = DispatchManager.Instance.RunWithParam (0f, 1f, 0f, delegate(float value, DispatchManager.DispatchItem item) {
			item.GraphicParam1.CrossFadeAlpha (item.FloatParam2, item.FloatParam1, false);
		}, key ?? graphic, delay);

		result.FloatParam1 = duration;
		result.FloatParam2 = to;
		result.GraphicParam1 = graphic;
		result.EaseType = easyType;
	}

	public void DoFadeImgTo(Image img, float from, float to, float duration, float delay = 0f, EaseType easyType = EaseType.None, object key = null) {
		float dealta = (to - from);
		Color c = img.color;
		c.a = from;
		img.color = c;
		DispatchManager.DispatchItem result = DispatchManager.Instance.RunWithParam (0f, duration, duration, delegate(float value, DispatchManager.DispatchItem item) {
			if (value > item.FloatParam1) {
				value = item.FloatParam1;
			}
			float alpha = 0;
			if (item.EaseType == EaseType.EaseIn) {
				alpha = QuadEaseIn(value, item.FloatParam2, item.FloatParam3, item.FloatParam1);
			}
			else if (item.EaseType == EaseType.EaseOut) {
				alpha = QuadEaseOut(value, item.FloatParam2, item.FloatParam3, item.FloatParam1);
			}
			else {
				alpha = Linear(value, item.FloatParam2, item.FloatParam3, item.FloatParam1);
			}
			Color col = item.ImageParam1.color;
			col.a = alpha;
			item.ImageParam1.color = col;
		}, key ?? img, delay);

		result.FloatParam1 = duration;
		result.FloatParam2 = from;
		result.FloatParam3 = dealta;
		result.ImageParam1 = img;
		result.EaseType = easyType;

	}

	public void DoFadeTo(CanvasGroup group, float from, float to, float duration, float delay = 0f, EaseType easyType = EaseType.None, object key = null) {
		float dealta = (to - from);
		group.alpha = from;
		DispatchManager.DispatchItem result = DispatchManager.Instance.RunWithParam (0f, duration, duration, delegate(float value, DispatchManager.DispatchItem item) {
			if (value > item.FloatParam1) {
				value = item.FloatParam1;
			}
			float alpha = 0;
			if (item.EaseType == EaseType.EaseIn) {
				alpha = QuadEaseIn(value, item.FloatParam2, item.FloatParam3, item.FloatParam1);
			}
			else if (item.EaseType == EaseType.EaseOut) {
				alpha = QuadEaseOut(value, item.FloatParam2, item.FloatParam3, item.FloatParam1);
			}
			else {
				alpha = Linear(value, item.FloatParam2, item.FloatParam3, item.FloatParam1);
			}

			item.CanvasParam1.alpha = alpha;
		}, key ?? group, delay);

		result.FloatParam1 = duration;
		result.FloatParam2 = from;
		result.FloatParam3 = dealta;
		result.CanvasParam1 = group;
		result.EaseType = easyType;

	}

	public void DoLocalScaleTo(Transform trans, Vector3 from, Vector3 to, float duration, float delay = 0f, EaseType easyType = EaseType.None, object key = null) {
		Vector3 dealta = (to - from);

		DispatchManager.DispatchItem result = DispatchManager.Instance.RunWithParam (0f, duration, duration, delegate(float value, DispatchManager.DispatchItem item) {
			if (value > item.FloatParam1) {
				value = item.FloatParam1;
			}
			Vector3 scale = Vector3.zero;
			if (item.EaseType == EaseType.EaseIn) {
				scale = QuadEaseIn(value, item.VecParam1, item.VecParam2, item.FloatParam1);
			}
			else if (item.EaseType == EaseType.EaseOut) {
				scale = QuadEaseOut(value, item.VecParam1, item.VecParam2, item.FloatParam1);
			}
			else {
				scale = Linear(value, item.VecParam1, item.VecParam2, item.FloatParam1);
			}

			item.TransParam1.localScale = scale;
		}, key ?? trans, delay);

		result.FloatParam1 = duration;
		result.VecParam1 = from;
		result.VecParam2 = dealta;
		result.TransParam1 = trans;
		result.EaseType = easyType;
	}

	public void DoMoveTo(Transform trans, Vector3 from, Vector3 to, float duration, float delay = 0f, EaseType easyType = EaseType.None, object key = null) {
		Vector3 dealta = (to - from);

		DispatchManager.DispatchItem result = DispatchManager.Instance.RunWithParam (0f, duration, duration, delegate(float value, DispatchManager.DispatchItem item) {
			if (value > item.FloatParam1) {
				value = item.FloatParam1;
			}
			if (item.VecParam3 == Vector3.zero) {
				item.VecParam3 = item.VecParam1;
			}
			Vector3 position = Vector3.zero;
			if (item.EaseType == EaseType.EaseIn) {
				position = QuadEaseIn(value, item.VecParam1, item.VecParam2, item.FloatParam1);
			}
			else if (item.EaseType == EaseType.EaseOut) {
				position = QuadEaseOut(value, item.VecParam1, item.VecParam2, item.FloatParam1);
			}
			else {
				position = Linear(value, item.VecParam1, item.VecParam2, item.FloatParam1);
			}

			item.TransParam1.position += (position - item.VecParam3);
			item.VecParam3 = position;
		}, key ?? trans, delay);

		result.FloatParam1 = duration;
		result.VecParam1 = from;
		result.VecParam2 = dealta;
		result.VecParam3 = Vector3.zero;
		result.TransParam1 = trans;
		result.EaseType = easyType;
	}

	public void DoLocalMoveXTo(Transform trans, float x, float duration, float delay = 0f, EaseType easyType = EaseType.None, object key = null) {
		Vector3 from = trans.localPosition;
		Vector3 to = from;
		to.x = x;
		DoLocalMoveTo (trans, from, to, duration, delay, easyType, key);
	}

	public void DoLocalMoveYTo(Transform trans, float y, float duration, float delay = 0f, EaseType easyType = EaseType.None, object key = null) {
		Vector3 from = trans.localPosition;
		Vector3 to = from;
		to.y = y;
		DoLocalMoveTo (trans, from, to, duration, delay, easyType, key);
	}

	public void DoLocalMoveZTo(Transform trans, float z, float duration, float delay = 0f, EaseType easyType = EaseType.None, object key = null) {
		Vector3 from = trans.localPosition;
		Vector3 to = from;
		to.z = z;
		DoLocalMoveTo (trans, from, to, duration, delay, easyType, key);
	}

	public void DoLocalMoveTo(Transform trans, Vector3 from, Vector3 to, float duration, float delay = 0f, EaseType easyType = EaseType.None, object key = null) {
		Vector3 dealta = (to - from);

		DispatchManager.DispatchItem result = DispatchManager.Instance.RunWithParam (0f, duration, duration, delegate(float value, DispatchManager.DispatchItem item) {
			if (value > item.FloatParam1) {
				value = item.FloatParam1;
			}
			if (item.VecParam3 == Vector3.zero) {
				item.VecParam3 = item.VecParam1;
			}
			Vector3 position = Vector3.zero;
			if (item.EaseType == EaseType.EaseIn) {
				position = QuadEaseIn(value, item.VecParam1, item.VecParam2, item.FloatParam1);
			}
			else if (item.EaseType == EaseType.EaseOut) {
				position = QuadEaseOut(value, item.VecParam1, item.VecParam2, item.FloatParam1);
			}
			else {
				position = Linear(value, item.VecParam1, item.VecParam2, item.FloatParam1);
			}

			item.TransParam1.localPosition += (position - item.VecParam3);
			item.VecParam3 = position;
		}, key ?? trans, delay);

		result.FloatParam1 = duration;
		result.VecParam1 = from;
		result.VecParam2 = dealta;
		result.VecParam3 = Vector3.zero;
		result.TransParam1 = trans;
		result.EaseType = easyType;
	}

//	public void DoLocalMoveXTo(Transform trans, Vector3 from, Vector3 to, float duration, float delay = 0f, EaseType easyType = EaseType.None, object key = null) {
//		Vector3 dealta = (to - from);
//
//		DispatchManager.DispatchItem result = DispatchManager.Instance.RunWithParam (0f, duration, duration, delegate(float value, DispatchManager.DispatchItem item) {
//			if (value > item.FloatParam1) {
//				value = item.FloatParam1;
//			}
//			if (item.VecParam3 == Vector3.zero) {
//				item.VecParam3 = item.VecParam1;
//			}
//			Vector3 position = Vector3.zero;
//			if (item.EaseType == EaseType.EaseIn) {
//				position = QuadEaseIn(value, item.VecParam1, item.VecParam2, item.FloatParam1);
//			}
//			else if (item.EaseType == EaseType.EaseOut) {
//				position = QuadEaseOut(value, item.VecParam1, item.VecParam2, item.FloatParam1);
//			}
//			else {
//				position = Linear(value, item.VecParam1, item.VecParam2, item.FloatParam1);
//			}
//
//			item.TransParam1.localPosition += (position - item.VecParam3);
//			item.VecParam3 = position;
//		}, key ?? trans, delay);
//
//		result.FloatParam1 = duration;
//		result.VecParam1 = from;
//		result.VecParam2 = dealta;
//		result.VecParam3 = Vector3.zero;
//		result.TransParam1 = trans;
//		result.EaseType = easyType;
//	}

	public void DoMoveBy(Transform trans, Vector3 delta, float duration, float delay = 0f, EaseType easyType = EaseType.None, object key = null) {

		DispatchManager.DispatchItem result = DispatchManager.Instance.RunWithParam (0f, duration, duration, delegate(float value, DispatchManager.DispatchItem item) {
			if (value > item.FloatParam1) {
				value = item.FloatParam1;
			}
			if (item.VecParam1 == Vector3.zero) {
				item.VecParam1 = item.TransParam1.position;
				item.VecParam3 = item.TransParam1.position;
			}

			Vector3 position = Vector3.zero;
			if (item.EaseType == EaseType.EaseIn) {
				position = QuadEaseIn(value, item.VecParam1, item.VecParam2, item.FloatParam1);
			}
			else if (item.EaseType == EaseType.EaseOut) {
				position = QuadEaseOut(value, item.VecParam1, item.VecParam2, item.FloatParam1);
			}
			else {
				position = Linear(value, item.VecParam1, item.VecParam2, item.FloatParam1);
			}

			item.TransParam1.position += (position - item.VecParam3);
			item.VecParam3 = position;
		}, key ?? trans, delay);

		result.FloatParam1 = duration;
		result.VecParam1 = Vector3.zero;
		result.VecParam2 = delta;
		result.VecParam3 = Vector3.zero;
		result.TransParam1 = trans;
		result.EaseType = easyType;

	}

	public void DoLocalRotateZ(Transform trans, float z, float duration, float delay = 0f, EaseType easyType = EaseType.None, object key = null) {
		Vector3 from = trans.localEulerAngles;
		Vector3 to = from;
		to.z = z;
		DoLocalRotateTo (trans, from, to, duration, delay, easyType, key);
	}

	public void DoLocalRotateTo(Transform trans, Vector3 from, Vector3 to, float duration, float delay = 0f, EaseType easyType = EaseType.None, object key = null) {
		Vector3 dealta = (to - from);

		DispatchManager.DispatchItem result = DispatchManager.Instance.RunWithParam (0f, duration, duration, delegate(float value, DispatchManager.DispatchItem item) {
			if (value > item.FloatParam1) {
				value = item.FloatParam1;
			}
			if (item.VecParam3 == Vector3.zero) {
				item.VecParam3 = item.VecParam1;
			}
			Vector3 rotate = Vector3.zero;
			if (item.EaseType == EaseType.EaseIn) {
				rotate = QuadEaseIn(value, item.VecParam1, item.VecParam2, item.FloatParam1);
			}
			else if (item.EaseType == EaseType.EaseOut) {
				rotate = QuadEaseOut(value, item.VecParam1, item.VecParam2, item.FloatParam1);
			}
			else {
				rotate = Linear(value, item.VecParam1, item.VecParam2, item.FloatParam1);
			}

			item.TransParam1.localEulerAngles += (rotate - item.VecParam3);
			item.VecParam3 = rotate;
		}, key ?? trans, delay);

		result.FloatParam1 = duration;
		result.VecParam1 = from;
		result.VecParam2 = dealta;
		result.VecParam3 = Vector3.zero;
		result.TransParam1 = trans;
		result.EaseType = easyType;
	}

	public static Vector3 QuadEaseOut(float t, Vector3 b, Vector3 c, float d)  
	{  
		return -c * (t /= d) * (t - 2) + b;  
	}  

	public static Vector3 QuadEaseIn(float t, Vector3 b, Vector3 c, float d)  
	{  
		return c * (t /= d) * t + b;  
	}

	public static Vector3 Linear(float t, Vector3 b, Vector3 c, float d)  
	{  
		return c * t / d + b;  
	}  

	public static float QuadEaseOut(float t, float b, float c, float d)  
	{  
		return -c * (t /= d) * (t - 2) + b;  
	}  

	public static float QuadEaseIn(float t, float b, float c, float d)  
	{  
		return c * (t /= d) * t + b;  
	}

	public static float Linear(float t, float b, float c, float d)  
	{  
		return c * t / d + b;  
	}  

//	public static Vector3 ExpoEaseOut(float t, Vector3 b, Vector3 c, float d)  
//	{  
//		return (t == d) ? b + c : c * (-Mathf.Pow(2, -10 * t / d) + 1) + b;  
//	}  
//
//	public static Vector3 ExpoEaseIn(float t, Vector3 b, Vector3 c, float d)  
//	{  
//		return (t == 0) ? b : c * Mathf.Pow(2, 10 * (t / d - 1)) + b;  
//	}  

}
