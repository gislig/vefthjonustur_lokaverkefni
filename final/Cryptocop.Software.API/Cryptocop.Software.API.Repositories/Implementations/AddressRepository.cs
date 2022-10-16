using System.Collections.Generic;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Repositories.Interfaces;

namespace Cryptocop.Software.API.Repositories.Implementations
{
    public class AddressRepository : IAddressRepository
    {
        private readonly CrytoDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ITokenRepository _tokenRepository;
        
        public AddressRepository(CrytoDbContext dbContext, IMapper mapper, ITokenRepository tokenRepository)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _tokenRepository = tokenRepository;
        }
        
        public void AddAddress(string email, AddressInputModel address)
        {
            Console.WriteLine("The email is : " + email);
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            
            try
            {
                var addressEntity = _mapper.Map<Address>(address);
                addressEntity.User = user;
                _dbContext.Addresses.Add(addressEntity);
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Could not add address");
            }
        }

        public IEnumerable<AddressDto> GetAllAddresses(string email)
        {
            var addresses = _dbContext.Addresses.Where(a => a.User.Email == email).ToList();
            return _mapper.Map<IEnumerable<AddressDto>>(addresses);
        }

        public void DeleteAddress(string email, int addressId)
        {
            var address = _dbContext.Addresses.FirstOrDefault(a => a.Id == addressId);
            if (address == null)
            {
                throw new Exception("Address not found");
            }
            
            if (address.User.Email != email)
            {
                throw new Exception("Address does not belong to user");
            }
            
            try
            {
                _dbContext.Addresses.Remove(address);
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Could not delete address");
            }
        }
    }
}