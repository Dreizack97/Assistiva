using AppUI.Models;
using AppUI.Models.User;
using AutoMapper;
using Entity;

namespace AppUI.Utilities
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Role, RoleModel>().ReverseMap();

            #region Users
            CreateMap<User, UserListModel>()
                .ForMember(d => d.Role, o => o.MapFrom(or => or.Role.Name))
                .ForMember(d => d.IsActive, o => o.MapFrom(or => or.IsActive ? "Sí" : "No"));

            CreateMap<UserModel, User>().ReverseMap();
            #endregion
        }
    }
}
