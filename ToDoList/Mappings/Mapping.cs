using AutoMapper;
using TodoListApp.Models.DTOs;
using ToDoListApp.Models;
using ToDoListApp.Models.DTOs;

namespace ToDoListApp.Mappings
{
    public class Mapping : Profile
    {
        public Mapping() 
        {
            CreateMap<PostClientDTO, Client>();
            CreateMap<PostTodoListDTO, TodoList>();
            CreateMap<PostClientTodoListDTO, PostTodoListDTO>();
            CreateMap<PatchTodoListDTO, TodoList>()
            .ForAllMembers(opts =>
                opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
