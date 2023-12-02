using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

string t1 = "Text";
string t2 = "Word";
Console.WriteLine("Text 1 : " + t1 + "\tText 2 : " + t2);
Swap<string>(ref t1,ref t2);
Console.WriteLine("Text 1 : " + t1 + "\tText 2 : " + t2 + "\n\n");


PriorityQueue<int> queue = new PriorityQueue<int>();
queue.Add(new Item<int>(20, 1));
queue.Add(new Item<int>(144, 10));
queue.Add(new Item<int>(81, 4));
queue.Add(new Item<int>(100, 7));
queue.Add(new Item<int>(121, 3));
queue.Add(new Item<int>(64, 7));
queue.Add(new Item<int>(169, 11));
queue.Info();


Console.WriteLine("\n\n");
LoopQueue<int> Lqueue = new LoopQueue<int>(6);
Lqueue.Add(1);
Lqueue.Add(144);
Lqueue.Add(9);
Lqueue.Add(441);
Lqueue.Add(64);
Lqueue.Add(256);
Lqueue.Info();
Lqueue.Add(-1000);
Lqueue.Add(-8);
Lqueue.Add(-27);
Lqueue.Info();
Lqueue.Pop();
Lqueue.Pop();
Lqueue.Pop();
Lqueue.Info();


SinglyLinkedList<float> Slist = new SinglyLinkedList<float>();
Slist.Add(1);
Slist.Add(10.1f);
Slist.Add(112.1f);
Slist.Add(13.11f);
Slist.Add(4.111f);
Slist.Add(5.001f);
Slist.Info();


Console.WriteLine("\n\n");
DoublyLinkedList<int> Dlist = new DoublyLinkedList<int>();
Dlist.Add(625);
Dlist.Add(576);
Dlist.Add(676);
Dlist.Add(441);
Dlist.Add(400);
Dlist.Add(361);
Dlist.Info();
Dlist.Reverse();
Console.WriteLine();
Dlist.Info();

static void Swap<T>(ref T value, ref T value2)
{
    T temp = value;
    value = value2;
    value2 = temp;
} 

class Item<T>
{
    private T value;
    public uint Priority { get; set; }

    public Item(T value, uint priority)
    {
        this.value = value;
        Priority = priority;
    }

    public void Info()
    {
        Console.WriteLine($"Value : {value}\tPriority : {Priority}");
    }

    //public Item()
    //{
    //    Priority = 0;
    //}
}

class PriorityQueue<T>
{
    Item<T>[] items;

    public PriorityQueue()
    {
        items = new Item<T>[0];
    }

    public void Add(Item<T> newItem)
    {
        Item<T>[] newItems = new Item<T>[items.Length + 1];
        for (int i = 0, j = 0; i < items.Length; i++, j++) {
            if (items[i].Priority < newItem.Priority && i == j) { newItems[j++] = newItem; }
            newItems[j] = items[i];
        }
        if (items.Length == 0 || items[items.Length - 1].Priority > newItem.Priority) newItems[items.Length] = newItem;
        items = newItems;
    }

    public void Info()
    {
        foreach (Item<T> item in items) item.Info();
    }
}

class LoopQueue<T>
{
    T[] Items;
    int LastIndex;
    int OldestIndex;


    public LoopQueue(int size)
    {
        Items = new T[size];
        LastIndex = -1;
        OldestIndex = -1;
    }

    public void Add(T newItem)
    {
        LastIndex = (LastIndex + 1) % Items.Length;
        OldestIndex = LastIndex == OldestIndex || OldestIndex == -1? (OldestIndex +  1) % Items.Length: OldestIndex;
        Items[LastIndex] = newItem;
    }


    public T Pop()
    {
        
        T item = Items[OldestIndex];
        Items[OldestIndex] = default(T);
        OldestIndex = (OldestIndex + 1) % Items.Length;
        return item;
    }

    public void Info()
    {
        bool check = true;
        for(int i = OldestIndex == -1 ? 0 : OldestIndex; i != OldestIndex || check; i = (i + 1) % Items.Length, check = check == true ? false : false)
        {
            Console.WriteLine($"Value : {Items[i]}");
        }Console.WriteLine();
    }
}

class Node<T>
{
    T Value;
    public Node<T> Next { get; set; }

    public Node(T value, Node<T> link = null)
    {
        Value = value;
        Next = link;
    }

    public void Info()
    {
        Console.WriteLine($"Value : {Value}");
    }
}

class SinglyLinkedList<T>
{
    Node<T> Head;
    public bool IsEmpty { get { return Head == null; } }
    public uint Size { get
        {
            uint size = 0;
            if (!IsEmpty)
            {
                for (Node<T> current = Head; current != null; current = current.Next)
                {
                    size++;
                }
            }
            return size;
        } } 

    public void Add(T value)
    {
        if(!IsEmpty)
        {
            Node<T> current = Head;
            while (current.Next != null) current = current.Next;
            current.Next = new Node<T>(value);
        }
        else Head = new Node<T>(value);
    }    

    public void Info()
    {
        for (Node<T> current = Head; current != null; current = current.Next)current.Info();
    }

}

class DoubleNode<T>
{
    T Value;
    public DoubleNode<T> Previous { get; set; }
    public DoubleNode<T> Next { get; set; }
    public DoubleNode(T value, DoubleNode<T> p = null, DoubleNode<T> n = null) {
        Value = value;
        Previous = p;
        Next = n;
    }

    public void Swap()
    {
        DoubleNode<T> temp = Previous;
        Previous = Next;
        Next = temp;
    }

    public void Info()
    {
        Console.WriteLine($"Value : {Value}");
    }
}

class DoublyLinkedList<T>
{
    DoubleNode<T> Head;
    DoubleNode<T> Tail;

    public bool IsEmpty { get { return Head == null; } }
    public uint Size
    {
        get
        {
            uint size = 0;
            if (!IsEmpty)
            {
                for (DoubleNode<T> current = Head; current != null; current = current.Next)
                {
                    size++;
                }
            }
            return size;
        }
    }

    public DoublyLinkedList() { Head = Tail = null; }

    public void Add(T value)
    {
        if (!IsEmpty) Tail.Next = Tail = new DoubleNode<T>(value, Tail);
        else Head = Tail = new DoubleNode<T>(value);
    }

    private void Swap(ref DoubleNode<T> v1,ref DoubleNode<T> v2)
    {
        DoubleNode<T> current = v1;
        v1 = v2;
        v2 = current;
    }

    public void Reverse()
    {
        Swap(ref Head,ref Tail);
        for (DoubleNode<T> current = Head; current != null; current = current.Next) current.Swap();
    }

    public void Info()
    {
        for (DoubleNode<T> current = Head; current != null; current = current.Next) current.Info();
    }
}