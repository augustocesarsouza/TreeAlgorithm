using BTree;

var b = new BTreeClass(2);

b.Insert(10);
b.Insert(5);
b.Insert(15);
b.Insert(20);
b.Insert(17);
b.Insert(18);

b.Insert(1);

b.Remove(15);

Console.WriteLine(b);