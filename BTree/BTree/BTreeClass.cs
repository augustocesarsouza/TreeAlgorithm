namespace BTree
{
    public class BTreeClass
    {
        public static int T { get; set; }
        public Node Root { get; set; }

        public BTreeClass(int t)
        {
            T = t;
            Root = new Node();
            Root.n = 0;
            Root.Leaf = true;
        }

        private void InsertValuee(Node x, int k)
        {
            if (x.Leaf) // Se não é Leaf quer dizer que está no Root tem que achar em que filho vai ficar
            {
                // Se o nó x é uma folha, insira a chave k neste nó.
                int i = 0;
                // Encontre a posição correta para a inserção da chave k mantendo a ordenação.
                for (i = x.n - 1; i >= 0 && k < x.Key[i]; i--)
                {
                    x.Key[i + 1] = x.Key[i];
                }

                // Inserir a chave k na posição correta.
                x.Key[i + 1] = k;

                // Atualizar o número de chaves no nó.
                x.n = x.n + 1;
            }
            else
            {
                // Se o nó x não é uma folha, continue a inserção em seus filhos.
                int i = 0;

                // Encontre o filho correto para continuar a inserção com base no valor da chave k.
                for (i = x.n - 1; i >= 0 && k < x.Key[i]; i--)
                {
                };
                // Obter o filho apropriado. para inserir se for maior ou menor
                i++;

                // Se o filho estiver cheio, divida-o.
                Node tmp = x.Child[i];
                if (tmp.n == 2 * T - 1)
                {// Se durante a inserção, um nó ficar cheio (ou seja, contém 2 * T - 1 chaves), ele é dividido em dois nós menores para manter a árvore balanceada.
                    Split(x, i, tmp);

                    // Após a divisão, verifique se k deve ser inserido no filho esquerdo ou direito.
                    if (k > x.Key[i])
                    {
                        i++;
                    }
                }
                // Continue a inserção recursivamente no filho apropriado.
                InsertValuee(x.Child[i], k);
            }
        }

        public void Insert(int key)
        {
            Node r = Root;
            if (r.n == 2 * T - 1) // 2 * 3 - 1 = 3
            {// Estiver cheio um novo nó é criado e a raiz é dividida.
                Node s = new Node();
                Root = s;
                s.Leaf = false;
                s.n = 0;
                s.Child[0] = r;
                Split(s, 0, r);
                InsertValuee(s, key);
            }
            else
            {
                InsertValuee(r, key);
            }
        }

        private void Split(Node x, int pos, Node y)
        {// Se durante a inserção, um nó ficar cheio (ou seja, contém 2 * T - 1 chaves), ele é dividido em dois nós menores para manter a árvore balanceada.
            Node z = new Node(); // Node Direita
            z.Leaf = y.Leaf;
            z.n = T - 1;

            // Copiar as chaves de y para z
            for (int j = 0; j < T - 1; j++)
            {
                z.Key[j] = y.Key[j + T];
                y.Key[j + T] = 0;
            }

            //A função Split é responsável por dividir o nó cheio y em dois nós menores, y e z, e promover a chave mediana para o nó pai (x).

            // Se y não for uma folha, copiar os filhos de y para z
            if (!y.Leaf)
            {
                for (int j = 0; j < T; j++)
                {
                    z.Child[j] = y.Child[j + T];
                    y.Child[j + T] = null;
                }
            }

            // Reduzir o número de chaves em y
            y.n = T - 1;

            // Abrir espaço para o novo filho
            for (int j = x.n; j >= pos + 1; j--)
            {
                x.Child[j + 1] = x.Child[j];
            }

            // Inserir o filho z no lugar certo
            x.Child[pos + 1] = z;

            // Mover a chave de y para x
            for (int j = x.n - 1; j >= pos; j--)
            {
                x.Key[j + 1] = x.Key[j];
            }
            x.Key[pos] = y.Key[T - 1];
            y.Key[T - 1] = 0; // Node Esquerda

            // Aumentar o número de chaves em x
            x.n = x.n + 1;
        }

        public Node Search(Node x, int key)
        {
            int i = 0;
            if (x == null)
                return x;
            for (i = 0; i < x.n; i++)
            {
                if (key < x.Key[i])
                {
                    break;
                }

                if (key == x.Key[i])
                {
                    return x;
                }
            }

            if (x.Leaf)
            {
                return null;
            }
            else
            {
                return Search(x.Child[i], key);
            }
        }

        private void Remove(Node x, int key)
        {
            int pos = x.Find(key);
            if (pos != -1)
            {
                if (x.Leaf)
                {
                    int i = 0;
                    for (i = 0; i < x.n && x.Key[i] != key; i++)
                    {
                    };

                    for (; i < x.n; i++)
                    {
                        if (i != 2 * T - 2)
                        {
                            x.Key[i] = x.Key[i + 1];
                        }
                    }
                    x.n--;
                    return;
                }
                if (!x.Leaf)
                {
                    Node pred = x.Child[pos];
                    int predKey = 0;
                    if (pred.n >= T)
                    {
                        for (; ; )
                        {
                            if (pred.Leaf)
                            {
                                Console.WriteLine(pred.n);
                                predKey = pred.Key[pred.n - 1];
                                break;
                            }
                            else
                            {
                                pred = pred.Child[pred.n];
                            }
                        }
                        Remove(pred, predKey);
                        x.Key[pos] = predKey;
                        return;
                    }

                    Node nextNode = x.Child[pos + 1];
                    if (nextNode.n >= T)
                    {
                        int nextKey = nextNode.Key[0];
                        if (!nextNode.Leaf)
                        {
                            nextNode = nextNode.Child[0];
                            for (; ; )
                            {
                                if (nextNode.Leaf)
                                {
                                    nextKey = nextNode.Key[nextNode.n - 1];
                                    break;
                                }
                                else
                                {
                                    nextNode = nextNode.Child[nextNode.n];
                                }
                            }
                        }
                        Remove(nextNode, nextKey);
                        x.Key[pos] = nextKey;
                        return;
                    }

                    int temp = pred.n + 1;
                    pred.Key[pred.n++] = x.Key[pos];
                    for (int i = 0, j = pred.n; i < nextNode.n; i++)
                    {
                        pred.Key[key++] = nextNode.Key[i];
                        pred.n++;
                    }

                    for (int i = 0; i < nextNode.n + 1; i++)
                    {
                        pred.Child[temp++] = nextNode.Child[i];
                    }

                    x.Child[pos] = pred;
                    for (int i = pos; i < x.n; i++)
                    {
                        if (i != 2 * T - 2)
                        {
                            x.Key[i] = x.Key[i + 1];
                        }
                    }

                    for (int i = pos + 1; i < x.n + 1; i++)
                    {
                        if (i != 2 * T - 1)
                        {
                            x.Child[i] = x.Child[i + 1];
                        }
                    }

                    x.n--;
                    if (x.n == 0)
                    {
                        if (x == Root)
                        {
                            Root = x.Child[0];
                        }
                        x = x.Child[0];
                    }
                    Remove(pred, key);
                    return;
                }
            }
            else
            {
                for (pos = 0; pos < x.n; pos++)
                {
                    if (x.Key[pos] > key)
                    {
                        break;
                    }
                }
                Node tmp = x.Child[pos];
                if (tmp.n >= T)
                {
                    Remove(tmp, key);
                    return;
                }
                if (true)
                {
                    Node nb = null;
                    int devider = -1;

                    if (pos != x.n && x.Child[pos + 1].n >= T)
                    {
                        devider = x.Key[pos];
                        nb = x.Child[pos + 1];
                        x.Key[pos] = nb.Key[0];
                        tmp.Key[tmp.n++] = devider;
                        tmp.Child[tmp.n] = nb.Child[0];
                        for (int i = 1; i < nb.n; i++)
                        {
                            nb.Key[i - 1] = nb.Key[i];
                        }
                        for (int i = 1; i <= nb.n; i++)
                        {
                            nb.Child[i - 1] = nb.Child[i];
                        }
                        nb.n--;
                        Remove(tmp, key);
                        return;
                    }
                    else if (pos != 0 && x.Child[pos - 1].n >= T)
                    {
                        devider = x.Key[pos - 1];
                        nb = x.Child[pos - 1];
                        x.Key[pos - 1] = nb.Key[nb.n - 1];
                        Node child = nb.Child[nb.n];
                        nb.n--;

                        for (int i = tmp.n; i > 0; i--)
                        {
                            tmp.Key[i] = tmp.Key[i - 1];
                        }
                        tmp.Key[0] = devider;
                        for (int i = tmp.n + 1; i > 0; i--)
                        {
                            tmp.Child[i] = tmp.Child[i - 1];
                        }
                        tmp.Child[0] = child;
                        tmp.n++;
                        Remove(tmp, key);
                        return;
                    }
                    else
                    {
                        Node lt = null;
                        Node rt = null;
                        bool last = false;
                        if(pos != x.n)
                        {
                            devider = x.Key[pos];
                            lt = x.Child[pos];
                            rt = x.Child[pos + 1];
                        }
                        else
                        {
                            devider = x.Key[pos - 1];
                            rt = x.Child[pos];
                            lt = x.Child[pos - 1];
                            last = true;
                            pos--;
                        }
                        for(int i = pos; i < x.n - 1; i++)
                        {
                            x.Key[i] = x.Key[i + 1];
                        }
                        for(int i = pos + 1; i < x.n; i++)
                        {
                            x.Child[i] = x.Child[i + 1];
                        }
                        x.n--;


                        for(int i = 0, j = lt.n; i < rt.n + 1; i++, j++)
                        {
                            if(i < rt.n)
                            {
                                lt.Key[j] = rt.Key[i];
                            }
                            lt.Child[j] = rt.Child[i];
                        }
                        lt.n += rt.n;
                        if(x.n == 0)
                        {
                            if(x == Root)
                            {
                                Root = x.Child[0];
                            }

                            x = x.Child[0];
                        }
                        Remove(lt, key);
                        return;
                    }
                }
            }
        }

        public void Remove(int key)
        {
            Node x = Search(Root, key);
            if (x == null)
            {
                return;
            }
            Remove(Root, key);
        }
    }
}
