using Application;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace TreeNodeApi.Controllers
{
    [Route("api/treeNode")]
    [ApiController]
    public class TreeNodeController : ControllerBase
    {
        private readonly ITreeNodeService _treeNodeService;

        public TreeNodeController(ITreeNodeService treeNodeService)
        {
            _treeNodeService = treeNodeService;
        }
        // GET: api/<TreeNodeController>

        [HttpGet("${treeName}")]
        public async Task<ActionResult<TreeNode>> Get(string treeName)
        {
            return await _treeNodeService.GetTree(treeName);
        }

        // POST api/<TreeNodeController>
        [HttpPost("addTreeNode")]
        public async Task AddTreeNode([FromQuery] string treeName, int parentId, string nodeName)
        {
            await _treeNodeService.AddTreeNode(treeName, parentId, nodeName);
        }

        // PUT api/<TreeNodeController>/5
        [HttpPut("renameTreeNode")]
        public async Task RenameTreeNode([FromQuery] string treeName,  string newName, int nodeId)
        {
            await _treeNodeService.RenameTreeNode(treeName, newName, nodeId);
        }

        // DELETE api/<TreeNodeController>/5
        [HttpDelete("deleteTreeNode")]
        public async Task DeleteTreeNode([FromQuery] string treeName, int nodeId)
        {
            await _treeNodeService.DeleteTreeNode(treeName, nodeId);
        }
    }
}
