namespace Cryptocop.Software.API.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;
        
        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }
        
        // TODO: Get all items within the shopping cart, see Models section for reference
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
        
        // TODO: Add item to the chopping cart, see Models section for reference. Note need to update FromBody InputModel
        [HttpPost]
        public async Task<IActionResult> AddItemToShoppingCart([FromBody] ShoppingCartItemInputModel shoppingCartItemInput)
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            try{
                var item = _shoppingCartService.AddCartItem(email, shoppingCartItemInput);
                return Ok(item);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        // TODO: Delete an item from the shoppin cart
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
        
        // TODO : Update an item from the shopping cart, remember to set the inputModel
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItemFromShoppingCart(int id, [FromBody] ShoppingCartItemInputModel shoppingCartItemInput)
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            try{
                _shoppingCartService.UpdateCartItemQuantity(email, id, shoppingCartItemInput.Quantity);
                return Ok();
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        // TODO: DELETE all items from the shopping cart
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