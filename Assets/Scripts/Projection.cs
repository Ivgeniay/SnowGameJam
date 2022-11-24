using Assets.Scripts.Player;
using Assets.Scripts.Player.Weapon;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projection : MonoBehaviour 
{
    [SerializeField] private LineRenderer line;
    [SerializeField] private int maxPhysicsFrameIterations = 100;
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

    public void SimulateTrajectory(IWeapon ballPrefab, Vector3 pos, Vector3 velocity)
    {
        var ghostObj = Instantiate(ballPrefab.GetPrefab(), pos, Quaternion.identity);
        var ghostScr = ghostObj.GetComponent<IWeapon>();
        SceneManager.MoveGameObjectToScene(ghostObj.gameObject, simulationScene);

        ghostScr.GhostSetup(velocity);

        line.positionCount = maxPhysicsFrameIterations;
        line.ResetBounds();
        line.SetPosition(0, pos);

        for (var i = 0; i < maxPhysicsFrameIterations; i++)
        {
            physicsScene.Simulate(Time.fixedDeltaTime);
            line.SetPosition(i, ghostObj.transform.position);

            if (ghostScr.isCollided) break;
        }
        Destroy(ghostObj.gameObject);
    }

}