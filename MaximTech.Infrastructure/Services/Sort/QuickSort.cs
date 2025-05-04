using MaximTech.Domain.Contracts;

namespace MaximTech.Infrastructure.Services.Sort;

public class QuickSort : ISort
{
    public void Sort(char[] array)
    {
        Sort(array, 0, array.Length - 1);
    }
    
    private static void Sort(char[] array, int l, int r)
    {
        var stack = new Stack<(int, int)>();
        stack.Push((l, r));

        while (stack.Count > 0)
        {
            (l, r) = stack.Pop();

            if (r <= l)
            {
                continue;
            }

            var i = Partition(array, l, r);

            if (i - l > r - i)
            {
                stack.Push((l, i - 1));
                stack.Push((i + 1, r));
            }
            else
            {
                stack.Push((i + 1, r));
                stack.Push((l, i - 1));
            }
        }
    }

    private static int Partition(char[] array, int l, int r)
    {
        var pivot = array[r];
        var i = l - 1;

        for (var j = l; j < r; j++)
        {
            if (array[j] > pivot)
            {
                continue;
            }

            i++;
            (array[i], array[j]) = (array[j], array[i]);
        }

        (array[i + 1], array[r]) = (array[r], array[i + 1]);

        return i + 1;
    }
}