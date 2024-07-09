using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public static class EyreUtility
{

    public static bool GetChanceResult(float chance)
    {
        return Random.Range(0f,1f) > chance ? true : false;
    }

    public static int Round(float value)
    {
        float intNum = Mathf.Floor(value);
        float fracNum = value - intNum;
        if(fracNum >= 0.5f)
        {
            return (int)intNum + 1;
        }
        else
        {
            return (int)intNum;
        }
    }

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
        Vector2 randomPos = new Vector2(Random.Range(-GlobalVar.mapWidth, GlobalVar.mapHeight), Random.Range(-GlobalVar.mapWidth,GlobalVar.mapHeight));
        while (Vector2.Distance(randomPos, circleCenter) < circleRadius)
        {
            randomPos = new Vector2(Random.Range(-GlobalVar.mapWidth,GlobalVar.mapHeight), Random.Range(-GlobalVar.mapWidth,GlobalVar.mapHeight));
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
        Vector2 randomPos = new Vector2(Random.Range(-GlobalVar.mapWidth,GlobalVar.mapHeight), Random.Range(-GlobalVar.mapWidth,GlobalVar.mapHeight));
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
        randomPos.x = Mathf.Clamp(randomPos.x,-GlobalVar.mapWidth,GlobalVar.mapHeight);
        randomPos.y = Mathf.Clamp(randomPos.y,-GlobalVar.mapWidth,GlobalVar.mapHeight);
        return randomPos;
    }

    public static float Divide(float number,float divideBy)
    {
        return number / divideBy;
    }

    public static float Divide(int number,float divideBy)
    {
        return (float)number / divideBy;
    }

    public static float Divide(int number,int divideBy)
    {
        return (float)number / (float)divideBy;
    }

    public static float Distance2D(Vector2 point1, Vector2 point2)
    {
        float xDis = Mathf.Abs(point1.x - point2.x);
        float yDis = Mathf.Abs(point2.y - point1.y);
        return Mathf.Sqrt(xDis * xDis + yDis * yDis);
    }

    public static float Distance2D(Vector3 point1, Vector3 point2)
    {
        float xDis = point1.x - point2.x;
        float yDis = point2.y - point1.y;
        return Mathf.Sqrt(xDis * xDis + yDis * yDis);
    }

    public static float Distance2DSquare(Vector2 point1,Vector2 point2)
    {
        float xDis = point1.x - point2.x;
        float yDis = point1.y - point2.y;
        return xDis * xDis + yDis * yDis;
    }

    public static float Distance2DSquare(Vector3 point1,Vector3 point2)
    {
        float xDis = point1.x - point2.x;
        float yDis = point1.y - point2.y;
        return xDis * xDis + yDis * yDis;
    }

    public static float Distance3D(Vector3 point1,Vector3 point2)
    {
        float xDis = point1.x - point2.x;
        float yDis = point1.y - point2.y;
        float zDis = point1.z - point2.z;
        return Mathf.Sqrt(xDis * xDis + yDis * yDis + zDis * zDis);
    }

    public static float Distance3DSquare(Vector3 point1,Vector3 point2)
    {
        float xDis = point1.x - point2.x;
        float yDis = point1.y - point2.y;
        float zDis = point1.z - point2.z;
        return xDis * xDis + yDis * yDis + zDis * zDis;
    }

    public static bool DistanceCompare2D(Vector2 point1,Vector2 point2,float targetDistance,eCompareSign compareSign)
    {
        switch (compareSign)
        {
            case eCompareSign.Equals:
                return Distance2DSquare(point1,point2) == targetDistance;
            case eCompareSign.Greater:
                return Distance2DSquare(point1,point2) > targetDistance;
            case eCompareSign.Less:
                return Distance2DSquare(point1,point2) < targetDistance;
            default:
                return true;
        }
    }

    public static bool DistanceCompare2D(float regiondisSquare,float targetDis,eCompareSign compareSign)
    {
        switch(compareSign)
        {
            case eCompareSign.Equals:
                return regiondisSquare == targetDis * targetDis;
            case eCompareSign.Greater:
                return regiondisSquare > targetDis * targetDis;
            case eCompareSign.Less:
                return regiondisSquare < targetDis * targetDis;
            default:
                return true;
        }
    }

    public static int[] GetRandomNumbersInBetween(int min,int max,int numCount)
    {
        int[] result = new int[numCount];
        int[] sampleArr = new int[max - min + 1];
        for(int i = 0; i <= max - min; i++)
        {
            sampleArr[i] = min + i;
        }
        int last = sampleArr.Length - 1;
        for (int i = last; i >= 0; --i)
        {
            // 从当0~当前索引位之间，选择一个数
            int selection = Random.Range(0,i + 1);

            // 索引位对应的数据交换
            int temp = sampleArr[i];
            sampleArr[i] = sampleArr[selection];
            sampleArr[selection] = temp;
        }
        for(int i = 0; i < numCount; i++)
        {
            result[i] = sampleArr[i];
        }
        return result;
    }

}