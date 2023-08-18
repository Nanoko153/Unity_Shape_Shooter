using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpItem : Collection
{
    public float health = 100;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //如果碰撞到的层级带有角色脚本调用相应函数
        if(other.gameObject.TryGetComponent<PlayerController>(out PlayerController character))
        {
            AudioManager.Instance.PlaySFX(getSFX);
            character.LevelUp();
            gameObject.SetActive(false);
        }
    }
}
