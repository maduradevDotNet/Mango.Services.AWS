using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    [Authorize]
    public class CouponApiController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;

        public CouponApiController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _response = new ResponseDto();
            _mapper = mapper;
        }

        [HttpGet]
       // [Authorize(Roles = "ADMIN")]
        public ResponseDto Get()
        {
            try
            {

                IEnumerable<Coupon> objList = _db.Coupones.ToList();
                _response.Result = _mapper.Map<IEnumerable<CouponDto>>(objList);



            }
            catch (Exception ex)
            {
                _response.ISSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {

                Coupon obj = _db.Coupones.First(u => u.CouponId == id);
                _response.Result = _mapper.Map<CouponDto>(obj);



            }
            catch (Exception ex)
            {
                _response.ISSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpGet]
        [Route("GetByCode/{Code}")]
        public ResponseDto GetByCode(string Code)
        {
            try
            {

                Coupon obj = _db.Coupones.First(u => u.CouponCode.ToLower() == Code.ToLower());
                if (obj == null)
                {
                    _response.ISSuccess = false;
                }
                _response.Result = _mapper.Map<CouponDto>(obj);


            }
            catch (Exception ex)
            {
                _response.ISSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
       // [Authorize(Roles ="ADMIN")]
        public ResponseDto put([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDto);
                _db.Coupones.Add(obj);
                _db.SaveChanges();

                _response.Result = _mapper.Map<CouponDto>(obj); ;
            }
            catch (Exception ex)
            {
                _response.ISSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Put([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDto);
                _db.Coupones.Update(obj);
                _db.SaveChanges();

                _response.Result = _mapper.Map<CouponDto>(obj); ;
            }
            catch (Exception ex)
            {
                _response.ISSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpDelete]
        [Route("{id:int}")]
       [Authorize(Roles = "ADMIN")]
        public ResponseDto Delete(int id)
        {
            try
            {
                Coupon obj = _db.Coupones.First(u => u.CouponId == id);
                _db.Coupones.Remove(obj);
                _db.SaveChanges();


            }
            catch (Exception ex)
            {
                _response.ISSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


    }
}
