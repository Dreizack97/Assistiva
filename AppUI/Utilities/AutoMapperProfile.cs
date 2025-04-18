using AppUI.Models;
using AppUI.Models.Formula;
using AppUI.Models.StudentDisability;
using AppUI.Models.User;
using AutoMapper;
using DTO;
using Entity;

namespace AppUI.Utilities
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Classroom
            CreateMap<Classroom, ClassroomModel>()
                .ForMember(d => d.Teacher, o => o.MapFrom(or => or.Teacher.Username));

            CreateMap<ClassroomModel, Classroom>()
                .ForMember(d => d.Teacher, o => o.Ignore());
            #endregion

            #region ClassroomStudent
            CreateMap<ClassroomStudent, ClassroomStudentModel>()
                .ForMember(d => d.StudentName, o => o.MapFrom(or => string.Join(' ', new[] { or.Student.FirstName, or.Student.PaternalLastName, or.Student.MaternalLastName })));

            CreateMap<ClassroomStudentModel, ClassroomStudent>()
                .ForMember(d => d.Student, o => o.Ignore());
            #endregion

            #region ClassroomSubject
            CreateMap<ClassroomSubject, ClassroomSubjectModel>()
                .ForMember(d => d.Code, o => o.MapFrom(or => or.Subject.Code))
                .ForMember(d => d.SubjectName, o => o.MapFrom(or => or.Subject.Name));

            CreateMap<ClassroomSubjectModel, ClassroomSubject>()
                .ForMember(d => d.Subject, o => o.Ignore());
            #endregion

            #region Disability
            CreateMap<Disability, DisabilityModel>()
                .ForMember(d => d.IsActive, o => o.MapFrom(or => or.IsActive ? "Sí" : "No"));

            CreateMap<DisabilityModel, Disability>()
                .ForMember(d => d.IsActive, o => o.Ignore());
            #endregion

            #region Formula
            CreateMap<Formula, FormulaModel>()
                .ForMember(d => d.Content, o => o.MapFrom(or => or.Content.Trim('$')));

            CreateMap<FormulaModel, Formula>()
                .ForMember(d => d.Content, o => o.MapFrom(or => string.Concat('$', or.Content, '$')));

            CreateMap<Formula, FormulaListModel>().ReverseMap();
            #endregion

            CreateMap<Role, RoleModel>().ReverseMap();

            CreateMap<Subject, SubjectModel>().ReverseMap();

            #region Student
            CreateMap<Student, StudentModel>()
                .ForMember(d => d.EmailAddress, o => o.MapFrom(or => or.User.Email))
                .ForMember(d => d.IsActive, o => o.MapFrom(or => or.IsActive ? "Sí" : "No"));

            CreateMap<StudentModel, Student>()
                .ForMember(d => d.IsActive, o => o.Ignore())
                .ForMember(d => d.User, o => o.Ignore());
            #endregion

            #region StudentDisability
            CreateMap<StudentDisability, StudentDisabilityModel>().ReverseMap();

            CreateMap<StudentDisability, StudentDisabilityListModel>()
                .ForMember(d => d.DisabilityName, o => o.MapFrom(or => or.Disability.Name));
            #endregion

            #region Users
            CreateMap<User, UserListModel>()
                .ForMember(d => d.Role, o => o.MapFrom(or => or.Role.Name))
                .ForMember(d => d.IsActive, o => o.MapFrom(or => or.IsActive ? "Sí" : "No"));

            CreateMap<UserModel, User>().ReverseMap();

            CreateMap<UserProfileDTO, UserProfileModel>().ReverseMap();

            CreateMap<UserProfileModel, User>()
                .ForMember(d => d.Students, o => o.Ignore());
            #endregion
        }
    }
}
