using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public int id;
    //爆破エフェクトの設定
    [SerializeField] GameObject explosionPrefab = default;
    public void Explosion()
    {
        //破壊後に
        Destroy(gameObject);
        //爆破エフェクトを生成
        GameObject explosion =　Instantiate(explosionPrefab, transform.position, transform.rotation);
        //0.2秒後爆破エフェクト消去
        Destroy(explosion, 0.2f);
    }
    //GameSystem と連動(Ball と bomb の判定)
    public bool IsBomb()
    {
        //if(id == -1)
        //{
        //    return true;
        //}
        //return false;
        //return のみで true or false か判定可能
        return id == -1;//bomb ならば true をかえす
    }
}
