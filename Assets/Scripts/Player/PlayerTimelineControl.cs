using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class PlayerTimelineControl : MonoBehaviour
{
    [SerializeField] private PlayableDirector jump;
    [SerializeField] private PlayableDirector attack;
    [SerializeField] private PlayableDirector dash;

    [Header("Attack Timelines")]
    [SerializeField] private PlayableDirector upAttack;
    [SerializeField] private PlayableDirector downAttack;
    [SerializeField] private PlayableDirector holdAttack;

    [Header("Energy Timelines")]
    [SerializeField] private PlayableDirector holdEnergy;
    [SerializeField] private PlayableDirector upEnergy;
    [SerializeField] private PlayableDirector downEnergy;

    [Header("Skill Timelines")]
    [SerializeField] private SkillSlot x;
    [SerializeField] private SkillSlot square;
    [SerializeField] private SkillSlot triangle;
    [SerializeField] private SkillSlot circle;
    private bool playing;

    public bool Playing { get => playing; set { playing = value; if (playing) { StartCoroutine(WaitToRead()); } } }

    // Start is called before the first frame update
    void Start() {
        //PlayerInputs.x += Jump;
        //x.Start();
        //square.Start();
        //triangle.Start();
        //circle.Start();
        // PlayerInputs.square += Attack;
    }
    private void Jump() {
        if (!Playing) {
            jump.Play();
            Playing = true;
        }
    }
    private void Attack() {
        if (!Playing) {
            attack.Play();
            Playing = true;
        }
    }
    private IEnumerator WaitToRead() {
        YieldInstruction wait = new WaitForSeconds(0.5f);
        yield return wait;
        Playing = false;
    }
    private void OnCheat() {
        //x.SkillAssign();
    }
    public void Dash() {
        dash.Play();
    }
    /*public void PlayX() {
        x.PlaySkill();
    }
    public void PlaySquare() {
        square.PlaySkill();
    }
    public void PlayTriangle() {
        triangle.PlaySkill();
    }
    public void PlayCircle() {
        circle.PlaySkill();
    }*/
    public void PlayUpAttack() {
        upAttack.Play();
    }
    public void PlayDownAttack() {
        downAttack.Play();
    }
    public void PlayHoldAttack() {
        holdAttack.Play();
    }
    public void PlayHoldEnergy() {
        holdEnergy.Play();
    }
    public void PlayUpEnergy() {
        if (upEnergy != null)
            upEnergy.Play();
    }
    public void PlayDownEnergy() {
        if (downEnergy != null)
            downEnergy.Play();
    }
    ///if a timeline is triggered have a bool that denies another timeline from playing and then a certain amount of time passes that unsets the bool.
}