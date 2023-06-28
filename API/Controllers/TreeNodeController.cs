using Application;
using Domain;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TreeNodeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreeNodeController : ControllerBase
    {
        private readonly ITreeNodeService _treeNodeService;

        public TreeNodeController(ITreeNodeService treeNodeService)
        {
            _treeNodeService = treeNodeService;
        }
        // GET: api/<TreeNodeController>
        [Route("api/tree")]
        [HttpGet("${treeName}")]
        public async Task<ActionResult<TreeNode>> Get(string treeName)
        {
            return await _treeNodeService.GetTree(treeName);
        }

        // POST api/<TreeNodeController>
        [HttpPost]
        public async Task AddTreeNode([FromQuery] string treeName, int parentId, string nodeName)
        {
            await _treeNodeService.AddTreeNode(treeName, parentId, nodeName);
        }

        // PUT api/<TreeNodeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TreeNodeController>/5
        [HttpDelete("{nodeId}")]
        public async Task DeleteTreeNode([FromQuery] string treeName, int nodeId)
        {
            await _treeNodeService.DeleteTreeNode(treeName, nodeId);
        }
    }
}
