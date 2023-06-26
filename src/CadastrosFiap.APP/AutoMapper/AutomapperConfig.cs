using AutoMapper;
using CadastrosFiap.APP.DTOs;
using CadastrosFiap.APP.ViewModels;

namespace CadastrosFiap.APP.AutoMapper
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<AlunoDTO, AlunoViewModel>().ReverseMap();
            //CreateMap<Turma, TurmaViewModel>().ReverseMap();
            //CreateMap<AlunoTurma, AlunoTurmaViewModel>().ReverseMap();

        }
    }
}
