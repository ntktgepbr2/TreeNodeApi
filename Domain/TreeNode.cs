using System.Text.Json.Serialization;

namespace Domain;

public class TreeNode
{
    public int Id { get; set; }
    public string Name { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public TreeNode? Parent { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int? ParentId { get; set; }

    public List<TreeNode> Children { get; set; }

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

    public TreeNode FindNode(TreeNode currentNode, int nodeId)
    {
        if (currentNode.Id == nodeId)
        {
            return currentNode;
        }

        foreach (var child in currentNode.Children)
        {
            var foundNode = FindNode(child, nodeId);

            if (foundNode != null)
            {
                return foundNode;
            }
        }

        return null;
    }

}