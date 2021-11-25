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
    public GameObject OverMenu;
    static public GameObject Over;
    public GameObject WinMenu;
    static public GameObject Win;

    void Awake() {
        BallsList = new DoublyLinkedList<GameObject>();
        Over = OverMenu;
        Win = WinMenu;
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

     static public float DistanceToPrev(DoublyNode<GameObject> curr) {
        if (curr.Previous != null) {
            return Vector2.Distance(curr.Data.transform.position, curr.Previous.Data.transform.position);
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

    static public void DestroyBalls() {
        DoublyNode<GameObject> Node = BallsList.head;
        int combo = 0;
        string lastSprite = "";
        while(Node.Next != null) {
            if (lastSprite == Node.Data.GetComponentInChildren<SpriteRenderer>().sprite.name) {
                combo++;
            } else {
                if (combo >= 2) {
                    DoublyNode<GameObject> tmp = Node.Previous;
                    for (int i = 0; i <= combo; i++) {
                        Destroy(tmp.Data.gameObject);
                        BallsList.Remove(tmp.Data);
                        tmp = tmp.Previous;
                    } 
                    tmp = Node;
                    while (tmp != null) {
                        tmp.Data.SendMessage("GoBack");
                        tmp = tmp.Previous;
                    }
                    audios[1].Play();
                } 
                combo = 0;
            }
            lastSprite = Node.Data.GetComponentInChildren<SpriteRenderer>().sprite.name;
            Node = Node.Next;
        }
        if(combo >= 2) {
            DoublyNode<GameObject> tmp = Node.Previous;
            for (int i = 0; i <= combo; i++) {
                Destroy(tmp.Data.gameObject);
                BallsList.Remove(tmp.Data);
                tmp = tmp.Previous;
            } 
            tmp = Node;
            while (tmp != null) {
                tmp.Data.SendMessage("GoBack");
                tmp = tmp.Previous;
            }
            audios[1].Play();
        }
        if (BallsList.count <= 1) {
            GameWin();
        }
    }

    static public void GameOver() {
        Time.timeScale = 0;
        Over.SetActive(true);
    }

    static public void GameWin() {
        GameObject.Destroy(BallsList.head.Data);
        Time.timeScale = 0;
        Win.SetActive(true);
    }

    static public void ChangeColors(DoublyNode<GameObject> Node, Sprite sp) {
        if (Node.Next != null) {
            ChangeColors(Node.Next, Node.Data.GetComponentInChildren<SpriteRenderer>().sprite);
        } else {
            GameObject spawner = GameObject.Find("Spawner");
            spawner.GetComponent<Spawner>().SpawnFromController(Node.Data.GetComponentInChildren<SpriteRenderer>().sprite);  
        } 
        Node.Data.GetComponentInChildren<SpriteRenderer>().sprite = sp;
    }

    static public void ChangeColors(GameObject curr, Sprite sp) {
        audios[0].Play();
        DoublyNode<GameObject> CurrNode = BallsList.Contains(curr);
        if (CurrNode is DoublyNode<GameObject>) {
            if (CurrNode.Next != null) {
                ChangeColors(CurrNode.Next, CurrNode.Data.GetComponentInChildren<SpriteRenderer>().sprite);
            } else {
                GameObject spawner = GameObject.Find("Spawner");
                spawner.GetComponent<Spawner>().SpawnFromController(CurrNode.Data.GetComponentInChildren<SpriteRenderer>().sprite);  
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

