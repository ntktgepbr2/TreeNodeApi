namespace Domain;

public class TreeNode
{
    public int Id { get; set; }
    public string Name { get; set; }
    public TreeNode? Parent { get; set; }
    public int? ParentId { get; set; }
    public List<TreeNode> Children { get; set; }

    public TreeNode(string name)
    {
        Name = name;
        Children = new List<TreeNode>();
    }

}