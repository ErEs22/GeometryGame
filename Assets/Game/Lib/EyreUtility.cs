using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

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

    /// <summary>
    /// 在矩形范围内生成随机点，排除指定圆内的点(50x50)
    /// </summary>
    /// <param name="circleCenter">排除圆的圆心</param>
    /// <param name="circleRadius">排除圆的半径</param>
    /// <returns>随机点</returns>
    public static Vector2 GenerateRandomPosInRectExcludeCircle(Vector2 circleCenter, float circleRadius)
    {
        Vector2 randomPos = new Vector2(Random.Range(-50, 50), Random.Range(-50, 50));
        while (Vector2.Distance(randomPos, circleCenter) < circleRadius)
        {
            randomPos = new Vector2(Random.Range(-50, 50), Random.Range(-50, 50));
        }
        return randomPos;
    }
    
    /// <summary>
    /// 在矩形范围内生成随机点，排除指定圆内的点(50x50)
    /// </summary>
    /// <param name="circleCenter">排除圆的圆心</param>
    /// <param name="circleRadius">排除圆的半径</param>
    /// <returns>随机点</returns>
    public static Vector2 GenerateRandomPosInRect()
    {
        Vector2 randomPos = new Vector2(Random.Range(-50, 50), Random.Range(-50, 50));
        return randomPos;
    }

    /// <summary>
    /// 以指定点为原点在矩形范围内生成随机点，排除指定圆内的点
    /// </summary>
    /// <param name="relatePos">指定点</param>
    /// <param name="circleCenter">排除圆的圆心</param>
    /// <param name="circleRadius">排除圆的半径</param>
    /// <param name="rectWidth">矩形宽</param>
    /// <param name="rectHeight">矩形高</param>
    /// <returns>随机点</returns>
    public static Vector2 GenerateRandomPosInRectByPosExcludeCircle(Vector2 relatePos, Vector2 circleCenter, float circleRadius, float rectWidth, float rectHeight)
    {
        Vector2 randomPos = new Vector2(Random.Range(-rectWidth / 2, rectWidth / 2), Random.Range(-rectHeight / 2, rectHeight / 2));
        randomPos = randomPos + relatePos;
        while(Vector2.Distance(randomPos,circleCenter) < circleRadius)
        {
            randomPos = new Vector2(Random.Range(-rectWidth / 2, rectWidth / 2), Random.Range(-rectHeight / 2, rectHeight / 2));
            randomPos = randomPos + relatePos;
        }
        randomPos.x = Mathf.Clamp(randomPos.x,-50,50);
        randomPos.y = Mathf.Clamp(randomPos.y,-50,50);
        return randomPos;
    }


}