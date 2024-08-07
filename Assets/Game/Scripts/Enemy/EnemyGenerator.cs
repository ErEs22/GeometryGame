using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemy;
    public GameObject specificEnemy;
    float thresholdDisToPlayer = 5f;
    private GameObject point;
    EnemyManager enemyManager;
    Vector2 playerPos;

    private void Awake() {
        enemyManager = GetComponent<EnemyManager>();
        playerPos = GlobalVar.playerTrans.position;
    }

    private void OnEnable()
    {
        GenerateEnemysAroundPoint(specificEnemy,5,Vector3.zero);
    }

    /// <summary>
    /// 获取距离玩家有一定距离的安全位置
    /// </summary>
    /// <returns></returns>
    Vector2 GetPosAwayFromPlayer()
    {
        float playerLeftSafeDis = playerPos.x - thresholdDisToPlayer + GameConstant.gameActiveRect.x;
        float playerRightSaveDis = GameConstant.gameActiveRect.x - playerPos.x + thresholdDisToPlayer;
        float playerUpSaveDis = GameConstant.gameActiveRect.y - playerPos.y + thresholdDisToPlayer;
        float playerDownSaveDis = playerPos.y - thresholdDisToPlayer + GameConstant.gameActiveRect.y;
        float rangeX, rangeY;
        if (playerLeftSafeDis > playerRightSaveDis)
        {
            rangeX = Random.Range(-GameConstant.gameActiveRect.x, playerPos.x - thresholdDisToPlayer);
        }
        else
        {
            rangeX = Random.Range(playerPos.x + thresholdDisToPlayer, GameConstant.gameActiveRect.x);
        }
        if (playerUpSaveDis > playerDownSaveDis)
        {
            rangeY = Random.Range(playerPos.y + thresholdDisToPlayer, GameConstant.gameActiveRect.y);
        }
        else
        {
            rangeY = Random.Range(-GameConstant.gameActiveRect.y, playerPos.x - thresholdDisToPlayer);
        }
        return new Vector2(rangeX, rangeY);
    }

#if false
    /// <summary>
    /// 在指定矩形内生成随机二维位置，排除指定圆内的点
    /// </summary>
    /// <param name="circleCenter">排除圆内点圆的圆心</param>
    /// <param name="circleRadius">排除圆内点圆的半径</param>
    /// <returns></returns>
    Vector2 EyreUtility.GenerateRandomPosInRectExcludeCircle(Vector2 circleCenter, float circleRadius)
    {
        //获取随机角度（以弧度表示）
        float randomAngle = Random.Range(0f, 359.9f) * Mathf.Deg2Rad;
        float minRadius = circleRadius;
        List<RadDis> disList = new List<RadDis>();
        //计算圆心到从圆心随机角度射出的直线与x = 50的交点的距离
        float xBoundPositiveDis = (50 - circleCenter.x) / Mathf.Cos(randomAngle);
        disList.Add(new RadDis(xBoundPositiveDis,xBoundPositiveDis > 0 ? 0 : 1));
        // float xBoundPositiveYPos = xBoundPositiveDis * Mathf.Sin(randomAngle);
        // Vector2 xBoundNegativeCrossPos = new Vector2(50, xBoundPositiveYPos);

        //计算圆心到从圆心随机角度射出的直线与y = 50的交点的距离
        float yBoundPositiveDis = (50 - circleCenter.y) / Mathf.Sin(randomAngle);
        disList.Add(new RadDis(yBoundPositiveDis,yBoundPositiveDis > 0 ? 0 : 1));
        // float yBoundPositiveXPos = yBoundPositiveDis * Mathf.Cos(randomAngle);
        // Vector2 yBoundPositiveCrossPos = new Vector2(yBoundPositiveXPos, 50);

        //计算圆心到从圆心随机角度射出的直线与x = -50的交的距离点
        float xBoundNegativeDis = (-50 - circleCenter.x) / Mathf.Cos(randomAngle);
        disList.Add(new RadDis(xBoundNegativeDis,xBoundNegativeDis > 0 ? 0 : 1));
        // float xBoundNegativeYPos = xBoundNegativeDis * Mathf.Sin(randomAngle);
        // Vector2 xBoundPositiveCrossPos = new Vector2(-50, xBoundNegativeYPos);

        //计算圆心到从圆心随机角度射出的直线与y = -50的交的距离点
        float yBoundNegativeDis = (-50 - circleCenter.y) / Mathf.Sin(randomAngle);
        disList.Add(new RadDis(yBoundNegativeDis,yBoundNegativeDis > 0 ? 0 : 1));
        // float yBoundNegativeXPos = yBoundNegativeDis * Mathf.Cos(randomAngle);
        // Vector2 yBoundNegativeCrossPos = new Vector2(yBoundNegativeXPos, -50);

        //将距离进行排序，从小到大，第二三位为当前角度直线与矩形边框相交点的有效距离，正值代表正向，负值代表反向
        disList.Sort(delegate(RadDis x,RadDis y){
            return x.radius.CompareTo(y.radius);
        });
        foreach (var dis in disList )
        {
            print(dis.radius);
        }
        float negativeRadius = Mathf.Clamp(Mathf.Abs(disList[1].radius) - minRadius,0,float.MaxValue);
        float positiveRadius = Mathf.Clamp(disList[2].radius - minRadius,0,float.MaxValue) + negativeRadius;
        float chance = negativeRadius / positiveRadius;
        print(chance);
        //随机取正向或反向的距离范围
        if (Random.Range(0f, 1f) < chance)
        {
            float randomRadius = Random.Range(minRadius,Mathf.Abs(disList[1].radius));
            float angle = randomAngle + Mathf.PI;
            return new Vector2(randomRadius * Mathf.Cos(angle) + circleCenter.x,randomRadius * Mathf.Sin(angle) + circleCenter.y);
        }
        else
        {
            float randomRadius = Random.Range(minRadius,Mathf.Abs(disList[2].radius));
            float angle = randomAngle;
            return new Vector2(randomRadius * Mathf.Cos(angle) + circleCenter.x,randomRadius * Mathf.Sin(angle) + circleCenter.y);
        }
    }
#endif

    void SetCrossPoint()
    {
        Vector2 relatePos = EyreUtility.GenerateRandomPosInRectExcludeCircle(new Vector2(40, 40), 20);
        for (int i = 0; i < 100; i++)
        {
            Transform point = Instantiate(this.point, transform).transform;
            point.position = EyreUtility.GenerateRandomPosInRectByPosExcludeCircle(relatePos,new Vector2(40,40),20,30,20);
        }
    }

    void GenerateEnemysInRandomPos(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Enemy newEnemy = PoolManager.Release(enemy, EyreUtility.GenerateRandomPosInRectExcludeCircle(playerPos,5)).GetComponent<Enemy>();
            enemyManager.enemies.Add(newEnemy);
            newEnemy.Init(enemyManager);
        }
    }

    public void GenerateEnemysInRandomPos(GameObject enemy,int count)
    {
        for (int i = 0; i < count; i++)
        {
            GenerateEnemy(enemy,EyreUtility.GenerateRandomPosInRectExcludeCircle(playerPos,5));
        }
    }
    
    public void GenerateEnemysAroundRandomPoint(GameObject enemy, int count)
    {
        // float radius = Mathf.Ceil(count / 6f) + 1;
        Vector2 randomCenterPos = EyreUtility.GenerateRandomPosInRectExcludeCircle(playerPos,5);
        for (int i = 0; i < count; i++)
        {
            Vector2 randomPos = EyreUtility.GenerateRandomPosInRectByPosExcludeCircle(randomCenterPos,playerPos,5,10,10);
            GenerateEnemy(enemy,randomPos);
        }
    }

    public void GenerateEnemysAroundPoint(GameObject enemy, int count,Vector3 point)
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 randomPos = EyreUtility.GenerateRandomPosInRectByPosExcludeCircle(point,playerPos,5,15,15);
            GenerateEnemy(enemy,randomPos);
        }
    }

    public void GenerateEnemy(GameObject enemy,Vector2 pos)
    {
        //关卡结束后停止生成敌人
        // if(LevelManager.levelStatus == eLevelStatus.Ended ||
        // GlobalVar.gameStatus == eGameStatus.Ended ||
        // GlobalVar.gameStatus == eGameStatus.MainMenu) return;

        Enemy newEnemy = PoolManager.Release(enemy, pos).GetComponent<Enemy>();
        enemyManager.enemies.Add(newEnemy);
        newEnemy.Init(enemyManager);
        newEnemy.enemyGenerator = this;
    }
}
