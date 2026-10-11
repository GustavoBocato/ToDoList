using AutoMapper;
using TodoListApp.Models.DTOs;
using TodoListApp.Models;

namespace TodoListApp.Mappings
{
    public class Mapping : Profile
    {
        public Mapping() 
        {
            CreateMap<PostClientDTO, Client>();
            CreateMap<PostTodoListDTO, TodoList>();
            CreateMap<PostClientTodoListDTO, PostTodoListDTO>();
            CreateMap<PostTodoItemDTO, TodoItem>();
            CreateMap<PatchTodoListDTO, TodoList>()
            .ForAllMembers(opts =>
                opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<PatchTodoItemDTO, TodoItem>()
            .ForAllMembers(opts =>
                opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<PatchClientDTO, Client>()
            .ForAllMembers(opts =>
                opts.Condition((src, dest, srcMember) => srcMember != null));

        }
    }
}
