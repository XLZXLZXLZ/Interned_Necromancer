using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ���״̬����,�������״̬�̳��ڸû���
/// </summary>
public class PlayerStates : ScriptableObject, IState
{
    //���״̬��
    public PlayerStateMachine stateMachine;

    //��Ҷ��������
    public Animator anim;

    //��Ҹ������
    public Rigidbody2D rb;

    //��Ҹ������
    public PlayerControl controller;

    //��Ҳ��������
    public PlayerInput input;

    //�����Ϸ����
    public GameObject player;

    //�����Ϣ���
    public PlayerInfo info;

    //�����ж�
    public DieAndRevive death;

    //״̬����ʱ��
    public float StateDuration => Time.time - stateStartTime;
    private float stateStartTime;

    public bool AnimEnd => anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1;


    /*
     * ��ΪScriptableObject��������������ϣ�������Ҫ���������������������͸�ֵ��״̬
     * ����Ϊ��ʼ��ʱ׼���õ��������ͣ�����ͬ״̬����ҵĲٿ�
     * ��һ����״̬������Ϊ״̬�л���Ҫ����������ϵ�״̬��
     * �ڶ��������ĸ�Ϊ������ϵĳ�����������ڳ��ã��ڴ�Ҳ��ֵ��
     * �����Ϊ��ұ�private �����Ϸ���壬�ڲ����ѵ�����¿���ֱ����player.GetComponent<T>���ҵ���Ӧ���
     */

    /// <suublic
    /// ��ʼ��������״̬������ʱ��������״̬���г�ʼ������������ϵ������ֵ��ȥ
    /// </summary>
    public void Initialize(PlayerStateMachine stateMachine, Animator animator, PlayerControl controller, PlayerInput input,Rigidbody2D rb,GameObject player,DieAndRevive death)
    {
        this.stateMachine = stateMachine;
        this.anim = animator;
        this.controller = controller;
        this.input = input;
        this.rb = rb;
        this.player = player;
        this.info = PlayerInfo.Instance;
        this.death = death;
    }

    public virtual void Enter()
    {
        stateStartTime = Time.time;
    }

    public virtual void Exit() 
    {
    
    }

    public virtual void LogicUpdate() 
    {

    }

    public virtual void PhysicsUpdate() { }

}
