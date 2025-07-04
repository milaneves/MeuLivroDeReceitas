using AutoMapper;

namespace MyRecipeBook.Application.Services.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            RequestToDomain();
            DomainToResponse();
        }
        private void RequestToDomain()
        {
            CreateMap<RequestRegisterUserJson, Domain.Entities.User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());

            CreateMap<ResponseUserProfileJson, Domain.Entities.User>()
              .ForMember(dest => dest.Password, opt => opt.Ignore());

        }

        private void DomainToResponse()
        {
            CreateMap<Domain.Entities.User, RequestRegisterUserJson>();
            CreateMap<Domain.Entities.User, ResponseUserProfileJson>();
        }
    }
}
