//ゲームシステム
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour
{
    [SerializeField] BallGenerator ballGenerator = default;
    bool isDragging;
    [SerializeField] List<Ball> removeBalls = new List<Ball>();
    //現在ドラッグしているBall
    Ball currentDraggingBall;
    public static int score;
    [SerializeField] Text scoreText = default;
    [SerializeField] GameObject pointEffectPrefab = default;

    void Start()
    {
        score = 0;
        //scoreを最初は0点追加する(0という表示になるようにする)
        AddScore(0);
        //ParamsSO で操作可能(初期のBallの数)
        StartCoroutine(ballGenerator.Spawns(ParamsSO.Entity.initBallCount));
    }
    void AddScore(int point)
    {
        score += point;
        scoreText.text = score.ToString();
    }
    public static int Getscore()
    {
        return score;
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //マウスの右クリック押し込みを感知:ボムならば周囲を含めて爆破(下に記載)
            OnDragBegin();
        }
        else if(Input.GetMouseButtonUp(0))
        {
            //マウスの右クリック押し込みを離したのを感知(下に記載)
            OnDragEnd();
        }
        else if(isDragging)
        {
            //Ballをドラッグ中(下に記載)
            OnDragging();
        }
    }
    void OnDragBegin()
    {
        //マウスによるオブジェクトの判定
        //マウスの位置情報は、スクリーン座標
        //ゲーム内オブジェクトの位置情報は、ワールド座標
        //マウスの判定をスクリーン座標からワールド座標へ変換
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Rayを出す
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        //マウスが対象に hit した + それが BallPrefab であった場合
        if (hit && hit.collider.GetComponent<Ball>())
        {
            Ball ball = hit.collider.GetComponent<Ball>();
            //ボムならば周囲を含めて爆破
            if(ball.IsBomb())
            {
                //下記(爆破処理)
                Explosion(ball);
            }
            else
            {
                //Ballならば選択してリストに入れる
                AddRemoveBall(ball);
                isDragging = true;
            }            
        }
    }
    void OnDragging()
    {
        //マウスによるオブジェクトの判定
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Rayを出す
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        //マウスが対象に hit した + それが BallPrefab であった場合
        if (hit && hit.collider.GetComponent<Ball>())
        {
            //現在ドラッグしている(Ball)オブジェクトと同じ種類であり、距離が近い場合Listに追加
            Ball ball = hit.collider.GetComponent<Ball>();
            //同じ種類
            if (ball.id == currentDraggingBall.id)
            {
                //距離が近い
                //Vector2.Distance = 2点間の距離を求める
                //(最初にドラッグした CurrentBall と以降にドラッグしようした Ball の距離)
                float distance = Vector2.Distance(ball.transform.position, currentDraggingBall.transform.position);
                //ParamsSO で設定した Ball を消せる判定距離よりも近かった場合
                if (distance < ParamsSO.Entity.ballDistance)
                {
                    //下記
                    AddRemoveBall(ball);
                }
            }            
        }
    }
    void OnDragEnd()
    {
        //クリックしてドラッグしたボールを消す処理
        int removeCount = removeBalls.Count;
        //3個以上でBallを消せるようにする処理
        if (removeCount >= 3)
        {
            for (int i = 0; i < removeCount; i++)
            {
                removeBalls[i].Explosion();
            }
            StartCoroutine(ballGenerator.Spawns(removeCount));
            //消した数×100点
            int score = removeCount * ParamsSO.Entity.scorePoint;
            AddScore(score);
            //下記
            SpawnPointEffect(removeBalls[removeBalls.Count - 1].transform.position, score);
        }
        //全てのremoveBallの大きさを離した時元に戻す
        for(int i = 0; i < removeCount; i++)
        {
            //ballを離した時
            removeBalls[i].transform.localScale = Vector3.one * 1.5f;
            //Ballの色を変える処理は割愛
            //removeBalls[i].GetComponent<SpriteRenderer>().color = Color.white;
        }
        removeBalls.Clear();
        isDragging = false;
    }
    void AddRemoveBall(Ball ball)
    {
        currentDraggingBall = ball;
        if (removeBalls.Contains(ball) == false)
        {
            //ballをリストに追加するとき(TAPしてドラッグしている時)にボールを大きくする処理
            //Vector3.one; = position(1, 1, 1);
            ball.transform.localScale = Vector3.one * 1.8f;
            //Ballの色を変える処理は既存の色と混ざってしまい色合いが汚くなってしまったのでボツ
            //ball.GetComponent<SpriteRenderer>().color = Color.yellow;
            removeBalls.Add(ball);
        }
    }
    //ボムによる爆破
    void Explosion(Ball bomb)
    {
        List<Ball> explosionList = new List<Ball>();
        //ボムを中心として周囲のBallを爆破する(OverlapCircleAll = 中心点から周囲にRayを飛ばす)
        //今回の場合、ボムが中心で半径が7.8(ParamsSO にて設定)
        Collider2D[] hitObj = Physics2D.OverlapCircleAll(bomb.transform.position, ParamsSO.Entity.bombRange);
        for(int i = 0; i < hitObj.Length; i++)
        {
            //Ballであれば爆破リストに追加する
            Ball ball = hitObj[i].GetComponent<Ball>();
            if(ball)
            {
                explosionList.Add(ball);
            }
        }
        //クリックしてドラッグしたボールを消す処理
        int removeCount = explosionList.Count;
        for (int i = 0; i < removeCount; i++)
        {
            explosionList[i].Explosion();
        }
        StartCoroutine(ballGenerator.Spawns(removeCount));
        //消した数×100点
        int score = removeCount * ParamsSO.Entity.scorePoint;
        AddScore(score);
        SpawnPointEffect(bomb.transform.position, score);
    }
    /// <summary>
    /// Ballを消した際に発生するポイントエフェクトの処理
    /// </summary>
    /// <param name="position"></param>
    /// <param name="score"></param>
    void SpawnPointEffect(Vector2 position, int score)
    {
        //生成した時に GameObj を取得( Prefab 化していた PointEffect を取得)
        //Quaternion.identity = 初期値
        GameObject effectObj = Instantiate(pointEffectPrefab, position, Quaternion.identity);
        //GameObj を取得した時にコンポーネントを取得
        PointEffect pointEffect = effectObj.GetComponent<PointEffect>();
        pointEffect.Show(score);
    }
}
