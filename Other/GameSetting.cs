using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting
{
    public const float 
    HIT_PLAYER_TIME = 2f,
    SPIKE_DAMAGE = 1f,
    LINE_LASER_DAMAGE_PER_SECOND = 1f,
    ROCKET_DAMAGE = 4f,

    #region Thunder Botl Skill
    THUNDER_BOLT_EFFECT_TIME = 4f,
    THUNDER_BOLT_COOLDOWN_TIME = 7f,
    THUNDER_BOLT_STUN_TIME = 3f,
    THUNDER_BOLT_DEAL_DAMAGE_EACH_TIME = 0.5f,
    THUNDER_BOLT_DAMAGE = 0.5f,

    #endregion


    #region Alien Spider (AS)
    AS_ATTACK_RANGE = 6,
    // nếu điều chỉnh thông số này thì phải thay đổi cả thời gian trên thanh anim attack
    // từ vị trí có key func đến hết để khớp anim
    AS_JUMP_ATTACK_TIME = 0.5f,
    AS_CHASE_RANGER = 10,
    AS_IDLE_TIME = 3,
    AS_WAIT_FOR_NEXT_ATTACK_TIME = 4;
    #endregion
    
}
