namespace BinaryRedBlackTree
{
    public sealed class RedBlackTreeNode<Tkey, TValue>
    {
        public enum ColorEnum 
        {
            Red,
            Black,
        };

        public readonly TValue Value;

        public readonly Tkey Key;

        public readonly bool IsLeaf;

        public readonly int HashedKey;

        public ColorEnum Color;

        public RedBlackTreeNode<Tkey, TValue> Parent;

        public RedBlackTreeNode<Tkey, TValue> Left;

        public RedBlackTreeNode<Tkey, TValue> Right;

        public static RedBlackTreeNode<Tkey, TValue> CreateLeaf()
        {
            return new RedBlackTreeNode<Tkey, TValue>();
        }

        public static RedBlackTreeNode<Tkey, TValue> CreateNode(Tkey key, TValue value, ColorEnum color, int hashedKey)
        {
            return new RedBlackTreeNode<Tkey, TValue>(key, value, color, hashedKey);
        }

        public RedBlackTreeNode(Tkey key, TValue value, ColorEnum color, int hashedKey)
        {
            Key = key;
            HashedKey = hashedKey;
            Color = color;
            Value = value;
        }

        public RedBlackTreeNode()
        {
            IsLeaf = true;
            Color = ColorEnum.Black;
            HashedKey = 0;
        }

        public RedBlackTreeNode<Tkey, TValue> Grandparent => Parent?.Parent;

        public RedBlackTreeNode<Tkey, TValue> Sibling =>
            Parent == null ? null : Parent.Left == this ? Parent.Right : Parent.Left;

        public RedBlackTreeNode<Tkey, TValue> Uncle => Parent?.Sibling;
    }
}
