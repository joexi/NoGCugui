using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Test : MonoBehaviour {
	public Image Img1;
	public Image Img2;
	// Use this for initialization
	void Start () {
		DispatchManager.Instance.Run (delegate(float value) {
			Debug.LogError("!");
		});
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ButtonOnClick() {
		Img2.CrossFadeAlpha (0, 1f, false);
		AnimationManager.Instance.DoFadeImgTo (Img1, 1f, 0, 1f, 0f, EaseType.None, this);
	}

	public void Button2OnClick() {
		Img2.CrossFadeAlpha (1f, 1f, false);
		AnimationManager.Instance.DoFadeImgTo (Img1, 0, 1f, 1f, 0f, EaseType.None, this);
	}
}
