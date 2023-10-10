using SegundaArvore;

var binary = new BinaryTree(5);

binary.InsertNode(10);
binary.InsertNode(15);
binary.InsertNode(11);
binary.InsertNode(18);
binary.InsertNode(7);
binary.InsertNode(6);
binary.InsertNode(3);
binary.InsertNode(1);
binary.InsertNode(4);

binary.RemoveNode(binary, 10, binary);

Console.WriteLine(binary.Valor);