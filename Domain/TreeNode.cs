namespace Domain;

public class TreeNode : BaseDomainModel
{
    public List<TreeNode> Children { get; set; }
    public TreeNode? Parent { get; set; }
    public int? ParentId { get; set; }

    public TreeNode(string name)
    {
        Name = name;
        Children = new List<TreeNode>();
    }

    public void AddNode(int parentId, string nodeName)
    {
        var parent = FindNode(this, parentId);

        if (parent != null)
        {
            var newNode = new TreeNode(nodeName);
            parent.Children.Add(newNode);
        }
        else
        {
            throw new InvalidOperationException("Parent node not found.");
        }
    }

    public TreeNode FindNode(TreeNode currentNode, int parentId)
    {
        if (currentNode.Id == parentId)
        {
            return currentNode;
        }

        foreach (var child in currentNode.Children)
        {
            var foundNode = FindNode(child, parentId);

            if (foundNode != null)
            {
                return foundNode;
            }
        }

        return null;
    }
}