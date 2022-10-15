namespace Cryptocop.Software.API.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly CrytoDbContext _dbContext;
        private readonly IMapper _mapper;
        
        public ShoppingCartController(CrytoDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        // TODO: Get all items within the shopping cart, see Models section for reference
        [HttpGet]
        public async Task<IActionResult> GetShoppingCartAllItems()
        {
            throw new NotImplementedException();
        }
        
        // TODO: Add item to the chopping cart, see Models section for reference. Note need to update FromBody InputModel
        [HttpPost]
        public async Task<IActionResult> AddItemToShoppingCart([FromBody] ShoppingCartItemInputModel shoppingCartItemInput)
        {
            throw new NotImplementedException();
        }
        
        // TODO: Delete an item from the shoppin cart
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemFromShoppingCart(int id)
        {
            throw new NotImplementedException();
        }
        
        // TODO : Update an item from the shopping cart, remember to set the inputModel
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItemFromShoppingCart(int id, [FromBody] ShoppingCartItemInputModel shoppingCartItemInput)
        {
            throw new NotImplementedException();
        }
        
        // TODO: DELETE all items from the shopping cart
        [HttpDelete]
        public async Task<IActionResult> DeleteAllItemsFromShoppingCart()
        {
            throw new NotImplementedException();
        }
    }
}