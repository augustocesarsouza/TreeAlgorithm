using BTree_;

BPlusTree bpt = new BPlusTree(3);
bpt.Insert(5, 5);
bpt.Insert(15, 15);
bpt.Insert(25, 25);
bpt.Insert(35, 35);
bpt.Insert(45, 45);

Console.WriteLine(bpt);