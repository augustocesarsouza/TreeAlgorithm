namespace RedBlackTreeNotDel
{
    public static class QuickSortImpr
    {
        public static void QuickSort(List<int> arr, int begin, int end)
        {
            int pivot = arr[(begin + (end - begin) / 2)];
            //pivot passa para direita do array todos elementos que são maior que PIVOT e a esquerda todos os menores
            int left = begin;
            int right = end;
            while (left <= right)
            {
                while (arr[left] < pivot)
                {
                    left++;
                }

                while (arr[right] > pivot)
                {
                    right--;
                }

                if (left <= right)
                {
                    swap(arr, left, right);
                    left++; // avança barra
                    right--;
                }
            }

            if (begin < right)
            {
                QuickSort(arr, begin, left - 1);
            }

            if (end > left)
            {
                QuickSort(arr, right + 1, end);
            }

        }

        static void swap(List<int> items, int x, int y)
        {
            int temp = items[x];
            items[x] = items[y];
            items[y] = temp;
        }
    }
}
