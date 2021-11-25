using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    static public DoublyLinkedList<GameObject> BallsList = new DoublyLinkedList<GameObject>();
    static public GameObject BallPrefab; 
    public GameObject _ballPrefab;
    static public Sprite[] Sprites;
    public Sprite[] _sprites;
    static private AudioSource[] audios;
    void Awake() {
        Sprites = _sprites;
        BallPrefab = _ballPrefab;
        audios = GetComponents<AudioSource>();
    }

    static public float DistanceToNext(DoublyNode<GameObject> curr) {
        if (curr.Next != null) {
            return Vector2.Distance(curr.Data.transform.position, curr.Next.Data.transform.position);
        } else {
            return 0f;
        }
    }

    static public bool IsNextsMove(DoublyNode<GameObject> curr) {
        DoublyNode<GameObject> c = curr;
        if (c.Next != null && c.Next.Data.GetComponent<Ball>().isStop) {
            return false;
        }
        return true;
    }

    static public void DestroyBalls(GameObject curr) {
        DoublyNode<GameObject> CurrBall = BallsList.Contains(curr);

        // PIZDEC NEED FIX
        if (CurrBall != null) {
            if(CurrBall != BallsList.head && CurrBall != BallsList.tail){
                if (NodeGetSprite(CurrBall) == NodeGetSprite(CurrBall.Next) && NodeGetSprite(CurrBall.Previous) == NodeGetSprite(CurrBall)) {
                    DestroyAndRemove(CurrBall, CurrBall.Next, CurrBall.Previous);
                    DoublyNode<GameObject> c = CurrBall.Previous;
                    while (c != null) {
                        c.Data.SendMessage("GoBack");
                        c = c.Previous;
                    }
                    return;
                }
            } 
            if (CurrBall.Next != null && CurrBall.Next.Next != null) {
                if (NodeGetSprite(CurrBall) == NodeGetSprite(CurrBall.Next) && NodeGetSprite(CurrBall.Next.Next) == NodeGetSprite(CurrBall)) {
                    DestroyAndRemove(CurrBall, CurrBall.Next, CurrBall.Next.Next);
                    DoublyNode<GameObject> c = CurrBall;
                    while (c != null) {
                        c.Data.SendMessage("GoBack");
                        c = c.Previous;
                    }
                    return;
                }
            }
            if (CurrBall.Previous != null && CurrBall.Previous.Previous != null) {
                if (NodeGetSprite(CurrBall) == NodeGetSprite(CurrBall.Previous) && NodeGetSprite(CurrBall.Previous.Previous) == NodeGetSprite(CurrBall)) {
                    DestroyAndRemove(CurrBall, CurrBall.Previous, CurrBall.Previous.Previous);
                    DoublyNode<GameObject> c = CurrBall.Previous.Previous;
                    while (c != null) {
                        c.Data.SendMessage("GoBack");
                        c = c.Previous;
                    }
                    return;
                }
            }
        }
    }

    static private void DestroyAndRemove(DoublyNode<GameObject> Node1, DoublyNode<GameObject> Node2, DoublyNode<GameObject> Node3) {
         Destroy(Node1.Data.gameObject);
         Destroy(Node2.Data.gameObject);
         Destroy(Node3.Data.gameObject);
         BallsList.Remove(Node1.Data);
         BallsList.Remove(Node2.Data);
         BallsList.Remove(Node3.Data);
         audios[1].Play();
    }
   
    static public void ChangeColors(DoublyNode<GameObject> Node, Sprite sp) {
        if (Node.Next != null) {
            ChangeColors(Node.Next, Node.Data.GetComponentInChildren<SpriteRenderer>().sprite);
        } else {
            GameObject spawner = GameObject.Find("Spawner");
            spawner.GetComponent<Spawner>().SpawnFromController(
                Node.Data.GetComponentInChildren<SpriteRenderer>().sprite, 
                Node.Data.GetComponent<Ball>().LastPos[1],
                Node.Data.GetComponent<Ball>().WaypointIdx);  
        }
        Node.Data.GetComponentInChildren<SpriteRenderer>().sprite = sp;
    }

    static public void ChangeColors(GameObject curr, Sprite sp) {
        audios[0].Play();
        DoublyNode<GameObject> CurrNode = BallsList.Contains(curr);
        if (CurrNode is DoublyNode<GameObject>) {
            if (CurrNode.Next != null) {
                ChangeColors(CurrNode.Next, CurrNode.Data.GetComponentInChildren<SpriteRenderer>().sprite);
            }         
            
            CurrNode.Data.GetComponentInChildren<SpriteRenderer>().sprite = sp;
        }
    }

    static private string NodeGetSprite(DoublyNode<GameObject> Node) {
        return Node.Data.GetComponentInChildren<SpriteRenderer>().sprite.name;
    }
}

public class DoublyNode<T>
{
    public DoublyNode(T data)
    {
        Data = data;
    }
    public T Data { get; set; }
    public DoublyNode<T> Previous { get; set; }
    public DoublyNode<T> Next { get; set; }
}

public class DoublyLinkedList<T> : IEnumerable<T>  // ���������� ������
    {
        public DoublyNode<T> head; // ��������/������ �������
        public DoublyNode<T> tail; // ���������/��������� �������
        public int count = 0;  // ���������� ��������� � ������
 
        // ���������� ��������
        public void Add(T data)
        {
            DoublyNode<T> node = new DoublyNode<T>(data);
 
            if (head == null)
                head = node;
            else
            {
                tail.Next = node;
                node.Previous = tail;
            }
            tail = node;
            count++;
        }
        public void AddFirst(T data)
        {
            DoublyNode<T> node = new DoublyNode<T>(data);
            DoublyNode<T> temp = head;
            node.Next = temp;
            head = node;
            if (count == 0)
                tail = head;
            else
                temp.Previous = node;
            count++;
        }
        // ��������
        public bool Remove(T data)
        {
            DoublyNode<T> current = head;
 
            // ����� ���������� ����
            while (current != null)
            {
                if (current.Data.Equals(data))
                {
                    break;
                }
                current = current.Next;
            }
            if(current!=null)
            {
                // ���� ���� �� ���������
                if(current.Next!=null)
                {
                    current.Next.Previous = current.Previous;
                }
                else
                {
                    // ���� ���������, ����������������� tail
                    tail = current.Previous;
                }
 
                // ���� ���� �� ������
                if(current.Previous!=null)
                {
                    current.Previous.Next = current.Next;
                }
                else
                {
                    // ���� ������, ����������������� head
                    head = current.Next;
                }
                count--;
                return true;
            }
            return false;
        }
 
        public int Count { get { return count; } }
        public bool IsEmpty { get { return count == 0; } }
 
        public void Clear()
        {
            head = null;
            tail = null;
            count = 0;
        }
 
        public DoublyNode<T> Contains(T data)
        {
            DoublyNode<T> current = head;
            while (current != null)
            {
                if (current.Data.Equals(data))
                    return current;
                current = current.Next;
            }
            return null;
        }
         
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }
 
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            DoublyNode<T> current = head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }
 
        public IEnumerable<T> BackEnumerator()
        {
            DoublyNode<T> current = tail;
            while (current != null)
            {
                yield return current.Data;
                current = current.Previous;
            }
        }
    }

