using Domain;

namespace Application
{
    public interface ITreeNodeService
    {
        public Task<TreeNode> GetTree(string name);
        public Task AddTreeNode(int parentId, string nodeName);
        public Task DeleteTreeNode(int nodeId);
        public Task RenameTreeNode( string newName, int nodeId);
    }
}