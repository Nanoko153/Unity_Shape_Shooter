using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryItem : Collection
{
    public float health = 100;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //�����ײ���Ĳ㼶���н�ɫ�ű�������Ӧ����
        if(other.gameObject.TryGetComponent<Character>(out Character character))
        {
            AudioManager.Instance.PlaySFX(getSFX);
            character.RestoreHealth(health);
            EventCenter.Instance.EventTrigger<float>("PlayerHpRecover", health);
            gameObject.SetActive(false);
        }
    }
}
