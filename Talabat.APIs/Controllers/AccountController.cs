using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services;
using Talabat.Service;

namespace Talabat.APIs.Controllers
{

    public class AccountController : ApiBaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            ITokenService tokenService,IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        [HttpPost("login")] // api/Account/login

        public async Task<ActionResult<UserDTO>> Login(LoginDTO model)
        {
            var User = await _userManager.FindByEmailAsync(model.Email);
            if (User is null) return Unauthorized(new ApiErrorResponse(401));
            var result = await _signInManager.CheckPasswordSignInAsync(User, model.Password, false);
            if (!result.Succeeded) return Unauthorized(new ApiErrorResponse(401));
            return (Ok(new UserDTO
            {
                DisplayName = User.DisplayName,
                Email = User.Email,
                Token = await _tokenService.CreateTokenAsync(User, _userManager)

            }));
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO model)
        {
            if (Checkemailexist(model.Email).Result.Value)
                return BadRequest(new ApiValidationErrorResponse()
                {
                    Errors=new string[] {"this Email is Already String"}
                });

            var User = new AppUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
                PhoneNumber = model.PhoneNumber
            };
            var Result = await _userManager.CreateAsync(User, model.Password);
            if (!Result.Succeeded)  return BadRequest(new ApiErrorResponse(400));

             return (Ok(new UserDTO
            {
                DisplayName = User.DisplayName,
                Email = User.Email,
                 Token = await _tokenService.CreateTokenAsync(User, _userManager)

             }));
        }

        [Authorize]
        [HttpGet("GetCurrentUser")]

        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user= await _userManager.FindByEmailAsync(email);

            return Ok(new UserDTO()
            {
                DisplayName= user.DisplayName,  
                Email = user.Email, 
                Token= await _tokenService.CreateTokenAsync(user,_userManager)  
            });

        }
        [Authorize]
        [HttpGet("UserAddress")]
        public async Task<ActionResult<AddressDTO>> GetUserAddress()
        {
            var user = await _userManager.FindUserWithAddressByEmailAsync(User);

            var mappedaddress=  _mapper.Map<Address,AddressDTO>(user.Address);

            return Ok(mappedaddress);

        }
        [Authorize]
        [HttpPut("UserAddress")]
        public async Task<ActionResult<AddressDTO>> UpdateUserAddress(AddressDTO Updateaddress)
        {
            var address = _mapper.Map<AddressDTO, Address>(Updateaddress);
            var user = await _userManager.FindUserWithAddressByEmailAsync(User);
            address.Id = user.Address.Id;
            user.Address = address;

            var result= await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return BadRequest(new ApiErrorResponse(400));
            return Ok(Updateaddress);

        }

        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> Checkemailexist(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }

    }
}
