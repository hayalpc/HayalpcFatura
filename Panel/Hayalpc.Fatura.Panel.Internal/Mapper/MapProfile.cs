using AutoMapper;
using Hayalpc.Library.Common.Dtos;
using Hayalpc.Fatura.Data.Models;

namespace Hayalpc.Fatura.Panel.Internal.Mapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<RolePermission, RolePermissionDto>();
            CreateMap<RolePermissionDto, RolePermission>();

            CreateMap<ResetPasswordDto, ResetPassword>();
            CreateMap<ResetPassword, ResetPasswordDto>();

            CreateMap<UserBulletin, UserBulletinDto>();
            CreateMap<UserBulletinDto, UserBulletin>();
        }
    }
}
