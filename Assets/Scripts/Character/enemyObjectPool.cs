using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyObjectPool : MonoBehaviour
{

    [SerializeField]private enemy enemyPrefab = default;

    private List<enemy> activeList = new List<enemy>();
    private Stack<enemy> inActivePool = new Stack<enemy>();

    void Start()
    {
        
    }

    void Update()
    {
        for(int i = activeList.Count - 1; i >= 0; i--)
        {
            var projectile = activeList[i];
            if(projectile.IsActive)
            {//gameObject.activeSelf
                projectile.onUpdate();
            }
            else
            {
                Remove(projectile);
            }
        }
    }

    public void EnemyBorn(){
        var targetEnemy = (inActivePool.Count > 0)
            ? inActivePool.Pop()
            : Instantiate(enemyPrefab, transform);
        //targetEnemy.Activate()
        activeList.Add(targetEnemy);
    }

    public void Remove(enemy targetEnemy){
        activeList.Remove(targetEnemy);
        //targetEnemy.Deactivate();
        inActivePool.Push(targetEnemy);
    }
}
