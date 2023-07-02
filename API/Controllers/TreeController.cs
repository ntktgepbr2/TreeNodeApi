using System.ComponentModel.DataAnnotations;
using Application;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace TreeNodeApi.Controllers
{
    /// <summary>
    /// Tree controller.
    /// </summary>
    [Route("api/tree")]
    [ApiController]
    public class TreeController : Controller
    {
        private readonly ITreeNodeService _treeNodeService;

        /// <summary>
        /// Tree controller constructor.
        /// </summary>
        /// <param name="treeNodeService"></param>
        public TreeController(ITreeNodeService treeNodeService)
        {
            _treeNodeService = treeNodeService;
        }

        /// <summary>
        /// Get tree.
        /// </summary>
        /// <param name="name">The tree name.</param>
        /// <returns>
        /// The tree.
        /// </returns>
        [SwaggerOperation(Description = "Returns your entire tree. If your tree doesn't exist it will be created automatically.")]
        [ProducesResponseType(typeof(TreeNode), StatusCodes.Status200OK)]
        [HttpGet("get")]
        public async Task<ActionResult<TreeNode>> Get([Required] string name)
        {
            return await _treeNodeService.GetTree(name);
        }
    }
}
