using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application
{
    public class TreeNodeService : ITreeNodeService
    {
        private readonly TreeNodeDbContext _context;
        public TreeNodeService(TreeNodeDbContext context)
        {
            _context = context;
        }

        public async Task<TreeNode> GetTree(string name)
        {
            var tree = await FindTree(name);

            if (tree == null)
            {
                await CreateTree(name);
            }

            return await _context.TreeNodes.FindAsync(name);
        }

        private async Task<TreeNode?> FindTree(string name)
        {
            return await _context.TreeNodes.FirstOrDefaultAsync(x => x.Name == name && x.Parent == null);
        }

        public async Task AddTreeNode( string treeName, int parentId, string nodeName)
        {
            var tree = await FindTree(treeName) ??
                       throw new SecureException($"Tree with the name {treeName} was not found");

            tree.AddNode(parentId, nodeName);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteTreeNode(string treeName, int nodeId)
        {
            var tree = await FindTree(treeName) ??
                       throw new SecureException($"Tree with the name {treeName} was not found");

            RemoveNode(tree, nodeId);
             
            await _context.SaveChangesAsync();
        }

        private async Task CreateTree(string name)
        {
            var newTree = new TreeNode(name);

            await _context.TreeNodes.AddAsync(newTree);
            await _context.SaveChangesAsync();
        }

        private void RemoveNode(TreeNode currentNode, int nodeId)
        {
            if (currentNode.Id == nodeId)
            {
                if (currentNode.Children.Any()) throw new SecureException("You have to delete all children nodes first");

                if(currentNode.Parent == null)
                {
                    _context.TreeNodes.Remove(currentNode);

                    return;
                };

                currentNode.Children.Remove(currentNode);

                return;
            }

            currentNode.Children.ForEach(x => RemoveNode(x, nodeId));
        }
    }
}