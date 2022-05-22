using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationControl : MonoBehaviour
{
    [SerializeField] protected Animator animator = null;
    [SerializeField] private string idleAnimationBlendTreeVar = "idleTree";
    [SerializeField] private string runAnimationBlendTreeVar = "runTree";
    [SerializeField] private string defeatAnimationBlendTreeVar = "defeatTree";
    [SerializeField] private string victoryAnimationBlendTreeVar = "victoryTree";

    private float runValue = -1;
    private float defeatValue = -1;
    private float victoryValue = -1;

    // Start is called before the first frame update
    void Start()
    {
        if (animator == null)
            animator = GetComponentInChildren<Animator>();
        if (animator == null)
            Debug.LogError("Character needs an animator to work");
        
        // add animation events script to animator object
        animator.gameObject.AddComponent<CharacterAudioEvents>();

        animator.SetFloat(idleAnimationBlendTreeVar, UnityEngine.Random.Range(0, 5));
    }

    public void EnableIdleAnimation() {
        runValue = defeatValue = victoryValue = -1;
        // set other tree vars to "disabled" values
        animator.SetFloat(runAnimationBlendTreeVar, runValue);
        animator.SetFloat(defeatAnimationBlendTreeVar, defeatValue);
        animator.SetFloat(victoryAnimationBlendTreeVar, victoryValue);
        animator.SetFloat(idleAnimationBlendTreeVar, UnityEngine.Random.Range(0, 5));
    }
    public void EnableRunAnimation()
    {
        if (runValue < 0) {
            defeatValue = victoryValue = -1;
            runValue = UnityEngine.Random.Range(0, 5);
            // set other tree vars to "disabled" values
            animator.SetFloat(defeatAnimationBlendTreeVar, defeatValue);
            animator.SetFloat(victoryAnimationBlendTreeVar, victoryValue);
            animator.SetFloat(runAnimationBlendTreeVar, runValue);
        }
    }
    public void EnableDefeatAnimation()
    {
        if (defeatValue < 0) {
            runValue = victoryValue = -1;
            defeatValue = UnityEngine.Random.Range(0, 5);
            // set other tree vars to "disabled" values
            animator.SetFloat(runAnimationBlendTreeVar, runValue);
            animator.SetFloat(victoryAnimationBlendTreeVar, victoryValue);
            animator.SetFloat(defeatAnimationBlendTreeVar, defeatValue);
        }
    }
    public void EnableVictoryAnimation()
    {
        if (animator.GetFloat(victoryAnimationBlendTreeVar) < 0) {
            runValue = defeatValue = -1;
            victoryValue = UnityEngine.Random.Range(0, 5);
            // set other tree vars to "disabled" values
            animator.SetFloat(runAnimationBlendTreeVar, runValue);
            animator.SetFloat(defeatAnimationBlendTreeVar, defeatValue);
            animator.SetFloat(victoryAnimationBlendTreeVar, victoryValue);
        }
    }

    private void ResetAnimations() {
        // set tree vars to "disabled" values
        runValue = defeatValue = victoryValue = -1;
        animator.SetFloat(runAnimationBlendTreeVar, runValue);
        animator.SetFloat(defeatAnimationBlendTreeVar, defeatValue);
        animator.SetFloat(victoryAnimationBlendTreeVar, victoryValue);
    }
}
