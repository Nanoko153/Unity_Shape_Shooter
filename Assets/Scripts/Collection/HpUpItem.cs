using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpUpItem : Collection
{
    public float healthUp = 50;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //如果碰撞到的层级带有角色脚本调用相应函数
        if(other.gameObject.TryGetComponent<PlayerController>(out PlayerController character))
        {
            AudioManager.Instance.PlaySFX(getSFX);
            character.maxHP += healthUp;
            character.RestoreHealth(character.maxHP);
            EventCenter.Instance.EventTrigger<float>("PlayerHpInit", character.maxHP);
            EventCenter.Instance.EventTrigger<float>("PlayerHpRecover", character.maxHP);
            gameObject.SetActive(false);
        }
    }
}
