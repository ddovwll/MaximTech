using MaximTech.Domain.Contracts;

namespace MaximTech.Infrastructure.Services.Sort;

public class TreeSort : ISort
{
    public void Sort(char[] array)
    {
        var root = new TreeNode(array[0]);

        for (var i = 1; i < array.Length; i++)
        {
            root.Insert(array[i]);
        }

        var sortedList = new List<char>();
        root.InOrderTraversal(sortedList);

        for (var i = 0; i < array.Length; i++)
        {
            array[i] = sortedList[i];
        }
    }

    private class TreeNode
    {
        private readonly char _value;
        private TreeNode? _left;
        private TreeNode? _right;

        public TreeNode(char value)
        {
            _value = value;
            _left = null;
            _right = null;
        }

        public void Insert(char newValue)
        {
            if (newValue <= _value)
            {
                if (_left is null)
                {
                    _left = new TreeNode(newValue);
                }
                else
                {
                    _left.Insert(newValue);
                }
            }
            else
            {
                if (_right is null)
                {
                    _right = new TreeNode(newValue);
                }
                else
                {
                    _right.Insert(newValue);
                }
            }
        }

        public void InOrderTraversal(List<char> sortedList)
        {
            _left?.InOrderTraversal(sortedList);

            sortedList.Add(_value);

            _right?.InOrderTraversal(sortedList);
        }
    }
}