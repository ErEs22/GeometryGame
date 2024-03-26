using System;
using Unity.VisualScripting;
using UnityEngine;

public static class EyreUtility{

    /// <summary>
    /// 生成圆环点
    /// </summary>
    /// <param name="pointCount">圆环点数量</param>
    /// <returns></returns>
    public static Vector3[] GenerateCirclePoints(Vector3 center,int pointCount){
        float splitAngle = Mathf.PI * 2 / pointCount;
        Vector3[] points = new Vector3[pointCount];
        for(int i = 0; i < pointCount; i++){
            points[i] = new Vector3(Mathf.Cos(splitAngle * (i + 1)),Mathf.Sin(splitAngle * (i + 1)),0) + center;
        }
        return points;
    }

}