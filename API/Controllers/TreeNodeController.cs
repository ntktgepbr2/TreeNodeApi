using System.ComponentModel.DataAnnotations;
using Application;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace TreeNodeApi.Controllers
{
    /// <summary>
    /// Tree node controller.
    /// </summary>
    [Route("api/treeNode")]
    [ApiController]
    public class TreeNodeController : ControllerBase
    {
        private readonly ITreeNodeService _treeNodeService;

        /// <summary>
        /// Tree node controller constructor.
        /// </summary>
        /// <param name="treeNodeService">The tree node service.</param>
        public TreeNodeController(ITreeNodeService treeNodeService)
        {
            _treeNodeService = treeNodeService;
        }

        /// <summary>
        /// Add new tree node.
        /// </summary>
        /// <param name="parentId">Parent node id.</param>
        /// <param name="nodeName">New node name.</param>
        [SwaggerOperation(Description = "Create a new node in your tree. You must to specify a parent node ID. A new node name must be unique across all siblings.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("addTreeNode")]
        public async Task AddTreeNode([Required]int parentId, [Required] string nodeName)
        {
            await _treeNodeService.AddTreeNode(parentId, nodeName);
        }

        /// <summary>
        /// Rename tree node.
        /// </summary>
        /// <param name="newName">Node new name.</param>
        /// <param name="nodeId">Node id.</param>
        [SwaggerOperation(Description = "Rename an existing node in your tree. You must specify a node ID that belongs your tree. A new name of the node must be unique across all siblings.")]
        [HttpPut("renameTreeNode")]
        public async Task RenameTreeNode([Required] string newName, [Required] int nodeId)
        {
            await _treeNodeService.RenameTreeNode(newName, nodeId);
        }

        /// <summary>
        /// Delete node.
        /// </summary>
        /// <param name="nodeId">Node id.</param>
        [SwaggerOperation(Description = "Delete an existing node in your tree. You must specify a node ID that belongs your tree.")]
        [HttpDelete("deleteTreeNode")]
        public async Task DeleteTreeNode([Required] int nodeId)
        {
            await _treeNodeService.DeleteTreeNode(nodeId);
        }
    }
}
