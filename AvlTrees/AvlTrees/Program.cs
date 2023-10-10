using AvlTrees;

var avlTree = new AvlTree();

avlTree.Root = avlTree.InsertNode(avlTree.Root, 33);
avlTree.Root = avlTree.InsertNode(avlTree.Root, 53);
avlTree.Root = avlTree.InsertNode(avlTree.Root, 13);
avlTree.Root = avlTree.InsertNode(avlTree.Root, 21);
avlTree.Root = avlTree.InsertNode(avlTree.Root, 61);
avlTree.Root = avlTree.InsertNode(avlTree.Root, 9);
avlTree.Root = avlTree.InsertNode(avlTree.Root, 11);
avlTree.Root = avlTree.InsertNode(avlTree.Root, 8);

avlTree.Root = avlTree.DeleteNode(avlTree.Root, 13);

Console.WriteLine(avlTree);
