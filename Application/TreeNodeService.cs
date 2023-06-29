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
                tree = await FindTree(name);
            }

            var treeHierarchy = GetTreeHierarchy(tree);

            return treeHierarchy;
        }

        public async Task AddTreeNode( string treeName, int parentId, string nodeName)
        {
            var tree = await FindTree(treeName) ??
                       throw new SecureException($"Tree with the name {treeName} was not found");
            var treeHierarchy = GetTreeHierarchy(tree);

            treeHierarchy.AddNode(parentId, nodeName);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteTreeNode(string treeName, int nodeId)
        {
            var tree = await FindTree(treeName) ??
                       throw new SecureException($"Tree with the name {treeName} was not found");
            var treeHierarchy = GetTreeHierarchy(tree);

            RemoveNode(treeHierarchy, nodeId);
             
            await _context.SaveChangesAsync();
        }

        public async Task RenameTreeNode(string treeName, string newName, int nodeId)
        {
            var tree = await FindTree(treeName) ??
                       throw new SecureException($"Tree with the name {treeName} was not found");

            if(tree.Id == nodeId) throw new SecureException("Couldn't rename root node");

            var treeHierarchy = GetTreeHierarchy(tree);
            var node = treeHierarchy.FindNode(treeHierarchy,nodeId);
            node.Name = newName;

            await _context.SaveChangesAsync();
        }

        private async Task<TreeNode?> FindTree(string name)
        {
            return await _context.TreeNodes.FirstOrDefaultAsync(x => x.Name == name && x.Parent == null);
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

                _context.TreeNodes.Remove(currentNode);

                return;
            }

            currentNode.Children.ForEach(x => RemoveNode(x, nodeId));

            throw new SecureException($"Node with id {nodeId} was not found in this tree.");
        }

        private TreeNode GetTreeHierarchy(TreeNode tree)
        {
            IQueryable<TreeNode> childNodesQuery = _context.TreeNodes
                .Where(t => t.ParentId == tree.Id)
                .Include(t => t.Children);

            var childNodes = childNodesQuery.ToList();

            foreach (var childNode in childNodes)
            {
                var child = GetTreeHierarchy(childNode);
                child.Parent = tree;

                if (!tree.Children.Contains(child))
                {
                    tree.Children.Add(child);
                }
            }

            return tree;
        }
    }
}