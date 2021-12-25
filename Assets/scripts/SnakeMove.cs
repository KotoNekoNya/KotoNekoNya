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
        //�D�r�y�w�u�~�y�u �x�}�u�y
        if (Ispause == false)
        {
            MoveSnake(_transform.position + _transform.forward * Speed * Time.deltaTime + _transform.right * Input.GetAxis("Horizontal") * dashspeed * Time.deltaTime);
        }
    }

    private void MoveSnake(Vector3 newPosition)
    {
        //�R���x�t�p�~�y�u �{�������u�z
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
        //�P���t�q���� �u�t��
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
        //�R�������{�p �� �B���p�s�y
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
        //�U�y�~�y��
        if (collision.gameObject.tag == "finish")
        {
            Time.timeScale = 0;
            WinScreen.SetActive(true);
            Ispause = true;
            return;
        }
    }
    
}