using UnityEngine;

public class CurvatureData
{
    public CurvatureData() { }
    public CurvatureData(Vector3 lForse, Vector3 rForse, float duration)
    {
        this.lForse = lForse;
        this.rForse = rForse;
        this.duration = duration;
    }
    public Vector3 lForse;
    public Vector3 rForse;
    public float duration = 0;

    public Vector3 GetForce() {
        return Vector3.Lerp(lForse, rForse, duration);
    }
    
}


