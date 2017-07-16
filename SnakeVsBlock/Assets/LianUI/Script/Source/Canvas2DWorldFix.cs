
namespace UnityEngine.UI
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(RectTransform))]
    public class Canvas2DWorldFix : MonoBehaviour
    {
        /// <summary>
        /// 参照适配的分辨率
        /// </summary>

        public Vector2 referenceResolution { get { return _referenceResolution; } set { _referenceResolution = value; SizeFix(); } }

        /// <summary>
        /// 长宽适配比
        /// </summary>

        public float match { get { return _match; } set { _match = value; if (_match > 1) _match = 1; if (_match < 0) _match = 0; SizeFix(); } }

        /// <summary>
        /// 进行适配调整
        /// </summary>
        
        public void SizeFix()
        {
            if (referenceResolution.x <= 0 || referenceResolution.y <= 0 || _canvas.renderMode != RenderMode.WorldSpace || _canvas.worldCamera == null) return;
            
            float sAspect = _screenSize.x / _screenSize.y;
            Vector2 widthMax = new Vector2(referenceResolution.x, referenceResolution.x / sAspect);
            Vector2 heightMax = new Vector2(referenceResolution.y * sAspect, referenceResolution.y);

            _rectTransform.sizeDelta = Vector2.Lerp(widthMax, heightMax, match);

            _rectTransform.localScale = Vector3.one;
            float cDistance = _canvas.worldCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f)).y - _canvas.worldCamera.ViewportToWorldPoint(new Vector3(0.5f, 1f)).y;
            float uiDistance = _rectTransform.TransformPoint(new Vector3(0, 0)).y - _rectTransform.TransformPoint(new Vector3(0, _rectTransform.sizeDelta.y / 2)).y;

            if (cDistance != uiDistance)
            {
                float scaleRate = cDistance / uiDistance;
                _rectTransform.localScale = Vector3.one * scaleRate;
            }
        }

        #region 私有部分

        [HideInInspector][SerializeField]private Vector2 _referenceResolution = Vector2.zero;
        [HideInInspector][SerializeField]private float _match;
        private Canvas __canvas;
        private Canvas _canvas { get { if (__canvas == null) __canvas = GetComponent<Canvas>(); return __canvas; } }

        private RectTransform __rectTransform;
        private RectTransform _rectTransform { get { if (__rectTransform == null) __rectTransform = GetComponent<RectTransform>(); return __rectTransform; } }
        
        static System.Reflection.MethodInfo s_GetSizeOfMainGameView;
        private Vector2 _screenSize
        {
            get
            {
#if UNITY_EDITOR
                if (s_GetSizeOfMainGameView == null)
                {
                    System.Type type = System.Type.GetType("UnityEditor.GameView,UnityEditor");
                    s_GetSizeOfMainGameView = type.GetMethod("GetSizeOfMainGameView",
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
                }
                return (Vector2)s_GetSizeOfMainGameView.Invoke(null, null);
#else
                if (!Application.isPlaying)
                {
                    if (s_GetSizeOfMainGameView == null)
                    {
                        System.Type type = System.Type.GetType("UnityEditor.GameView,UnityEditor");
                        s_GetSizeOfMainGameView = type.GetMethod("GetSizeOfMainGameView",
                            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
                    }
                    return (Vector2)s_GetSizeOfMainGameView.Invoke(null, null);
                }
                else
                    return new Vector2(Screen.width, Screen.height);
#endif
            }
        }

        private void Start()
        {
            SizeFix();
        }

#endregion
    }
}