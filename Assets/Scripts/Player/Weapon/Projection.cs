using Assets.Scripts.Player;
using Assets.Scripts.Player.Weapon;
using Assets.Scripts.Utilities;
using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(LineRenderer))]
public class Projection : SerializedMonoBehaviour
{
    public event Action<int> OnMaxPhysicsFrameIterationsChanged;

    [SerializeField] private LineRenderer line;
    private int _maxPhysicsFrameIterations;
    [OdinSerialize] public int maxPhysicsFrameIterations { 
        get => _maxPhysicsFrameIterations;
        private set
        {
            _maxPhysicsFrameIterations = value;
            OnMaxPhysicsFrameIterationsChanged?.Invoke(value);
        }
    } 
    [SerializeField] private Transform obstaclesParent;


    private PlayerBehavior playerBehavior;
    private Scene simulationScene;
    private PhysicsScene physicsScene;
    private readonly Dictionary<Transform, Transform> _spawnedObjects = new Dictionary<Transform, Transform>();

    private void Awake() {
        if (line is null) line = GetComponent<LineRenderer>();
        if (playerBehavior is null) playerBehavior = GetComponent<PlayerBehavior>();
    }

    private void Start() {
        CreatePhysicsScene();
        if (maxPhysicsFrameIterations == 0) maxPhysicsFrameIterations = 100;
    }

    private void CreatePhysicsScene() {
        simulationScene = SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
        physicsScene = simulationScene.GetPhysicsScene();

        foreach (Transform obj in obstaclesParent) {
            var ghostObj = Instantiate(obj.gameObject, obj.position, obj.rotation);
            ghostObj.GetComponent<Renderer>().enabled = false;
            SceneManager.MoveGameObjectToScene(ghostObj, simulationScene);
            if (!ghostObj.isStatic) _spawnedObjects.Add(obj, ghostObj.transform);
        }
    }

    private void Update() {
        foreach (var item in _spawnedObjects) {
            item.Value.position = item.Key.position;
            item.Value.rotation = item.Key.rotation;
        }
    }
    
    public void EnableLine() {
        line.enabled = true;
    }
    
    public void DisableLine() {
        line.enabled = false;
    }

    public void SimulateTrajectory(IWeapon ballPrefab, Transform pos, Vector3 velocity, CurvatureData curvatureData = null)
    {
        var ghostObj = Instantiate(ballPrefab.GetPrefab(), pos.transform.position, Quaternion.identity);
        var ghostScr = ghostObj.GetComponent<IWeapon>();
        SceneManager.MoveGameObjectToScene(ghostObj.gameObject, simulationScene);

        ghostScr.GhostSetup(velocity, curvatureData);
        

        line.positionCount = maxPhysicsFrameIterations;
        line.ResetBounds();
        line.SetPosition(0, pos.transform.position);

        Rigidbody rb = ghostObj.GetComponent<Rigidbody>();
        for (var i = 0; i < maxPhysicsFrameIterations; i++)
        {
            if (curvatureData is not null) rb.AddForce(curvatureData.GetForce());
            
            physicsScene.Simulate(Time.fixedDeltaTime);
            line.SetPosition(i, ghostObj.transform.position);

            if (ghostScr.isCollided) break;
        }
        Destroy(ghostObj.gameObject);
    }

}