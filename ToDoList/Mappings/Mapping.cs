﻿using AutoMapper;
using ToDoListApp.Models;
using ToDoListApp.Models.DTOs;

namespace ToDoListApp.Mappings
{
    public class Mapping : Profile
    {
        public Mapping() 
        {
            CreateMap<ClientDTO, Client>();
            CreateMap<ToDoListDTO, ToDoListDTO>();
        }
    }
}
