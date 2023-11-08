using UnityEngine;
using Random = UnityEngine.Random;

public class SlashHandler : MonoBehaviour
{
    public int slashLevel;

    [SerializeField] private Transform[] slashPivots;
    [SerializeField] private Animator[] slashAnimators;
    
    public float slashTime;
    private float slashTimer;

    private void Update()
    {
        if (slashLevel == 0)
            return;
        
        slashTimer += Time.deltaTime;
        
        if (slashTimer > slashTime)
        {
            slashTimer = 0f;

            Slash();
        }
    }

    public void Init(float slashTime, int slashLevel)
    {
        this.slashTime = slashTime;
        this.slashLevel = slashLevel;
    }

    private void Slash()
    {
        SoundManager.I.PlayKnife();
        
        for (int i = 0; i < slashLevel; i++)
        {
            slashPivots[i].transform.localEulerAngles = Vector3.forward * Random.Range(0f, 360f);
            slashAnimators[i].SetTrigger("Attack");
        }
    }
}
