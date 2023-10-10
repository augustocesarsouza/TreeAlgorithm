using System.Runtime.CompilerServices;

namespace BinaryRedBlackTree
{
    public sealed class RedBlackTree<TKey, TValue>
    {
        private readonly RedBlackTreeNode<TKey, TValue> _left = RedBlackTreeNode<TKey, TValue>.CreateLeaf();
        public RedBlackTreeNode<TKey, TValue> Root { get; private set; }

        public RedBlackTree()
        {
            Root = _left;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TValue Get(TKey key)
        {
            try
            {
                int hashedKey = key.GetHashCode();
                RedBlackTreeNode<TKey, TValue> node = Root;
                do
                {
                    if (node.HashedKey == hashedKey)
                        return node.Value;

                    node = hashedKey < node.HashedKey ? node.Left : node.Right;

                } while (true);
            }
            catch (NullReferenceException)
            {
                throw new KeyNotFoundException();
            }
        }

        public void Add(TKey key, TValue value)
        {
            RedBlackTreeNode<TKey, TValue> newNode = RedBlackTreeNode<TKey, TValue>.CreateNode(key, value, RedBlackTreeNode<TKey, TValue>.ColorEnum.Red, key.GetHashCode());
            Insert(newNode);
        }

        public void Insert(RedBlackTreeNode<TKey, TValue> z)
        {
            var y = _left;
            var x = Root;
            while (x != _left)
            {
                y = x;
                x = z.HashedKey < x.HashedKey ? x.Left : x.Right;
            }

            z.Parent = y;
            if (y == _left)
            {//Só primeiro caso vazio
                Root = z;
            }
            else if (z.HashedKey < y.HashedKey)
            {
                y.Left = z;
            }
            else
            {
                y.Right = z;
            }

            z.Left = _left;
            z.Right = _left;
            z.Color = RedBlackTreeNode<TKey, TValue>.ColorEnum.Red;
            InsertFixup(z);
        }

        public void InsertFixup(RedBlackTreeNode<TKey, TValue> z)
        {
            while (z.Parent.Color == RedBlackTreeNode<TKey, TValue>.ColorEnum.Red)
            {
                if (z.Parent == z.Parent.Parent.Left)
                {
                    var y = z.Parent.Parent.Right;
                    if (y.Color == RedBlackTreeNode<TKey, TValue>.ColorEnum.Red)
                    {
                        z.Parent.Color = RedBlackTreeNode<TKey, TValue>.ColorEnum.Black;
                        y.Color = RedBlackTreeNode<TKey, TValue>.ColorEnum.Black;
                        z.Parent.Parent.Color = RedBlackTreeNode<TKey, TValue>.ColorEnum.Red; //Assim que red sai do while
                        z = z.Parent.Parent; // paso o z como parent para sair do while
                    }
                    else
                    {
                        if (z == z.Parent.Right)
                        {//Aqui quando vou virar para esquerda e subir porque está red red e o pai de cima está seem direita e não pode isso
                            z = z.Parent;
                            RotateLeft(z);
                        }

                        z.Parent.Color = RedBlackTreeNode<TKey, TValue>.ColorEnum.Black; // Muda cor do pai para black e os filho vai fica red
                        z.Parent.Parent.Color = RedBlackTreeNode<TKey, TValue>.ColorEnum.Red;
                        RotateRight(z.Parent.Parent);
                    }
                }
                else
                {
                    var y = z.Parent.Parent.Left;
                    if (y.Color == RedBlackTreeNode<TKey, TValue>.ColorEnum.Red)
                    {
                        z.Parent.Color = RedBlackTreeNode<TKey, TValue>.ColorEnum.Black;
                        y.Color = RedBlackTreeNode<TKey, TValue>.ColorEnum.Black;
                        z.Parent.Parent.Color = RedBlackTreeNode<TKey, TValue>.ColorEnum.Red;
                        z = z.Parent.Parent;
                    }
                    else
                    {
                        if (z == z.Parent.Left)
                        {
                            z = z.Parent;
                            RotateRight(z);
                        }

                        z.Parent.Color = RedBlackTreeNode<TKey, TValue>.ColorEnum.Black;
                        z.Parent.Parent.Color = RedBlackTreeNode<TKey, TValue>.ColorEnum.Red;
                        RotateLeft(z.Parent.Parent);
                    }
                }
            }

            Root.Color = RedBlackTreeNode<TKey, TValue>.ColorEnum.Black;
        }

        public void RotateLeft(RedBlackTreeNode<TKey, TValue> x)
        {
            var y = x.Right;
            x.Right = y.Left;
            if (y.Left != _left)
            {
                y.Left.Parent = x;
            }

            y.Parent = x.Parent;
            if (x.Parent == _left)
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

        public void RotateRight(RedBlackTreeNode<TKey, TValue> x)
        {
            var y = x.Left;
            x.Left = y.Right;
            if (y.Right != _left)
            {
                y.Right.Parent = x;
            }

            y.Parent = x.Parent;
            if (x.Parent == _left)
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

            y.Right = x;
            x.Parent = y;
        }
    }
}
