using AutoMapper;
using zs.bcs.BobsCatSalesServices.Application.SalesAssociate.Commands;
using zs.bcs.BobsCatSalesServices.ServicesApi.Contracts.Requests;

namespace zs.bcs.BobsCatSalesServices.ServicesApi.Mapping
{
    /// <summary>
    /// AutoMapper mapping profile.
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateSalesAssociateRequest, CreateSalesAssociateCommand>();
        }
    }
}
