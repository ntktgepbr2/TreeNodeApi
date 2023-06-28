using Domain;

namespace Application
{
    public interface ITreeNodeService
    {
        public Task<TreeNode> GetTree(string name);
        public Task AddTreeNode( string treeName, int parentId, string nodeName);
        public Task DeleteTreeNode(string treeName, int nodeId);
    }
}