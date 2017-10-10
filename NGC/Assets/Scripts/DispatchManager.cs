using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum EaseType {
	None = 0,
	EaseIn = 1,
	EaseOut = 2
}



public class DispatchManager : MonoBehaviour {
    
	public class DispatchItem
	{
		public float Time;
		public float ValueChangeSpd;
		public float ValueSrc;
		public float ValueDst;
		public bool Once = false;
		public object Key;
		public float Delay = 0;
		public float FloatParam1;
		public float FloatParam2;
		public float FloatParam3;
		public Vector3 VecParam1;
		public Vector3 VecParam2;
		public Vector3 VecParam3;
		public Transform TransParam1;
		public CanvasGroup CanvasParam1;
		public Graphic GraphicParam1;
		public Image ImageParam1;

		public EaseType EaseType;
		public AniamtionSchedulerValueDelegate Callback;
		public AniamtionSchedulerValueParamDelegate ParamCallback;
	}

	public delegate void AniamtionSchedulerValueDelegate(float value);
	public delegate void AniamtionSchedulerValueParamDelegate(float value, DispatchItem item);

    private static DispatchManager instance;
    public static DispatchManager Instance {
        get { 
			if (instance == null) {
				GameObject obj = new GameObject ("Dispatch");
				GameObject.DontDestroyOnLoad (obj);
				instance = obj.AddComponent<DispatchManager> ();
			}
			return instance;
		}
    }

    private List<DispatchItem> dispatchItems = new List<DispatchItem>();
	private List<DispatchItem> dispatchItemPool = new List<DispatchItem>();
    void Awake() {
        instance = this; 
    }

	private DispatchItem popItem() {
		if (dispatchItemPool.Count > 0) {
			DispatchItem item = dispatchItemPool [0];
			dispatchItemPool.RemoveAt (0);
			return item;
		}
		return new DispatchItem();
	}

	private void pushItem(DispatchItem item) {
		item.Callback = null;
		item.ParamCallback = null;
		item.TransParam1 = null;
		item.CanvasParam1 = null;
		item.GraphicParam1 = null;
		dispatchItemPool.Add (item);
	}

	public void Run(float from, float to, float duration, AniamtionSchedulerValueDelegate cb, object key = null, float delay = 0) {
		DispatchItem item = popItem ();
        item.Time = duration;
        item.ValueSrc = from;
        item.ValueDst = to;
        item.ValueChangeSpd = (to - from) / duration;
        item.Callback = cb;
		item.ParamCallback = null;
		item.Key = key;
		item.Once = false;
		item.Delay = delay;
        dispatchItems.Add(item);
    }

	public DispatchItem RunWithParam(float from, float to, float duration, AniamtionSchedulerValueParamDelegate cb, object key = null, float delay = 0) {
		DispatchItem item = popItem ();
		item.Time = duration;
		item.ValueSrc = from;
		item.ValueDst = to;
		item.ValueChangeSpd = (to - from) / duration;
		item.Callback = null;
		item.ParamCallback = cb;
		item.Key = key;
		item.Once = false;
		item.Delay = delay;
		dispatchItems.Add(item);
		return item;
	}

	public void Run(AniamtionSchedulerValueDelegate cb, object key = null) {
		DispatchItem item = popItem();
		item.Time = 0;
		item.ValueSrc = 1;
		item.ValueDst = 1;
		item.ValueChangeSpd = 1;
		item.Callback = cb;
		item.ParamCallback = null;
		item.Key = key;
		item.Once = false;
		item.Delay = 0;
		dispatchItems.Add(item);
	}

	public void RunAfter(AniamtionSchedulerValueDelegate cb, float delay = 0f, object key = null) {
		DispatchItem item = popItem();
		item.Time = delay;
		item.ValueSrc = 1;
		item.ValueDst = 1;
		item.ValueChangeSpd = 1;
		item.Callback = cb;
		item.ParamCallback = null;
		item.Once = true;
		item.Key = key;
		item.Delay = 0;
		dispatchItems.Add(item);
	}


	public void Clear(object key){
		for (int i = 0; i < dispatchItems.Count; i++) {
			DispatchItem item = dispatchItems [i];
			if (item != null && item.Key == key) {
				Debug.LogError ("clear success" + key);
				dispatchItems.RemoveAt (i--);
				pushItem (item);
			}
		}
	}

	public void Finish(object key){
		for (int i = 0; i < dispatchItems.Count; i++) {
			DispatchItem item = dispatchItems [i];
			if (item != null && item.Key == key) {
				dispatchItems.RemoveAt (i--);
				item.Callback (1f);
				pushItem (item);
			}
		}
	}

	public void Clear(){
		dispatchItems.Clear ();
		dispatchItemPool.Clear ();
	}

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (dispatchItems.Count > 0)
        {
            for (int i = 0; i < dispatchItems.Count; i++)
            {
                DispatchItem item = dispatchItems[i];
				if (item.Delay > 0) {
					item.Delay -= Time.deltaTime;
				} else {
					item.Time -= Time.deltaTime;
					item.ValueSrc += item.ValueChangeSpd * Time.deltaTime;
					if (item.Time < 0) {
						if (item.Callback != null) {
							item.Callback (item.ValueDst);
						}
						if (item.ParamCallback != null) {
							item.ParamCallback (item.ValueDst, item);
						}
						if (dispatchItems.Count > i) {
							dispatchItems.RemoveAt (i--);
						}
						pushItem (item);
					} else {
						if (!item.Once) {
							if (item.Callback != null) {
								item.Callback (item.ValueSrc);
							}
							if (item.ParamCallback != null) {
								item.ParamCallback (item.ValueSrc, item);
							}
						}
					}
				}
            }
        }
    }
}
