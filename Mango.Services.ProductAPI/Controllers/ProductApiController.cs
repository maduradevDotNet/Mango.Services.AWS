using AutoMapper;
using Mango.Services.ProductAPI.Data;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ProductAPI.Controllers
{
    [Route("api/product")]
    [ApiController]
  
    public class ProductApiController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;

        public ProductApiController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _response = new ResponseDto();
            _mapper = mapper;
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {

                IEnumerable<Product> objList = _db.products.ToList();
                _response.Result = _mapper.Map<IEnumerable<ProductDto>>(objList);



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

                Product obj = _db.products.First(u => u.ProductId == id);
                _response.Result = _mapper.Map<ProductDto>(obj);



            }
            catch (Exception ex)
            {
                _response.ISSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


      

        [HttpPost]
        [Authorize(Roles ="ADMIN")]
        public ResponseDto put([FromBody] ProductDto ProductDto)
        {
            try
            {
                Product obj = _mapper.Map<Product>(ProductDto);
                _db.products.Add(obj);
                _db.SaveChanges();

                _response.Result = _mapper.Map<ProductDto>(obj); ;
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
        public ResponseDto Put([FromBody] ProductDto ProductDto)
        {
            try
            {
                Product obj = _mapper.Map<Product>(ProductDto);
                _db.products.Update(obj);
                _db.SaveChanges();

                _response.Result = _mapper.Map<ProductDto>(obj); ;
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
                Product obj = _db.products.First(u => u.ProductId == id);
                _db.products.Remove(obj);
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
