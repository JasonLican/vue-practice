using AutoMapper;
using ICan.Core.EFDbContext;
using ICan.Core.IService;
using ICan.Module;
using ICan.Module.Entity;
using ICan.Module.ReuqestModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICan.Core.Service
{
    public class UserService : IUserService
    {
        public readonly ApplicationDbContext _dbContext;
        public readonly IMapper _mapper;
        public UserService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<SysUser> GetUser(string userName)
        {
            return _dbContext.Users.FirstOrDefault(p => p.UserName == userName);
        }

        public async Task<bool> CheckUser(string userName, string pwd)
        {
            return _dbContext.Users.Any(p => p.UserName == userName && p.Password == pwd && p.IsActive == true);
        }
        #region 增删改查
        public async Task<BasePageDataResponse> Get(UserSearchRequest request)
        {
            BasePageDataResponse response = new BasePageDataResponse();
            var userList = _dbContext.Users.Where(p => (!string.IsNullOrEmpty(request.UserName) && p.UserName.Contains(request.UserName))).ToList();
            response.Total = userList.Count;
            var list = userList.Skip((request.PageIndex - 1) * request.PageCount).Take(request.PageCount);
            response.Code = 200;
            response.Message = "成功";
            response.Data = list;
            return response;
        }
        public async Task UpdateAsync(SysUser user)
        {
            user.UpdateTime = DateTime.Now;
            user.UpdateBy = 1;
            _dbContext.Update(user);
            _dbContext.SaveChanges();
        }
        public async Task Add(SysUser user)
        {
            user.CreateBy = 1;
            user.CreateTime = DateTime.Now;
            _dbContext.Add(user);
            _dbContext.SaveChanges();
        }
        public async Task Del(SysUser user)
        {
            _dbContext.Remove(user);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}
