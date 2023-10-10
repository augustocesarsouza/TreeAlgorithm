namespace BTree_
{
    public class BPlusTree
    {
        public  int M { get; set; } // Ordem da árvore B+
        public  InternalNode Root { get; set; } // root tem pelo menos dois filhos
        public  LeafNode FirstLeaf { get; set; } // Primeira folha da árvore B+

        public BPlusTree(int m)
        {
            M = m;
            Root = null;
        }

        public void Insert(int key, double value)
        {
            if (IsEmpty())
            {
                // Se a árvore estiver vazia, cria uma nova folha e a define como a primeira folha.
                LeafNode ln = new LeafNode(M, new DictionaryPair(key, value));

                FirstLeaf = ln;
            }
            else
            {
                // Caso contrário, encontre a folha apropriada para inserir o novo par chave-valor.
                LeafNode ln = (Root == null) ? FirstLeaf : FindLeafNode(key);

                if (!ln.Insert(new DictionaryPair(key, value)))
                {
                    // Se a folha estiver cheia após a inserção, realize uma divisão.
                    ln.Dictionary[ln.NumPairs] = new DictionaryPair(key, value);
                    ln.NumPairs++;
                    SortDictionary(ln.Dictionary);

                    int midPoint = GetMidpoint();
                    var halfDict = SplitDictionary(ln, midPoint);

                    if (ln.Parent == null)
                    {
                        // Se a folha não tem pai, cria um novo nó interno como raiz da árvore.
                        var parentKeys = new int[M];
                        parentKeys[0] = halfDict[0].Key;
                        InternalNode parent = new InternalNode(M, parentKeys);
                        ln.Parent = parent;
                        parent.AppendChildPointer(ln);
                    }
                    else
                    {
                        // Caso contrário, atualize a chave do pai e insira a nova folha no pai.
                        int newParentKey = halfDict[0].Key;
                        ln.Parent.Keys[ln.Parent.Degree - 1] = newParentKey;
                        Array.Sort(ln.Parent.Keys, 0, ln.Parent.Degree);
                    }

                    // Crie uma nova folha para a metade da direita e insira-a no lugar apropriado.
                    LeafNode newLeafNode = new LeafNode(M, halfDict, ln.Parent);
                    int pointerIndex = ln.Parent.FindIndexOfPointer(ln) + 1;
                    ln.Parent.InsertChildPointer(newLeafNode, pointerIndex);

                    // Atualize os apontadores dos irmãos e pai conforme necessário.
                    newLeafNode.RightSibling = ln.RightSibling;
                    if (newLeafNode.RightSibling != null)
                    {
                        newLeafNode.RightSibling.LeftSibling = newLeafNode;
                    }
                    ln.RightSibling = newLeafNode;
                    newLeafNode.LeftSibling = ln;

                    if (Root == null)
                    {
                        // Se a árvore estava vazia antes, atualize a raiz.
                        Root = ln.Parent;
                    }
                    else
                    {
                        InternalNode inNode = ln.Parent;
                        while (inNode != null)
                        {
                            // Verifique se o nó interno está acima do limite de capacidade e, se necessário, realize uma divisão.
                            if (inNode.IsOverFull())
                            {
                                SplitInternalNode(inNode);
                            }
                            else
                            {
                                break;
                            }
                            inNode = inNode.Parent;
                        }
                    }
                }
            }
        }

        private LeafNode FindLeafNode(int key)
        {
            var keys = Root.Keys;
            int i;

            for (i = 0; i < Root.Degree - 1; i++)
            {
                if (key < keys[i])
                {
                    break;
                }
            }

            var child = Root.ChildPointers[i];
            if (child is LeafNode leafNode)
            {
                return leafNode;
            }
            else
            {
                return FindLeafNode((InternalNode)child, key);
            }
        }

        private LeafNode FindLeafNode(InternalNode node, int key)
        {
            var keys = node.Keys;
            int i;

            for (i = 0; i < node.Degree - 1; i++)
            {
                if (key < keys[i])
                {
                    break;
                }
            }

            var childNode = node.ChildPointers[i];
            if (childNode is LeafNode leafNode)
            {
                return leafNode;
            }
            else
            {
                return FindLeafNode((InternalNode)node.ChildPointers[i], key);
            }
        }

        private void SplitInternalNode(InternalNode inNode)
        {
            var parent = inNode.Parent;

            int midPoint = GetMidpoint();
            int newParentKey = inNode.Keys[midPoint];
            var halfKeys = SplitKeys(inNode.Keys, midPoint);
            var halfPointers = SplitChildPointers(inNode, midPoint);

            inNode.Degree = LinearNullSearch(inNode.ChildPointers);

            InternalNode sibling = new InternalNode(M, halfKeys, halfPointers);
            foreach (Node pointer in halfPointers)
            {
                if (pointer != null)
                {
                    pointer.Parent = sibling;
                }
            }

            sibling.RightSibling = inNode.RightSibling;
            if(sibling.RightSibling != null)
            {
                sibling.RightSibling.LeftSibling = sibling;
            }
            inNode.RightSibling = sibling;
            sibling.LeftSibling = inNode;

            if(parent == null)
            {
                var keys = new int[M];
                keys[0] = newParentKey;
                InternalNode newRoot = new InternalNode(M, keys);
                newRoot.AppendChildPointer(inNode);
                newRoot.AppendChildPointer(sibling);
                Root = newRoot;

                inNode.Parent = newRoot;
                sibling.Parent = newRoot;
            }
            else
            {
                parent.Keys[parent.Degree - 1] = newParentKey;
                Array.Sort(parent.Keys, 0, parent.Degree);

                int pointerIndex = parent.FindIndexOfPointer(inNode) + 1;
                parent.InsertChildPointer(sibling, pointerIndex);
                sibling.Parent = parent;
            }
        }

        public int[] SplitKeys(int[] keys, int split)
        {
            int[] halfKeys = new int[M];

            keys[split] = 0; // erra null tem que ver isso - // Define para algum valor padrão (por exemplo, 0) em vez de null.

            for (int i = split + 1; i < keys.Length; i++)
            {
                halfKeys[i - split - 1] = keys[i];
                keys[i] = 0; // erra null tem que ver isso - // Define para algum valor padrão (por exemplo, 0) em vez de null.
            }

            return halfKeys;
        }

        private DictionaryPair[] SplitDictionary(LeafNode ln, int split)
        {
            DictionaryPair[] dictionary = ln.Dictionary;

            DictionaryPair[] halfDict = new DictionaryPair[M];

            for (int i = split; i < dictionary.Length; i++)
            {
                halfDict[i - split] = dictionary[i];
                ln.Delete(i);
            }

            return halfDict;
        }

        private Node[] SplitChildPointers(InternalNode inNode, int split)
        {
            Node[] pointers = inNode.ChildPointers;
            Node[] halfPointers = new Node[M + 1];

            for (int i = split + 1; i < pointers.Length; i++)
            {
                halfPointers[i - split - 1] = pointers[i];
                inNode.RemovePointer(i);
            }

            return halfPointers;
        }

        private int LinearNullSearch(Node[] pointers)
        {
            for (int i = 0; i < pointers.Length; i++)
            {
                if (pointers[i] == null)
                {
                    return i;
                }
            }
            return -1;
        }

        private int GetMidpoint()
        {
            return (int)Math.Ceiling((M + 1) / 2.0) - 1;
        }

        private void SortDictionary(DictionaryPair[] dictionary)
        {
            Array.Sort(dictionary, new DictionaryPairComparer());
        }

        private bool IsEmpty()
        {
            return FirstLeaf == null;
        }
    }
}
