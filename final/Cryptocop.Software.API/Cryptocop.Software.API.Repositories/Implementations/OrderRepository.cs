using System;
using System.Collections.Generic;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Repositories.Interfaces;

namespace Cryptocop.Software.API.Repositories.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly CrytoDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ITokenRepository _tokenRepository;
        
        public OrderRepository(CrytoDbContext dbContext, IMapper mapper, ITokenRepository tokenRepository)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _tokenRepository = tokenRepository;
        }
        public IEnumerable<OrderDto> GetOrders(string email)
        {
            throw new NotImplementedException();
        }

        public OrderDto CreateNewOrder(string email, OrderInputModel order)
        {
            throw new NotImplementedException();
        }
    }
}