namespace BTree_
{
    public class InternalNode : Node
    {
        public int MaxDegree { get; set; } // Armazena a ordem máxima (máximo número de chaves) para o nó interno.
        public int MinDegree { get; set; } // Armazena a ordem mínima (mínimo número de chaves) para o nó interno. Geralmente, é aproximadamente metade do MaxDegree.
        public int Degree { get; set; } // Armazena o número atual de chaves no nó interno.
        public InternalNode LeftSibling { get; set; } // Referências para os nós irmãos à esquerda e à direita do nó interno, respectivamente.
        public InternalNode RightSibling { get; set; } // // Referências para os nós irmãos à esquerda e à direita do nó interno, respectivamente.
        public int[] Keys { get; set; } // Uma matriz que armazena as chaves no nó interno.
        public Node[] ChildPointers { get; set; } // Uma matriz que armazena os ponteiros para os nós filhos (sejam eles nós internos ou folhas).

        public void AppendChildPointer(Node Pointer) // Adiciona um ponteiro para um nó filho ao final da matriz de ponteiros.
        {
            ChildPointers[Degree] = Pointer;
            Degree++;
        }

        public int FindIndexOfPointer(Node pointer) // Encontra o índice de um determinado ponteiro na matriz de ponteiros.
        {
            for(int i = 0; i < ChildPointers.Length; i++)
            {
                if (ChildPointers[i] == pointer)
                {
                    return i;
                }
            }
            return -1;
        }

        public void InsertChildPointer(Node pointer, int index) // Insere um ponteiro para um nó filho em uma posição específica na matriz de ponteiros.
        {
            for(int i = Degree - 1; i >= index; i--)
            {
                ChildPointers[i + 1] = ChildPointers[i];
            }

            ChildPointers[index] = pointer;
            Degree++;
        }

        public bool IsDeficient() // Verifica se o nó interno está abaixo do limite mínimo de chaves.
        {
            return Degree < MinDegree;
        }

        public bool IsLendable() //  Verifica se o nó interno possui mais chaves do que o mínimo, tornando possível emprestar chaves para ele em caso de necessidade.
        {
            return Degree > MinDegree;
        }

        public bool IsMergeable() // Verifica se o nó interno possui o número mínimo de chaves, o que significa que ele pode ser fundido com outro nó.
        {
            return Degree == MinDegree;
        }

        public bool IsOverFull() // Verifica se o nó interno está com mais chaves do que o permitido, o que ocorre após uma divisão.
        {
            return Degree == MaxDegree + 1;
        }

        public void PrependChildPointer(Node pointer) // Insere um ponteiro para um nó filho no início da matriz de ponteiros.
        {
            for(int i = Degree - 1; i >= 0; i--)
            {
                ChildPointers[i + 1] = ChildPointers[i];
            }
            ChildPointers[0] = pointer;
            Degree++;
        }

        public void RemoveKey(int index)
        {
            Keys[index] = 0;
        }
        // RemoveKey e RemovePointer: São utilizados para remover chaves e ponteiros do nó interno.
        public void RemovePointer(int index)
        {
            ChildPointers[index] = null;
            Degree--;
        }

        public void RemovePointer(Node pointer) 
        {
            for(int i = 0; i < ChildPointers.Length; i++)
            {
                if (ChildPointers[i] == pointer)
                {
                    ChildPointers[i] = null;
                }
            }
            Degree--;
        }

        public InternalNode(int m, int[] keys)
        {
            MaxDegree = m;
            MinDegree = (int)Math.Ceiling(m / 2.0);
            Degree = 0;
            Keys = keys;
            ChildPointers = new Node[MaxDegree + 1];
        }

        public InternalNode(int m, int[] keys, Node[] pointers)
        {
            MaxDegree = m;
            MinDegree = (int)Math.Ceiling(m / 2.0);
            Degree = LinearNullSearch(pointers);
            Keys = keys;
            ChildPointers = pointers;
        }

        public int LinearNullSearch(Node[] pointers) // Realiza uma pesquisa linear para encontrar o primeiro índice nulo (representando um espaço vazio) na matriz de ponteiros.
        {
            for(int i = 0; i < pointers.Length; i++)
            {
                if (pointers[i] == null)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
