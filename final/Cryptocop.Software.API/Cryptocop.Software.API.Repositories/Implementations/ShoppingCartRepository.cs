﻿using System.Collections.Generic;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Repositories.Interfaces;

namespace Cryptocop.Software.API.Repositories.Implementations
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly CrytoDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ITokenRepository _tokenRepository;
        
        public ShoppingCartRepository(CrytoDbContext dbContext, IMapper mapper, ITokenRepository tokenRepository)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _tokenRepository = tokenRepository;
        }
        
        public IEnumerable<ShoppingCartItemDto> GetCartItems(string email)
        {
            throw new System.NotImplementedException();
        }

        public void AddCartItem(string email, ShoppingCartItemInputModel shoppingCartItemItem, float priceInUsd)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveCartItem(string email, int id)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCartItemQuantity(string email, int id, float quantity)
        {
            throw new System.NotImplementedException();
        }

        public void ClearCart(string email)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCart(string email)
        {
            throw new System.NotImplementedException();
        }
    }
}