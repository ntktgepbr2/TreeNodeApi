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

        public async Task AddTreeNode(int parentId, string nodeName)
        {
            var parentNode = await _context.TreeNodes
                .Include(n => n.Children)
                .FirstOrDefaultAsync(x => x.Id == parentId) ??
                             throw new SecureException($"Parent node with an Id {parentId} was not found");

            if (parentNode.Children.Any(x => x.Name == nodeName)) throw new SecureException("Duplicate name");

            var newChildNode = new TreeNode(nodeName);
            parentNode.Children.Add(newChildNode);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteTreeNode(int nodeId)
        {
            var nodeToDelete = await _context.TreeNodes
                .Include(n => n.Children)
                .FirstOrDefaultAsync(x => x.Id == nodeId) ??
                               throw new SecureException($"Node with an Id {nodeId} was not found");

            if (nodeToDelete.Children.Any()) throw new SecureException("You have to delete all children nodes first");

            _context.TreeNodes.Remove(nodeToDelete);

            await _context.SaveChangesAsync();
        }

        public async Task RenameTreeNode(string newName, int nodeId)
        {

            var nodeToRename = await _context.TreeNodes
                .Include(p => p.Parent)
                .ThenInclude(p => p.Children)
                .FirstOrDefaultAsync(x => x.Id == nodeId) ?? 
                               throw new SecureException($"Node with an Id {nodeId} was not found");

            if (nodeToRename.Parent == null) throw new SecureException("Couldn't rename root node");

            if (nodeToRename.Parent.Children.Any(x => x.Name == newName)) throw new SecureException("Duplicate name");

            nodeToRename.Name = newName;

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