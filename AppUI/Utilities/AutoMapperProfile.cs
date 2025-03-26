using AppUI.Models.User;
using AutoMapper;
using Entity;

namespace AppUI.Utilities
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Users
            CreateMap<User, UserListModel>()
                .ForMember(d => d.Role, o => o.MapFrom(or => or.Role.Name))
                .ForMember(d => d.IsPasswordReset, o => o.MapFrom(or => or.IsPasswordReset != null ? or.IsPasswordReset == true ? "Sí" : "No" : "No"))
                .ForMember(d => d.IsPasswordDefect, o => o.MapFrom(or => or.IsPasswordDefect ? "Sí" : "No"))
                .ForMember(d => d.IsActive, o => o.MapFrom(or => or.IsActive ? "Sí" : "No"));
            #endregion
        }
    }
}
