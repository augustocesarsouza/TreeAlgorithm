namespace RedBlackTreeNotDel
{
    public class RedBlackTreee
    {
        private Node Root { get; set; }
        private Node TNULL { get; set; }

        public RedBlackTreee()
        {
            TNULL = new Node();
            TNULL.Color = 0;
            TNULL.Left = null;
            TNULL.Right = null;
            Root = TNULL;
        }

        public Node Get(int registration)
        {
            var node = Root;
            do
            {
                if (registration == node.Data)
                    return node;

                if (registration < node.Data)
                {
                    node = node.Left;
                }
                else
                {
                    node = node.Right;
                }

            } while (true);
        }

        public void Adv()
        {
            preOrderHelper(Root);
        }

        public void preOrderHelper(Node node)
        {
            if (node != TNULL)
            {
                preOrderHelper(node.Left);
                Console.WriteLine(node.Student.Registration + " ");
                preOrderHelper(node.Right);
            }
        }

        public void Insert(
            int key
            // Student student
            )
        {
            Node node = new Node();
            node.Parent = null;
            node.Data = key;
            node.Left = TNULL;
            node.Right = TNULL;
            node.Color = 1;
            //node.Student = student;

            Node y = null;
            Node x = Root;

            while (x != TNULL)
            {

                y = x;
                if (node.Data < x.Data)
                    x = x.Left;
                else
                    x = x.Right;
            }

            node.Parent = y;
            if (y == null)
            {
                Root = node;
            }
            else if (node.Data < y.Data)
            {
                y.Left = node;
            }
            else
            {
                y.Right = node;
            }

            if (node.Parent == null)
            {
                node.Color = 0;
                return;
            }

            if (node.Parent.Parent == null)
                return;

            FixInsert(node);

        }

        public void FixInsert(Node k)
        {
            Node u;
            while (k.Parent.Color == 1)
            {
                if (k.Parent == k.Parent.Parent.Left)
                {
                    u = k.Parent.Parent.Right;
                    if (u.Color == 1)
                    {
                        u.Color = 0;
                        k.Parent.Color = 0;
                        k.Parent.Parent.Color = 1;
                        k = k.Parent.Parent;
                    }
                    else
                    {
                        if (k == k.Parent.Right)
                        {
                            k = k.Parent;
                            LeftRotate(k);
                        }

                        k.Parent.Color = 0;
                        k.Parent.Parent.Color = 1;
                        RightRotate(k.Parent.Parent);
                    }
                }
                else
                {
                    u = k.Parent.Parent.Left;

                    if (u.Color == 1)
                    {
                        u.Color = 0;
                        k.Parent.Color = 0;
                        k.Parent.Parent.Color = 1;
                        k = k.Parent.Parent;
                    }
                    else
                    {
                        if (k == k.Parent.Left)
                        {
                            k = k.Parent;
                            RightRotate(k);
                        }

                        k.Parent.Color = 0;
                        k.Parent.Parent.Color = 1;
                        LeftRotate(k.Parent.Parent);
                    }
                }
                if (k == Root)
                    break;
            }

            Root.Color = 0;
        }

        public void LeftRotate(Node x)
        {
            var y = x.Right;
            x.Right = y.Left;
            if (y.Left != TNULL)
            {
                y.Left.Parent = x;
            }

            y.Parent = x.Parent;
            if (x.Parent == null)
            {
                Root = y;
            }
            else if (x == x.Parent.Left)
            {
                x.Parent.Left = y;
            }
            else
            {
                x.Parent.Right = y;
            }

            y.Left = x;
            x.Parent = y;
        }

        public void RightRotate(Node x)
        {
            var y = x.Left;
            x.Left = y.Right;
            if (y.Right != TNULL)
            {
                y.Right.Parent = x;
            }

            y.Parent = x.Parent;
            if (x.Parent == null)
            {
                Root = y;
            }
            else if (x == x.Parent.Right)
            {
                x.Parent.Right = y;
            }
            else if (x == x.Parent.Left)
            {
                x.Parent.Left = y;
            }

            y.Right = x;
            x.Parent = y;
        }

        public void DeleteNodeHelper(Node node, int key)
        {
            Node z = TNULL;
            Node x, y;
            while (node != TNULL)
            {
                if (node.Data == key)
                {
                    z = node;
                    break;
                }

                if (node.Data <= key)
                    node = node.Right;
                else
                    node = node.Left;
            }

            if (z == TNULL)
            {
                Console.WriteLine("Couldn't find key in the tree");
                return;
            }

            y = z;
            int yOriginalColor = y.Color;
            if (z.Left == TNULL)
            {
                x = z.Right;
                RbTransplant(z, z.Right);
            }
            else if (z.Right == TNULL)
            {
                x = z.Left;
                RbTransplant(z, z.Left);
            }
            else
            {
                y = Minimum(z.Right);
                yOriginalColor = y.Color;
                x = y.Right;
                if (y.Parent == z)
                    x.Parent = y;
                else
                {
                    RbTransplant(y, y.Right);
                    y.Right = z.Right;
                    y.Right.Parent = y;
                }

                RbTransplant(z, y);
                y.Left = z.Left;
                y.Left.Parent = y;
                y.Color = z.Color;
            }

            if (yOriginalColor == 0)
                FixDelete(x);
        }

        public void FixDelete(Node x)
        {
            Node s;
            while (x != Root && x.Color == 0)
            {
                if (x == x.Parent.Left)
                {
                    s = x.Parent.Right;
                    if (s.Color == 1)
                    {
                        s.Color = 0;
                        x.Parent.Color = 1;
                        LeftRotate(x.Parent);
                        s = x.Parent.Right;
                    }

                    if (s.Left.Color == 0 && s.Right.Color == 0)
                    {
                        s.Color = 1;
                        x = x.Parent;
                    }
                    else
                    {
                        if (s.Right.Color == 0)
                        {
                            s.Left.Color = 0;
                            s.Color = 1;
                            RightRotate(s);
                            s = x.Parent.Right;
                        }

                        s.Color = x.Parent.Color;
                        x.Parent.Color = 0;
                        s.Right.Color = 0;
                        LeftRotate(x.Parent);
                        x = Root;
                    }
                }
                else
                {
                    s = x.Parent.Left;
                    if (s.Color == 1)
                    {
                        s.Color = 0;
                        x.Parent.Color = 1;
                        RightRotate(x.Parent);
                        s = x.Parent.Left;
                    }

                    if (s.Right.Color == 0 && s.Right.Color == 0)
                    {
                        s.Color = 1;
                        x = x.Parent;
                    }
                    else
                    {
                        if (s.Left.Color == 0)
                        {
                            s.Right.Color = 0;
                            s.Color = 1;
                            LeftRotate(s);
                            s = x.Parent.Left;
                        }

                        s.Color = x.Parent.Color;
                        x.Parent.Color = 0;
                        s.Left.Color = 0;
                        RightRotate(x.Parent);
                        x = Root;
                    }
                }
            }
            x.Color = 0;
        }

        public void DeleteNode(int data)
        {
            DeleteNodeHelper(Root, data);
        }

        public void RbTransplant(Node u, Node v)
        {
            if (u.Parent == null)
                Root = v;
            else if (u == u.Parent.Left)
                u.Parent.Left = v;
            else
                u.Parent.Right = v;

            v.Parent = u.Parent;
        }

        public Node Minimum(Node node)
        {
            while (node.Left != TNULL)
                node = node.Left;

            return node;
        }

        public Node Maximum(Node node)
        {
            while (node.Right != TNULL)
                node = node.Right;

            return node;
        }
    }
}
