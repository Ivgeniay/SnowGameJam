namespace FSharp
open UnityEngine

type WheelRotate () =
    inherit MonoBehaviour()
    [<DefaultValue>] val mutable rotateVector: Vector3

    member public this.Update () = this.transform.Rotate(this.rotateVector);
