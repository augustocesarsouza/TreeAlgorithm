namespace AvlTrees
{
    public class AvlTree
    {
        public Node Root { get; set; }

        public int Height(Node n)
        {
            if (n == null)
                return -1;

            return n.Height;
        }

        public int max(int a, int b)
        {
            return a > b ? a : b;
        }

        public int GetBalanceFactor(Node n)
        {
            if (n == null)
                return 0;

            return Height(n.Left) - Height(n.Right);
        }

        public Node InsertNode(Node node, int item)
        {
            // Find the position and insert the node
            if (node == null)
                return new Node(item);
            if (item < node.Item)
                node.Left = InsertNode(node.Left, item);
            else if (item > node.Item)
                node.Right = InsertNode(node.Right, item);
            else
                return node;

            // Update The balance factor of each node
            // And, balance the tree
            node.Height = 1 + max(Height(node.Left), Height(node.Right)); // Apartir da maior altura consigo calcula a HEIGHT do node
            int balanceFactor = GetBalanceFactor(node);
            if (balanceFactor > 1)
            {
                if (item < node.Left.Item)
                    return RightRotate(node);
                else if (item > node.Left.Item)
                {
                    node.Left = LeftRotate(node.Left);
                    return RightRotate(node);
                }
            }
            if (balanceFactor < -1)
            {
                if (item > node.Right.Item)
                    return LeftRotate(node);
                else if (item < node.Right.Item)
                {
                    node.Right = RightRotate(node.Right);
                    return LeftRotate(node);
                }
            }
            return node;
        }

        public Node RightRotate(Node y)
        {
            Node x = y.Left; // Nova Raiz  
            Node T2 = x.Right;

            x.Right = y;
            y.Left = T2;

            y.Height = max(Height(y.Left), Height(y.Right)) + 1;
            x.Height = max(Height(x.Left), Height(x.Right)) + 1;
            return x;
        }

        public Node LeftRotate(Node x)
        {
            Node y = x.Right;//nova Raiz
            Node T2 = y.Left;

            y.Left = x;
            x.Right = T2;

            x.Height = max(Height(x.Left), Height(x.Right)) + 1;
            y.Height = max(Height(y.Left), Height(y.Right)) + 1;
            return y;
        }

        public Node DeleteNode(Node root, int item)
        {
            // Find The node to be deleted and remove it
            if (root == null)
                return root;
            if (item < root.Item)
                root.Left = DeleteNode(root.Left, item);
            else if (item > root.Item)
                root.Right = DeleteNode(root.Right, item);
            else
            {
                if ((root.Left == null) || (root.Right == null))
                {
                    Node temp = null;
                    if (temp == root.Left)
                        temp = root.Right;
                    else
                        temp = root.Left;
                    if (temp == null)
                    {
                        temp = root;
                        root = null;
                    }
                    else
                        root = temp;
                }
                else
                {
                    Node temp = NodeWithMinimumValue(root.Right);
                    root.Item = temp.Item;
                    root.Right = DeleteNode(root.Right, temp.Item);
                }
            }
            if (root == null)
                return root;

            // Update the balance factor of each node and balance the tree
            root.Height = max(Height(root.Left), Height(root.Right)) + 1;
            int balanceFactor = GetBalanceFactor(root);
            if(balanceFactor > 1)
            {
                if (GetBalanceFactor(root.Left) >= 0)
                    return RightRotate(root);
                else
                {
                    root.Left = LeftRotate(root.Left);
                    return RightRotate(root);
                }
            }

            if(balanceFactor < -1)
            {
                if(GetBalanceFactor(root.Right) <= 0)
                    return LeftRotate(root);
                else
                {
                    root.Right = RightRotate(root.Right);
                    return LeftRotate(root);
                }
            }

            return root;
        }

        public Node NodeWithMinimumValue(Node node)
        {
            Node current = node;
            while(current.Left != null)
                current = current.Left;

            return current;
        }
    }
}
