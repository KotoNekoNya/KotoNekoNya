using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SnakeMove : MonoBehaviour


{
    public List<Transform> Tails;
    [Range(0, 3)]
    public float BonesDistance;
    public GameObject BonePrefab;
    [Range(0, 10)]
    public float Speed;
    [Range(0, 10)]
    public float dashspeed;
    public bool Ispause;
    public GameObject GameOverScreen;
    public GameObject WinScreen;
    public UnityEvent OnEat;
    public UnityEvent OnHit;

    public Rigidbody Snake;
    public Vector3 Force;

    private Transform _transform;

    private void Start()
    {
        Time.timeScale = 1;
        _transform = GetComponent<Transform>();
        Ispause = false;
        
    }
    private void Update()
    {
        //ÑDÑrÑyÑwÑuÑ~ÑyÑu ÑxÑ}ÑuÑy
        if (Ispause == false)
        {
            MoveSnake(_transform.position + _transform.forward * Speed * Time.deltaTime + _transform.right * Input.GetAxis("Horizontal") * dashspeed * Time.deltaTime);
        }
    }

    private void MoveSnake(Vector3 newPosition)
    {
        //ÑRÑÄÑxÑtÑpÑ~ÑyÑu Ñ{ÑÄÑÉÑÑÑuÑz
        float sqrDistance = BonesDistance * BonesDistance;
        Vector3 previosPosition = _transform.position;

        foreach (var bone in Tails)
        {
            if((bone.position - previosPosition).sqrMagnitude > sqrDistance)
            {
                var temp = bone.position;
                bone.position = previosPosition;
                previosPosition = temp;
            }
            else
            {
                break;
            }
            
        }
        _transform.position = newPosition;
    }


    private void OnCollisionEnter(Collision collision)
    {
        //ÑPÑÄÑtÑqÑÄÑÇ ÑuÑtÑç
        if(collision.gameObject.tag == "food")
        {
            Destroy(collision.gameObject);

            var bone = Instantiate(BonePrefab);
            Tails.Add(bone.transform);

            if(OnEat != null)
            {
                OnEat.Invoke();
            }
        }
        //ÑRÑÑÑçÑâÑ{Ñp ÑÉ ÑBÑÇÑpÑsÑy
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            


            if (enemy.HP <= Tails.Count)
            {
                Destroy(collision.gameObject);
                var bone = Instantiate(BonePrefab);
                foreach (Transform Tail in Tails.GetRange(Tails.Count - enemy.HP, enemy.HP))
                {
                    Destroy(Tail.gameObject);
                }
                
                Tails.RemoveRange(Tails.Count - enemy.HP, enemy.HP);

                if (OnHit != null)
                {
                    OnHit.Invoke();
                }
            }

            else
            {
                Time.timeScale = 0;
                GameOverScreen.SetActive(true);
                Ispause = true;
                return;
            }
            
        }
        //ÑUÑyÑ~ÑyÑä
        if (collision.gameObject.tag == "finish")
        {
            Time.timeScale = 0;
            WinScreen.SetActive(true);
            Ispause = true;
            return;
        }
    }
    
}