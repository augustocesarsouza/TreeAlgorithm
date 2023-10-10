using System.Runtime.Intrinsics.Arm;

namespace BTree_
{
    public class LeafNode : Node
    {
        public int MaxNumPairs { get; set; } // Armazena o número máximo de pares chave-valor que podem ser mantidos no nó folha.
        public int MinNumPairs { get; set; } // Armazena o número mínimo de pares chave-valor que o nó folha deve conter. Isso é geralmente aproximadamente metade do MaxNumPairs.
        public int NumPairs { get; set; } // Armazena o número atual de pares chave-valor no nó folha.
        public LeafNode LeftSibling { get; set; } // Referências para os nós folhas à esquerda e à direita do nó folha atual, respectivamente.
        public LeafNode RightSibling { get; set; }
        public DictionaryPair[] Dictionary { get; set; } // Uma matriz que armazena os pares chave-valor. Inicialmente mas pode crescer conforme os pares chave-valor são inseridos.

        public LeafNode(int m, DictionaryPair dp)
        {
            MaxNumPairs = m - 1;
            MinNumPairs = (int)(Math.Ceiling(m / 2.0) - 1);
            Dictionary = new DictionaryPair[m];
            NumPairs = 0;
            Insert(dp);
        }

        public LeafNode(int m, DictionaryPair[] dps, InternalNode parent)
        {
            MaxNumPairs = m - 1;
            MinNumPairs = (int)(Math.Ceiling(m / 2.0) - 1);
            Dictionary = dps;
            NumPairs = LinearNullSearch(dps);
            Parent = parent;
        }

        public void Delete(int index) // Remove um par chave-valor do nó folha, dado um índice.
        {
            Dictionary[index] = null;
            NumPairs--;
        }

        public bool Insert(DictionaryPair dp) // Insere um novo par chave-valor no nó folha. Se o nó folha estiver cheio, a inserção falha.
        {
            if (NumPairs == MaxNumPairs)
            {
                return false;
            }
            else
            {
                Dictionary[NumPairs] = dp;
                NumPairs++;
                Array.Sort(Dictionary, 0, NumPairs);

                return true;
            }
        }

        public bool IsDeficient() // Verifica se o nó folha está abaixo do limite mínimo de pares chave-valor.
        {
            return NumPairs < MinNumPairs;
        }

        public bool IsFull() // Verifica se o nó folha está completamente cheio e não pode aceitar mais inserções.
        {
            return NumPairs == MaxNumPairs;
        }

        public bool IsLendable() // Verifica se o nó folha possui mais pares chave-valor do que o mínimo, tornando possível emprestar pares para ele em caso de necessidade.
        {
            return NumPairs > MinNumPairs;
        }

        public bool IsMergeable() // Verifica se o nó folha possui o número mínimo de pares chave-valor, o que significa que ele pode ser fundido com outro nó folha.
        {
            return NumPairs == MinNumPairs;
        }

        public int LinearNullSearch(DictionaryPair[] dps) // Realiza uma pesquisa linear para encontrar o primeiro índice nulo (representando um espaço vazio) na matriz de pares chave-valor.
        {
            for (int i = 0; i < dps.Length; i++)
            {
                if (dps[i] == null)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
