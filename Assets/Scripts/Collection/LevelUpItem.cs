using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpItem : Collection
{
    public float health = 100;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //�����ײ���Ĳ㼶���н�ɫ�ű�������Ӧ����
        if(other.gameObject.TryGetComponent<PlayerController>(out PlayerController character))
        {
            AudioManager.Instance.PlaySFX(getSFX);
            character.LevelUp();
            gameObject.SetActive(false);
        }
    }
}
