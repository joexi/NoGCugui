# NoGCugui
Avoid the unnessary gc when using ugui. 

For example ugui method CrossFadeAlpha may cause 1.1kb gc each time, that was unacceptable, using AnimationManager will save you from this shit.

## PROVIDE
* set graphic enable & disable without gc
* do animation with gc far less than ugui system & DoTween 
* stop and clear the animation

## HOW TO USE
* move
``` c#
AnimationManager.Instance.DoMoveBy (Img1.transform, new Vector3(0, 100, 0), 1f, 0f, EaseType.None, this);
```

* fade 
``` c#
AnimationManager.Instance.DoFadeImgTo (Img1, 1f, 0, 1f, 0f, EaseType.None, this);
```

* clear
``` c#
AnimationManager.Instance.Clear (this);
```

* dispatch
``` c# 
DispatchManager.Instance.Run (delegate(float value) {
	Debug.LogError("dispatch!");
});
```
