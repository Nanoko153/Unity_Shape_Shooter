using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
    [HideInInspector]
    private PlayerActionsInput playerActionsInput;

    [Header("方向")]
    public Vector2 lookAtDir;
    public Transform planeTransform;

    [Header("移动设置")]
    public float moveSpeed = 5f;

    [Header("发射位置")]
    public Transform shootPos_1;
    public Transform shootPos_2;
    public Transform shootPos_3;

    [Header("子弹1")]
    public GameObject bulletPrefab_1;
    public float shootCD_1 = 0.1f;
    public AudioClip bulletSFX_1;

    [Header("子弹2")]
    public GameObject bulletPrefab_2;
    public float shootCD_2 = 0.5f;
    public AudioClip bulletSFX_2;

    [Header("子弹3")]
    public GameObject bulletPrefab_3;
    public float shootCD_3 = 0.5f;
    public AudioClip bulletSFX_3;

    [Header("子弹4")]
    public GameObject bulletPrefab_4;
    public float shootCD_4 = 0.5f;
    public AudioClip bulletSFX_4;

    public bool isCanShooting;

    [Header("SFX")]
    public AudioClip hitSFX;
    public AudioClip deathSFX;
    public AudioClip SwitchTypeSFX;

    [Header("FVX")]
    public GameObject deathVFX;
    public GameObject hurtVFX;

    bool isCanLevelDown = true;

    protected override void Awake()
    {
        base.Awake();
        playerActionsInput = GetComponent<PlayerActionsInput>();
    }
    void Start()
    {
        InputManager.Instance.EnableGamePlayingInputInput();
        EventCenter.Instance.EventTrigger<PlayerController>("RegisterPlayer", this);
        EventCenter.Instance.EventTrigger<float>("PlayerHpInit", maxHP);
        EventCenter.Instance.EventTrigger<string>("SwitchLevelUI", level.ToString());
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    void Update()
    {
        if(playerActionsInput.IsKeyDown_LeftMouse)
            isCanShooting = true;
        else if(playerActionsInput.IsKeyUp_LeftMouse)
            isCanShooting = false;
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        EventCenter.Instance.EventTrigger<float>("PlayerHpReduce", damage);
        AudioManager.Instance.PlaySFX_RandomPitch(hitSFX);
        if(isCanLevelDown)
        {
            LevelDown();
            if(gameObject.activeSelf)
                Wait_IsCanLevelDownCD();
        }
    }

    public override void Die()
    {
        base.Die();
        PlayDeathVFX();
        AudioManager.Instance.PlayUI(deathSFX);
        EventCenter.Instance.EventTrigger("GameOver");
        gameObject.SetActive(false);
    }
    #region Level相关
    public void LevelUp()
    {
        if(level == 5)
        {
            EventCenter.Instance.EventTrigger<int>("AddScore", 150);
            return;
        }
        level = Mathf.Clamp(level+1, 1, 5);
        EventCenter.Instance.EventTrigger<string>("SwitchLevelUI" ,level.ToString());
    }

    public void LevelDown()
    {
        level = Mathf.Clamp(level-1, 1, 5);
        EventCenter.Instance.EventTrigger<string>("SwitchLevelUI" ,level.ToString());
    }
    #endregion
    #region Player刚体速度设置

    public void Move (float speed)
    {
        SetVelocity(speed * playerActionsInput.MoveInputValue);

        if(playerActionsInput.IsMove)
            transform.position = ViewportManager.Instance.PlayerMoveablePosition(transform.position, 0, 0);
    }
    #endregion

    #region Update LookAtDir 更新鼠标指向方向
    public void UpDateLookAtDir()
    {
        lookAtDir = (playerActionsInput.GetMouseInWorldPosition() - transform.position).normalized;
    }

    public void UpdatePlaneDir()
    {
        UpDateLookAtDir();

        //手部跟随角度旋转
        float angle = Mathf.Acos(Vector2.Dot(Vector2.right, lookAtDir)) * Mathf.Rad2Deg;
        if(lookAtDir.y < 0)
            angle = -angle;
        planeTransform.rotation = Quaternion.Euler(0, 0, angle);
    }
    #endregion

    #region 使用VFX
    public void PlayDeathVFX()
    {
        PoolManager.Release(deathVFX);
    }
    #endregion
    #region 技能CD更新
    public void Wait_IsCanLevelDownCD()
    {
        isCanLevelDown = false;
        StartCoroutine(Wait_IsCanLevelDownCoroutine());
    }

    IEnumerator Wait_IsCanLevelDownCoroutine()
    {
        float cd = 1f;
        while(cd > 0)
        {
            cd = Mathf.Clamp(cd - Time.fixedDeltaTime, 0, 1);
            yield return new WaitForFixedUpdate();
        }
        isCanLevelDown = true;
    }
    #endregion
    // void OnDrawGizmos()
    // {
    //     //Gizmos.color = Color.blue;
    //     //Gizmos.DrawWireSphere(transform.position, closeCombatAttackRang);
    //     //Gizmos.DrawLine(InputManager.Instance.mouseInGamePosition - Vector3.forward, InputManager.Instance.mouseInGamePosition + Vector3.forward*50);
    // }
}
