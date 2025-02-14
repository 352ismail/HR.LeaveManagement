using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation;
using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.DTOs.LeaveType;
using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LeaveRequest, LeaveRequestDTO>().ReverseMap();
            CreateMap<LeaveType, LeaveTypeDTO>().ReverseMap();
            CreateMap<LeaveRequest, LeaveRequestListDTO>()
                .ForMember(dest => dest.LeaveType, q => q.MapFrom(src => src.LeaveType))
                .ReverseMap();
            CreateMap<LeaveAllocation, LeaveAllocationDTO>().ReverseMap();
            CreateMap<CreateLeaveAllocationDTO, LeaveAllocation>().ReverseMap();
            CreateMap<CreateLeaveRequestDTO, LeaveRequest>().ReverseMap();
            CreateMap<UpdateLeaveAllocationDTO, LeaveAllocation>().ReverseMap();
            CreateMap<UpdateLeaveRequestDTO, LeaveRequest>().ReverseMap();
            CreateMap<UpdateLeaveTypeDTO, LeaveType>().ReverseMap();
        }
    }
}
