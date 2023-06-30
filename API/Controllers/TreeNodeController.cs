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

        public TreeNodeController(ITreeNodeService treeNodeService)
        {
            _treeNodeService = treeNodeService;
        }

        [SwaggerOperation(Description = "Returns your entire tree. If your tree doesn't exist it will be created automatically.")]
        [ProducesResponseType(typeof(TreeNode),StatusCodes.Status200OK)]
        [HttpGet("${treeName}")]
        public async Task<ActionResult<TreeNode>> Get([Required] string treeName)
        {
            return await _treeNodeService.GetTree(treeName);
        }


        [SwaggerOperation(Description = "Create a new node in your tree. You must to specify a parent node ID that belongs to your tree. A new node name must be unique across all siblings.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("addTreeNode")]
        public async Task AddTreeNode([FromQuery][Required] string treeName, int parentId, string nodeName)
        {
            await _treeNodeService.AddTreeNode(treeName, parentId, nodeName);
        }

        [SwaggerOperation(Description = "Rename an existing node in your tree. You must specify a node ID that belongs your tree. A new name of the node must be unique across all siblings.")]
        [HttpPut("renameTreeNode")]
        public async Task RenameTreeNode([FromQuery][Required] string treeName,  string newName, int nodeId)
        {
            await _treeNodeService.RenameTreeNode(treeName, newName, nodeId);
        }

        [SwaggerOperation(Description = "Delete an existing node in your tree. You must specify a node ID that belongs your tree.")]
        [HttpDelete("deleteTreeNode")]
        public async Task DeleteTreeNode([FromQuery][Required] string treeName, int nodeId)
        {
            await _treeNodeService.DeleteTreeNode(treeName, nodeId);
        }
    }
}
