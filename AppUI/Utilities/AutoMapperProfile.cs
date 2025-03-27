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
            #region Disability
            CreateMap<Disability, DisabilityModel>()
                .ForMember(d => d.IsActive, o => o.MapFrom(or => or.IsActive ? "Sí" : "No"));

            CreateMap<DisabilityModel, Disability>()
                .ForMember(d => d.IsActive, o => o.Ignore());
            #endregion

            CreateMap<Role, RoleModel>().ReverseMap();

            #region Student
            CreateMap<Student, StudentModel>()
                .ForMember(d => d.EmailAddress, o => o.MapFrom(or => or.User.Email))
                .ForMember(d => d.IsActive, o => o.MapFrom(or => or.IsActive ? "Sí" : "No"));

            CreateMap<StudentModel, Student>()
                .ForMember(d => d.IsActive, o => o.Ignore())
                .ForMember(d => d.User, o => o.Ignore());
            #endregion

            #region Users
            CreateMap<User, UserListModel>()
                .ForMember(d => d.Role, o => o.MapFrom(or => or.Role.Name))
                .ForMember(d => d.IsActive, o => o.MapFrom(or => or.IsActive ? "Sí" : "No"));

            CreateMap<UserModel, User>().ReverseMap();
            #endregion
        }
    }
}
