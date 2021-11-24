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
    static private int combo = 0;

    void Start() {
        BallPrefab = _ballPrefab;
        Sprites = _sprites;
    }

    static public void DestroyBalls() {
        combo = 0;
        CheckBall(BallsList.head);
    }

    static private void CheckBall(DoublyNode<GameObject> Node) {
        if(Node.Next != null && Node.Data.GetComponentInChildren<SpriteRenderer>().sprite.name 
            == Node.Next.Data.GetComponentInChildren<SpriteRenderer>().sprite.name) {
            combo++;
            if(combo >= 3) {
                DelBalls(Node);  
            }
        } 
        if(Node.Next != null) {
            CheckBall(Node.Next);
        }
    }
    static private void DelBalls(DoublyNode<GameObject> Node) {
        if(combo > 0) {
             if(Node.Previous != null) {
                Node.Previous.Next = Node.Next != null ? Node.Next.Next : null;
            } else {
                BallsList.head = Node.Next != null ? Node.Next : null;
            }
            Destroy(Node.Data.gameObject);
            combo--;
            DelBalls(Node.Previous);
        }
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
 
        public dynamic  Contains(T data)
        {
            DoublyNode<T> current = head;
            while (current != null)
            {
                if (current.Data.Equals(data))
                    return current;
                current = current.Next;
            }
            return false;
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

