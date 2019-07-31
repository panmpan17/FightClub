using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    private Animator Anim;
    private Rigidbody2D RBody;
    
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private BoxingGlove leftGlove, rightGlove;

    [SerializeField]
    private int comboNum;
    [SerializeField]
    private float comboColddown, comboProgress;
    [SerializeField]
    private int metaComboNeed;

    private bool canWalk = true;

    [SerializeField]
    private int maxHealth;
    private int health;
    public float HealthPercentage { get { return (float) health / (float) maxHealth; } }

    private FighterUICtrl UICtrl;

    void Awake() {
        Anim = GetComponent<Animator>();
        RBody = GetComponent<Rigidbody2D>();
        UICtrl = GetComponent<FighterUICtrl>();
        leftGlove.owner = this;
        rightGlove.owner = this;
        health = maxHealth;
    }

    void Update() {
        if (comboNum > 0) {
            comboProgress += Time.deltaTime;
            if (comboProgress >= comboColddown) {
                comboNum = 0;
                comboProgress = 0;
            }
        }
    }

#region PUNCING
    public void PunchRight() {
        if (rightGlove.punching || !leftGlove.retracted) { return; }
        rightGlove.StartPunch();
        Anim.SetTrigger("Punch1");
    }
    public void PunchLeft() {
        if (leftGlove.punching || !rightGlove.retracted) { return; }
        leftGlove.StartPunch();
        Anim.SetTrigger("Punch2");
    }
    public void MetaPunch() {
        if (comboNum < metaComboNeed) { return; }
        if (leftGlove.punching || rightGlove.punching) { return; }

        leftGlove.StartPunch();
        canWalk = false;
        StopWalk();
        Anim.SetBool("MetaPunch", true);
    }
    public void AddCombo() {
        comboNum++;
        comboProgress = 0;
    }
    public void RightRetract() { rightGlove.RetractPunch(); }
    public void LeftRetract() {leftGlove.RetractPunch(); }
    public void RightPunchEnd() { rightGlove.EndPunch(); }
    public void LeftPunchEnd() { leftGlove.EndPunch(); }
    public void MetaPunchEnd() {
        leftGlove.EndPunch();
        canWalk = true;
        Anim.SetBool("MetaPunch", false);
    }
#endregion

#region WALKING
    public void WalkLeft() {
        if (!canWalk) { return; }
        Anim.SetTrigger("Walk");
        RBody.velocity = new Vector2(-moveSpeed, 0f);
    }
    public void WalkRight() {
        if (!canWalk) { return; }
        Anim.SetTrigger("WalkBackword");
        RBody.velocity = new Vector2(moveSpeed, 0f);
    }
    public void StopWalk() {
        Anim.SetTrigger("Stop");
        RBody.velocity = Vector2.zero;
    }
#endregion

    void OnTriggerEnter2D(Collider2D collider) {
        BoxingGlove glove = collider.GetComponent<BoxingGlove>();
        if (glove != null) {
            if (glove.punching && !glove.punched) {
                glove.Punched();
                Anim.SetTrigger("Hurt");
                health -= 1;
                UICtrl.UpdateHealth();
            }
        }
    }
}
