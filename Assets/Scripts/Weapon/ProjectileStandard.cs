using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileStandard : ProjectileBase
{
    public Transform root;
    public float speed = 20f;
    public float maxLifeTime = 5f;

    private ProjectileBase projectileBase;


    //�̵�
    private Vector3 lastRootPosition;
    private Vector3 velocity;

    //�߷�
    public float gravityDownAcceleration = 0f;

    //Hit
    private List<Collider> m_ignoredColliders;
    public float radius = 0.01f;
    public float attackDamage = 10f;

    public GameObject impactVfxPrefab;
    public float impactVfxOffset = 0.01f;
    public float impactVfxLifeTime = 3f;

    //���� ����
    public LayerMask hittable;
    private void OnEnable()
    {
        projectileBase = this.GetComponent<ProjectileBase>();
        projectileBase.Onshoot += Onshoot;

        Destroy(gameObject, maxLifeTime);
    }

    new void Onshoot()
    {
        lastRootPosition = root.position;
        velocity = transform.forward * speed;

        //���õǴ� �浹ü ��������
        m_ignoredColliders = new List<Collider>();
        Collider[] ownerColliders = projectileBase.owner.GetComponentsInParent<Collider>();
        m_ignoredColliders.AddRange(ownerColliders);
    }

    private void Update()
    {
        //Move
        transform.position += velocity * Time.deltaTime;

        //�߷�
        if(gravityDownAcceleration > 0f)
        {
            velocity += Vector3.down * gravityDownAcceleration * Time.deltaTime;
        }

        //Hit
        RaycastHit nearHit = new RaycastHit();
        nearHit.distance = Mathf.Infinity;
        bool isfindHit = false;

        Vector3 displamentSinceLastFrame = root.position - lastRootPosition;
        RaycastHit[] hits = Physics.SphereCastAll(lastRootPosition, radius,
            displamentSinceLastFrame.normalized, displamentSinceLastFrame.magnitude);
        foreach (var hit in hits)
        {
            if(IsHitValid(hit))
            {
                if(hit.distance < nearHit.distance)
                {
                    nearHit = hit;
                    isfindHit = true;
                }
            }
        }

        if (isfindHit)
        {
           
            if(nearHit.collider.GetComponentInParent<Enemy>())
            {
                //OnHit(nearHit.point, nearHit.normal, nearHit.collider);
                Enemy enemy = nearHit.collider.GetComponentInParent<Enemy>();
                //enemy.SetState(EnemyState.E_OnDamage);

            }
        }

        //
        transform.forward = velocity.normalized;
        lastRootPosition = root.position;
    }
    //private void OnHit(Vector3 point, Vector3 normal,Collider collider)
    //{
    //    //������ �ֱ�
    //    DamageArea damageArea = GetComponent<DamageArea>();
    //    if(damageArea !=null)
    //    {
    //        damageArea.InplictDamageArea(attackDamage,point,
    //            projectileBase.owner,hittable,QueryTriggerInteraction.Collide);
    //    }
    //    else
    //    {
    //        Damageable damageable = collider.GetComponent<Damageable>();
    //        if (damageable != null)
    //            damageable.InflictDamage(attackDamage, false, projectileBase.owner);

    //    }


    //    //����Ʈ
    //    if (impactVfxPrefab!=null)
    //    {
    //        GameObject impactEff = Instantiate(impactVfxPrefab, point + (normal * impactVfxOffset), Quaternion.LookRotation(normal));
    //        if (impactVfxLifeTime > 0)
    //        {
    //            Destroy(impactEff.gameObject, impactVfxLifeTime);

    //        }
    //    }
    //    //����

    //    //UI�� Ÿ�� ���ʹ� ����
    //    EnemyHealthUI.instance.TargetEnemy(collider.transform.parent);

    //    //
    //    Destroy(gameObject);
    //}

    private bool IsHitValid(RaycastHit hit)
    {
        //if (hit.collider.isTrigger && hit.collider.GetComponent<Health>()==null)
        //{
        //    return false;
        //}

        //if(m_ignoredColliders != null && m_ignoredColliders.Contains(hit.collider))
        //{
        //    return false;
        //}
        return true;
    }
}
