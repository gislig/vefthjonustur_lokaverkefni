namespace Cryptocop.Software.API.Controllers
{
    [Route("api/cart"), Authorize]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;
        
        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }
        
        /// <summary>Get all items in shopping cart</summary>
        /// <response code="200">Returns OK all items in shoppin cart</response>
        [SwaggerResponse(200, "Shows all items in the shopoping carts of the user", Type = typeof(IActionResult))]
        [HttpGet]
        public async Task<IActionResult> GetShoppingCartAllItems()
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            try
            {
                var items = _shoppingCartService.GetCartItems(email);
                return Ok(items);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        /// <summary>Add item to shopping cart</summary>
        /// <response code="200">Returns OK if item has been added</response>
        [SwaggerResponse(200, "Add item to shopping cart", Type = typeof(IActionResult))]
        [HttpPost]
        public async Task<IActionResult> AddItemToShoppingCart([FromBody] ShoppingCartItemInputModel shoppingCartItemInput)
        {
            if(!ModelState.IsValid)
                return BadRequest("Shopping cart item is incorrect");
            
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            try{
                var item = _shoppingCartService.AddCartItem(email, shoppingCartItemInput);
                return Ok(item);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        /// <summary>Delete item in shopping cart</summary>
        /// <response code="200">Returns OK if item has been deleted</response>
        [SwaggerResponse(200, "Delete shopping item in cart", Type = typeof(IActionResult))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemFromShoppingCart(int id)
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            try{
                _shoppingCartService.RemoveCartItem(email, id);
                return Ok();
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        /// <summary>Update item in shopping cart</summary>
        /// <response code="200">Returns OK if item has been updated</response>
        [SwaggerResponse(200, "Updates shopping item in cart", Type = typeof(IActionResult))]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItemFromShoppingCart(int id, [FromBody] ShoppingCartItemInputModel shoppingCartItemInput)
        {
            if(!ModelState.IsValid)
                return BadRequest("Shopping cart items incorrect");
            
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            try{
                _shoppingCartService.UpdateCartItemQuantity(email, id, shoppingCartItemInput.Quantity);
                return Ok();
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        /// <summary>Delete all items from shopping cart</summary>
        /// <response code="200">Returns OK if items have been removed</response>
        [SwaggerResponse(200, "Removes shopping items from cart", Type = typeof(IActionResult))]
        [HttpDelete]
        public async Task<IActionResult> DeleteAllItemsFromShoppingCart()
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            try
            {
                _shoppingCartService.ClearCart(email);
                return Ok();
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}