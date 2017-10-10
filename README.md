# NoGCugui
Avoid the unnessary gc when using ugui. 

For example ugui method CrossFadeAlpha may cause 1.1kb gc each time, that was unacceptable, using AnimationManager will save you from this shit.

## PROVIDE
* Set graphic enable & disable without gc
* Do animation with gc far less than ugui system & DoTween 
* Stop and clear the animation

## HOW TO USE
* Move
``` c#
AnimationManager.Instance.DoMoveBy (Img1.transform, new Vector3(0, 100, 0), 1f, 0f, EaseType.None, this);
```

* Fade 
``` c#
AnimationManager.Instance.DoFadeImgTo (Img1, 1f, 0, 1f, 0f, EaseType.None, this);
```

* Clear
``` c#
AnimationManager.Instance.Clear (this);
```

* Dispatch
``` c# 
DispatchManager.Instance.Run (delegate(float value) {
	Debug.LogError("dispatch!");
});
```
