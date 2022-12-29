using Assets.Scripts.Game;
using Assets.Scripts.Minimap;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Map : SerializedMonoBehaviour, IRestartable
{
    private event Action<bool> OnIcon;
    private event Action<bool> OnOrtographic;
    private event Action<float> OnIconScale;
    private event Action<float> OnFar;

    [SerializeField] private Transform followPoint;
    [SerializeField] private LayerMask viewLayerMask;
    [SerializeField] private List<Transform> icons;
    [SerializeField] private bool rotateFollow;

    private float _offsetY;
    [OdinSerialize][HideIf("isOrthographic", true)] private float offsetY;


    private bool _isIcon;
    [OdinSerialize] private bool isIcons {
        get => _isIcon;
        set {
            _isIcon = value;
            OnIcon?.Invoke(value);
        }
    }

    private float _iconScale;
    [SerializeField][ShowIf("_isIcon", true)]private float iconScale
    {
        get => _iconScale;
        set
        {
            _iconScale = value;
            OnIconScale?.Invoke(value);
        }
    }

    private bool _isOrthographic;
    [OdinSerialize] private bool isOrthographic
    {
        get => _isOrthographic;
        set
        {
            _isOrthographic = value;
            OnOrtographic?.Invoke(value);
        } 
    }

    private float _far;
    [OdinSerialize][ShowIf("isOrthographic", true)] private float far
    {
        get => _far;
        set {
            _far = value;
            OnFar?.Invoke(value);
        }
    }
    private Camera RenderCamera;

    private int cullingMask;

    private void Awake()
    {
        RenderCamera = GetComponent<Camera>();
        cullingMask = RenderCamera.cullingMask;

        OnFar += OnFarHandler;
        OnIcon += OnIconHandler;
        OnOrtographic += OnOrtographicHandler;
        OnIconScale += OnIconScaleHandler;

        Game.Manager.OnInitialized += OnGameInitializedHandler;
        Game.Manager.OnNpcInstantiate += OnNpcInstantiateHandler;
        Game.Manager.OnNpcDied += OnNpcDiedHandler;
    }
    void Start() {
        ValidateCamera();
    }
    private void ValidateCamera()
    {
        var arr = GameObject.FindObjectsOfType<IconMinimap>();
        arr.ForEach(el => icons.Add(el.GetComponent<Transform>()));

        if (isIcons == true) {
            RenderCamera.cullingMask = viewLayerMask;
            icons.ForEach(el => el.gameObject.SetActive(true));
            icons.ForEach(el => el.localScale = new Vector3(iconScale, iconScale, el.localScale.z));
        }
        else {
            RenderCamera.cullingMask = cullingMask;
            icons.ForEach(el => el.gameObject.SetActive(false));
        }

        if (isOrthographic) {
            RenderCamera.orthographic = true;
            RenderCamera.orthographicSize = far;
        }
        else {
            RenderCamera.orthographic = false;
        }
    }

    void LateUpdate()
    {
        Vector3 newPosition = new Vector3 (followPoint.position.x, offsetY, followPoint.transform.position.z);
        transform.position = newPosition;

        if(rotateFollow) {
            RotateIcons(icons);
            transform.rotation = Quaternion.Euler(90f, followPoint.eulerAngles.y, 0f);
        }
    }
    private void RotateIcons(List<Transform> transforms)
    {
        foreach (Transform t in transforms) {
            if (t.gameObject.IsDestroyed()) transforms.Remove(t);
            t.rotation = Quaternion.Euler(90f, followPoint.eulerAngles.y, 0f);
        }
    }

    private void OnIconHandler(bool obj) {
        if (obj) {
            icons.ForEach(el => el.gameObject.SetActive(true));
            RenderCamera.cullingMask = viewLayerMask;
        }
        else {
            icons.ForEach(el => el.gameObject.SetActive(false));
            RenderCamera.cullingMask = cullingMask;
        }
    }
    private void OnOrtographicHandler(bool obj) {
        RenderCamera.orthographic = obj;
    }

    private void OnFarHandler(float obj) {
        if (RenderCamera.orthographic)
            RenderCamera.orthographicSize = obj;
    }
    private void OnIconScaleHandler(float obj) {
        icons.ForEach(el => el.localScale = new Vector3(obj, obj, 1));
    }

    private void OnGameInitializedHandler() {
        Game.Manager.Restart.Register(this);
    }

    private void OnNpcDiedHandler(object sender, OnNpcDieEventArg e)
    {
        var icon = e.UnitBehavior.GetComponentInChildren<IconMinimap>();
        if (icon is not null)
            icons.Remove(icon.transform);
    }

    private void OnNpcInstantiateHandler(object sender, OnNpcInstantiateEventArg e)
    {
        var icon = e.UnitBehavior.GetComponentInChildren<IconMinimap>();
        if (icon is not null) {
            icon.transform.localScale = new Vector3(iconScale, iconScale, 1);
            icons.Add(icon.transform);
        }
    }

    public void Restart() {
        icons.Clear();
        StartCoroutine(DelayedValidation());
    }

    private IEnumerator DelayedValidation()
    {
        yield return null;
        ValidateCamera();
    }
}
