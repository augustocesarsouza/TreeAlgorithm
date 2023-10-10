using System.Drawing;
using System.Numerics;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics.X86;
using System;

namespace BTree_
{
    public class DictionaryPair : IComparable<DictionaryPair>
    {
        public int Key { get; set; }
        public double Value { get; set; }

        public DictionaryPair(int key, double value)
        {
            Key = key;
            Value = value;
        }

        public int CompareTo(DictionaryPair o)
        {
            if (Key == o.Key)
            {
                return 0;
            }
            else if (Key > o.Key)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
        // Key: A chave é usada para identificar exclusivamente um valor na árvore B+. 
        // Cada valor que você insere na árvore deve ter uma chave associada a ele.
        // A chave deve ser um valor inteiro que é usado para organizar os dados na árvore de forma ordenada. 
        // Na sua árvore B+, as chaves são usadas para determinar a posição de um valor na árvore e permitir a busca eficiente.

        // Value: O valor é a informação que você deseja armazenar na árvore B+.
        // No contexto de uma árvore B+ usada como estrutura de dados, esse valor pode ser qualquer tipo de dado, 
        // como um número, uma string ou um objeto complexo.No seu código, o valor é um número de ponto flutuante (double),
        // mas em outras aplicações, ele poderia ser de um tipo diferente.
    }
}
