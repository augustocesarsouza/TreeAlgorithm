namespace SegundaArvore
{
    public class BinaryTree
    {
        public int? Valor { get; set; }
        public BinaryTree? LeftChild { get; set; }
        public BinaryTree? RightChild { get; set; }

        public BinaryTree(int? valor)
        {
            Valor = valor;
            LeftChild = null;
            RightChild = null;
        }

        public void InsertLeft(int value)
        {
            if (LeftChild == null)
            {
                LeftChild = new BinaryTree(value);
            }
            else
            {
                var newNode = new BinaryTree(value);
                newNode.LeftChild = LeftChild;
                LeftChild = newNode;
            }
        }

        public void InsertRight(int value)
        {
            if (RightChild == null)
            {
                RightChild = new BinaryTree(value);
            }
            else
            {
                var newNode = new BinaryTree(value);
                newNode.RightChild = RightChild;
                RightChild = newNode;
            }
        }

        public void PreOrder()
        {
            Console.WriteLine(Valor);

            if (LeftChild != null)
                LeftChild.PreOrder();

            if (RightChild != null)
                RightChild.PreOrder();
        }

        public void InOrder()
        {
            if (LeftChild != null)
                LeftChild.InOrder();

            Console.WriteLine(Valor);

            if (RightChild != null)
                RightChild.InOrder();
        }

        public void PostOrder()
        {
            if (LeftChild != null)
                LeftChild.PostOrder();

            if (RightChild != null)
                RightChild.PostOrder();

            Console.WriteLine(Valor);
        }

        public void Bfs(BinaryTree head)
        {
            var queue = new Queue<BinaryTree>();
            queue.Enqueue(head);

            while (queue.Count > 0)
            {
                var nodeAtual = queue.Dequeue();
                Console.WriteLine(nodeAtual.Valor);

                if (nodeAtual.LeftChild != null)
                    queue.Enqueue(nodeAtual.LeftChild);

                if (nodeAtual.RightChild != null)
                    queue.Enqueue(nodeAtual.RightChild);
            }
        }

        public void InsertNode(int valor)
        {
            if (valor <= Valor && LeftChild != null)
            {
                LeftChild.InsertNode(valor);
            }
            else if (valor <= Valor)
            {
                LeftChild = new BinaryTree(valor);
            }
            else if (valor > Valor && RightChild != null)
            {
                RightChild.InsertNode(valor);
            }
            else
            {
                RightChild = new BinaryTree(valor);
            }
        }

        public int FindNode(int valor)
        {
            if (valor < Valor && LeftChild != null)
            {
                LeftChild.FindNode(valor);
            }
            else if (valor > Valor && RightChild != null)
            {
                RightChild.FindNode(valor);
            }
            else if (valor == Valor)
            {
                return valor;
            }

            return -1;
        }

        public bool RemoveNode(BinaryTree self, int? value, BinaryTree parent)
        {
            if (value < self.Valor && self.LeftChild != null)
            {
                return self.LeftChild.RemoveNode(self.LeftChild, value, self);
            }
            else if (value < self.Valor)
            {
                return false;
            }
            if (value > self.Valor && self.RightChild != null)
            {
                return self.RightChild.RemoveNode(self.RightChild, value, self);
            }
            else if (value > self.Valor)
            {
                return false;
            }
            else
            {
                if (self.LeftChild == null && self.RightChild == null && self == parent.LeftChild)
                {
                    parent.LeftChild = null;
                    self.CleanNode(self);
                }
                else if (self.RightChild == null && self.RightChild == null && self == parent.RightChild)
                {
                    parent.RightChild = null;
                    self.CleanNode(self);
                }
                else if (self.LeftChild != null && self.RightChild == null && self == parent.LeftChild)
                {
                    parent.LeftChild = self.LeftChild;
                    self.CleanNode(self);
                }
                else if (self.LeftChild != null && self.RightChild == null && self == parent.RightChild)
                {
                    parent.RightChild = self.LeftChild;
                    self.CleanNode(self);
                }
                else if (self.RightChild != null && self.LeftChild == null && self == parent.LeftChild)
                {
                    parent.LeftChild = self.RightChild;
                    self.CleanNode(self);
                }
                else if (self.RightChild != null && self.LeftChild == null && self == parent.RightChild)
                {
                    parent.RightChild = self.RightChild;
                    self.CleanNode(self);
                }
                else
                {
                    int? minValu = self.RightChild.FindMinimumValue();
                    self.Valor = m  inValu;
                    self.RightChild.RemoveNode(self.RightChild, minValu, self);
                }
                return true;
            }
        }

        public int? FindMinimumValue()
        {
            if(LeftChild != null)
            {
                return LeftChild.FindMinimumValue();
            }
            else 
            {
                return Valor;
            }
        }

        public void CleanNode(BinaryTree node)
        {
            node.Valor = null;
            node.LeftChild = null;
            node.RightChild = null;
        }
    }
}
